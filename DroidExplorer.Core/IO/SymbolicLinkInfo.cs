using System;
using System.Collections.Generic;
using System.Text;

namespace DroidExplorer.Core.IO {
	public class SymbolicLinkInfo : FileSystemInfo {
		private string _link;

		internal SymbolicLinkInfo ( string name, string link, long size, Permission userPermission, Permission groupPermission, Permission otherPermission, DateTime lastMod, bool isDirectory, bool isExec, string fullPath )
			: base ( name, size, userPermission, groupPermission, otherPermission, lastMod, isExec,fullPath ) {
			this.IsDirectory = isDirectory;
			this.Link = link;
		}


		public string Link {
			get { return _link; }
			set { _link = value; }
		}

		public override bool IsLink {
			get { return true; }
			protected set { return; }
		}
	}
}
