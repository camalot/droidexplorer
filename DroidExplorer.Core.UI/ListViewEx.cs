using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Globalization;
using DroidExplorer.Core.UI;
using DroidExplorer.Core.UI.Components;

namespace DroidExplorer.Core.UI {
  public class ListViewEx : ListView {
    public enum SortStyles {
      SortDefault,
      SortAllColumns,
      SortSelectedColumn
    }

    private Image _watermarkImage;
    private int _watermarkAlpha;
    private bool _allowColumnReorder;
    private SortStyles _sortStyle = SortStyles.SortDefault;

    public ListViewEx ( ) {
      this.DoubleBuffered = true;
      this.SetStyle ( ControlStyles.OptimizedDoubleBuffer, true );
      this.WatermarkAlpha = 200;
      this.SelectedSortColumn = -1;
    }
    // m_SortSubitems(i) is the i-th sub-item
    // in the sort order for all column sorting.
    private int[] _sortSubitems = null;

    // Initialize the sort item order to the order given by the column headers.
    private void SetSortSubitems ( ) {
      _sortSubitems = new int[ this.Columns.Count ];
      for ( int i = 0; i <= this.Columns.Count - 1; i++ ) {
        _sortSubitems[ this.Columns[ i ].DisplayIndex ] = i;
      }
    }

    public int SelectedSortColumn { get; set; }

    // Whether we sort by all columns, one column, or not at all.
    public SortStyles SortStyle {
      get {
        return _sortStyle;
      }
      set {
        _sortStyle = value;
        // If the current style is SortSelectedColumn,
        // remove the column sort indicator.
        if ( _sortStyle == SortStyles.SortSelectedColumn ) {
          if ( SelectedSortColumn >= 0 ) {
            this.Columns[ SelectedSortColumn ].ImageKey = null;
            SelectedSortColumn = -1;
          }
        }

        // Save the new value.
        _sortStyle = value;

        switch ( _sortStyle ) {
          case SortStyles.SortDefault:
            this.ListViewItemSorter = null;
            break;
          case SortStyles.SortAllColumns:
            this.ListViewItemSorter = new ListViewEx.AllColumnSorter ( );
            break;
          case SortStyles.SortSelectedColumn:
            this.ListViewItemSorter = new SelectedColumnSorter ( );
            break;
        }
      }
    }


    /// <summary>
    /// Gets or sets a value indicating whether the user can drag column headers to reorder columns in the control.
    /// </summary>
    /// <value></value>
    /// <returns>true if drag-and-drop column reordering is allowed; otherwise, false. The default is false.</returns>
    /// <PermissionSet>
    /// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
    /// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
    /// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
    /// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
    /// </PermissionSet>
    public bool AllowColumnReorder {
      get { return this._allowColumnReorder; }
      set {
        this._allowColumnReorder = value;
        this.SetAllowDraggableColumns ( value );
      }
    }

    /// <summary>
    /// Gets or sets the columns order.
    /// </summary>
    /// <value>The columns order.</value>
    public Dictionary<string, int> ColumnsOrder {
      get {
        Dictionary<string,int> order = new Dictionary<string, int> ( );
        foreach ( ColumnHeader item in this.Columns ) {
          order.Add ( item.Text, item.DisplayIndex );
        }
        return order;
      }
      set {
        foreach ( string s in value.Keys ) {
          foreach ( ColumnHeader item in this.Columns ) {
            if ( string.Compare ( s, item.Text, true, CultureInfo.InvariantCulture ) == 0 ) {
              item.DisplayIndex = value[ s ];
            }
          }
        }
      }
    }

    // The user reordered the columns. Resort.
    protected override void OnColumnReordered ( System.Windows.Forms.ColumnReorderedEventArgs e ) {
      // This raises the ColumnReordered event.
      base.OnColumnReordered ( e );

      // If the main program canceled, do nothing.
      if ( e.Cancel )
        return;

      // Rebuild the list of sort sub-items.
      SetSortSubitems ( );

      // Fix the list up to account for the moved column.
      MoveArrayItem ( _sortSubitems, e.OldDisplayIndex, e.NewDisplayIndex );

      // Resort.
      this.Sort ( );
    }

