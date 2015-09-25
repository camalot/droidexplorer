using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DroidExplorer.Service {
  /// <summary>
  /// 
  /// </summary>
  [RunInstaller ( true )]
  public partial class ServiceInstaller : System.ServiceProcess.ServiceInstaller {
    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceInstaller"/> class.
    /// </summary>
    public ServiceInstaller ( ) {
      InitializeComponent ( );
    }
  }
}
