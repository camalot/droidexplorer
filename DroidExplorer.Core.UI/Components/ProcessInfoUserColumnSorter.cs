using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace DroidExplorer.Core.UI.Components {
  public class ProcessInfoUserColumnSorter : IComparer {
    #region IComparer Members

    public int Compare ( object a, object b ) {
      if ( a is CpuProcessInfoListViewItem && b is CpuProcessInfoListViewItem ) {
        ListViewEx lv = ( a as CpuProcessInfoListViewItem ).ListView as ListViewEx;
        if ( lv.Sorting == SortOrder.Ascending ) {
          return ( a as CpuProcessInfoListViewItem ).ProcessInfo.User.CompareTo ( ( b as CpuProcessInfoListViewItem ).ProcessInfo.User );
        } else {
          return -( a as CpuProcessInfoListViewItem ).ProcessInfo.User.CompareTo ( ( b as CpuProcessInfoListViewItem ).ProcessInfo.User );
        }
      } else {
        return 0;
      }
    }

    #endregion
  }
}
