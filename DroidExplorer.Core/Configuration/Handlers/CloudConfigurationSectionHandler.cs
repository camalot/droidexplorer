using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DroidExplorer.Core.Configuration.Handlers {
	public class CloudConfigurationSectionHandler : IConfigurationSectionHandler {
		public CloudConfiguration Create ( object parent, object configContext, System.Xml.XmlNode section ) {
			var cc = new CloudConfiguration ( );
			var serializer = new XmlSerializer ( typeof ( CloudConfiguration ) );
			var doc = new XmlDocument ( );
			var ele = (XmlElement)doc.ImportNode ( section, true );
			doc.AppendChild ( doc.CreateXmlDeclaration ( "1.0", "utf-8", string.Empty ) );
			doc.AppendChild ( ele );
			using ( var ms = new MemoryStream ( ) ) {
				doc.Save ( ms );
				ms.Position = 0;
				cc = (CloudConfiguration)serializer.Deserialize ( ms );
			}
			return cc;
		}



		#region IConfigurationSectionHandler Members

		/// <summary>
		/// Creates a configuration section handler.
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="configContext">Configuration context object.</param>
		/// <param name="section"></param>
		/// <returns>The created section handler object.</returns>
		object IConfigurationSectionHandler.Create ( object parent, object configContext, System.Xml.XmlNode section ) {
			return this.Create ( parent, configContext, (XmlElement)section );
		}

		#endregion
	}

	[XmlRoot ( "cloud" )]
	public class CloudConfiguration {
		public CloudConfiguration ( ) {
			Port = 80;
			Scheme = "http";
		}

		[XmlAttribute ( "hostName" )]
		public String Host { get; set; }
		[XmlAttribute ( "path" )]
		public String Path { get; set; }
		[XmlAttribute("port")]
		public int Port { get; set; }
		[XmlAttribute ( "scheme" )]
		public String Scheme { get; set; }
		[XmlAttribute ( "key" )]
		public String ApiKey { get; set; }
		[XmlAttribute ( "app" )]
		public String AppId { get; set; }
	}
}
