using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace DroidExplorer.Core.UI.Components {
  public class NumericColumnSorter : IComparer {
    #region IComparer Members

    public int Compare ( object a, object b ) {
      decimal da = 0;
      decimal db = 0;
      if ( a is ListViewItem && b is ListViewItem ) {
        ListViewEx lv = ( a as ListViewItem ).ListView as ListViewEx;
        decimal.TryParse ( ( a as ListViewItem ).Text, out da );
        decimal.TryParse ( ( b as ListViewItem ).Text, out db );
        if ( lv.Sorting == SortOrder.Ascending ) {
          return da.CompareTo ( db );
        } else {
          return -da.CompareTo ( db );
        }
      } else {
        return 0;
      }
    }

    #endregion
  }
}
