using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using DroidExplorer.Core;
using DroidExplorer.Core.IO;
using DroidExplorer.Core.Configuration;
using Camalot.Common.Extensions;

namespace DroidExplorer.Configuration {
	/// <summary>
	/// 
	/// </summary>
	public class RegistrySettings : ISystemStoredSettings {
		/// <summary>
		/// 
		/// </summary>
#if PLATFORMX64 || PLATFORMIA64
		public const string SETTINGS_KEY = @"SOFTWARE\WOW6432Node\DroidExplorer\";
#else // PLATFORMX86
		public const string SETTINGS_KEY = @"SOFTWARE\DroidExplorer\";
#endif
		/// <summary>
		/// 
		/// </summary>
		private static RegistrySettings _instance = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="RegistrySettings"/> class.
		/// </summary>
		protected RegistrySettings() {

		}

		/// <summary>
		/// Reloads this instance.
		/// </summary>
		public void Reload() {
			_instance = null;
		}

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static RegistrySettings Instance {
			get {
				if(_instance == null) {
					_instance = new RegistrySettings();
				}
				return _instance;
			}
		}

		/// <summary>
		/// Gets or sets the SDK path.
		/// </summary>
		/// <value>The SDK path.</value>
		/// <remarks>Admin required to set!!!</remarks>
		public string SdkPath {
			get {
				string keyPath = string.Format(@"{0}InstallPath\", SETTINGS_KEY);
				using(var ukey = Registry.CurrentUser.OpenSubKey(keyPath)) {
					using(var lkey = Registry.LocalMachine.OpenSubKey(keyPath)) {
						if(ValueExists(ukey, Settings.SDK_PATH_REGISTRY_VALUE)) {
							return GetValue(ukey, Settings.SDK_PATH_REGISTRY_VALUE, string.Empty);
						} else if(ValueExists(lkey, Settings.SDK_PATH_REGISTRY_VALUE)) {
							return GetValue(lkey, Settings.SDK_PATH_REGISTRY_VALUE, string.Empty);
						} else {
							throw new ApplicationException(String.Format("Unable to locate the install path in 'HKLM\\{0}'. Try reinstalling the application.", SETTINGS_KEY));
						}
					}
				}
			}
			set {
				// we set to the HKCU so it doesn't require admin
				using(RegistryKey key = Registry.CurrentUser.CreateSubKey(string.Format(@"{0}InstallPath\", SETTINGS_KEY))) {
					WriteValue(key, Settings.SDK_PATH_REGISTRY_VALUE, value);
				}
			}
		}

		/// <summary>
		/// Gets or sets the install path.
		/// </summary>
		/// <value>The install path.</value>
		/// <remarks>Admin required to set!!!</remarks>
		public string InstallPath {
			get {
				string keyPath = string.Format(@"{0}InstallPath\", SETTINGS_KEY);
				if(KeyExists(Registry.CurrentUser, keyPath)) {
					using(RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath)) {
						string val = GetValue<string>(key, string.Empty, string.Empty);
						return val;
					}
				} else if(KeyExists(Registry.LocalMachine, keyPath)) {
					using(RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath)) {
						string val = GetValue<string>(key, string.Empty, string.Empty);
						return val;
					}
				} else {
					throw new ApplicationException("Unable to locate the install path in 'HKLM\\{0}'. Try reinstalling the application.");
				}
			}
			set {
				using(RegistryKey key = Registry.CurrentUser.CreateSubKey(string.Format(@"{0}InstallPath\", SETTINGS_KEY))) {
					WriteValue(key, string.Empty, value);
				}
			}
		}

		/// <summary>
		/// Gets or sets the SDK platform version.
		/// </summary>
		/// <value>The SDK platform version.</value>
		public int SdkPlatformVersion {
			get {

				string keyPath = string.Format(@"{0}InstallPath\", SETTINGS_KEY);
				using(var ukey = Registry.CurrentUser.OpenSubKey(keyPath)) {
					using(var lkey = Registry.LocalMachine.OpenSubKey(keyPath)) {
						if(ValueExists(ukey, "Platform")) {
							return GetValue(ukey, "Platform", 0);
						} else if(ValueExists(lkey, "Platform")) {
							return GetValue(lkey, "Platform", 0);
						} else {
							throw new ApplicationException(String.Format("Unable to locate the SDK Version in 'HKLM\\{0}'. Try reinstalling the application.", SETTINGS_KEY));
						}
					}
				}
			}
			set {
				using(RegistryKey key = Registry.CurrentUser.CreateSubKey(string.Format(@"{0}InstallPath\", SETTINGS_KEY))) {
					WriteValue(key, "Platform", value);
				}
			}
		}

		/// <summary>
		/// Gets the platform tools path.
		/// </summary>
		/// <value>The platform tools path.</value>
		public String PlatformToolsPath {
			get {
				var sdkPlatformTools = System.IO.Path.Combine(SdkPath, "platform-tools");
				return sdkPlatformTools;
			}
		}

		/// <summary>
		/// Gets the SDK tools path.
		/// </summary>
		/// <value>The SDK tools path.</value>
		public String SdkToolsPath {
			get {
				return System.IO.Path.Combine(SdkPath, "tools");
			}
		}

		public String BuildToolsPath {
			get {
				return System.IO.Path.Combine(SdkPath, "build-tools");
			}
		}

		/// <summary>
		/// Gets a value indicating whether we are using an existing SDK.
		/// </summary>
		/// <value><c>true</c> if we are using an existing SDK; otherwise, <c>false</c>.</value>
		public bool UseExistingSdk { get { return true; } set { return; } }

		public DateTime GetLastRecordCloud(string deviceId) {
			string keyPath = string.Format(@"{0}{1}\", SETTINGS_KEY, "Cloud");
			var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			using(var ukey = Registry.CurrentUser.OpenSubKey(keyPath)) {
				if(ValueExists(ukey, deviceId)) {
					return GetValue(ukey, deviceId, epoch.Date.ToUnixEpoch()).FromUnixEpoch();
				} else {
					return epoch.Date;
				}
			}
		}
		public void SetLastRecordCloud(string deviceId) {
			string keyPath = string.Format(@"{0}{1}\", SETTINGS_KEY, "Cloud");
			var now = DateTime.UtcNow.Date.ToUnixEpoch();
			using(var ukey = Registry.CurrentUser.CreateSubKey(keyPath)) {
				WriteValue(ukey, deviceId, now);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether device information will be recorded to the cloud.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if device information will be recorded to the cloud; otherwise, <c>false</c>.
		/// </value>
		public bool RecordDeviceInformationToCloud {
			get {
				String keyPath = SETTINGS_KEY;
				var cloudValueName = "CloudStatistics";
				using(var ukey = Registry.CurrentUser.OpenSubKey(keyPath)) {
					using(var lkey = Registry.LocalMachine.OpenSubKey(keyPath)) {
						if(ValueExists(ukey, cloudValueName)) {
							return GetValue<int>(ukey, cloudValueName, 0) == 1;
						} else if(ValueExists(lkey, cloudValueName)) {
							return GetValue<int>(lkey, cloudValueName, 0) == 1;
						}
					}
				}
				return false;
			}
			set {
				String path = SETTINGS_KEY;
				var cloudValueName = "CloudStatistics";
				using(var key = Registry.CurrentUser.CreateSubKey(path)) {
					WriteValue(key, cloudValueName, value ? 1 : 0);
				}
			}
		}

		/// <summary>
		/// Gets the root.
		/// </summary>
		/// <value>The root.</value>
		public virtual RegistryKey Root {
			get { return Registry.LocalMachine.CreateSubKey(SETTINGS_KEY); }
		}

		/// <summary>
		/// Writes the value.
		/// </summary>
		/// <param name="root">The root.</param>
		/// <param name="name">The name.</param>
		/// <param name="value">The value.</param>
		protected void WriteValue<T>(RegistryKey root, string name, T value) {
			var kind = GetRegistryKind(value);
			root.SetValue(name, value, kind);
		}

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="root">The root.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		protected T GetValue<T>(RegistryKey root, string name) {
			return GetValue<T>(root, name, default(T));
		}

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="root">The root.</param>
		/// <param name="name">The name.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		protected T GetValue<T>(RegistryKey root, string name, T defaultValue) {
			var val = root.GetValue(name, defaultValue, RegistryValueOptions.None);
			return (T)val;
		}

		/// <summary>
		/// Keys the exists.
		/// </summary>
		/// <param name="root">The root.</param>
		/// <param name="subKey">The sub key.</param>
		/// <returns></returns>
		protected bool KeyExists(RegistryKey root, string subKey) {
			try {
				using(RegistryKey rk = root.OpenSubKey(subKey)) {
					return rk != null;
				}
			} catch(Exception ex) {
				this.LogError(ex.Message, ex);
				return false;
			}
		}

		/// <summary>
		/// Values the exists.
		/// </summary>
		/// <param name="root">The root.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		protected bool ValueExists(RegistryKey root, string name) {
			try {
				return root != null && root.GetValue(name) != null;
			} catch(Exception ex) {
				this.LogError(ex.Message, ex);
				return false;
			}
		}

		protected RegistryValueKind GetRegistryKind<T>(T value) {
			if(value.Is<string>()) {
				if(value.ToString().IsMatch(@"%\S+%")) {
					return RegistryValueKind.ExpandString;
				} else {
					return RegistryValueKind.String;
				}
			} else if(value.Is<IEnumerable<string>>()) {
				return RegistryValueKind.MultiString;
			} else if(value.Is<byte[]>()) {
				return RegistryValueKind.Binary;
			} else if(value.Is<Int16>() || value.Is<Int32>()) {
				return RegistryValueKind.DWord;
			} else if(value.Is<Int64>()) {
				return RegistryValueKind.QWord;
			} else {
				return RegistryValueKind.Unknown;
			}
		}
	}
}
