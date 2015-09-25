using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using DroidExplorer.Core;
using DroidExplorer.Core.Configuration;

namespace DroidExplorer.Configuration {
	/// <summary>
	/// 
	/// </summary>
	public class KnownDeviceManager : RegistrySettings {
		public const string KNOWNDEVICES_KEY = @"KnownDevices";

		/// <summary>
		/// Initializes a new instance of the <see cref="KnownDeviceManager"/> class.
		/// </summary>
		protected KnownDeviceManager ( ) {

		}

		public override RegistryKey Root {
			get { return Registry.CurrentUser.CreateSubKey ( SETTINGS_KEY ); }
		}

		private static KnownDeviceManager _instance = null;
		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public new static KnownDeviceManager Instance {
			get {
				if ( _instance == null ) {
					_instance = new KnownDeviceManager ( );
				}
				return _instance;
			}
		}

		/// <summary>
		/// Gets the device GUID.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <returns></returns>
		public Guid GetDeviceGuid ( string device ) {
			try {
				Guid g = Guid.NewGuid ( );
				if ( KeyExists ( Root, KNOWNDEVICES_KEY ) ) {
					using ( RegistryKey rk = Root.OpenSubKey ( KNOWNDEVICES_KEY ) ) {
						if ( ValueExists ( rk, device ) ) {
							g = new Guid ( rk.GetValue ( device, g.ToString ( "B" ), RegistryValueOptions.None ).ToString ( ) );
						} else {
							using ( RegistryKey nkey = Root.CreateSubKey ( KNOWNDEVICES_KEY ) ) {
								WriteValue ( nkey, device, g.ToString ( "B" ) );
								WriteValue ( nkey, g.ToString ( "B" ), device );
							}
						}
					}
				} else {
					using ( RegistryKey nkey = Root.CreateSubKey ( KNOWNDEVICES_KEY, RegistryKeyPermissionCheck.ReadWriteSubTree ) ) {
						WriteValue ( nkey, device, g.ToString ( "B" ) );
						WriteValue ( nkey, g.ToString ( "B" ), device );
					}
				}
				return g;
			} catch ( Exception ex ) {
				this.LogError ( ex.Message, ex );
				return Guid.NewGuid ( );
				//throw;
			}
		}

		/// <summary>
		/// Gets the name of the device friendly.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <returns></returns>
		public string GetDeviceFriendlyName ( string device ) {
			Guid guid = GetDeviceGuid ( device );
			string friendlyName = device;
			try {
				using ( RegistryKey rk = Root.OpenSubKey ( KNOWNDEVICES_KEY ) ) {
					if ( rk != null ) {
						friendlyName = rk.GetValue ( guid.ToString ( "B" ), device ).ToString ( );
					}
				}
			} catch ( Exception ex ) {
				this.LogError ( ex.Message, ex );
			}
			return friendlyName;
		}

		/// <summary>
		/// Sets the name of the device friendly.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="name">The name.</param>
		public void SetDeviceFriendlyName ( string device, string name ) {
			Guid guid = GetDeviceGuid ( device );
			using ( RegistryKey nkey = Root.CreateSubKey ( KNOWNDEVICES_KEY ) ) {
				try {
					WriteValue ( nkey, guid.ToString ( "B" ), name );
				} catch ( Exception ex ) {
					this.LogError ( ex.Message, ex );
				}
			}
		}

		/// <summary>
		/// Gets the known devices.
		/// </summary>
		/// <returns></returns>
		public List<String> GetKnownDevices ( ) {
			if ( KeyExists ( Root, KNOWNDEVICES_KEY ) ) {
				using ( RegistryKey rk = Root.OpenSubKey ( KNOWNDEVICES_KEY ) ) {
					List<string> devices = new List<string> ( );
					foreach ( var item in rk.GetValueNames ( ) ) {
						if ( !item.StartsWith ( "{" ) && !item.EndsWith ( "}" ) ) {
							devices.Add ( item );
						}
					}
					return devices;
				}
			} else {
				return new List<String> ( );
			}
		}

		/// <summary>
		/// Deletes the known device.
		/// </summary>
		/// <param name="device">The device.</param>
		public void DeleteKnownDevice ( string device ) {
			Guid g = GetDeviceGuid ( device );
			using ( RegistryKey rk = Root.OpenSubKey ( KNOWNDEVICES_KEY ) ) {
				try {
					if ( ValueExists ( rk, g.ToString ( "B" ) ) ) {
						rk.DeleteValue ( g.ToString ( "B" ) );
					}
					if ( ValueExists ( rk, device ) ) {
						rk.DeleteValue ( device );
					}
				} catch ( Exception ex ) {
					this.LogError ( ex.Message, ex );
				}
			}
		}


	}
}
