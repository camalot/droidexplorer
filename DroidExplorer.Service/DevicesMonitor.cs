using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Core;
using System.Reflection;
using Microsoft.Win32;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using DroidExplorer.Configuration;
using DroidExplorer.Configuration.Net;

namespace DroidExplorer.Service {
	// service account user directory:
	// C:\Windows\system32\config\systemprofile\AppData\Roaming\DroidExplorer\assets\

	/// <summary>
	/// 
	/// </summary>
	internal class DevicesMonitor {
		/// <summary>
		/// Occurs when [device added].
		/// </summary>
		public event EventHandler<DeviceEventArgs> DeviceAdded;
		/// <summary>
		/// Occurs when [device removed].
		/// </summary>
		public event EventHandler<DeviceEventArgs> DeviceRemoved;
		/// <summary>
		/// Occurs when [device state changed].
		/// </summary>
		public event EventHandler<DeviceEventArgs> DeviceStateChanged;

		private int _updateInterval = 5;

		/// <summary>
		/// Initializes a new instance of the <see cref="DevicesMonitor"/> class.
		/// </summary>
		internal DevicesMonitor() {
			this.LogDebug("Initializing Device Monitor");
			Devices = new List<string>();
			DeviceStatusChangedListeners = new Dictionary<string, EventHandler<DeviceEventArgs>>();
			UpdateTimer = new System.Threading.Timer(new System.Threading.TimerCallback(TimerUpdate), null, -1, -1);
		}

		/// <summary>
		/// Starts this instance.
		/// </summary>
		public void Start() {
			try {
				if(!Architecture.IsAssemblyX64 && Architecture.IsRunningX64) {
					throw new UnsupportedPlatformException();
				}
				if(!Running) {
					this.LogInfo("Starting Device Monitor");
					if(UpdateTimer != null) {
						UpdateTimer.Change(0, this.UpdateInterval * 1000);
					} else {
						UpdateTimer = new System.Threading.Timer(new System.Threading.TimerCallback(TimerUpdate), null, 0, this.UpdateInterval * 1000);
					}
					Running = true;
				}
			} catch(Exception ex) {
				this.LogError(ex.Message, ex);
				Running = false;
			}
		}

