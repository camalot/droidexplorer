using System;
using System.Collections.Generic;
using System.Text;

namespace DroidExplorer.Core.IO {
	public class ApkFileInfo : FileInfo {
		public ApkFileInfo ( string name, string displayName, long size, Permission userPermission, Permission groupPermission, Permission otherPermission, DateTime lastMod, string fullPath )
			: base ( name, size, userPermission, groupPermission, otherPermission, lastMod, false, fullPath ) {
			this.DisplayName = displayName;
		}

		public string DisplayName { get; set; }

    public static ApkFileInfo Create ( string name, string displayName, long size, Permission userPermission, Permission groupPermission, Permission otherPermission, DateTime lastMod, string fullPath ) {
      return new ApkFileInfo ( name, displayName, size, userPermission, groupPermission, otherPermission, lastMod, fullPath );
    }
	}
}
