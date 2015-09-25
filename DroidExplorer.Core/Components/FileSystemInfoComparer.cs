using System;
using System.Collections.Generic;
using System.Text;

namespace DroidExplorer.Components {
	public class FileSystemInfoComparer : Comparer<DroidExplorer.Core.IO.FileSystemInfo>{
		public override int Compare ( DroidExplorer.Core.IO.FileSystemInfo x, DroidExplorer.Core.IO.FileSystemInfo y ) {
			int dirDiff = y.IsDirectory.CompareTo ( x.IsDirectory );
			if ( dirDiff != 0 )
				return dirDiff;
			return x.Name.CompareTo ( y.Name );
		}
	}
}
