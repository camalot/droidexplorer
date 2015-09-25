using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Core.IO;

namespace DroidExplorer.Core.Components {
  public class ProcessInfoEventArgs : EventArgs {
    
    public ProcessInfoEventArgs ( ProcessInfo pi ) {
      ProcessInfo = pi;
    }

    public ProcessInfo ProcessInfo { get; set; }
  }
}
