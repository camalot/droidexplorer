using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DroidExplorer.Core;
using Microsoft.Win32;
using DroidExplorer.Core.Configuration;
using DroidExplorer.Core.Exceptions;

namespace DroidExplorer.Configuration.UI {
	public class SdkUIEditor : Control, IUIEditor {
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
		private Label adbBuildToolsVersion;
		private Label label5;

		private string usbDriverVersionString;

		/// <summary>
		/// Initializes a new instance of the <see cref="SdkUIEditor"/> class.
		/// </summary>
		public SdkUIEditor ( ) {
			InitializeComponent ( );
			sdkPath.Text = Settings.Instance.SystemSettings.SdkPath;
			adbVersion.Text = CommandRunner.Instance.GetAdbVersion ( );
			aaptVersion.Text = CommandRunner.Instance.GetAaptVersion ( );
			adbBuildToolsVersion.Text = GetBuildToolsVersion();
			driverVersion.Text = "Loading...";
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
		/// Initializes the component.
		/// </summary>
		private void InitializeComponent ( ) {
			this.label1 = new System.Windows.Forms.Label();
			this.browse = new System.Windows.Forms.Button();
			this.sdkPath = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.driverVersion = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.aaptVersion = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.adbVersion = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.adbBuildToolsVersion = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Android SDK Path:";
			// 
			// browse
			// 
			this.browse.Location = new System.Drawing.Point(323, 25);
			this.browse.Name = "browse";
			this.browse.Size = new System.Drawing.Size(75, 23);
			this.browse.TabIndex = 1;
			this.browse.Text = "&Browse";
			this.browse.UseVisualStyleBackColor = true;
			this.browse.Click += new System.EventHandler(this.browse_Click);
			// 
			// sdkPath
			// 
			this.sdkPath.Location = new System.Drawing.Point(13, 27);
			this.sdkPath.Name = "sdkPath";
			this.sdkPath.ReadOnly = true;
			this.sdkPath.Size = new System.Drawing.Size(305, 20);
			this.sdkPath.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.adbBuildToolsVersion);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.driverVersion);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.aaptVersion);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.adbVersion);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(13, 65);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(385, 161);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "SDK Tools Information";
			// 
			// driverVersion
			// 
			this.driverVersion.Location = new System.Drawing.Point(258, 97);
			this.driverVersion.Name = "driverVersion";
			this.driverVersion.Size = new System.Drawing.Size(121, 13);
			this.driverVersion.TabIndex = 8;
			this.driverVersion.Text = "{0} ({1})";
			this.driverVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 97);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(101, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "USB Driver Version:";
			// 
			// aaptVersion
			// 
			this.aaptVersion.Location = new System.Drawing.Point(279, 70);
			this.aaptVersion.Name = "aaptVersion";
			this.aaptVersion.Size = new System.Drawing.Size(100, 13);
			this.aaptVersion.TabIndex = 6;
			this.aaptVersion.Text = "0.0.0.0";
			this.aaptVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 70);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(191, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "Android Asset Packaging Tool Version:";
			// 
			// adbVersion
			// 
			this.adbVersion.Location = new System.Drawing.Point(279, 44);
			this.adbVersion.Name = "adbVersion";
			this.adbVersion.Size = new System.Drawing.Size(100, 13);
			this.adbVersion.TabIndex = 4;
			this.adbVersion.Text = "0.0.0.0";
			this.adbVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(152, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Android Debug Bridge Version:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 21);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(139, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Android Build Tools Version:";
			// 
			// adbBuildToolsVersion
			// 
			this.adbBuildToolsVersion.Location = new System.Drawing.Point(279, 21);
			this.adbBuildToolsVersion.Name = "adbBuildToolsVersion";
			this.adbBuildToolsVersion.Size = new System.Drawing.Size(100, 13);
			this.adbBuildToolsVersion.TabIndex = 12;
			this.adbBuildToolsVersion.Text = "0.0.0.0";
			this.adbBuildToolsVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

			this.Controls.Add ( this.label1 );
			this.Controls.Add ( this.browse );
			this.Controls.Add ( this.sdkPath );
			this.Controls.Add ( this.groupBox1 );
			this.Dock = DockStyle.Fill;
			
			this.groupBox1.ResumeLayout ( false );
			this.groupBox1.PerformLayout ( );
			this.ResumeLayout ( false );

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

		private string GetBuildToolsVersion() {
			try {
				return FolderManagement.GetBuildToolVersion ().ToString();
			} catch(AdbException aex) {
				this.LogWarn(aex.Message);
				return "0.0.0.0";
			}
		}

		private bool VerifySdkPath ( String path ) {
			// i dont like this duplication. this should use code from somewhere else just passing in the test path.
			if(String.IsNullOrEmpty(path)) {
				return false;
			}
			try {
				DirectoryInfo sdk = new DirectoryInfo(path);



				if(!sdk.Exists) {
					return false;
				} else {
					DirectoryInfo tools = new DirectoryInfo(Path.Combine(sdk.FullName, "tools"));
					if(!tools.Exists) {
						return false;
					}

					// the sdk update moved adb to platform-tools
					DirectoryInfo ptools = new DirectoryInfo(Path.Combine(sdk.FullName, "platform-tools"));
					if(ptools.Exists) {
						FileInfo adb = new FileInfo(Path.Combine(ptools.FullName, CommandRunner.ADB_COMMAND));
						if(!adb.Exists) {
							return false;
						}
					} else {
						FileInfo adb = new FileInfo(Path.Combine(tools.FullName, CommandRunner.ADB_COMMAND));
						if(!adb.Exists) {
							return false;
						}
					}
					return GetLatestSdkPlatform(path);
				}
			} catch(ArgumentException aex) {
				return false;
			} catch(Exception e) {
				this.LogError(e.Message, e);
				return false;
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
