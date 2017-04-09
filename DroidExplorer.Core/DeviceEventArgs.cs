using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Managed.Adb;

namespace DroidExplorer.Core {
  public class DeviceEventArgs : EventArgs {

    public DeviceEventArgs ( string device )
      : this ( device, DeviceState.Unknown ) {
    }

    public DeviceEventArgs ( string device, DeviceState state ) {
      this.Device = device;
      this.State = state;
    }

    public string Device { get; set; }
    public DeviceState State { get; set; }
  }
}
