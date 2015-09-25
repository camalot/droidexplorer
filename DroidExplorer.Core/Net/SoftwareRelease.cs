using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace DroidExplorer.Core.Net {
	public class SoftwareRelease {
		[JsonProperty ( "ID" )]
		public int ID { get; set; }
		[JsonProperty ( "Version" )]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public String VersionString { get; set; }

		[JsonIgnore]
		public Version Version {
			get {
				return new Version ( VersionString );
			}
			set {
				VersionString = value.ToString ( );
			}
		}

		[JsonProperty ( "Description" )]
		public String Description { get; set; }
		[JsonProperty ( "Name" )]
		public String Name { get; set; }
		[JsonProperty ( "Url" )]
		public String Url { get; set; }
		[JsonProperty ( "TimeStamp" )]
		public DateTime TimeStamp { get; set; }
	}
}
