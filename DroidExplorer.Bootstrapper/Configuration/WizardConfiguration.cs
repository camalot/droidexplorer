using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DroidExplorer.Bootstrapper.Configuration {
	/// <summary>
	/// 
	/// </summary>
	[XmlRoot("Wizard")]
	public class WizardConfiguration {
		/// <summary>
		/// Initializes a new instance of the <see cref="WizardConfiguration"/> class.
		/// </summary>
		public WizardConfiguration ( ) {
			this.InstallSteps = new List<WizardStep> ( );
			this.UninstallSteps = new List<WizardStep> ( );
		}

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		[XmlElement("Title")]
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the steps.
		/// </summary>
		/// <value>The steps.</value>
		[XmlArray("Install"),XmlArrayItem("Step")]
		public List<WizardStep> InstallSteps { get; set; }
		/// <summary>
		/// Gets or sets the uninstall steps.
		/// </summary>
		/// <value>The uninstall steps.</value>
		[XmlArray ( "Uninstall" ), XmlArrayItem ( "Step" )]
		public List<WizardStep> UninstallSteps { get; set; }

		/// <summary>
		/// Gets or sets the cancel.
		/// </summary>
		/// <value>The cancel.</value>
		[XmlElement ( "Cancel" )]
		public WizardStep Cancel { get; set; }

		/// <summary>
		/// Gets or sets the error.
		/// </summary>
		/// <value>The error.</value>
		[XmlElement ( "Error" )]
		public WizardStep Error { get; set; }
	}
}
