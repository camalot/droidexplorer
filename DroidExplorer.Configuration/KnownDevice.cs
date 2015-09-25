using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DroidExplorer.Configuration {
  public class KnownDevice {

    [Category ( "Device" ),ReadOnly(true)]
    public string SerialNumber { get; set; }
    [Category ( "Device" ), Description("A 'friendly name' for the device")]
    public string DisplayName { get; set; }
    [Category ( "Device" ), DisplayName ( "(GUID)" ), ReadOnly(true),
    Description("The GUID that is generated for the device when it is added to the system for the first time")]
    public Guid Guid { get; set; }

  }
}
