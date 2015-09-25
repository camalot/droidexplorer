using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Core {
  public class DeviceEventArgs : EventArgs {

    public DeviceEventArgs ( string device )
      : this ( device, CommandRunner.DeviceState.Unknown ) {
    }

    public DeviceEventArgs ( string device, CommandRunner.DeviceState state ) {
      this.Device = device;
      this.State = state;
    }

    public string Device { get; set; }
    public CommandRunner.DeviceState State { get; set; }
  }
}