    // Move an item from position idx_fr to idx_to.
    private void MoveArrayItem ( int[ ] values, int idx_fr, int idx_to ) {
      int moved_value = values[ idx_fr ];
      int num_moved = Math.Abs ( idx_fr - idx_to );

      if ( idx_to < idx_fr ) {
        Array.Copy ( values, idx_to, values, idx_to + 1, num_moved );
      } else {
        Array.Copy ( values, idx_fr + 1, values, idx_fr, num_moved );
      }

      values[ idx_to ] = moved_value;
    }


    // Change the selected sort column.
    protected override void OnColumnClick ( ColumnClickEventArgs e ) {
      base.OnColumnClick ( e );

      if ( this.SortStyle == SortStyles.SortSelectedColumn ) {
        // If this is the same sort column, switch the sort order.
        if ( e.Column == SelectedSortColumn ) {
          if ( this.Sorting == SortOrder.Ascending ) {
            this.Sorting = SortOrder.Descending;
          } else {
            this.Sorting = SortOrder.Ascending;
          }
        }

        // Remove the image from the previous sort column.
        if ( SelectedSortColumn >= 0 ) {
          this.Columns[ SelectedSortColumn ].ImageIndex = -1;
        }

        // If we're not currently sorting, sort ascending.
        if ( this.Sorting == SortOrder.None ) {
          this.Sorting = SortOrder.Ascending;
        }

        // Save the new sort column and give it an image.
        SelectedSortColumn = e.Column;
        if ( this.Columns[ e.Column ] is SortableColumnHeader ) {
          this.ListViewItemSorter = ( this.Columns[ e.Column ] as SortableColumnHeader ).ColumnComparer;
        }
        this.SetSortIcon ( e.Column, this.Sorting );
        // Resort.
        this.Sort ( );
      }
    }

    [Category ( "Appearance" ), DefaultValue ( 200 )]
    public int WatermarkAlpha {
      get {
        return this._watermarkAlpha;
      }
      set {
        this._watermarkAlpha = value;
        SetBackground ( );
      }
    }
    [Category ( "Appearance" )]
    public Image WatermarkImage {
      get {
        return this._watermarkImage;
      }
      set {
        this._watermarkImage = value;
        SetBackground ( );
      }
    }

    protected override void OnPaint ( PaintEventArgs e ) {
      base.OnPaint ( e );
    }

    void SetBackground ( ) {
      IntPtr hBmp = GetBitmap ( this.WatermarkImage );

      NativeApi.LVBKIMAGE lv = new NativeApi.LVBKIMAGE ( );

      lv.hbm = hBmp;
      lv.ulFlags = /*NativeApi.LVBKIF_SOURCE_URL | NativeApi.LVBKIF_STYLE_TILE | */NativeApi.LVBKIF_TYPE_WATERMARK;

      IntPtr lvPTR = Marshal.AllocCoTaskMem ( Marshal.SizeOf ( lv ) );
      Marshal.StructureToPtr ( lv, lvPTR, false );

      /*NativeApi.SendMessage ( this.Handle, NativeApi.LVM_SETBKIMAGE, 0, lvPTR );*/
      NativeApi.SendMessage ( this.Handle, NativeApi.LVM_SETBKIMAGEW, 0, lvPTR );
      NativeApi.SendMessage ( this.Handle, NativeApi.LVM_SETTEXTBKCOLOR, 0, NativeApi.CLR_NONE );
      Marshal.FreeCoTaskMem ( lvPTR );
    }

    private IntPtr GetBitmap ( Image image ) {
      if ( image == null ) {
        return IntPtr.Zero;
      } else {
        using ( Bitmap bmp = new Bitmap ( image.Width, image.Height ) ) {
          using ( Graphics g = Graphics.FromImage ( bmp ) ) {
            g.Clear ( this.BackColor );
            g.DrawImage ( image, 0, 0, bmp.Width, bmp.Height );
            g.FillRectangle ( new SolidBrush ( Color.FromArgb ( WatermarkAlpha, this.BackColor.R, this.BackColor.G, this.BackColor.B ) ), new Rectangle ( 0, 0, bmp.Width, bmp.Height ) );
          }
          return bmp.GetHbitmap ( );
        }
      }
    }


