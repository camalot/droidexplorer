using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
//using DroidExplorer.Core.Reflection;
using System.Text.RegularExpressions;
using System.IO;

namespace DroidExplorer.Core.Adb {
	public enum DeviceState {
		/// <summary>
		/// 
		/// </summary>
		Unknown,
		/// <summary>
		/// 
		/// </summary>
		Device,
		/// <summary>
		/// 
		/// </summary>
		Offline,
		/// <summary>
		/// 
		/// </summary>
		Online,
		/// <summary>
		/// 
		/// </summary>
		BootLoader,
		/// <summary>
		/// 
		/// </summary>
		Recovery,
		/// <summary>
		/// 
		/// </summary>
		Unauthorized
	}

	public sealed class Device : IDevice {
		public Device ( String serial, DeviceState state ) {
			this.SerialNumber = serial;
			this.State = state;
			Properties = new Dictionary<string, string> ( );
		}
		public const String PROP_BUILD_VERSION = "ro.build.version.release";
		public const String PROP_BUILD_API_LEVEL = "ro.build.version.sdk";
		public const String PROP_BUILD_CODENAME = "ro.build.version.codename";

		public const String PROP_DEBUGGABLE = "ro.debuggable";

		/** Serial number of the first connected emulator. */
		public const String FIRST_EMULATOR_SN = "emulator-5554"; //$NON-NLS-1$
		/** Device change bit mask: {@link DeviceState} change. */
		public const int CHANGE_STATE = 0x0001;
		/** Device change bit mask: {@link Client} list change. */
		public const int CHANGE_CLIENT_LIST = 0x0002;
		/** Device change bit mask: build info change. */
		public const int CHANGE_BUILD_INFO = 0x0004;

		/** @deprecated Use {@link #PROP_BUILD_API_LEVEL}. */
		public const String PROP_BUILD_VERSION_NUMBER = PROP_BUILD_API_LEVEL;

		/**
		 * The state of a device.
		 */
		

		public static DeviceState GetStateFromString ( String state ) {
			if ( Enum.IsDefined ( typeof ( DeviceState ), state ) ) {
				return (DeviceState)Enum.Parse ( typeof ( DeviceState ), state, true );
			} else {
				foreach ( var fi in typeof ( DeviceState ).GetFields ( ) ) {
					/*
					FieldDisplayNameAttribute dna = ReflectionHelper.GetCustomAttribute<FieldDisplayNameAttribute> ( fi );
					if ( dna != null ) {
						if ( string.Compare ( dna.DisplayName, state, false ) == 0 ) {
							return (DeviceState)fi.GetValue ( null );
						}
					} else { */
						if ( string.Compare ( fi.Name, state, true ) == 0 ) {
							return (DeviceState)fi.GetValue ( null );
						}
					// }
				}
			}
			return DeviceState.Unknown;
		}

		public static Device CreateFromAdbData ( String data ) {
			Regex re = new Regex ( RE_DEVICELIST_INFO, RegexOptions.Compiled | RegexOptions.IgnoreCase );
			Match m = re.Match ( data );
			if ( m.Success ) {
				return new Device ( m.Groups[1].Value, GetStateFromString ( m.Groups[2].Value ) );
			} else {
				throw new ArgumentException ( "Invalid device list data" );
			}
		}

		/** Emulator Serial Number regexp. */
		const String RE_EMULATOR_SN = @"emulator-(\d+)"; //$NON-NLS-1$
		const String RE_DEVICELIST_INFO = @"^([^\s]+)\s+(device|offline|unknown|bootloader)$";
		private const String LOG_TAG = "Device";
		private string avdName;

	

		/*
		 * (non-Javadoc)
		 * @see com.android.ddmlib.IDevice#getSerialNumber()
		 */
		public String SerialNumber { get; private set; }

		/** {@inheritDoc} */
		public String AvdName {
			get { return avdName; }
			private set {
				if ( !IsEmulator ) {
					throw new ArgumentException ( "Cannot set the AVD name of the device is not an emulator" );
				}
				avdName = value;
			}
		}

		/*
		 * (non-Javadoc)
		 * @see com.android.ddmlib.IDevice#getState()
		 */
		public DeviceState State { get; private set; }


		/*
		 * (non-Javadoc)
		 * @see com.android.ddmlib.IDevice#getProperties()
		 */
		public Dictionary<String, String> Properties { get; private set; }

		/*
		 * (non-Javadoc)
		 * @see com.android.ddmlib.IDevice#getPropertyCount()
		 */
		public int PropertyCount {
			get {
				return Properties.Count;
			}
		}

		/*
		 * (non-Javadoc)
		 * @see com.android.ddmlib.IDevice#getProperty(java.lang.String)
		 */
		public String GetProperty ( String name ) {
			return GetProperty ( new String[] { name } );
		}

		public String GetProperty ( params String[] propertyList ) {
			foreach ( var item in propertyList ) {
				if ( Properties.ContainsKey ( item ) ) {
					return Properties[item];
				}
			}

			throw new IndexOutOfRangeException ( "Unable to locate any of the specified properties" );
		}


		//@Override
		public override String ToString ( ) {
			return SerialNumber;
		}

		/*
		 * (non-Javadoc)
		 * @see com.android.ddmlib.IDevice#isOnline()
		 */
		public bool IsOnline {
			get {
				return State == DeviceState.Online;
			}
		}

		/*
		 * (non-Javadoc)
		 * @see com.android.ddmlib.IDevice#isEmulator()
		 */
		public bool IsEmulator {
			get {
				return Regex.Match ( SerialNumber, RE_EMULATOR_SN ).Success;
			}
		}

		/*
		 * (non-Javadoc)
		 * @see com.android.ddmlib.IDevice#isOffline()
		 */
		public bool IsOffline {
			get {
				return State == DeviceState.Offline;
			}
		}

		/*
		 * (non-Javadoc)
		 * @see com.android.ddmlib.IDevice#isBootLoader()
		 */
		public bool IsBootLoader {
			get {
				return State == DeviceState.BootLoader;
			}
		}

		public RawImage Screenshot {
			get {
				return AdbHelper.Instance.GetFrameBuffer ( AndroidDebugBridge.SocketAddress, this );
			}
		}

		public void ExecuteShellCommand ( String command, IShellOutputReceiver receiver ) {
			AdbHelper.Instance.ExecuteRemoteCommand ( AndroidDebugBridge.SocketAddress, command, this,
							receiver );
		}
	}
}
