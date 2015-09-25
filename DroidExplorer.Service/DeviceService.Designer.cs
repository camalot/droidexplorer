using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Service {
  /// <summary>
  /// 
  /// </summary>
  partial class DeviceService {
    /// <summary>
    /// 
    /// </summary>
    private System.ComponentModel.Container components = null;

    /// <summary>
    /// Initializes the component.
    /// </summary>
    private void InitializeComponent ( ) {

    }

    /// <summary>
    /// Disposes of the resources (other than memory) used by the <see cref="T:System.ServiceProcess.ServiceBase"/>.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose ( bool disposing ) {
      if ( disposing ) {
        if ( components != null ) {
          components.Dispose ( );
        }
      }
      base.Dispose ( disposing );
    }
  }
}
