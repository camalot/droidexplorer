using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Core.IO;

namespace DroidExplorer.Core.UI.Components {
	public class FileSystemInfoListViewItem : ListViewItem {

		public enum Icons {
			Directory = 0,
			DirectoryLink,
			File,
			Executable,
			FileLink,
			ExecutableLink
		}

		public FileSystemInfoListViewItem ( DroidExplorer.Core.IO.FileSystemInfo fsi, int imageIndex )
			: this ( fsi ) {
			this.ImageIndex = imageIndex;
		}

		public FileSystemInfoListViewItem ( DroidExplorer.Core.IO.FileSystemInfo fsi )
			: base ( fsi.Name ) {
			FileSystemInfo = fsi;
			string type = "Directory";
			this.ImageIndex = (int)Icons.Directory;
			if ( fsi.IsLink ) {
				this.ImageIndex = fsi.IsDirectory ? (int)Icons.DirectoryLink : fsi.IsExecutable ? (int)Icons.ExecutableLink : (int)Icons.FileLink;
				type = "Link";
			}

			if ( fsi is DroidExplorer.Core.IO.FileInfo ) {
				if ( fsi.IsExecutable ) {
					this.ImageIndex = (int)Icons.Executable;
					type = "Executable";
				} else {
					this.ImageIndex = (int)Icons.File;
					type = "File";
				}
			}

			this.SubItems.Add ( type );
			this.SubItems.Add ( fsi.LastModificationDateTime.ToString ( ) );
			this.SubItems.Add ( String.Format ( new FileSizeFormatProvider ( ), "{0:fs}", fsi.Size ) );
		}

		private FileSystemInfo _fileSystemInfo = null;
		public DroidExplorer.Core.IO.FileSystemInfo FileSystemInfo { get { return _fileSystemInfo; } private set { _fileSystemInfo = value; } }
	}
}
