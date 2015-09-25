using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Core.IO;
using System.Globalization;

namespace DroidExplorer.Core.UI.Components {
  public class CpuProcessInfoListViewItem : ListViewItem {
    public CpuProcessInfoListViewItem ( ProcessInfo pi )
      : base ( pi.PID.ToString ( ) ) {
      this.ProcessInfo = pi;

      this.SubItems.Add ( string.Format ( CultureInfo.InvariantCulture, "{0}%", ProcessInfo.Cpu ) );
      this.SubItems.Add ( ProcessInfo.Name );
      this.SubItems.Add ( ProcessInfo.Thread.ToString ( ) );
      this.SubItems.Add ( string.Format ( CultureInfo.InvariantCulture, "{0}K", ProcessInfo.Vss ) );
      this.SubItems.Add ( string.Format ( CultureInfo.InvariantCulture, "{0}K", ProcessInfo.Rss ) );
      this.SubItems.Add ( ProcessInfo.User );
    }

    public ProcessInfo ProcessInfo { get; set; }
  }
}
