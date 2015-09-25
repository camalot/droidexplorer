using System;
using System.Collections.Generic;
using System.Text;

namespace DroidExplorer.Core.IO {
	public class DirectoryInfo : FileSystemInfo {
		internal DirectoryInfo ( string name, long size, Permission userPermission, Permission groupPermission, Permission otherPermission, DateTime lastMod, string fullPath )
			: base ( name, size, userPermission, groupPermission, otherPermission, lastMod, false,fullPath ) {
		}


		public override bool IsDirectory {
			get { return true; }
			protected set { return; }
		}

		public override bool IsLink {
			get { return false; }
			protected set { return; }
		}


		public static DirectoryInfo CreateRoot ( ) {
			return new DirectoryInfo ( "/", 0, new Permission ( "rwx" ), new Permission ( "rwx" ), new Permission ( "rwx" ), DateTime.Now, "/" );
		}
	}
}
