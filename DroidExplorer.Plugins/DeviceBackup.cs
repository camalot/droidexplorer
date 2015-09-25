using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Core;
using DroidExplorer.Core.Plugins;
using DroidExplorer.Core.UI;
using DroidExplorer.Plugins.UI;

namespace DroidExplorer.Plugins {
	/// <summary>
	/// 
	/// </summary>
	public class DeviceBackup : BasePlugin {
		/// <summary>
		/// Initializes a new instance of the <see cref="DeviceBackup" /> class.
		/// </summary>
		/// <param name="host">The host.</param>
		public DeviceBackup ( IPluginHost host )
			: base ( host ) {

		}


		#region IPluginExtendedInfo Members

		/// <summary>
		/// Gets the author.
		/// </summary>
		/// <value>
		/// The author.
		/// </value>
		public override string Author {
			get { return "Ryan Conrad"; }
		}

		/// <summary>
		/// Gets the URL.
		/// </summary>
		/// <value>
		/// The URL.
		/// </value>
		public override string Url {
			get { return DroidExplorer.Resources.Strings.ApplicationWebsiteUrl; }
		}

		/// <summary>
		/// Gets the contact.
		/// </summary>
		/// <value>
		/// The contact.
		/// </value>
		public override string Contact {
			get { return string.Empty; }
		}

		/// <summary>
		/// Gets the copyright.
		/// </summary>
		/// <value>
		/// The copyright.
		/// </value>
		public override string Copyright {
			get { return String.Format ( "Copyright © Ryan Conrad 2009 - {0}", DateTime.Now.Year ); }
		}

		#endregion

		#region IPlugin Members

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public override string Name {
			get { return "Device Backup"; }
		}
		public override string Group { get { return "Applications and Data"; } }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public override string Description {
			get { return "Performs a backup/restore of your device."; }
		}


		/// <summary>
		/// Gets the image.
		/// </summary>
		/// <value>
		/// The image.
		/// </value>
		public override System.Drawing.Image Image {
			get { return DroidExplorer.Plugins.Properties.Resources.backup; }
		}

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		public override string Text {
			get { return "Backup Device"; }
		}

		/// <summary>
		/// Gets a value indicating whether [create tool button].
		/// </summary>
		/// <value>
		///   <c>true</c> if [create tool button]; otherwise, <c>false</c>.
		/// </value>
		public override bool CreateToolButton {
			get { return true; }
		}


		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		/// <param name="args">The args.</param>
		public override void Execute ( IPluginHost pluginHost, Core.IO.LinuxDirectoryInfo currentDirectory, string[] args ) {
			var arguments = new Arguments ( args ?? new string[] { "/backup" } );
			if ( arguments.Contains ( "b", "backup" ) || !arguments.Contains ( "r", "restore" ) ) {
				if ( !PluginHelper.CanExecute ( this ) ) {
					MessageBox.Show ( "This plugin cannot execute on this device. The platform is not supported", "Unsupported Device", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
					return;
				}

				var bdf = new BackupDeviceForm ( pluginHost );

				if ( pluginHost.GetHostWindow ( ) != null ) {
					bdf.Show ( );
				} else {
					bdf.ShowInTaskbar = true;
					bdf.ShowDialog ( );
				}
			} else if ( arguments.Contains ( "r", "restore" ) ) {
				var backupFile = String.Empty;
				var bc = new BackupConverter ( pluginHost );
				var defDevice = String.Empty;

				var isExtended = arguments.Contains ( "extended" );

				if ( arguments.Contains ( "file", "f" ) ) {
					backupFile = System.IO.Path.GetFullPath ( arguments["file", "f"] );
				} else {
					var ofd = new System.Windows.Forms.OpenFileDialog ( );
					ofd.Title = "Select Android Backup";
					ofd.Filter = "Android Backup|*.ab;*.abex";
					ofd.CheckFileExists = true;
					ofd.Multiselect = false;
					if ( ofd.ShowDialog ( ) == DialogResult.OK ) {
						backupFile = ofd.FileName;
					} else {
						return;
					}
				}

				if ( isExtended && bc.IsExtendedBackup ( backupFile ) ) {
					var devices = CommandRunner.Instance.GetDevices ( ).Select ( d => d.SerialNumber ).ToList ( );
					this.LogDebug ( "Restoring from Extended Backup" );
					var extInfo = bc.GetExtendedHeader ( backupFile );
					if ( extInfo != null ) {
						this.LogDebug ( "Found Extended Info" );
						defDevice = extInfo.Device;
						this.LogDebug ( "Backup for {0}", defDevice );
						var device = devices.FirstOrDefault ( m => String.Compare ( defDevice, m, true ) == 0 );
						if ( String.IsNullOrEmpty ( device ) ) {
							MessageBox.Show ( "The device that this backup is tied to is not connected. If you want to apply to another device, first convert to a normal Android Backup", "Device not connected", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1 );
							return;
						}
					} else {
						this.LogDebug ( "Unable to load Extended info" );
						defDevice = UseDevicePicker ( );
					}
				} else {
					this.LogDebug ( "Basic Backup" );
					defDevice = UseDevicePicker ( );
				}

				if ( !String.IsNullOrEmpty ( defDevice ) ) {
					Application.Run ( new RestoreDeviceForm (this.PluginHost, backupFile ) );
				}
			}
		}

		private String UseDevicePicker ( ) {
			var result = String.Empty;
			// if its not an extended backup, we need to get a device
			var devices = CommandRunner.Instance.GetDevices ( ).Select ( d => d.SerialNumber ).ToList ( );
			if ( devices.Count != 1 ) {
				GenericDeviceSelectionForm selectDevice = new GenericDeviceSelectionForm ( );
				if ( selectDevice.ShowDialog ( ) == DialogResult.OK ) {
					result = selectDevice.SelectedDevice;
					CommandRunner.Instance.DefaultDevice = result;
				}
			} else {
				result = devices[0];
			}
			return result;
		}

		/// <summary>
		/// Indicates the minimum SDK Tools Version that is required for this plugin. If no requirement, then default the value to 0.
		/// </summary>
		public override int MinimumSDKToolsVersion {
			get { return 13; }
		}
		/// <summary>
		/// Indicates the minimum SDK Platform Tools Version that is required for this plugin. If no requirement, then default the value to 0.
		/// </summary>
		public override int MinimumSDKPlatformToolsVersion {
			get { return 13; }
		}
		#endregion

	}
}
