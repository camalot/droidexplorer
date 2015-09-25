using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using DroidExplorer.Configuration.Net;
using DroidExplorer.Configuration;
using System.Linq;
using DroidExplorer.Core.UI.Renderers.ToolStrip;
//using DroidExplorer.Configuration;

namespace DroidExplorer.Core.UI {
	public partial class GenericDeviceSelectionForm : Form {
		List<DeviceListItem> devices = null;
		public GenericDeviceSelectionForm ( ) {
			InitializeComponent ( );
			ToolStripManager.Renderer = new VisualStudio2012Renderer ( );
			// icon setup for the toolstrip items
			refreshToolStripButton.Image = Resources.Images.Refresh;
			tcpipConnect.Image = Resources.Images.wifi;
			adbRestart.Image = Resources.Images.WorkflowActivity_16xLG;

			SelectedDevice = String.Empty;
			RefreshDevices ( );
		}

		private ListViewGroup DevicesGroup { get; set; }
		private ListViewGroup OtherGroup { get; set; }

		public string SelectedDevice { get; set; }

		private void RefreshDevices ( ) {
			// BUG: 15695
		  devices = CommandRunner.Instance.GetDevices ( ).ToList();
			//devices = CommandRunner.Instance.Bridge.Devices.Select(x => new DeviceListItem(x.SerialNumber, x.State.ToString(), x.Product, x.Model, x.DeviceProperty)).ToList();
			devicesList.Items.Clear ( );

			// the default device type
			String model = "[DEFAULT]";

			DevicesGroup = new ListViewGroup ( "Devices", "Devices" );
			OtherGroup = new ListViewGroup ( "Other", "Other" );

			devicesList.Groups.Add ( OtherGroup );
			devicesList.Groups.Add ( DevicesGroup );


			// get the device icons for the devices
			// get the default image
			var fi = CloudImage.Instance.GetImage ( model );
			if ( fi.Exists ) {
				Image img = Image.FromFile ( fi.FullName );
				imageList.Images.Add ( Path.GetFileNameWithoutExtension ( fi.Name ), img );
			}

			devices.ForEach ( d => {
				try {
					// get the image for each device
					model = d.DeviceName;
					fi = CloudImage.Instance.GetImage ( model );
					if ( fi.Exists ) {
						Image img = Image.FromFile ( fi.FullName );
						imageList.Images.Add ( Path.GetFileNameWithoutExtension ( fi.Name ), img );
					}
				} catch ( Exception e ) {
					this.LogWarn ( e.ToString ( ) );
					model = "[DEFAULT]";
				}
				var friendlyName = KnownDeviceManager.Instance.GetDeviceFriendlyName(d.SerialNumber);
				ListViewItem lvi = new ListViewItem ( string.Format("{0}\n[{1}]",d.ModelName,friendlyName), model, DevicesGroup );
				lvi.Tag = d;
				devicesList.Items.Add ( lvi );
			} );
		}

		private void helpVideoLink_LinkClicked ( object sender, LinkLabelLinkClickedEventArgs e ) {
			Process.Start ( Resources.Strings.UsbDebuggingHelpLink );
		}

		private void devicesList_DoubleClick ( object sender, EventArgs e ) {
			if ( devicesList.SelectedItems.Count == 1 ) {
				var lvi = devicesList.SelectedItems[0];
				if ( lvi.Tag != null ) {
					this.SelectedDevice = ((DeviceListItem)lvi.Tag).SerialNumber;
				} else {
					this.SelectedDevice = lvi.Text;
				}
				DialogResult = DialogResult.OK;
			}
		}

		private void refreshToolStripButton_Click ( object sender, EventArgs e ) {
			RefreshDevices ( );
		}

		private void tcpipConnect_Click(object sender, EventArgs e) {
			var connector = new TcpIpDialog();
			var dr = connector.ShowDialog();
			if(dr == System.Windows.Forms.DialogResult.OK) {
				var connection = connector.ConnectionString;

				// process the new connection string and lets connect and then refresh the devices.
				var result = CommandRunner.Instance.TcpConnect(connection);
				if(result) {
					RefreshDevices();
				}
			}
		}

		private void adbRestart_Click ( object sender, EventArgs e ) {
			CommandRunner.Instance.RestartServer ( );
		}
	}
}
