using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using DroidExplorer.Bootstrapper.UI;
using System.Diagnostics;
using DroidExplorer.Bootstrapper.Properties;

namespace DroidExplorer.Bootstrapper.Panels {
	public class UseExistingSdkPanel : WizardPanel {
		//private System.Windows.Forms.LinkLabel autoSetupLink;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button browse;
		private System.Windows.Forms.TextBox path;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel downloadSdkLink;

		private const string DOWNLOAD_SDK = "http://developer.android.com/sdk/index.html#Other";
									
		private const string SDK_INSTALLED_KEY = @"SOFTWARE\Android SDK Tools\";
		private const string SDK_INSTALLED_VALUE_NAME = "Path";
		private const string DE_INSTALLPATH_KEY = @"SOFTWARE\DroidExplorer\InstallPath";
		private const string DE_SDKPATH_VALUE_NAME = "SdkPath";

		internal UseExistingSdkPanel ( )
			: base ( ) {

		}

		public UseExistingSdkPanel ( IWizard wizard )
			: base ( wizard ) {
			wizard.NextClick += new EventHandler ( wizard_NextClick );
		}

		void wizard_NextClick ( object sender, EventArgs e ) {
		}

		public override void InitializeWizardPanel ( ) {
			// get existing value from registry, if it exists.
			var sdkpath = CheckForInstalledSDK ( );
			if ( VerifySdkPath ( sdkpath ) ) {
				this.LogDebug ( "Verified existing path. Setting to existing." );
				this.Wizard.NextButton.Enabled = true;
				this.path.Text = sdkpath;
				SetupSdkPath ( sdkpath );
			} else {
				this.LogDebug ( "Unable to verify existing found SdkPath: {0}", sdkpath );
			}
		}

