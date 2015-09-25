using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace DroidExplorer.Core.UI.Components {
  public class StringColumnSorter : IComparer {
    #region IComparer Members

    public int Compare ( object a, object b ) {
      if ( a is ListViewItem && b is ListViewItem ) {
        ListViewEx lv = ( a as ListViewItem ).ListView as ListViewEx;
        if ( lv.Sorting == SortOrder.Ascending ) {
          return string.Compare ( ( a as ListViewItem ).Text, ( b as ListViewItem ).Text );
        } else {
          return -string.Compare ( ( a as ListViewItem ).Text, ( b as ListViewItem ).Text );
        }
      } else {
        return 0;
      }
    }

    #endregion
  }
}
