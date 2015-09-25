using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.IO;
using Microsoft.Win32;
using ICSharpCode.SharpZipLib.Zip;
using System.Collections;
using DroidExplorer.Bootstrapper.UI;
using DroidExplorer.Bootstrapper.Authentication;
using System.Diagnostics;

namespace DroidExplorer.Bootstrapper.Panels {
	/// <summary>
	/// 
	/// </summary>
	public class DownloadPanel : WizardPanel {

		private const string URL_FORMAT = "http://droidexplorer.googlecode.com/files/{0}";
		private const string SDK_XML_NAMESPACE = "http://schemas.android.com/sdk/android/repository/1";
		private const string REPOSITORY = "repository.xml";
		private const string PLATFORM_VERSION = "2.2";
		private const string PLATFORM_REVISION = "01";
		private const string TOOLS_REVISION = "06";
		private const int PROXY_RETRY_MAX = 3;

		private Label overallStatus;
		private Label downloadStatus;
		private ProgressBar overallProgress;
		private ProgressBar downloadProgress;
		private Label label1;

		private Thread runThread = null;
		private string installPath = string.Empty;
		private int proxyRetryCount = 0;

		/// <summary>
		/// Initializes a new instance of the <see cref="DownloadPanel"/> class.
		/// </summary>
		internal DownloadPanel ( )
			: base ( ) {

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DownloadPanel"/> class.
		/// </summary>
		/// <param name="wizard">The wizard.</param>
		public DownloadPanel ( IWizard wizard )
			: base ( wizard ) {
				


			this.Controls.AddRange ( new Control[] { this.label1, this.downloadProgress, this.downloadStatus, this.overallStatus, this.overallProgress } );

			Wizard.CancelRequest += new EventHandler ( wizard_CancelRequest );
			this.LogDebug ( "Initializing Download Panel" );
			installPath = Wizard.GetInstallPath ( );
			this.LogDebug ( "Install path: {0}", installPath );
			SdkPath = Wizard.GetSdkPath ( );
			this.LogDebug ( "SDK Path: {0}", SdkPath );

			CredentialsDialog = new CredentialsDialog ( );
			var proxy = HttpWebRequest.GetSystemWebProxy();
				var bypassed = proxy.IsBypassed(new Uri("http://google.com"));
			if ( !bypassed ) {
				var phost = proxy.GetProxy ( new Uri ( "http://google.com" ) );
				this.LogDebug ( "Setting proxy info based on internet explorer" );
				CredentialsDialog.ForceUI = true;
				CredentialsDialog.Caption = "Proxy Authentication";
				CredentialsDialog.TargetName = phost.Host;
			}

		}

		

		private void CopyStandaloneArchives ( ) {
			if ( !UseWebDownload ) {
				this.LogDebug ( "Copying platform tools to temp path" );
				using ( Stream strm = this.GetType ( ).Assembly.GetManifestResourceStream ( string.Format ( CultureInfo.InvariantCulture, "DroidExplorer.Bootstrapper.Installs.{0}", PlatformArchiveName ) ) ) {
					using ( FileStream fs = new FileStream ( Path.Combine ( Path.GetTempPath ( ), PlatformArchiveName ), FileMode.Create, FileAccess.Write ) ) {
						byte[] buffer = new byte[ 2048 ];
						int bread = 0;
						while ( ( bread = strm.Read ( buffer, 0, buffer.Length ) ) > 0 ) {
							fs.Write ( buffer, 0, bread );
						}
						fs.Close ( );
					}
					strm.Close ( );
				}

				this.LogDebug ( "Copying sdk tools to temp path" );
				using ( Stream strm = this.GetType ( ).Assembly.GetManifestResourceStream ( string.Format ( CultureInfo.InvariantCulture, "DroidExplorer.Bootstrapper.Installs.{0}", ToolsArchiveName ) ) ) {
					using ( FileStream fs = new FileStream ( Path.Combine ( Path.GetTempPath ( ), ToolsArchiveName ), FileMode.Create, FileAccess.Write ) ) {
						byte[] buffer = new byte[ 2048 ];
						int bread = 0;
						while ( ( bread = strm.Read ( buffer, 0, buffer.Length ) ) > 0 ) {
							fs.Write ( buffer, 0, bread );
						}
						fs.Close ( );
					}
					strm.Close ( );
				}
			}
		}

		/// <summary>
		/// Gets or sets the proxy credentials.
		/// </summary>
		/// <value>The proxy credentials.</value>
		private NetworkCredential ProxyCredentials { get; set; }
		/// <summary>
		/// Gets or sets the credentials dialog.
		/// </summary>
		/// <value>The credentials dialog.</value>
		private CredentialsDialog CredentialsDialog { get; set; }


		private bool UseWebDownload { get; set; }

		/// <summary>
		/// Initializes the wizard panel.
		/// </summary>
		public override void InitializeWizardPanel ( ) {
			this.LogDebug ( "Checking if using Existing SDK: {0}", Wizard.UseExistingSdk );

			if ( Wizard.UseExistingSdk ) {
				this.LogDebug ( "Next Panel: Using Existing SDK" );
				Wizard.Next ( );
				return;
			}


			if ( string.IsNullOrEmpty ( SdkPath ) ) {
				throw new KeyNotFoundException ( "Unable to locate the needed registry key to finish installation." );
			}


			string[] resources = this.GetType ( ).Assembly.GetManifestResourceNames ( );
			UseWebDownload = !resources.Contains<string> ( string.Format ( CultureInfo.InvariantCulture, "DroidExplorer.Bootstrapper.Installs.{0}", ToolsArchiveName ) ) ||
				!resources.Contains<string> ( string.Format ( CultureInfo.InvariantCulture, "DroidExplorer.Bootstrapper.Installs.{0}", PlatformArchiveName ) );
			CopyStandaloneArchives ( );


			runThread = new Thread ( new ThreadStart ( RunSetup ) );
			runThread.Start ( );
		}

		/// <summary>
		/// Handles the CancelRequest event of the wizard control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void wizard_CancelRequest ( object sender, EventArgs e ) {
			this.LogWarning ( "Step canceled by user" );
			if ( runThread != null && runThread.IsAlive ) {
				try {
					runThread.Abort ( );
				} catch ( ThreadAbortException tae ) {

				}
			}
		}

