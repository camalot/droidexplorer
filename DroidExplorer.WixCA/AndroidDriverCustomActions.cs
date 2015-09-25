using System;
using Microsoft.Deployment.WindowsInstaller;
using Microsoft.Win32;
using System.Globalization;

namespace DroidExplorer.WixCA {
	public class AndroidDriverCustomActions {

		// Registry Base
		// HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Enum\

		// HTC Dream locations:
		// USB\VID_0BB4&PID_0C01
		// USB\VID_0BB4&PID_0C02&MI_01
		// USB\VID_0BB4&PID_0FFF

		// HTC Magic locations:
		// USB\VID_0BB4&PID_0C03&MI_01

		// Motorola Sholes locations:
		// USB\VID_18D1&PID_D00D
		// USB\VID_18D1&PID_0002
		// USB\VID_18D1&PID_0002&MI_01
		// USB\VID_22B8&PID_41DB
		// USB\VID_22B8&PID_41DB&MI_01


		// {3F966BD9-FA04-4ec5-991C-D326973B5128} REVISION 2
		// {F72FE0D4-CBCB-407d-8814-9ED673D0DD6B} REVISION 1

		private const string ANDROIDUSBDRIVERVERSION = "ANDROIDUSBDRIVERVERSION";
		private const string ANDROIDUSBDRIVERSINSTALLED = "ANDROIDUSBDRIVERSINSTALLED";

		public const string REVISION2 = "2";
		public const string REVISION1 = "1";

		private const string REGISTRY_BASE = @"SYSTEM\ControlSet001\Enum\USB\{0}";

		private const string REVISION2_GUID = "{3F966BD9-FA04-4ec5-991C-D326973B5128}";
		private const string REVISION1_GUID = "{F72FE0D4-CBCB-407d-8814-9ED673D0DD6B}";

		private const string DREAM_SINGLEADBINTERFACE = @"VID_0BB4&PID_0C01";
		private const string DREAM_COMPOSITEADBINTERFACE = @"VID_0BB4&PID_0C02&MI_01";
		private const string DREAM_SINGLEBOOTLOADERINTERFACE = @"VID_0BB4&PID_0FFF";

		private const string MAGIC_COMPOSITEADBINTERFACE = @"VID_0BB4&PID_0C03&MI_01";

		private const string SHOLES_SINGLEBOOTLOADERINTERFACE = @"VID_18D1&PID_D00D";
		private const string SHOLES_SINGLEADBINTERFACE = @"VID_18D1&PID_0002";
		private const string SHOLES_COMPOSITEADBINTERFACE = @"VID_18D1&PID_0002&MI_01";
		private const string SHOLES_SINGLEADBINTERFACE2 = @"VID_22B8&PID_41DB";
		private const string SHOLES_COMPOSITEADBINTERFACE2 = @"VID_22B8&PID_41DB&MI_01";

		private const string WMI_QUERY = "Select ClassGuid, DriverVersion from Win32_PnPSignedDriver WHERE ClassGuid = \"{0}\"";

