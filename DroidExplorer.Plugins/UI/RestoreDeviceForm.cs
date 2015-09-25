using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DroidExplorer.Configuration;
using DroidExplorer.Core;
using DroidExplorer.Core.UI;
using Camalot.Common.Extensions;
using DroidExplorer.Core.Plugins;

namespace DroidExplorer.Plugins.UI {
	/// <summary>
	/// 
	/// </summary>
	public partial class RestoreDeviceForm : PluginForm {
		private delegate void CloseDelegate ( PluginForm f );

		/// <summary>
		/// Initializes a new instance of the <see cref="RestoreDeviceForm" /> class.
		/// </summary>
		/// <param name="file">The file.</param>
		public RestoreDeviceForm ( IPluginHost host, string file ) : base(host) {
			InitializeComponent ( );
			this.BackgroundImage = DroidExplorer.Resources.Images.BackupDialog;
			this.BackupFile = new FileInfo ( file );
			if ( this.BackupFile.Exists ) {
				this.device.Text = host.GetDeviceFriendlyName(host.Device);
				this.backupName.Text = Path.GetFileNameWithoutExtension ( BackupFile.Name );
			}
		}

		/// <summary>
		/// Gets or sets the backup file.
		/// </summary>
		/// <value>
		/// The backup file.
		/// </value>
		public FileInfo BackupFile { get; set; }
		private string TargetDevice { get; set; }
		/// <summary>
		/// Handles the Click event of the restore control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
		private void restore_Click ( object sender, EventArgs e ) {
			this.cancel.Enabled = false;
			this.restore.Enabled = false;

			this.progress.Visible = this.unlock.Visible = true;

			/// <summary>
			/// Initializes a new instance of the <see cref="RestoreDeviceForm" /> class.
			/// </summary>
			new Thread ( delegate ( ) {
				this.LogDebug ( "Starting Restore" );

				CommandRunner.Instance.DeviceRestore (this.TargetDevice, this.BackupFile.FullName );

				this.LogDebug ( "Restore Completed" );
				if ( this.InvokeRequired ) {
					this.Invoke ( (CloseDelegate)delegate ( PluginForm f ) {
						f.Close ( );
					}, this );
				}
			} ).Start ( );
		}

		/// <summary>
		/// Handles the Click event of the cancel control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
		private void cancel_Click ( object sender, EventArgs e ) {
			this.Close ( );
		}
	}
}
