using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Core;
using DroidExplorer.Core.Exceptions;
using Microsoft.Win32;

namespace DroidExplorer.Configuration {
	public partial class DummyForm : Form {

		private delegate void SetControlTextDelegate ( Control ctrl, string text );

		private Label label1;
		private Button browse;
		private TextBox sdkPath;
		private GroupBox groupBox1;
		private Label aaptVersion;
		private Label label4;
		private Label adbVersion;
		private Label label2;
		private Label label3;
		private Label driverVersion;

		private string usbDriverVersionString;

		/// <summary>
		/// Initializes a new instance of the <see cref="SdkUIEditor"/> class.
		/// </summary>
		public DummyForm() {
			InitializeComponent ( );
			sdkPath.Text = Settings.Instance.SystemSettings.SdkPath;
			adbVersion.Text = CommandRunner.Instance.GetAdbVersion ( );
			aaptVersion.Text = CommandRunner.Instance.GetAaptVersion ( );
			adbBuildToolsVersion.Text = GetBuildToolsVersion();
			driverVersion.Text = "Loading...";
		}

		private string GetBuildToolsVersion() {
			try {
				return FolderManagement.GetBuildToolVersion().ToString();
			} catch(AdbException aex) {
				this.LogWarn(aex.Message);
				return "0.0.0.0";
			}
		}

		#region IUIEditor Members

		/// <summary>
		/// Sets the source object.
		/// </summary>
		/// <param name="obj">The obj.</param>
		public void SetSourceObject ( object obj ) {
			usbDriverVersionString = obj != null ? obj.ToString() : string.Empty;
			if ( this.InvokeRequired ) {
				this.Invoke ( new SetControlTextDelegate ( SetControlText ), driverVersion, usbDriverVersionString );
			} else {
				SetControlText ( driverVersion, usbDriverVersionString );
			}
		}

		#endregion

		/// <summary>
		/// Sets the control text.
		/// </summary>
		/// <param name="ctrl">The CTRL.</param>
		/// <param name="text">The text.</param>
		private void SetControlText ( Control ctrl, string text ) {
			ctrl.Text = text;
		}

		/// <summary>
		/// Handles the Click event of the browse control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void browse_Click ( object sender, EventArgs e ) {
			FolderBrowserDialog fbd = new FolderBrowserDialog ( );
			fbd.Description = "Select Root of Android SDK Directory";
			if ( fbd.ShowDialog ( this.FindForm ( ) ) == DialogResult.OK ) {
				string dir = fbd.SelectedPath;
				if ( VerifySdkPath ( dir ) ) {
					this.sdkPath.Text = dir;
					Settings.Instance.SystemSettings.SdkPath = this.sdkPath.Text;
					CommandRunner.Instance.SdkPath = Settings.Instance.SystemSettings.SdkPath;
					Settings.Instance.SystemSettings.UseExistingSdk = true;
					adbVersion.Text = CommandRunner.Instance.GetAdbVersion ( );
					aaptVersion.Text = CommandRunner.Instance.GetAaptVersion ( );
				} else {
					MessageBox.Show ( "Unable to locate required SDK tools in the selected directory. Make sure you are selecting the ROOT of the SDK directory.", "Error Locating SDK Tools", MessageBoxButtons.OK, MessageBoxIcon.Error );
				}
			}
		}

		private bool VerifySdkPath ( String path ) {
			DirectoryInfo sdk = new DirectoryInfo ( path );
			if ( !sdk.Exists ) {
				return false;
			} else {
				DirectoryInfo tools = new DirectoryInfo ( Path.Combine ( sdk.FullName, "tools" ) );
				if ( !tools.Exists ) {
					return false;
				}

				FileInfo adb = new FileInfo ( Path.Combine ( tools.FullName, "adb.exe" ) );
				if ( !adb.Exists ) {
					return false;
				}
				return GetLatestSdkPlatform ( path );
			}
		}

		public bool GetLatestSdkPlatform ( String path ) {
			DirectoryInfo sdk = new DirectoryInfo ( path );
			if ( !sdk.Exists ) {
				return false;
			} else {
				DirectoryInfo platforms = new DirectoryInfo ( Path.Combine ( path, "platforms" ) );
				String platformBaseName = "android-";
				List<String> versions = new List<String> ( );
				if ( !platforms.Exists ) {
					return false;
				}
				foreach ( var item in platforms.GetDirectories ( platformBaseName + "*" ) ) {
					try {
						String version = item.Name.Substring ( platformBaseName.Length );
						this.LogDebug ( "Found platform version {0}", version );
						versions.Add ( version );
					} catch ( Exception ex ) {
						this.LogError ( ex.Message, ex );
					}
				}

				if ( versions.Count == 0 ) {
					return false;
				}

				versions.Sort ( );

				try {
					using ( RegistryKey key = Registry.CurrentUser.CreateSubKey ( String.Format(@"{0}\InstallPath",RegistrySettings.SETTINGS_KEY ) ) ) {
						if ( key != null ) {
							this.LogDebug ( "Setting platform data: {0}", versions[versions.Count - 1] );
							key.SetValue ( "Platform", versions[versions.Count - 1] );
							return true;
						}
					}
				} catch ( UnauthorizedAccessException ex ) {
					this.LogWarn ( "Unable to set platform version", ex );
				}

				this.LogWarn ( "Unable to set platform version" );
				return false;
			}
		}
	}
}