		[CustomAction]
		public static ActionResult GetAndroidUsbDriverVersion ( Session session ) {
			session.Log ( "(DECA) Begin GetAndroidUsbDriverVersion" );
			try {
				string[] deviceChecks = new string[] { 
				DREAM_SINGLEADBINTERFACE, 
				DREAM_COMPOSITEADBINTERFACE, 
				DREAM_SINGLEBOOTLOADERINTERFACE,

				MAGIC_COMPOSITEADBINTERFACE,

				SHOLES_SINGLEBOOTLOADERINTERFACE,
				SHOLES_SINGLEADBINTERFACE,
				SHOLES_COMPOSITEADBINTERFACE,
				SHOLES_SINGLEADBINTERFACE2,
				SHOLES_COMPOSITEADBINTERFACE2
			};

				foreach ( string deviceCheck in deviceChecks ) {

					using ( RegistryKey key = Registry.LocalMachine.OpenSubKey ( string.Format ( REGISTRY_BASE, deviceCheck ) ) ) {
						session.Log ( "(DECA) Checking Key: \"{0}\"", deviceCheck );
						if ( key != null ) {
							string[] subkeys = key.GetSubKeyNames ( );

							foreach ( string subkey in subkeys ) {
								session.Log ( "(DECA) Checking Subkey: \"{0}\"", subkey );
								using ( RegistryKey skey = key.OpenSubKey ( subkey ) ) {
									if ( skey != null ) {
										string driverGuid = (string)skey.GetValue ( "ClassGUID", string.Empty );
										session.Log ( @"(DECA) {0}\ClassGuid=""{1}""", skey.Name, driverGuid );
										switch ( driverGuid ) {
											case REVISION2_GUID:
												session[ANDROIDUSBDRIVERVERSION] = REVISION2;
												session[ANDROIDUSBDRIVERSINSTALLED] = "1";
												session.Log ( "(DECA) Found Revision 2 version of Android USB Driver" );
												return ActionResult.Success;
											case REVISION1_GUID:
												session[ANDROIDUSBDRIVERVERSION] = REVISION1;
												session[ANDROIDUSBDRIVERSINSTALLED] = "1";
												session.Log ( "(DECA) Found Revision 1 version of Android USB Driver" );
												return ActionResult.Success;
											default:
												// didnt find the classGUID so check the next subkey / device
												break;
										}
									}
								}
							}

						}
					}
				}
			} catch ( Exception ex ) {
				session.Log ( ex.ToString ( ) );
			}

			// if we make it here set that the drivers are not installed.
			session[ANDROIDUSBDRIVERVERSION] = "0";
			session[ANDROIDUSBDRIVERSINSTALLED] = "0";
			session.Log ( "(DECA) Did not find any version of Android USB Driver" );
			return ActionResult.Success;
		}

		/// <summary>
		/// Gets the android usb driver version via WMI.
		/// </summary>
		/// <remarks>It may be a little slower, but it is more accurate</remarks>
		/// <returns></returns>
		[CustomAction]
		public static ActionResult GetAndroidUsbDriverVersionWmi ( Session session ) {
			session.Log ( "(DECA) Begin GetAndroidUsbDriverVersionWmi" );
			try {
				System.Management.ManagementClass wmi = new System.Management.ManagementClass ( );
				System.Management.ObjectQuery oQuery = new System.Management.ObjectQuery ( string.Format ( CultureInfo.InvariantCulture, WMI_QUERY, REVISION2_GUID ) );
				System.Management.ManagementScope oMs = new System.Management.ManagementScope ( );
				System.Management.ManagementObjectSearcher oSearcher = new System.Management.ManagementObjectSearcher ( oMs, oQuery );
				System.Management.ManagementObjectCollection oResults = oSearcher.Get ( );
				if ( oResults.Count > 0 ) {
					foreach ( var item in oResults ) {
						string cg = string.Format ( CultureInfo.InvariantCulture, "{0}", item["ClassGuid"] );
						if ( !string.IsNullOrEmpty ( cg ) ) {
							bool foundR1 = string.Compare ( cg, REVISION1_GUID, true ) == 0;
							bool foundR2 = string.Compare ( cg, REVISION2_GUID, true ) == 0;
							if ( foundR1 ) {
								session[ANDROIDUSBDRIVERVERSION] = REVISION1;
								session[ANDROIDUSBDRIVERSINSTALLED] = "1";
								session.Log ( "(DECA) Found Revision 1 version of Android USB Driver" );
								return ActionResult.Success;
							} else if ( foundR2 ) {
								session[ANDROIDUSBDRIVERVERSION] = REVISION2;
								session[ANDROIDUSBDRIVERSINSTALLED] = "0";
								session.Log ( "(DECA) Found Revision 2 version of Android USB Driver" );
								return ActionResult.Success;
							} else {
								session.Log ( "(DECA) Did not find a know ClassGuid: {0}", cg );
							}
						} else {
							session.Log ( "(DECA) ClassGuid was Empty" );
						}
					}
				} else {
					session.Log ( "(DECA) No WMI Results were returned." );
				}
			} catch ( Exception ex ) {
				session.Log ( ex.ToString() );
			}
			session[ANDROIDUSBDRIVERVERSION] = "0";
			session[ANDROIDUSBDRIVERSINSTALLED] = "0";
			session.Log ( "(DECA) Did not find any version of Android USB Driver" );
			return ActionResult.Success;

		}
	}
}
