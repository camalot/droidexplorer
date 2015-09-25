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
		Image GetLargeImage(DroidExplorer.Core.IO.FileSystemInfo file);
		Image GetSmallImage(DroidExplorer.Core.IO.FileSystemInfo file);

		string GetKeyName(DroidExplorer.Core.IO.FileSystemInfo file);

		ListViewItem GetListViewItem(DroidExplorer.Core.IO.FileSystemInfo fsi);

	}
}