		/// <summary>
		/// Runs the setup.
		/// </summary>
		private void RunSetup ( ) {

			try {
				this.overallProgress.SetMaximum ( 7 );
				this.overallProgress.SetMinimum ( 0 );
				this.overallProgress.SetValue ( 0 );

				this.downloadProgress.SetMaximum ( 1 );
				this.downloadProgress.SetMinimum ( 0 );
				this.downloadProgress.SetValue ( 0 );

				this.overallStatus.SetText ( Properties.Resources.PrepSdkPathMessage );
				this.LogDebug ( "Cleaning up sdk path" );
				SdkPathCleanup ( );
				this.overallProgress.SetValue ( 1 );

				this.downloadStatus.SetText ( Properties.Resources.GetRepositoryMesage );
				this.overallStatus.SetText ( Properties.Resources.DownloadPrepMessage );
				if ( UseWebDownload ) {
					this.LogDebug ( "Downloading Repository" );
					Repository = GetRepository ( );
				}
				this.overallProgress.SetValue ( 2 );

				this.overallStatus.SetText ( Properties.Resources.DownloadingPlatformToolsMessage );
				this.LogDebug ( "Getting platform tools" );
				string platformTools = UseWebDownload ? DownloadPlatformTools ( ) : Path.Combine ( Path.GetTempPath ( ), PlatformArchiveName );
				this.overallProgress.SetValue ( 3 );
				this.downloadStatus.SetText ( string.Empty );
				this.downloadProgress.SetValue ( 0 );

				this.overallStatus.SetText ( Properties.Resources.ExtractingPlatformToolsMessage );
				this.LogDebug ( "Extracting platform tools archive" );
				ExtractPlatformTools ( platformTools );
				this.overallProgress.SetValue ( 4 );

				this.overallStatus.SetText ( Properties.Resources.DownloadingSdkToolsMessage );
				this.LogDebug ( "Getting sdk tools" );
				string sdkTools = UseWebDownload ? DownloadSdkTools ( ) : Path.Combine ( Path.GetTempPath ( ), ToolsArchiveName );
				this.overallProgress.SetValue ( 5 );
				this.downloadStatus.SetText ( string.Empty );
				this.downloadProgress.SetValue ( 0 );

				this.overallStatus.SetText ( Properties.Resources.ExtractingSdkToolsMessage );
				this.LogDebug ( "Extracting sdk tools archive" );
				ExtractSdkTools ( sdkTools );
				this.overallProgress.SetValue ( 6 );

				this.downloadProgress.SetMaximum ( 0 );
				this.downloadProgress.SetMinimum ( 0 );
				this.downloadProgress.SetValue ( 0 );
				this.downloadStatus.SetText ( string.Empty );

				this.overallStatus.SetText ( Properties.Resources.SettingupSdkPathMessage );
				this.LogDebug ( "Setting up SDK path in registry" );
				SetupSdkPath ( );
				this.overallProgress.SetValue ( 7 );

				this.LogDebug ( "Download step completed" );
				FinishStep ( );
			} catch ( ThreadAbortException ) {

			} catch ( Exception ex ) {
				this.LogFatal ( ex.Message, ex );
				Wizard.Error ( ex );
			}
		}

