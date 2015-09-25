using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DroidExplorer.Core.UI {
  public delegate void GenericDelegate ( );
  public delegate void SetListViewItemImageIndexDelegate ( ListViewItem lvi, int index );
  public delegate void AddListViewItemDelegate ( ListView lv, ListViewItem lvi );
  public delegate int GetSystemIconIndexDelegate ( SystemImageList sysImgList, string file );
  public delegate Image GetSystemBitmapDelegate ( SystemImageList sysImgList, int index );
	public delegate void SetToolStripItemEnabledDelegate ( ToolStripItem tsi, bool enabled );
	public delegate void SetComboBoxExDisplayValueDelegate ( ComboBoxEx cmbo, string text );
  public delegate void AutoResizeColumnsDelegate ( ListView lv, ColumnHeaderAutoResizeStyle resizeStyle);

}
