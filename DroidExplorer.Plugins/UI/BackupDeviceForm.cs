using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DroidExplorer.Configuration;
using DroidExplorer.Core;
using DroidExplorer.Core.Plugins;
using DroidExplorer.Core.UI;

namespace DroidExplorer.Plugins.UI {
	public partial class BackupDeviceForm : PluginForm {
		private delegate void CloseDelegate ( PluginForm f );
		private delegate bool GetCheckedDelegate ( CheckBox c );

		public BackupDeviceForm ( IPluginHost host ) : base( host ) {
			InitializeComponent ( );
			this.BackgroundImage = DroidExplorer.Resources.Images.BackupDialog;
		}


		private void backup_Click ( object sender, EventArgs e ) {
			this.unlock.Visible = true;
			this.progress.Visible = true;

			this.cancel.Enabled = false;
			this.backup.Enabled = false;

			this.system.Enabled = this.apk.Enabled = this.shared.Enabled = this.extended.Enabled = false;

			new Thread ( delegate ( ) {
				var output = new System.IO.FileInfo ( System.IO.Path.Combine ( Environment.GetEnvironmentVariable("USERPROFILE"),
					String.Format ( @"Android Backups\{1}\{0}.ab",
					DateTime.Now.ToString ( "s" ).Replace ( ":", "" ),
					KnownDeviceManager.Instance.GetDeviceFriendlyName( CommandRunner.Instance.DefaultDevice ) ) ) );
				var bapk = false;
				var bshared = false; 
				var bsystem = false;
				var bextended = false;

				if ( this.InvokeRequired ) {
					bapk = (bool)this.Invoke((GetCheckedDelegate)delegate(CheckBox c) {
						return c.Checked;
					},this.apk);
					bshared = (bool)this.Invoke((GetCheckedDelegate)delegate(CheckBox c) {
						return c.Checked;
					},this.shared);
					bsystem = (bool)this.Invoke((GetCheckedDelegate)delegate(CheckBox c) {
						return c.Checked;
					},this.system);
					bextended = (bool)this.Invoke((GetCheckedDelegate)delegate(CheckBox c) {
						return c.Checked;
					},this.extended);
				}

				CommandRunner.Instance.DeviceBackup ( output.FullName, bapk, bsystem, bshared );
				if ( bextended ) {
					var bc = new BackupConverter ( PluginHost );
					bc.ConvertTo(BackupConverter.ConvertType.Extended,output.FullName);
					System.IO.File.Delete(output.FullName);
				}
				if ( this.InvokeRequired ) {
					this.Invoke ( (CloseDelegate)delegate ( PluginForm f ) {
						f.Close ( );
					}, this );
				}
			} ).Start ( );
		}

		private void cancel_Click ( object sender, EventArgs e ) {
			this.Close ( );
		}

		private void readMore_LinkClicked ( object sender, LinkLabelLinkClickedEventArgs e ) {
			new Process {
				StartInfo = new ProcessStartInfo {
					FileName = "http://blog.twimager.com/2012/08/introducing-abex-file-format.html"
				}
			}.Start ( );
		}
	}
}
