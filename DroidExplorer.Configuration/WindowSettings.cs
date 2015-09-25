using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace DroidExplorer.Configuration {
	/// <summary>
	/// 
	/// </summary>
	[XmlRoot ( "Window" ), Serializable]
	public class WindowSettings {
		/// <summary>
		/// Initializes a new instance of the <see cref="WindowSettings"/> class.
		/// </summary>
		public WindowSettings ( ) {
			Size = Size.Empty;
			Location = Point.Empty;
			WindowState = FormWindowState.Normal;
			StartPosition = FormStartPosition.WindowsDefaultLocation;
			FolderView = View.Details;
		}

		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>The size.</value>
		[XmlElement ( "Size" )]
		public Size Size { get; set; }

		/// <summary>
		/// Gets or sets the location.
		/// </summary>
		/// <value>The location.</value>
		[XmlElement ( "Location" )]
		public Point Location { get; set; }

		/// <summary>
		/// Gets or sets the state of the window.
		/// </summary>
		/// <value>The state of the window.</value>
		[XmlElement ( "WindowState" ), DefaultValue ( FormWindowState.Normal )]
		public FormWindowState WindowState { get; set; }

		/// <summary>
		/// Gets or sets the start position.
		/// </summary>
		/// <value>The start position.</value>
		[XmlElement ( "StartPosition" ), DefaultValue ( FormStartPosition.WindowsDefaultLocation )]
		public FormStartPosition StartPosition { get; set; }

		/// <summary>
		/// Gets or sets the folder view.
		/// </summary>
		/// <value>The folder view.</value>
		[XmlElement ( "FolderView" ), DefaultValue ( View.Details )]
		public View FolderView { get; set; }
	}
}
