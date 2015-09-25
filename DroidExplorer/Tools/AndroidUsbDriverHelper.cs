using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using DroidExplorer.Core;

namespace DroidExplorer.Tools {
  internal static class AndroidUsbDriverHelper {
    // 1.6 ClassGuid = {3F966BD9-FA04-4ec5-991C-D326973B5128}
    // 1.5 ClassGuid = {F72FE0D4-CBCB-407d-8814-9ED673D0DD6B}
    private const string DRIVER_CLASS_GUID_REVISION1 = "{F72FE0D4-CBCB-407d-8814-9ED673D0DD6B}";
    private const string DRIVER_CLASS_GUID_REVISION2 = "{3F966BD9-FA04-4ec5-991C-D326973B5128}";
    private const string QUERY = "Select ClassGuid, DriverVersion from Win32_PnPSignedDriver WHERE ClassGuid = \"{3F966BD9-FA04-4ec5-991C-D326973B5128}\" OR ClassGuid = \"{F72FE0D4-CBCB-407d-8814-9ED673D0DD6B}\"";
    static AndroidUsbDriverHelper ( ) {
			try {
				System.Management.ManagementClass wmi = new System.Management.ManagementClass ( );
				System.Management.ObjectQuery oQuery = new System.Management.ObjectQuery ( QUERY );
				System.Management.ManagementScope oMs = new System.Management.ManagementScope ( );
				System.Management.ManagementObjectSearcher oSearcher = new System.Management.ManagementObjectSearcher ( oMs, oQuery );
				System.Management.ManagementObjectCollection oResults = oSearcher.Get ( );
				if ( oResults.Count == 1 ) {
					foreach ( var item in oResults ) {
						string dv = string.Format ( CultureInfo.InvariantCulture, "{0}", item["DriverVersion"] );
						if ( !string.IsNullOrEmpty ( dv ) ) {
							DriverVersion = new Version ( dv );
						} else {
							// default
							DriverVersion = new Version ( "0.0.0.0" );
						}

						string cg = string.Format ( CultureInfo.InvariantCulture, "{0}", item["ClassGuid"] );
						if ( !string.IsNullOrEmpty ( cg ) ) {
							IsRevision2Driver = string.Compare ( cg, DRIVER_CLASS_GUID_REVISION2, true ) == 0;
							IsRevision1Driver = !IsRevision2Driver;
						} else {
							// default to cupcake
							IsRevision1Driver = true;
							IsRevision2Driver = false;
						}
					}
				}
			} catch ( Exception ex ) {
				Logger.LogError ( typeof ( AndroidUsbDriverHelper ), ex.Message, ex );
				DriverVersion = new Version ( "0.0.0.0" );
			}
    }

    public static bool IsRevision2Driver { get; private set; }
    public static bool IsRevision1Driver { get; private set; }

    public static Version DriverVersion { get; private set; }
  }
}
