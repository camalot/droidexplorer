using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Configuration.UI;
using DroidExplorer.Core;
using DroidExplorer.Core.Exceptions;

namespace DroidExplorer.Configuration.DataLoaders {
	public class DevicesDataLoader : IOptionNodeDataLoader {
		public DevicesDataLoader ( ) {

		}

		#region IOptionNodeDataLoader Members

		public void Load ( TreeNode parentNode ) {
			foreach ( string sn in KnownDeviceManager.Instance.GetKnownDevices ( ) ) {
				// if we dont have a serial number, we dont have a device...
				if ( string.IsNullOrEmpty ( sn ) ) {
					continue;
				}

				KnownDevice kd = new KnownDevice ( );
				kd.SerialNumber = sn;
				kd.DisplayName = KnownDeviceManager.Instance.GetDeviceFriendlyName ( kd.SerialNumber );
				kd.Guid = KnownDeviceManager.Instance.GetDeviceGuid ( kd.SerialNumber );
				OptionItemTreeNode tn = new OptionItemTreeNode ( kd.SerialNumber );
				PropertyGridEditor pge = new PropertyGridEditor ( );
				pge.PropertyValueChanged += delegate ( object s, PropertyValueChangedEventArgs e ) {
					GridItem gi = e.ChangedItem;
					KnownDevice kdi = ( s as PropertyGridEditor ).SelectedObject as KnownDevice;
					switch ( gi.Label ) {
						case "DisplayName":
							KnownDeviceManager.Instance.SetDeviceFriendlyName ( kdi.SerialNumber, gi.Value.ToString ( ) );
							break;
					}
				};
				tn.UIEditor = pge;
				tn.UIEditor.SetSourceObject ( kd );
				try {
					OptionItemTreeNode propItem = new OptionItemTreeNode ( "Properties" );
					propItem.UIEditor = new DevicePropertiesEditor ( kd.SerialNumber );
					tn.Nodes.Add ( propItem );
				} catch ( AdbException ex ) {
					this.LogWarn ( ex.Message, ex );
				}
				parentNode.Nodes.Add ( tn );
			}
		}

		#endregion
	}
}
