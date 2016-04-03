using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DroidExplorer.Core.Plugins {
	/// <summary>
	/// FileTypeIconHandlers allow for specific handling of files. Example, displaying thumbnails of images.
	/// </summary>
	public interface IFileTypeIconHandler {
		/// <summary>
		/// Gets the large image.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		Image GetLargeImage (DroidExplorer.Core.IO.FileSystemInfo file);
		/// <summary>
		/// Gets the small image.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		Image GetSmallImage (DroidExplorer.Core.IO.FileSystemInfo file);

		/// <summary>
		/// Gets the name of the key.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		string GetKeyName (DroidExplorer.Core.IO.FileSystemInfo file);

		/// <summary>
		/// Gets the ListView item.
		/// </summary>
		/// <param name="fsi">The fsi.</param>
		/// <returns></returns>
		ListViewItem GetListViewItem (DroidExplorer.Core.IO.FileSystemInfo fsi);

	}
}