		protected override void InitializeComponent ( ) {
			this.downloadSdkLink = new System.Windows.Forms.LinkLabel ( );
			this.label2 = new System.Windows.Forms.Label ( );
			this.browse = new System.Windows.Forms.Button ( );
			this.path = new System.Windows.Forms.TextBox ( );
			this.label1 = new System.Windows.Forms.Label ( );
			this.SuspendLayout ( );

			// 
			// downloadSdkLink
			// 
			this.downloadSdkLink.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.downloadSdkLink.AutoSize = true;
			this.downloadSdkLink.Location = new System.Drawing.Point ( 10, 225 );
			this.downloadSdkLink.Name = "downloadSdkLink";
			this.downloadSdkLink.Size = new System.Drawing.Size ( 134, 13 );
			this.downloadSdkLink.TabIndex = 4;
			this.downloadSdkLink.TabStop = true;
			this.downloadSdkLink.Text = Resources.DownloadSDKMessage;
			this.downloadSdkLink.Click += new EventHandler ( downloadSdkLink_Click );
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point ( 13, 67 );
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size ( 96, 13 );
			this.label2.TabIndex = 3;
			this.label2.Text = Resources.SDKPathText;
			// 
			// browse
			// 
			this.browse.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.browse.Location = new System.Drawing.Point ( 486, 81 );
			this.browse.Name = "browse";
			this.browse.Size = new System.Drawing.Size ( 75, 23 );
			this.browse.TabIndex = 2;
			this.browse.Text = Resources.BrowseText;
			this.browse.UseVisualStyleBackColor = true;
			this.browse.Click += new EventHandler ( browse_Click );
			// 
			// path
			// 
			this.path.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.path.Location = new System.Drawing.Point ( 16, 83 );
			this.path.Name = "path";
			this.path.Size = new System.Drawing.Size ( 464, 20 );
			this.path.TabIndex = 1;
			this.path.ReadOnly = true;
			// 
			// label1
			// 
			this.label1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.label1.Location = new System.Drawing.Point ( 13, 13 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size ( 548, 37 );
			this.label1.TabIndex = 0;
			this.label1.Text = Resources.SelectSDKInfoMessage;

			this.Controls.AddRange ( new Control[] { this.label1, this.label2, this.path, this.browse, this.downloadSdkLink } );


			this.ResumeLayout ( false );

		}

		void browse_Click ( object sender, EventArgs e ) {
			FolderBrowserDialog fbd = new FolderBrowserDialog {
				Description = Resources.SelectSDKRootTitle,
				ShowNewFolderButton = false
			};
			if ( fbd.ShowDialog ( (Form)Wizard ) == DialogResult.OK ) {
				String path = fbd.SelectedPath;
				if ( VerifySdkPath ( path ) ) {
					Wizard.NextButton.Enabled = true;
					this.path.Text = path;
					SetupSdkPath ( path );
				} else {
					MessageBox.Show ( (Form)Wizard, Resources.NotSDKPathMessage, Resources.NotSDKPathTitle, MessageBoxButtons.OK, MessageBoxIcon.Error );
					Wizard.NextButton.Enabled = false;
				}
			}
		}

		void downloadSdkLink_Click( object sender, EventArgs e ) {
			Process.Start ( new ProcessStartInfo ( DOWNLOAD_SDK ) );
		}

		private string CheckForInstalledSDK ( ) {
			// get existing value from registry, if it exists.
			try {
				using ( var key = Registry.LocalMachine.OpenSubKey ( SDK_INSTALLED_KEY ) ) {
					if ( key != null ) {
						string s = (string)key.GetValue ( SDK_INSTALLED_VALUE_NAME, null );
						if ( !string.IsNullOrEmpty ( s ) ) {
							this.LogDebug ( "Found SDK in HKLM: {0}", s );
							return s;
						}
					}
				}
			} catch ( Exception e ) {
				this.LogWarning ( e.Message, e );
			}


			try {
				using ( var key = Registry.CurrentUser.OpenSubKey ( SDK_INSTALLED_KEY ) ) {
					if ( key != null ) {
						string s = (string)key.GetValue ( SDK_INSTALLED_VALUE_NAME, null );
						if ( !string.IsNullOrEmpty ( s ) ) {
							this.LogDebug ( "Found SDK in HKCU: {0}", s );
							return s;
						}
					}
				}
			} catch ( Exception e ) {
				this.LogWarning ( e.Message, e );
			}

			// if we are here, lets see if they have installed before and use the previous value:
			try {
				using (var key = Registry.LocalMachine.OpenSubKey( DE_INSTALLPATH_KEY ) ) {
					if(key != null ) {
						var s = (string)key.GetValue ( DE_SDKPATH_VALUE_NAME, null );
						if(!string.IsNullOrEmpty( s ) ) {
							this.LogDebug ( "Found SDK in from previous install: {0}", s );
							return s;
						}
					}
				}
			} catch ( Exception e ) {
				this.LogWarning ( e.Message, e );
			}

			return string.Empty;
		}

		private void SetupSdkPath ( string path ) {
			using ( RegistryKey key = Registry.LocalMachine.CreateSubKey ( WizardForm.ROOT_REGISTRY_KEY ) ) {
				if ( key != null ) {
					this.LogDebug ( "Setting SDKPath: {0}", path );
					key.SetValue ( WizardForm.SDK_PATH_REGISTRY_VALUE, path );
				} else {
					throw new UnauthorizedAccessException ( Properties.Resources.RegistryAccessExceptionMessage );
				}
			}
		}

		private bool VerifySdkPath ( String path ) {
			if ( String.IsNullOrEmpty(path) ) {
				this.LogWarning ( "Path to verify is empty. Failing Verification." );
				return false;
			}
			try {
				DirectoryInfo sdk = new DirectoryInfo ( path );
				if ( !sdk.Exists ) {
					this.LogWarning ( "SDK Path '{0}' does not exist. Failing Verification.", path );
					return false;
				} else {
					DirectoryInfo tools = new DirectoryInfo ( Path.Combine ( sdk.FullName, "tools" ) );
					if ( !tools.Exists ) {
						this.LogWarning ( "SDK tools Path '{0}' does not exist. Failing Verification.", tools );
						return false;
					}

					// the sdk update moved adb to platform-tools
					DirectoryInfo ptools = new DirectoryInfo ( Path.Combine ( sdk.FullName, "platform-tools" ) );
					if ( ptools.Exists ) {
						FileInfo adb = new FileInfo ( Path.Combine ( ptools.FullName, "adb.exe" ) );
						if ( !adb.Exists ) {
							this.LogWarning ( "adb.exe ('{0}') does not exist. Failing Verification.", adb );
							return false;
						}
					} else {
						FileInfo adb = new FileInfo ( Path.Combine ( tools.FullName, "adb.exe" ) );
						if ( !adb.Exists ) {
							this.LogWarning ( "adb.exe ('{0}') does not exist. Failing Verification.", adb );
							return false;
						}
					}
					return GetLatestSdkPlatform ( path );
				}
			} catch ( ArgumentException aex ) {
				this.LogError ( aex.Message, aex );
				return false;
			} catch ( Exception e ) {
				this.LogError ( e.Message, e );
				return false;
			}
		}

		public bool GetLatestSdkPlatform ( string path ) {
			DirectoryInfo sdk = new DirectoryInfo ( path );
			if ( !sdk.Exists ) {
				this.LogWarning ( "Sdk Path does not exist: '{0}'", sdk.FullName );
				return false;
			} else {
				var platforms = new DirectoryInfo ( Path.Combine ( path, "platforms" ) );
				var platformBaseName = "android-";
				var versions = new List<int> ( );
				foreach ( var item in platforms.GetDirectories ( platformBaseName + "*" ) ) {
					try {
						var version = item.Name.Substring ( platformBaseName.Length );
						this.LogDebug ( "Found platform version {0}", version );
						var vi = 0;
						if ( int.TryParse ( version, out vi ) ) {
							versions.Add ( vi );
						}
					} catch ( Exception ex ) {
						this.LogError ( ex.Message, ex );
					}
				}

				if ( versions.Count == 0 ) {
					this.LogError ( "No versions found: GetLatestSdkPlatform" );
					return false;
				}

				try {
					using ( RegistryKey key = Registry.LocalMachine.CreateSubKey ( WizardForm.ROOT_REGISTRY_KEY ) ) {
						if ( key != null ) {
							this.LogDebug ( "Setting platform data: {0}", versions.Max() );
							key.SetValue ( "Platform", versions.Max() );
							return true;
						}
					}
				} catch ( UnauthorizedAccessException ex ) {
					this.LogWarning ( "Unable to set platform version", ex );
				}

				this.LogWarning ( "Unable to set platform version" );
				return false;
			}
		}

	}
}
