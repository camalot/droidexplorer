using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace DroidExplorer.Core {
	[XmlRoot ( "AaptBranding" )]
	public class AaptBrandingCommandResult : CommandResult {
		
		public AaptBrandingCommandResult ( string data )
			: base ( ) {
			ProcessData ( data );
		}

		[Obsolete ( "Used for the xml serialization only", true )]
		public AaptBrandingCommandResult ( ) {

		}


		/// <summary>
		/// Gets or sets the device path.
		/// </summary>
		/// <value>The device path.</value>
		public string DevicePath { get; set; }

		/// <summary>
		/// Gets or sets the icon.
		/// </summary>
		/// <value>The icon.</value>
		public string Icon { get; set;  }

		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <value>The version.</value>
		public string Version { get; set; }

		/// <summary>
		/// Gets or sets the package.
		/// </summary>
		/// <value>The package.</value>
		public string Package { get; set; }

		/// <summary>
		/// Gets or sets the label.
		/// </summary>
		/// <value>The label.</value>
		public string Label { get; set; }

		/// <summary>
		/// Gets or sets the local apk.
		/// </summary>
		/// <value>The local apk.</value>
		public string LocalApk { get; set; }

		/// <summary>
		/// Processes the data.
		/// </summary>
		/// <param name="data">The data.</param>
		protected override void ProcessData ( string data ) {
			base.ProcessData ( data );
			Regex regex = new Regex ( Properties.Resources.PackageIconRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline );
			Match m = regex.Match ( data );
			if ( m.Success ) {
				this.Package = m.Groups[1].Value;
				this.Version = m.Groups[2].Value;
				this.Label = m.Groups[3].Value;
				this.Icon = m.Groups[4].Value;
			}
		}
	}
}