		/// <summary>
		/// Stops this instance.
		/// </summary>
		public void Stop() {
			if(UpdateTimer != null && Running) {
				this.LogInfo("Stopping Device Monitor");

				// unregister all connected devices and all "known" devices
				Devices.Union(KnownDeviceManager.Instance.GetKnownDevices()).ToList().ForEach(d => {
					OnDeviceStateChanged(new DeviceEventArgs(d, CommandRunner.DeviceState.Offline));
				});

				UpdateTimer.Change(-1, -1);
				Running = false;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="DevicesMonitor"/> is running.
		/// </summary>
		/// <value><c>true</c> if running; otherwise, <c>false</c>.</value>
		public bool Running { get; private set; }
		private System.Threading.Timer UpdateTimer { get; set; }
		private Dictionary<string, EventHandler<DeviceEventArgs>> DeviceStatusChangedListeners { get; set; }

		/// <summary>
		/// Gets or sets the devices.
		/// </summary>
		/// <value>The devices.</value>
		internal List<string> Devices { get; private set; }

		/// <summary>
		/// The update interval in seconds
		/// </summary>
		internal int UpdateInterval {
			get {
				return this._updateInterval;
			}
			set {
				if(value <= 0) {
					this._updateInterval = -1;
					Stop();
				} else {
					this._updateInterval = value;
					Start();
				}
			}
		}

		/// <summary>
		/// Timers the update.
		/// </summary>
		/// <param name="state">The state.</param>
		private void TimerUpdate(object state) {
			try {

				var allDevices = CommandRunner.Instance.GetDevices();
				var connected = allDevices.Select(s => s.SerialNumber);
				var known = Devices.Intersect(connected).ToList();
				var unknown = connected.Except(known).ToList();
				var disconnected = Devices.Except(connected).ToList();

				unknown.ForEach(d => {
					OnDeviceAdded(new DeviceEventArgs(d, CommandRunner.DeviceState.Device));
				});

				disconnected.ForEach(d => {
					OnDeviceRemoved(new DeviceEventArgs(d, CommandRunner.DeviceState.Offline));
				});

				Devices.Clear();
				Devices.AddRange(unknown.Union(known));

				/*List<string> foundDevices = CommandRunner.Instance.GetDevices ( );
				List<string> newDevices = new List<string> ( );
				List<string> oldDevices = new List<string> ( );


				for ( int i = 0; i < foundDevices.Count; i++ ) {
					if ( this.Devices.Contains ( foundDevices[i] ) ) {
						oldDevices.Add ( foundDevices[i] );
						Devices.Remove ( foundDevices[i] );
					} else {
						newDevices.Add ( foundDevices[i] );
						OnDeviceAdded ( new DeviceEventArgs ( foundDevices[i], CommandRunner.DeviceState.Device ) );
					}
				}

				List<string> tempDevices = new List<string> ( );
				tempDevices.AddRange ( oldDevices );
				tempDevices.AddRange ( newDevices );

				for ( int i = 0; i < Devices.Count; i++ ) {
					OnDeviceRemoved ( new DeviceEventArgs ( Devices[i], CommandRunner.DeviceState.Offline ) );
				}

				Devices.Clear ( );
				Devices.AddRange ( tempDevices );*/
			} catch(System.ComponentModel.Win32Exception wex) {
			} catch(FileNotFoundException fex) {
			} catch(Exception ex) {
				this.LogError(ex.Message, ex);
			}
		}

		/// <summary>
		/// Raises the <see cref="E:DeviceRemoved"/> event.
		/// </summary>
		/// <param name="e">The <see cref="DroidExplorer.Core.DeviceEventArgs"/> instance containing the event data.</param>
		protected void OnDeviceRemoved(DeviceEventArgs e) {
			if(this.DeviceRemoved != null) {
				this.DeviceRemoved(this, e);
			}

			if(DeviceStatusChangedListeners.ContainsKey(e.Device)) {
				CommandRunner.Instance.DeviceStateChanged -= DeviceStatusChangedListeners[e.Device];
				DeviceStatusChangedListeners.Remove(e.Device);
			}

			OnDeviceStateChanged(new DeviceEventArgs(e.Device, CommandRunner.DeviceState.Offline));
			this.LogInfo("Device Removed: {0}:{1}", e.Device, e.State);
		}

		/// <summary>
		/// Raises the <see cref="E:DeviceAdded"/> event.
		/// </summary>
		/// <param name="e">The <see cref="DroidExplorer.Core.DeviceEventArgs"/> instance containing the event data.</param>
		protected void OnDeviceAdded(DeviceEventArgs e) {
			if(this.DeviceAdded != null) {
				this.DeviceAdded(this, e);
			}

			if(!DeviceStatusChangedListeners.ContainsKey(e.Device)) {
				DeviceStatusChangedListeners.Add(e.Device, delegate(object sender, DeviceEventArgs e1) {
					OnDeviceStateChanged(e1);
				});
				CommandRunner.Instance.DeviceStateChanged += DeviceStatusChangedListeners[e.Device];
			}



			if(e.State == CommandRunner.DeviceState.Device) {
				OnDeviceStateChanged(e);
			}

			var deviceId = e.Device;
			if(!String.IsNullOrEmpty(deviceId)) {
				var info = CommandRunner.Instance.GetDevices().SingleOrDefault(m => m.SerialNumber == deviceId);
				// cache the device icon and images
				if(!String.IsNullOrEmpty(info.DeviceName)) {
					var ico = CloudImage.Instance.GetIcon(info.DeviceName);
					var png = CloudImage.Instance.GetImage(info.DeviceName);
				}

				if(Settings.Instance.SystemSettings.RecordDeviceInformationToCloud) {
					var d = Managed.Adb.AdbHelper.Instance.GetDevices ( Managed.Adb.AndroidDebugBridge.SocketAddress ).Single ( m => m.SerialNumber == deviceId );

					//Managed.Adb.Device d = new Managed.Adb.Device(deviceId, Managed.Adb.DeviceState.Online);
					if ( d != null) {
						CommandRunner.Instance.GetProperties(deviceId).ToList().ForEach(x => {
							d.Properties.Add(x.Key, x.Value);
						});

						// this will register devices when plugged in, if the user has opt'd to do so
						// and if the service is running
						CloudStatistics.Instance.RegisterDevice(d, info);
					}
				}
			}
			this.LogInfo("Device Added: {0}:{1}", e.Device, e.State);
		}

		/// <summary>
		/// Raises the <see cref="E:DeviceStateChanged"/> event.
		/// </summary>
		/// <param name="e">The <see cref="DroidExplorer.Core.DeviceEventArgs"/> instance containing the event data.</param>
		protected void OnDeviceStateChanged(DeviceEventArgs e) {
			if(this.DeviceStateChanged != null) {
				this.DeviceStateChanged(this, e);
			}

			switch(e.State) {
				case CommandRunner.DeviceState.Device:
					DeviceExplorerRegistration.Instance.Register(e.Device);
					break;
				case CommandRunner.DeviceState.Unknown:
				case CommandRunner.DeviceState.Offline:
				case CommandRunner.DeviceState.Bootloader:
					DeviceExplorerRegistration.Instance.Unregister(e.Device);
					break;
			}
			this.LogInfo("Device State Changed: {0}:{1}", e.Device, e.State);
		}
	}
}
