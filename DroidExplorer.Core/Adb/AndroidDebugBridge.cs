using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace DroidExplorer.Core.Adb {
	public sealed class AndroidDebugBridge {
		private const int ADB_VERSION_MICRO_MIN = 20;
		private const int ADB_VERSION_MICRO_MAX = -1;


		private const String ADB_VERSION_PATTERN = "^.*(\\d+)\\.(\\d+)\\.(\\d+)$"; //$NON-NLS-1$

		private const String ADB = "adb"; //$NON-NLS-1$
		private const String DDMS = "ddms"; //$NON-NLS-1$

		// Where to find the ADB bridge.
		public const String ADB_HOST = "127.0.0.1"; //$NON-NLS-1$
		public const int ADB_PORT = 5037;


		#region statics
		private static IPAddress _hostAddr;
		private static AndroidDebugBridge _instance;
		private static bool _clientSupport;

		static AndroidDebugBridge ( ) {
			// built-in local address/port for ADB.
			try {
				_hostAddr = IPAddress.Parse ( ADB_HOST );

				SocketAddress = new IPEndPoint ( _hostAddr, ADB_PORT );
			} catch ( ArgumentOutOfRangeException e ) {
				throw;
			}
		}

		public static AndroidDebugBridge Instance {
			get {
				if ( _instance == null ) {
					_instance = CreateBridge ( );
				}
				return _instance;
			}
		}

		public static AndroidDebugBridge Bridge {
			get { return Instance; }
		}

		public static AndroidDebugBridge CreateBridge ( ) {
			_instance = new AndroidDebugBridge ( );
			return _instance;
		}

		public static IPEndPoint SocketAddress { get; private set; }
		#endregion
	}
}
