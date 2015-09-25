using System;
using System.Collections.Generic;
using System.Text;

namespace DroidExplorer.Core.Components {
  public class ApkFileInfoComparer : Comparer<DroidExplorer.Core.IO.ApkFileInfo>{
    public override int Compare ( DroidExplorer.Core.IO.ApkFileInfo x, DroidExplorer.Core.IO.ApkFileInfo y ) {
      return x.DisplayName.CompareTo ( y.DisplayName );
    }
  }
}
