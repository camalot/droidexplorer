using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Managed.Adb.IO;

namespace DroidExplorer.Core.UI {
	/// <summary>
	/// 
	/// </summary>
	/// <seealso cref="System.Windows.Forms.ToolStripButton" />
	public class FileDialogCustomPlace : ToolStripButton {
		/// <summary>
		/// Initializes a new instance of the <see cref="FileDialogCustomPlace"/> class.
		/// </summary>
		public FileDialogCustomPlace ( )
			: this ( string.Empty, (EventHandler)null ) {

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FileDialogCustomPlace"/> class.
		/// </summary>
		/// <param name="path">The path.</param>
		public FileDialogCustomPlace ( string path )
			: this ( path, null, (EventHandler)null ) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FileDialogCustomPlace"/> class.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="click">The click.</param>
		public FileDialogCustomPlace ( string path, EventHandler click )
			: this ( path, null, click ) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FileDialogCustomPlace"/> class.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="image">The image.</param>
		public FileDialogCustomPlace ( string path, Image image )
			: this ( path, image, null ) {

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FileDialogCustomPlace"/> class.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="image">The image.</param>
		/// <param name="click">The click.</param>
		public FileDialogCustomPlace ( string path, Image image, EventHandler click )
			: this ( path, string.Empty, image, click ) {

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FileDialogCustomPlace"/> class.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="text">The text.</param>
		/// <param name="image">The image.</param>
		public FileDialogCustomPlace ( string path, string text, Image image )
			: this ( path, text, image, null ) {

		}


		/// <summary>
		/// Initializes a new instance of the <see cref="FileDialogCustomPlace"/> class.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="text">The text.</param>
		/// <param name="image">The image.</param>
		/// <param name="click">The click.</param>
		public FileDialogCustomPlace ( string path, string text, Image image, EventHandler click )
			: base ( text, image, click, path ) {
			this.Path = path;

			if ( string.IsNullOrEmpty ( text ) ) {
				this.Text = LinuxPath.GetDirectoryName ( Path );
			} else {
				this.Text = text;
			}

			if ( string.IsNullOrEmpty ( this.Text ) && this.Path == new string ( new char[ ] { LinuxPath.DirectorySeparatorChar } ) ) {
				this.Text = string.IsNullOrEmpty ( CommandRunner.Instance.DefaultDevice ) ? CommandRunner.Instance.GetSerialNumber ( ) : CommandRunner.Instance.DefaultDevice;
			}

			this.TextImageRelation = TextImageRelation.ImageAboveText;
			this.ImageAlign = ContentAlignment.TopCenter;
			this.TextAlign = ContentAlignment.BottomCenter;
			this.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

			this.Image = image;

			if ( this.Image == null ) {
				if ( this.Path == new string ( new char[ ] { LinuxPath.DirectorySeparatorChar } ) ) {
					this.Image = DroidExplorer.Resources.Images.mobile_32xLG;
				} else {
					this.Image = DroidExplorer.Resources.Images.folder_Closed_32xLG;
				}
			}
		}



		/// <summary>
		/// Gets or sets the path.
		/// </summary>
		/// <value>
		/// The path.
		/// </value>
		public string Path { get; set; }
	}
}
