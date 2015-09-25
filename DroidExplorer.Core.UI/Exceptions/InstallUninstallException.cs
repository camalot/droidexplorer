using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Core.UI.Exceptions {
  public class InstallUninstallException : Exception {
    public InstallUninstallException ( DroidExplorer.Core.UI.InstallDialog.InstallMode mode, string message, string content )
      : base ( message ) {
      this.Mode = mode;
      this.Content = content;
    }

    public DroidExplorer.Core.UI.InstallDialog.InstallMode Mode { get; set; }
    public string Content { get; set; }
  }
}
