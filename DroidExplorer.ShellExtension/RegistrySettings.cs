using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace DroidExplorer.ShellExtension {
	internal class RegistrySettings {
		internal const String SDK_PATH_REGISTRY_VALUE = "SdkPath";
#if PLATFORMX64 || PLATFORMIA64
		internal const string SETTINGS_KEY = @"SOFTWARE\WOW6432Node\DroidExplorer\";
#else // PLATFORMX86
		internal const string SETTINGS_KEY = @"SOFTWARE\DroidExplorer\";
#endif

		internal const String AAPT_COMMAND = "aapt.exe";

		private RegistrySettings ( ) {

		}
		private static RegistrySettings _instance;
		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		internal static RegistrySettings Instance {
			get {
				if ( _instance == null ) {
					_instance = new RegistrySettings ( );
				}
				return _instance;
			}
		}

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="root">The root.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		protected T GetValue<T> ( RegistryKey root, string name ) {
			return GetValue<T> ( root, name, default ( T ) );
		}

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="root">The root.</param>
		/// <param name="name">The name.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		protected T GetValue<T> ( RegistryKey root, string name, T defaultValue ) {
			return (T)root.GetValue ( name, defaultValue, RegistryValueOptions.None );
		}

		/// <summary>
		/// Values the exists.
		/// </summary>
		/// <param name="root">The root.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		protected bool ValueExists ( RegistryKey root, string name ) {
			try {
				return root != null && root.GetValue ( name ) != null;
			} catch ( Exception ex ) {
				Logger.Write( ex.ToString() );
				return false;
			}
		}

		internal String PlatformToolsPath {
			get {
				var sdkPlatformTools = System.IO.Path.Combine ( SdkPath, "platform-tools" );
				return sdkPlatformTools;
			}
		}

		internal string SdkPath {
			get {
				string keyPath = string.Format ( @"{0}InstallPath\", SETTINGS_KEY );
				using ( var ukey = Registry.CurrentUser.OpenSubKey ( keyPath ) ) {
					using ( var lkey = Registry.LocalMachine.OpenSubKey ( keyPath ) ) {
						if ( ValueExists ( ukey, SDK_PATH_REGISTRY_VALUE ) ) {
							return GetValue ( ukey, SDK_PATH_REGISTRY_VALUE, string.Empty );
						} else if ( ValueExists ( lkey, SDK_PATH_REGISTRY_VALUE ) ) {
							return GetValue ( lkey, SDK_PATH_REGISTRY_VALUE, string.Empty );
						} else {
							throw new ApplicationException ( String.Format ( "Unable to locate the install path in 'HKLM\\{0}'. Try reinstalling the application.", SETTINGS_KEY ) );
						}
					}
				}
			}
		}
	}
}
