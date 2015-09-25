using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace DroidExplorer.Core.UI.Components {
  public class SortableColumnHeader : ColumnHeader {

    #region Constructors
    public SortableColumnHeader ( )
      : base ( ) {
      this.ColumnComparer = new ListViewEx.SelectedColumnSorter ( );
    }

    public SortableColumnHeader ( string imageKey )
      : base ( imageKey ) {
      this.ColumnComparer = new ListViewEx.SelectedColumnSorter ( );
    }

    public SortableColumnHeader ( int imageIndex )
      : base ( imageIndex ) {
      this.ColumnComparer = new ListViewEx.SelectedColumnSorter ( );
    }

    public SortableColumnHeader ( string key, string text, int width )
      : this ( key, text, width, ( IComparer )new StringColumnSorter ( ) ) {
      this.Name = key;
      this.Text = text;
      this.Width = width;
    }

    public SortableColumnHeader ( string key, string text, int width, IComparer comparer )
      : this ( ) {
      this.Name = key;
      this.Text = text;
      this.Width = width;
      this.ColumnComparer = comparer;
    }
    #endregion

    private IComparer _columnComparer;

    public IComparer ColumnComparer {
      get { return _columnComparer; }
      set { _columnComparer = value; }
    }


  }
}
