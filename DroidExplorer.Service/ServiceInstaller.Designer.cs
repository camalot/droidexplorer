using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Service {
  /// <summary>
  /// 
  /// </summary>
  public partial class ServiceInstaller : System.ServiceProcess.ServiceInstaller {
    /// <summary>
    /// 
    /// </summary>
    private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    /// <summary>
    /// Initializes the component.
    /// </summary>
    private void InitializeComponent ( ) {
      this.serviceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller ( );
      // 
      // serviceProcessInstaller
      // 
      this.serviceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
      this.serviceProcessInstaller.Password = null;
      this.serviceProcessInstaller.Username = null;
      // 
      // serviceInstaller
      // 
      this.ServiceName = "DroidExplorer";
      this.DisplayName = "Droid Explorer Service";
      this.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
      // 
      // ServiceInstaller
      // 
      this.Installers.AddRange ( new System.Configuration.Install.Installer[ ] {
																this.serviceProcessInstaller} );

    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component"/> and optionally releases the managed resources.
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
