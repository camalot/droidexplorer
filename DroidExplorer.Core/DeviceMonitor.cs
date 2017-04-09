using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Managed.Adb;

namespace DroidExplorer.Core {
	/// <summary>
	/// 
	/// </summary>
	public class DeviceMonitor {
		/// <summary>
		/// Occurs when [device state changed].
		/// </summary>
		public event EventHandler<DeviceEventArgs> DeviceStateChanged;
		/// <summary>
		/// Occurs when [connected].
		/// </summary>
		public event EventHandler<DeviceEventArgs> Connected;
		/// <summary>
		/// Occurs when [disconnected].
		/// </summary>
		public event EventHandler<DeviceEventArgs> Disconnected;


		/// <summary>
		/// Initializes a new instance of the <see cref="DeviceMonitor"/> class.
		/// </summary>
		/// <param name="device">The device.</param>
		public DeviceMonitor ( string device ) {

		}
		public bool HasExited { get; set; }
		public string Device { get; private set; } = CommandRunner.Instance.DefaultDevice;
		private DeviceState State { get; set; } = DeviceState.Unknown;
		private Timer Timer { get; set; }
		public void Start ( ) {
			HasExited = false;
			if ( Timer == null ) {
				Timer = new Timer ( device => {
					var pstate = State;
					State = CommandRunner.Instance.GetDeviceStatus ( device as string );
					if ( pstate != State ) {
						if ( State == DeviceState.Device || State == DeviceState.Recovery ) {
							if ( this.Connected != null ) {
								this.LogDebug ( "Connected: {0}", State );
								this.Connected ( this, new DeviceEventArgs ( Device, State ) );
							}
						} else {
							if ( this.Disconnected != null ) {
								this.LogDebug ( "Disconnected: {0}", State );
								this.Disconnected ( this, new DeviceEventArgs ( Device, State ) );
							}
						}

						if ( DeviceStateChanged != null ) {
							this.LogDebug ( "State Changed: {0}", State );
							this.DeviceStateChanged ( this, new DeviceEventArgs ( Device, State ) );
						}
					}
				}, this.Device, 0, 1000 );
			}
		}

		public void Stop ( ) {
			if ( Timer != null ) {
				Timer.Change ( Timeout.Infinite, Timeout.Infinite );
				Timer = null;

				State = DeviceState.Offline;
				if ( this.Disconnected != null ) {
					this.Disconnected ( this, new DeviceEventArgs ( Device, State ) );
				}
				if ( DeviceStateChanged != null ) {
					this.LogDebug ( "State Changed: {0}", State );
					this.DeviceStateChanged ( this, new DeviceEventArgs ( Device, State ) );
				}
				this.LogDebug ( "Stop" );
				HasExited = true;
			}
		}

	}
}
