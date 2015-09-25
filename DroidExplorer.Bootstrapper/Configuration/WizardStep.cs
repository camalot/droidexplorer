using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;

namespace DroidExplorer.Bootstrapper.Configuration {
	public class WizardStep {

		[XmlElement ( "Content" )]
		public string Content { get; set; }
		[XmlElement ( "Title" )]
		public string Title { get; set; }
		[XmlElement ( "SubTitle" )]
		public string Subtitle { get; set; }
		[XmlElement ( "BackEnabled" )]
		public bool BackEnabled { get; set; }
		[XmlElement ( "NextEnabled" )]
		public bool NextEnabled { get; set; }
		[XmlElement ( "NextText" )]
		public string NextText { get; set; }
		[XmlElement ( "CancelEnabled" )]
		public bool CancelEnabled { get; set; }
		[XmlElement ( "BannerVisible" )]
		public bool BannerVisible { get; set; }
		[XmlElement("DialogVisible")]
		public bool DialogVisible { get; set; }

		[XmlElement("ContentBackColor")]
		public string ContentBackColorString { get; set; }
		/// <summary>
		/// Gets the color of the content back.
		/// </summary>
		/// <value>The color of the content back.</value>
		[XmlIgnore]
		public Color ContentBackColor {
			get {
				if ( ContentBackColorString.StartsWith ( "#" ) ) {
					return ColorTranslator.FromHtml ( ContentBackColorString );
				} else {
					return Color.FromName ( ContentBackColorString );
				}
			}
		}
	}
}
