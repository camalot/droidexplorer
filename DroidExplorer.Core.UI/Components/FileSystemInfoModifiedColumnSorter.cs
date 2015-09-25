using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace DroidExplorer.Core.UI.Components {
  public class FileSystemInfoModifiedColumnSorter : IComparer {
    #region IComparer Members

    public int Compare ( object x, object y ) {
      if ( x is FileSystemInfoListViewItem && y is FileSystemInfoListViewItem ) {
        ListViewEx lv = ( x as ListViewItem ).ListView as ListViewEx;
        FileSystemInfoListViewItem a = x as FileSystemInfoListViewItem;
        FileSystemInfoListViewItem b = y as FileSystemInfoListViewItem;
        if ( lv.Sorting == SortOrder.Ascending ) {
          int diff = a.FileSystemInfo.IsDirectory.CompareTo ( b.FileSystemInfo.IsDirectory );
          if ( diff != 0 )
            return -diff;
          return a.FileSystemInfo.LastModificationDateTime.CompareTo ( b.FileSystemInfo.LastModificationDateTime );
        } else {
          int diff = a.FileSystemInfo.IsDirectory.CompareTo ( b.FileSystemInfo.IsDirectory );
          if ( diff != 0 )
            return diff;
          return -a.FileSystemInfo.LastModificationDateTime.CompareTo ( b.FileSystemInfo.LastModificationDateTime );
        }
      } else {
        return 0;
      }
    }

    #endregion
  }
}
