using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Camalot.Common.Extensions;

namespace DroidExplorer.Core {
	public class DeviceListCommandResult : CommandResult {
		public DeviceListCommandResult ( string data )
			: base ( ) {
				Devices = new List<DeviceListItem>();
			ProcessData ( data );
		}

		public List<DeviceListItem> Devices { get;private set; }

		protected override void ProcessData ( string data ) {
			base.ProcessData ( data );
			// ^([a-z0-9_-]+(?:\s?[\.a-z0-9_-]+)?(?:\:\d{1,})?)\s+(device|offline|unknown|bootloader|recovery|download|unauthorized)(?:\s+product:([^:]+)\s+model\:([^:]+)\s+device\:([^:]+))?$
			// [SN] [State] [Product] [Model] [Device]
			var options = RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;
			var regex = new Regex(Properties.Resources.DeviceListExRegexPattern, options);

			Match m = regex.Match ( data );
			if(!m.Success) {
				// fallback to old pattern if it doesn't match with the new pattern. 
				//^([a-z0-9\-_]+(?:\s[a-z0-9\-_]+)?)\s+(device|offline|unknown|bootloader|recovery|download|unauthorized)\s*$
				regex = new Regex(Properties.Resources.DeviceListRegexPattern, options);
				m = regex.Match(data);
				m.ForEach(x => {
					string sn = m.Groups[1].Value;
					if(!string.IsNullOrEmpty(sn)) {
						var state = m.Groups[2].Value.Trim();
						var device = new DeviceListItem(sn, state, "unknown", "unknown", "unknown");
						Devices.Add(device);
					}
				});
			} else {
				while(m.Success) {
					string sn = m.Groups[1].Value;
					if(!string.IsNullOrEmpty(sn)) {
						var state = m.Groups[2].Value.Trim();
						var product = m.Groups[3].Value.Trim();
						var model = m.Groups[4].Value.Trim();
						var dn = m.Groups[5].Value.Trim();
						var device = new DeviceListItem(sn, state, product, model, dn);
						Devices.Add(device);
					}
					m = m.NextMatch();
				}
			}
			Devices.Sort ( );

		}
	}

	public class DeviceListItem : IComparable<DeviceListItem>{
		public DeviceListItem(string sn, string state, string product, string model, string device) {
			SerialNumber = sn;
			State = state;
			ProductName = product;
			ModelName = model;
			DeviceName = device;
		}
		public string SerialNumber { get; private set; }
		public string State { get; private set; }
		public string ProductName { get; private set; }
		public string ModelName { get; private set; }
		public string DeviceName { get; private set; }


		public int CompareTo(DeviceListItem other) {
			return this.ModelName.CompareTo(other.ModelName).CompareTo(
				this.DeviceName.CompareTo(other.DeviceName).CompareTo(
					this.ProductName.CompareTo(other.ProductName).CompareTo(
						this.State.CompareTo(other.State).CompareTo(
							this.SerialNumber.CompareTo(other.SerialNumber)
						)
					)
				)
			);
		}
	}

}
