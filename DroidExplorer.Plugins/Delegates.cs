using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DroidExplorer.Plugins {
  public delegate void SetToolStripItemEnabledDelegate ( ToolStripItem tsi, bool enabled );

  public static class DelegateExtensions {
    public static void SetEnabled ( this ToolStripItem tsi, bool enabled ) {
      if ( tsi.Owner != null && tsi.Owner.InvokeRequired ) {
        tsi.Owner.Invoke ( new SetToolStripItemEnabledDelegate ( SetToolStripItemEnabled ), tsi, enabled );
      } else {
        SetToolStripItemEnabled ( tsi, enabled );
      }
    }

    private static void SetToolStripItemEnabled ( ToolStripItem tsi, bool enabled ) {
      tsi.Enabled = enabled;
    }
  }

}
