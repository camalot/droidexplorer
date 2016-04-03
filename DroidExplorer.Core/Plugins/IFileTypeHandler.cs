using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DroidExplorer.Core.Plugins {
	/// <summary>
	/// 
	/// </summary>
	public interface IFileTypeHandler {
		/// <summary>
		/// Opens the specified file.
		/// </summary>
		/// <param name="file">The file.</param>
		void Open ( DroidExplorer.Core.IO.FileInfo file );
		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		string Name { get; }
		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		string Text { get; }
		/// <summary>
		/// Gets the image.
		/// </summary>
		/// <value>
		/// The image.
		/// </value>
		Image Image { get; }
  }
}
