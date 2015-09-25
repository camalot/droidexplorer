using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using DroidExplorer.UI;
using DroidExplorer.Core;
using DroidExplorer.Core.UI;
using DroidExplorer.Core.UI.Components;
using DroidExplorer.Core.Plugins;
// http://www.cyberciti.biz/faq/show-all-running-processes-in-linux/

namespace DroidExplorer.Plugins.UI {
  public partial class ProcessViewerForm : PluginForm {

    private delegate ListView.ListViewItemCollection GetListViewItemsDelegate ( ListView lv );
    private delegate ListViewItem GetListViewItemDelegate ( ListView lv, int index );
    private delegate void AddListViewSubItemDelegate ( ListViewItem lvi, string text );
    private delegate void SetListViewItemTextDelegate ( ListViewItem lvi, string text );



    public ProcessViewerForm ( IPluginHost host ) : base(host) {
      InitializeComponent ( );

      SetUpCpuListView ( );

      CommandRunner.Instance.TopProcessUpdateStarted += delegate ( object sender, EventArgs e ) {
        /*if ( this.InvokeRequired ) {
          this.Invoke ( new GenericDelegate ( this.cpuInfo.Items.Clear ) );
        } else {
          this.cpuInfo.Items.Clear ( );
        }*/
      };

      CommandRunner.Instance.TopProcessUpdated += delegate ( object sender, DroidExplorer.Core.Components.ProcessInfoEventArgs e ) {
        UpdateOrAddCpuProcessInfo ( e.ProcessInfo );
      };

      CommandRunner.Instance.TopProcessRun ( );


    }

    private void SetUpCpuListView ( ) {
      this.cpuInfo.Columns.Add ( new SortableColumnHeader ( "id", "ID", 50, new ProcessInfoPidColumnSorter ( ) ) );
      this.cpuInfo.Columns.Add ( new SortableColumnHeader ( "cpu", "CPU", 50, new ProcessInfoCpuColumnSorter ( ) ) );
      this.cpuInfo.Columns.Add ( new SortableColumnHeader ( "name", "Name", 100, new ProcessInfoNameColumnSorter ( ) ) );
      this.cpuInfo.Columns.Add ( new SortableColumnHeader ( "thread", "Thread", 50, new ProcessInfoThreadColumnSorter ( ) ) );
      this.cpuInfo.Columns.Add ( new SortableColumnHeader ( "vss", "VSS", 50, new ProcessInfoVssColumnSorter ( ) ) );
      this.cpuInfo.Columns.Add ( new SortableColumnHeader ( "rss", "RSS", 50, new ProcessInfoRssColumnSorter ( ) ) );
      this.cpuInfo.Columns.Add ( new SortableColumnHeader ( "user", "User", 100, new ProcessInfoUserColumnSorter ( ) ) );

      this.cpuInfo.SelectedSortColumn = 0;
      this.cpuInfo.SetSortIcon ( 0, this.cpuInfo.Sorting );

    }

    private List<int> StaleItems { get; set; }

    protected override void OnClosing ( CancelEventArgs e ) {
      if ( !e.Cancel ) {
        CommandRunner.Instance.TopProcessKill ( );
      }
      base.OnClosing ( e );

    }

    private void UpdateOrAddCpuProcessInfo ( DroidExplorer.Core.IO.ProcessInfo processInfo ) {
      bool found = false;
      try {
        ListView.ListViewItemCollection items = null;
        if ( this.InvokeRequired ) {
          items = ( ListView.ListViewItemCollection )this.Invoke ( new GetListViewItemsDelegate ( this.GetListViewItems ), this.cpuInfo );
        } else {
          items = GetListViewItems ( this.cpuInfo );
        }

        for ( int i = 0; i < items.Count; i++ ) {
          CpuProcessInfoListViewItem lvi = null;

          if ( this.InvokeRequired ) {
            lvi = ( CpuProcessInfoListViewItem )this.Invoke ( new GetListViewItemDelegate ( this.GetListViewItem ), this.cpuInfo, i );
          } else {
            lvi = GetListViewItem ( this.cpuInfo, i ) as CpuProcessInfoListViewItem;
          }

          DroidExplorer.Core.IO.ProcessInfo pi = lvi.ProcessInfo as DroidExplorer.Core.IO.ProcessInfo;

          if ( processInfo.PID == pi.PID ) {
            found = true;
            //cpu, name,thread,vss,user
            if ( this.InvokeRequired ) {
              this.Invoke ( new GenericDelegate ( lvi.SubItems.Clear ) );
              this.Invoke ( new SetListViewItemTextDelegate ( this.SetListViewItemText ), lvi, processInfo.PID.ToString ( ) );
              this.Invoke ( new AddListViewSubItemDelegate ( this.AddListViewSubItem ), lvi, string.Format ( CultureInfo.InvariantCulture, "{0}%", processInfo.Cpu ) );
              this.Invoke ( new AddListViewSubItemDelegate ( this.AddListViewSubItem ), lvi, processInfo.Name );
              this.Invoke ( new AddListViewSubItemDelegate ( this.AddListViewSubItem ), lvi, processInfo.Thread.ToString ( ) );
              this.Invoke ( new AddListViewSubItemDelegate ( this.AddListViewSubItem ), lvi, string.Format ( CultureInfo.InvariantCulture, "{0}K", processInfo.Vss ) );
              this.Invoke ( new AddListViewSubItemDelegate ( this.AddListViewSubItem ), lvi, string.Format ( CultureInfo.InvariantCulture, "{0}K", processInfo.Rss ) );
              this.Invoke ( new AddListViewSubItemDelegate ( this.AddListViewSubItem ), lvi, processInfo.User );
            } else {
              lvi.SubItems.Clear ( );
              SetListViewItemText ( lvi, processInfo.PID.ToString ( ) );
              AddListViewSubItem ( lvi, string.Format ( CultureInfo.InvariantCulture, "{0}%", processInfo.Cpu ) );
              AddListViewSubItem ( lvi, processInfo.Name );
              AddListViewSubItem ( lvi, processInfo.Thread.ToString ( ) );
              AddListViewSubItem ( lvi, string.Format ( CultureInfo.InvariantCulture, "{0}K", processInfo.Vss ) );
              AddListViewSubItem ( lvi, string.Format ( CultureInfo.InvariantCulture, "{0}K", processInfo.Rss ) );
              AddListViewSubItem ( lvi, processInfo.User );
            }
          }

        }


        if ( !found ) {
          CpuProcessInfoListViewItem lvi = new CpuProcessInfoListViewItem ( processInfo );
          if ( InvokeRequired ) {
            this.Invoke ( new AddListViewItemDelegate ( AddListViewItem ), this.cpuInfo, lvi );
          } else {
            AddListViewItem ( this.cpuInfo, lvi );
          }
        }
      } catch ( ObjectDisposedException ode ) {

      } catch ( Exception ex ) {

      }
    }

    private void AddListViewItem ( ListView lv, ListViewItem lvi ) {
      lv.Items.Add ( lvi );
    }

    private ListView.ListViewItemCollection GetListViewItems ( ListView lv ) {
      return lv.Items;
    }

    private ListViewItem GetListViewItem ( ListView lv, int index ) {
      return lv.Items[ index ];
    }

    private void AddListViewSubItem ( ListViewItem lvi, string text ) {
      lvi.SubItems.Add ( text );
    }

    private void SetListViewItemText ( ListViewItem lvi, string text ) {
      lvi.Text = text;
    }

  }
}