    // ---------------------------------------
    // Sort the ListView items by all columns.
    private class AllColumnSorter : System.Collections.IComparer {
      #region IComparer Members

      // Compare two ListViewItems.
      public int Compare ( object x, object y ) {
        ListViewItem itemx = ( ListViewItem )( x );
        ListViewItem itemy = ( ListViewItem )( y );

        // Compare the items' strings.
        if ( itemx.ListView.Sorting == SortOrder.Ascending ) {
          return String.Compare ( ItemString ( itemx ), ItemString ( itemy ) );
        } else {
          return -String.Compare ( ItemString ( itemx ), ItemString ( itemy ) );
        }
      }

      #endregion


      // Return a string representing this item as a
      // null-separated list of the item sub-item values.
      private string ItemString ( ListViewItem listview_item ) {
        ListViewEx slvw = ( ListViewEx )listview_item.ListView;

        // Make sure we have the sort sub-items' order.
        if ( slvw._sortSubitems == null )
          slvw.SetSortSubitems ( );

        // Make an array to hold the sort sub-items' values.
        int num_cols = slvw.Columns.Count;
        string[] values = new string[ num_cols ];

        // Build the list of fields in display order.
        for ( int i = 0; i <= slvw._sortSubitems.Length - 1; i++ ) {
          int idx = slvw._sortSubitems[ i ];

          // Get this sub-item's value.
          string item_value = "";
          if ( idx < listview_item.SubItems.Count ) {
            item_value = listview_item.SubItems[ idx ].Text;
          }

          // Align appropriately.
          if ( slvw.Columns[ idx ].TextAlign == HorizontalAlignment.Right ) {
            // Pad so numeric values sort properly.
            values[ i ] = item_value.PadLeft ( 20 );
          } else {
            values[ i ] = item_value;
          }
        }

        // Save the sub-item values in display order.
        for ( int i = 0; i <= slvw._sortSubitems.Length - 1; i++ ) {
          int idx = slvw._sortSubitems[ i ];
          // Make sure this item has this sub-item.
          if ( idx < listview_item.SubItems.Count ) {
            // Add the sub-item's value.
            if ( slvw.Columns[ idx ].TextAlign == HorizontalAlignment.Right ) {
              // Pad so numeric values sort properly.
              values[ i ] = listview_item.SubItems[ idx ].Text.PadLeft ( 20 );
            } else {
              values[ i ] = listview_item.SubItems[ idx ].Text;
            }
          }
        }

        // Console.WriteLine(String.Join("|", values));

        // Concatenate the values to build the result.
        return String.Join ( "\0", values );
      }
    }

    // ---------------------------------------
    // Sort the ListView items by the selected column.
    internal class SelectedColumnSorter : System.Collections.IComparer {
      #region IComparer Members

      // Compare two ListViewItems.
      public int Compare ( object x, object y ) {
        ListViewItem itemx = ( ListViewItem )( x );
        ListViewItem itemy = ( ListViewItem )( y );

        // Get the selected column index.
        ListViewEx slvw = ( ListViewEx )itemx.ListView;
        int idx = slvw.SelectedSortColumn;
        if ( idx < 0 )
          return 0;

        // Compare the items' strings.
        if ( itemx.ListView.Sorting == SortOrder.Ascending ) {
          return String.Compare ( ItemString ( itemx, idx ), ItemString ( itemy, idx ) );
        } else {
          return -String.Compare ( ItemString ( itemx, idx ), ItemString ( itemy, idx ) );
        }
      }

      #endregion

      // Return a string representing this item's sub-item.
      private string ItemString ( ListViewItem listview_item, int idx ) {
        ListViewEx slvw = ( ListViewEx )listview_item.ListView;

        // Make sure the item has the needed sub-item.
        string value = "";
        if ( idx <= listview_item.SubItems.Count - 1 ) {
          value = listview_item.SubItems[ idx ].Text;
        }

        // Return the sub-item's value.
        if ( slvw.Columns[ idx ].TextAlign == HorizontalAlignment.Right ) {
          // Pad so numeric values sort properly.
          return value.PadLeft ( 20 );
        } else {
          return value;
        }
      }
    }
  }
}
