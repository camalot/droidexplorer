using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Core.Plugins;
using DroidExplorer.Core.UI.Components;
using System.Globalization;

namespace DroidExplorer.Components {
	public class FileTypeHandlerToolStripMenuItem : ToolStripMenuItem {
		
		public FileTypeHandlerToolStripMenuItem ( FileSystemInfoListViewItem item, IFileTypeHandler handler ) :
			base ( ) {
			this.Item = item;
			this.Handler = handler;
			this.Text = string.Format ( CultureInfo.InvariantCulture, "Open with {0}", handler.Text );
			this.Image = handler.Image;
			this.Click += new EventHandler ( FileTypeHandlerToolStripMenuItem_Click );
		}

		private FileSystemInfoListViewItem Item { get; set; }
		private IFileTypeHandler Handler { get; set; }
		void FileTypeHandlerToolStripMenuItem_Click ( object sender, EventArgs e ) {
			this.Handler.Open ( this.Item.FileSystemInfo as DroidExplorer.Core.IO.FileInfo );
		}
	}
}
