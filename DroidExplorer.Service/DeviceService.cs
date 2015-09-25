using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Core;

namespace DroidExplorer.Service {
  /// <summary>
  /// 
  /// </summary>
  partial class DeviceService : System.ServiceProcess.ServiceBase {
    /// <summary>
    /// Initializes a new instance of the <see cref="Service"/> class.
    /// </summary>
    public DeviceService ( ) {
      Monitor = new DevicesMonitor ( );
      Monitor.DeviceAdded += new EventHandler<DeviceEventArgs> ( Monitor_DeviceAdded );
      Monitor.DeviceRemoved += new EventHandler<DeviceEventArgs> ( Monitor_DeviceRemoved );
      Monitor.DeviceStateChanged += new EventHandler<DeviceEventArgs> ( Monitor_DeviceStateChanged );
    }

    void Monitor_DeviceStateChanged ( object sender, DeviceEventArgs e ) {

    }

    void Monitor_DeviceRemoved ( object sender, DeviceEventArgs e ) {

    }

    void Monitor_DeviceAdded ( object sender, DeviceEventArgs e ) {

    }

    private DevicesMonitor Monitor { get; set; }
    /// <summary>
    /// When implemented in a derived class, <see cref="M:System.ServiceProcess.ServiceBase.OnContinue"/> runs when a Continue command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service resumes normal functioning after being paused.
    /// </summary>
    protected override void OnContinue ( ) {
      base.OnContinue ( );
      if ( !Monitor.Running )
        Monitor.Start ( );
    }

    /// <summary>
    /// When implemented in a derived class, <see cref="M:System.ServiceProcess.ServiceBase.OnCustomCommand(System.Int32)"/> executes when the Service Control Manager (SCM) passes a custom command to the service. Specifies actions to take when a command with the specified parameter value occurs.
    /// </summary>
    /// <param name="command">The command message sent to the service.</param>
    protected override void OnCustomCommand ( int command ) {
      base.OnCustomCommand ( command );
    }

    /// <summary>
    /// When implemented in a derived class, executes when a Pause command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service pauses.
    /// </summary>
    protected override void OnPause ( ) {
      base.OnPause ( );
      if ( Monitor.Running )
        Monitor.Stop ( );

    }

    /// <summary>
    /// When implemented in a derived class, executes when the computer's power status has changed. This applies to laptop computers when they go into suspended mode, which is not the same as a system shutdown.
    /// </summary>
    /// <param name="powerStatus">A <see cref="T:System.ServiceProcess.PowerBroadcastStatus"/> that indicates a notification from the system about its power status.</param>
    /// <returns>
    /// When implemented in a derived class, the needs of your application determine what value to return. For example, if a QuerySuspend broadcast status is passed, you could cause your application to reject the query by returning false.
    /// </returns>
    protected override bool OnPowerEvent ( System.ServiceProcess.PowerBroadcastStatus powerStatus ) {
      return base.OnPowerEvent ( powerStatus );
    }

    /// <summary>
    /// Executes when a change event is received from a Terminal Server session.
    /// </summary>
    /// <param name="changeDescription">A <see cref="T:System.ServiceProcess.SessionChangeDescription"/> structure that identifies the change type.</param>
    protected override void OnSessionChange ( System.ServiceProcess.SessionChangeDescription changeDescription ) {
      base.OnSessionChange ( changeDescription );
    }

    /// <summary>
    /// When implemented in a derived class, executes when the system is shutting down. Specifies what should occur immediately prior to the system shutting down.
    /// </summary>
    protected override void OnShutdown ( ) {
      base.OnShutdown ( );
      if ( Monitor.Running )
        Monitor.Stop ( );
    }

    /// <summary>
    /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
    /// </summary>
    /// <param name="args">Data passed by the start command.</param>
    protected override void OnStart ( string[ ] args ) {
      base.OnStart ( args );
      if ( !Monitor.Running )
        Monitor.Start ( );
    }



    /// <summary>
    /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
    /// </summary>
    protected override void OnStop ( ) {
      base.OnStop ( );
      if ( Monitor.Running )
        Monitor.Stop ( );
    }


  }
}
