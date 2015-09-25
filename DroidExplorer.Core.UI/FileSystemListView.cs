using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Core.UI.Components;

namespace DroidExplorer.Core.UI {
  public class FileSystemListView : ListViewEx {
    public event EventHandler ViewChanged;

    public FileSystemListView ( )
      : base ( ) {
      InitializeComponent ( );
    }

    public override bool AllowDrop {
      get {
        return true;
      }
      set {
        base.AllowDrop = true;
      }
    }

    public new View View {
      get {
        return base.View;
      }
      set {
        View v = this.View;
        if ( v != value ) {
          base.View = value;
          OnViewModeChanged ( EventArgs.Empty );
        }
      }
    }

    protected void OnViewModeChanged ( EventArgs e ) {
      if ( ViewChanged != null ) {
        ViewChanged ( this, e );
      }
    }

    protected void InitializeComponent ( ) {
      this.Columns.Add ( new SortableColumnHeader ( "name", "Name", -2, new FileSystemInfoColumnSorter ( ) ) );
      this.Columns.Add ( new SortableColumnHeader ( "type", "Type", -2, new FileSystemInfoStringColumnSorter ( ) ) );
      this.Columns.Add ( new SortableColumnHeader ( "lastModified", "Last Modified", -2, new FileSystemInfoModifiedColumnSorter ( ) ) );
      this.Columns.Add ( new SortableColumnHeader ( "size", "Size", -2, new FileSystemInfoSizeColumnSorter ( ) ) );

      this.AutoResizeColumns ( ColumnHeaderAutoResizeStyle.HeaderSize );

      if ( NativeApi.IsWindowsVistaOrLater ) {
        this.SetVistaExplorerStyle ( );
      }

      this.SetAllowDraggableColumns ( true );
      this.SortStyle = SortStyles.SortSelectedColumn;
      this.Sorting = SortOrder.Ascending;
      this.SetSortIcon ( 0, this.Sorting );
			base.AllowDrop = true;
			this.FullRowSelect = true;
    }
  }
}