		/// <summary>
		/// SDKs the path cleanup.
		/// </summary>
		private void SdkPathCleanup ( ) {
			try {
				if ( Wizard.UseExistingSdk ) {
					return;
				}

				Thread.Sleep ( 500 );
		
				if ( Directory.Exists ( SdkPath ) ) {
					Directory.Delete ( SdkPath, true );
				}
				Directory.CreateDirectory ( SdkPath );
			} catch ( Exception ex ) {
				throw;
			}
		}

		private string ToolsArchiveName {
			get { return string.Format ( "tools_r{0}-windows.zip", TOOLS_REVISION ); }
		}

		private string PlatformArchiveName {
			get { return string.Format ( "android-{0}_r{1}-windows.zip", PLATFORM_VERSION, PLATFORM_REVISION ); }
		}

		/// <summary>
		/// Extracts the SDK tools.
		/// </summary>
		/// <param name="path">The path.</param>
		private void ExtractSdkTools ( string path ) {
			ExtractZip ( path, string.Format ( "tools_r{0}-windows", TOOLS_REVISION ), "tools", @"\tools\" );
		}

		/// <summary>
		/// Extracts the platform tools.
		/// </summary>
		/// <param name="path">The path.</param>
		private void ExtractPlatformTools ( string path ) {
			ExtractZip ( path, string.Format ( "android-{0}_r{1}-windows", PLATFORM_VERSION, PLATFORM_REVISION ), "platform", @"\tools\" );
		}

		/// <summary>
		/// Extracts the zip.
		/// </summary>
		/// <param name="zip">The zip.</param>
		/// <param name="rootFolder">The root folder.</param>
		/// <param name="newFolder">The new folder.</param>
		/// <param name="matchPath">The match path.</param>
		private void ExtractZip ( string zip, string rootFolder, string newFolder, string matchPath ) {
			try {
				long totalEntries = 0;
				using ( ZipFile z = new ZipFile ( zip ) ) {
					totalEntries = z.Count;
					this.downloadProgress.SetMaximum ( (int)z.Count );
					this.downloadProgress.SetMinimum ( 0 );
					this.downloadProgress.SetValue ( 0 );
					z.Close ( );
				}
				using ( FileStream zfs = new FileStream ( zip, FileMode.Open, FileAccess.Read ) ) {
					long currentEntryIndex = 0;

					using ( ZipInputStream zis = new ZipInputStream ( zfs ) ) {
						ZipEntry ze = zis.GetNextEntry ( );
						while ( ze != null ) {
							string entryPath = Path.GetDirectoryName ( ze.Name ).Replace ( rootFolder, newFolder );
							string fullDirectoryPath = Path.Combine ( SdkPath, entryPath );

							string fileName = Path.GetFileName ( ze.Name );
							string fullPath = Path.Combine ( fullDirectoryPath, fileName );

							if ( !fullPath.Contains ( matchPath ) ) {
								ze = zis.GetNextEntry ( );
								currentEntryIndex++;
								continue;
							}

							if ( !Directory.Exists ( fullDirectoryPath ) ) {
								Directory.CreateDirectory ( fullDirectoryPath );
							}

							this.downloadStatus.SetText ( string.Format ( CultureInfo.InvariantCulture, Properties.Resources.ExtractingFileFormat, ze.Name ) );
							if ( ze.IsFile ) {
								using ( FileStream fs = new FileStream ( fullPath, FileMode.Create, FileAccess.Write, FileShare.Read ) ) {
									int bread = 0;
									byte[] buffer = new byte[ 2048 ];
									while ( ( bread = zis.Read ( buffer, 0, buffer.Length ) ) > 0 ) {
										fs.Write ( buffer, 0, bread );
									}
									fs.Close ( );
								}
							}
							this.downloadProgress.SetValue ( (int)currentEntryIndex );
							ze = zis.GetNextEntry ( );
							currentEntryIndex++;
						}
						zis.Close ( );
					}
					zfs.Close ( );
					this.downloadProgress.SetValue ( 0 );
					this.downloadStatus.SetText ( string.Empty );
				}
			} catch ( Exception ex ) {
				throw;
			}
		}

		/// <summary>
		/// Setups the SDK path.
		/// </summary>
		private void SetupSdkPath ( ) {
			using ( RegistryKey key = Registry.LocalMachine.CreateSubKey ( WizardForm.ROOT_REGISTRY_KEY ) ) {
				if ( key != null ) {
					key.SetValue ( WizardForm.SDK_PATH_REGISTRY_VALUE, SdkPath );
				} else {
					throw new UnauthorizedAccessException ( Properties.Resources.RegistryAccessExceptionMessage );
				}
			}
		}

		/// <summary>
		/// Finishes the step.
		/// </summary>
		private void FinishStep ( ) {
			Wizard.Next ( );
		}

		/// <summary>
		/// Downloads the SDK tools.
		/// </summary>
		/// <returns></returns>
		private string DownloadSdkTools ( ) {
			string tempFile = string.Empty;
			try {
				XmlElement ele = Repository.DocumentElement.SelectSingleNode ( ToolsXpath, this.NSManager ) as XmlElement;
				if ( ele == null ) {
					throw new ArgumentException ( Properties.Resources.SdkUrlNotFoundMessage );
				}
				int size = 0;
				int.TryParse ( ele.SelectSingleNode ( "sdk:size", NSManager ).InnerText, out size );
				string file = ele.SelectSingleNode ( "sdk:url", NSManager ).InnerText;
				tempFile = Path.Combine ( Path.GetTempPath ( ), file );
				string toolsUrl = string.Format ( URL_FORMAT, file );

				DownloadFile ( toolsUrl, tempFile, size );
			} catch ( WebException wex ) {
				if ( wex.Message.Contains ( Properties.Resources.ProxyExceptionMessage ) && proxyRetryCount < PROXY_RETRY_MAX ) {
					//get credentials
					proxyRetryCount++;
					if ( CredentialsDialog.ShowDialog ( this.FindForm ( ), true ) == DialogResult.OK ) {
						if ( string.IsNullOrEmpty ( CredentialsDialog.Username ) || string.IsNullOrEmpty ( CredentialsDialog.Password ) ) {
							throw;
						} else {
							ProxyCredentials = new NetworkCredential ( CredentialsDialog.Username, CredentialsDialog.Password );
						}
					} else {
						throw;
					}
					return DownloadSdkTools ( );
				} else {
					throw;
				}
			}
			proxyRetryCount = 0;
			return tempFile;
		}

		/// <summary>
		/// Downloads the platform tools.
		/// </summary>
		/// <returns></returns>
		private string DownloadPlatformTools ( ) {
			string tempFile = string.Empty;
			try {
				XmlElement ele = Repository.DocumentElement.SelectSingleNode ( PlatformXpath, this.NSManager ) as XmlElement;
				if ( ele == null ) {
					throw new ArgumentException ( Properties.Resources.PlatformUrlNotFoundMessage );
				}
				int size = 0;
				int.TryParse ( ele.SelectSingleNode ( "sdk:size", NSManager ).InnerText, out size );
				string file = ele.SelectSingleNode ( "sdk:url", NSManager ).InnerText;
				tempFile = Path.Combine ( Path.GetTempPath ( ), file );
				string toolsUrl = string.Format ( URL_FORMAT, file );

				DownloadFile ( toolsUrl, tempFile, size );
			} catch ( WebException wex ) {
				if ( wex.Message.Contains ( Properties.Resources.ProxyExceptionMessage ) && proxyRetryCount < PROXY_RETRY_MAX ) {
					//get credentials
					proxyRetryCount++;
					if ( CredentialsDialog.ShowDialog ( this.FindForm ( ), true ) == DialogResult.OK ) {
						if ( string.IsNullOrEmpty ( CredentialsDialog.Username ) || string.IsNullOrEmpty ( CredentialsDialog.Password ) ) {
							throw;
						} else {
							ProxyCredentials = new NetworkCredential ( CredentialsDialog.Username, CredentialsDialog.Password );
						}
					} else {
						throw;
					}
					return DownloadSdkTools ( );
				} else {
					throw;
				}
			}
			proxyRetryCount = 0;
			return tempFile;
		}

		/// <summary>
		/// Downloads the file.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <param name="file">The file.</param>
		/// <param name="size">The size.</param>
		private void DownloadFile ( string url, string file, int size ) {
			try {

				this.LogDebug ( "Downloading {0}", url );
				if ( File.Exists ( file ) ) {
					File.Delete ( file );
				}

				int totalDownloaded = 0;
				this.downloadProgress.SetMaximum ( size );
				this.downloadProgress.SetMinimum ( 0 );
				this.downloadProgress.SetValue ( 0 );

				HttpWebRequest req = HttpWebRequest.Create ( url ) as HttpWebRequest;
				if ( req.Proxy != null && ProxyCredentials != null ) {
					req.Proxy.Credentials = ProxyCredentials;
				}
				req.UserAgent = string.Format ( CultureInfo.InvariantCulture, Properties.Resources.UserAgent, this.GetType ( ).Assembly.GetName ( ).Version.ToString ( ) );
				using ( HttpWebResponse resp = req.GetResponse ( ) as HttpWebResponse ) {
					using ( Stream rstream = resp.GetResponseStream ( ) ) {
						using ( FileStream fs = new FileStream ( file, FileMode.Create, FileAccess.Write ) ) {
							byte[] buffer = new byte[ 1024 ];
							int bytesRead = 0;
							while ( ( bytesRead = rstream.Read ( buffer, 0, buffer.Length ) ) > 0 ) {
								totalDownloaded += bytesRead;
								if ( size > 0 ) {
									this.downloadProgress.IncrementExt ( bytesRead );
									this.downloadStatus.SetText ( string.Format ( CultureInfo.InvariantCulture, Properties.Resources.DownloadInfoFormat, totalDownloaded, size ) );
								}
								fs.Write ( buffer, 0, bytesRead );
							}
							fs.Close ( );
						}
						rstream.Close ( );
					}
				}
			} catch ( Exception ex ) {
				Console.WriteLine ( ex.ToString ( ) );
			}
		}

		/// <summary>
		/// Gets the tools xpath.
		/// </summary>
		/// <value>The tools xpath.</value>
		private string ToolsXpath {
			get {
				return string.Format ( CultureInfo.InvariantCulture, Properties.Resources.ToolsArchiveXPath, TOOLS_REVISION );
			}
		}

		/// <summary>
		/// Gets the platform xpath.
		/// </summary>
		/// <value>The platform xpath.</value>
		private string PlatformXpath {
			get {
				return string.Format ( CultureInfo.InvariantCulture, Properties.Resources.PlatformArchiveXPath, PLATFORM_VERSION, PLATFORM_REVISION );
			}
		}

		/// <summary>
		/// Gets or sets the SDK path.
		/// </summary>
		/// <value>The SDK path.</value>
		private string SdkPath { get; set; }

		/// <summary>
		/// Gets or sets the repository.
		/// </summary>
		/// <value>The repository.</value>
		private XmlDocument Repository { get; set; }

		/// <summary>
		/// Gets the NS manager.
		/// </summary>
		/// <value>The NS manager.</value>
		private XmlNamespaceManager NSManager {
			get {
				XmlNamespaceManager nsm = new XmlNamespaceManager ( Repository.NameTable );
				nsm.AddNamespace ( string.Empty, SDK_XML_NAMESPACE );
				nsm.AddNamespace ( "sdk", SDK_XML_NAMESPACE );
				return nsm;
			}
		}

		/// <summary>
		/// Initializes the component.
		/// </summary>
		protected override void InitializeComponent ( ) {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager ( typeof ( DownloadPanel ) );
			this.overallStatus = new System.Windows.Forms.Label ( );
			this.downloadStatus = new System.Windows.Forms.Label ( );
			this.overallProgress = new System.Windows.Forms.ProgressBar ( );
			this.downloadProgress = new System.Windows.Forms.ProgressBar ( );
			this.label1 = new System.Windows.Forms.Label ( );
			this.SuspendLayout ( );
			// 
			// overallStatus
			// 
			this.overallStatus.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.overallStatus.AutoEllipsis = true;
			this.overallStatus.Location = new System.Drawing.Point ( 33, 162 );
			this.overallStatus.Name = "overallStatus";
			this.overallStatus.Size = new System.Drawing.Size ( 511, 17 );
			this.overallStatus.TabIndex = 4;
			this.overallStatus.Font = new System.Drawing.Font ( "Tahoma", 8 );
			this.overallStatus.Text = "";
			// 
			// downloadStatus
			// 
			this.downloadStatus.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.downloadStatus.AutoEllipsis = true;
			this.downloadStatus.Location = new System.Drawing.Point ( 33, 92 );
			this.downloadStatus.Name = "downloadStatus";
			this.downloadStatus.Size = new System.Drawing.Size ( 511, 17 );
			this.downloadStatus.TabIndex = 3;
			this.downloadStatus.Font = new System.Drawing.Font ( "Tahoma", 8 );
			this.downloadStatus.Text = "";
			// 
			// overallProgress
			// 
			this.overallProgress.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.overallProgress.Location = new System.Drawing.Point ( 33, 182 );
			this.overallProgress.Name = "overallProgress";
			this.overallProgress.Size = new System.Drawing.Size ( 511, 24 );
			this.overallProgress.TabIndex = 2;
			// 
			// downloadProgress
			// 
			this.downloadProgress.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.downloadProgress.Location = new System.Drawing.Point ( 33, 112 );
			this.downloadProgress.Name = "downloadProgress";
			this.downloadProgress.Size = new System.Drawing.Size ( 511, 24 );
			this.downloadProgress.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.label1.Location = new System.Drawing.Point ( 33, 22 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size ( 511, 58 );
			this.label1.Font = new System.Drawing.Font ( "Tahoma", 8 );
			this.label1.TabIndex = 0;
			this.label1.Text = resources.GetString ( "label1.Text" );
			this.ResumeLayout ( false );

		}

		/// <summary>
		/// Gets the repository.
		/// </summary>
		/// <returns></returns>
		private XmlDocument GetRepository ( ) {
			try {
				XmlDocument doc = new XmlDocument ( );
				string url = string.Format ( URL_FORMAT, REPOSITORY );
				HttpWebRequest req = HttpWebRequest.Create ( url ) as HttpWebRequest;
				if ( req.Proxy != null && ProxyCredentials != null ) {
					req.Proxy.Credentials = ProxyCredentials;
				}
				req.UserAgent = string.Format ( CultureInfo.InvariantCulture, Properties.Resources.UserAgent, this.GetType ( ).Assembly.GetName ( ).Version.ToString ( ) );
				using ( HttpWebResponse resp = req.GetResponse ( ) as HttpWebResponse ) {
					using ( Stream strm = resp.GetResponseStream ( ) ) {
						doc.Load ( strm );
						this.downloadProgress.SetValue ( 1 );
					}
				}
				proxyRetryCount = 0;
				return doc;
			} catch ( WebException wex ) {
				if ( wex.Message.Contains ( Properties.Resources.ProxyExceptionMessage ) && proxyRetryCount < PROXY_RETRY_MAX ) {
					//get credentials
					proxyRetryCount++;
					if ( CredentialsDialog.ShowDialog ( this.FindForm ( ), true ) == DialogResult.OK ) {
						if ( string.IsNullOrEmpty ( CredentialsDialog.Username ) || string.IsNullOrEmpty ( CredentialsDialog.Password ) ) {
							throw;
						} else {
							ProxyCredentials = new NetworkCredential ( CredentialsDialog.Username, CredentialsDialog.Password );
						}
					} else {
						throw;
					}
					return GetRepository ( );
				} else {
					throw;
				}
			} catch ( Exception ex ) {
				throw;
			}
		}

	}
}
