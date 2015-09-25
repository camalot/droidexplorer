using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using DroidExplorer.Core;
using System.Globalization;
using DroidExplorer.Core.Xml.Serialization;
using System.Xml;
using DroidExplorer.Core.Net;
using DroidExplorer.Core.Configuration;

namespace DroidExplorer.Configuration {
	/// <summary>
	/// 
	/// </summary>
	[XmlRoot ( "DroidExplorer" )]
	public class Settings : ISettings {
		public const String SDK_PATH_REGISTRY_VALUE = "SdkPath";

		/// <summary>
		/// Occurs when [loaded].
		/// </summary>
		public event EventHandler<EventArgs> Loaded;
		/// <summary>
		/// Occurs when [saved].
		/// </summary>
		public event EventHandler<EventArgs> Saved;

		/// <summary>
		/// 
		/// </summary>
		protected static FileInfo _settingsFile = null;
		/// <summary>
		/// 
		/// </summary>
		protected static FileInfo _defaultSettingsFile = null;
		/// <summary>
		/// 
		/// </summary>
		private static Settings _instance = null;



		/// <summary>
		/// Initializes the <see cref="Settings"/> class.
		/// </summary>
		static Settings ( ) {
			String dataDir = FolderManagement.UserDataFolder;
			_settingsFile = new FileInfo ( Path.Combine ( dataDir, @"Data\DroidExplorer.config" ) );
			_defaultSettingsFile = new FileInfo ( Path.Combine ( Path.GetDirectoryName ( typeof ( Settings ).Assembly.Location ), @"Data\DroidExplorer.config" ) );
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Settings"/> class.
		/// </summary>
		[Obsolete ( "Use the Instance Property, this is for the xml serializer", false )]
		public Settings ( ) {
			Window = new WindowSettings ( );
			Proxy = new ProxyInfo ( );
			Proxy.Enabled = false;
			WarnWhenNoAdbRoot = true;
			// monitor the settings file and reload when it changes
			FileSystemWatcher watcher = new FileSystemWatcher ( Settings.SettingsFile.DirectoryName, Settings.SettingsFile.Name );
			watcher.NotifyFilter = NotifyFilters.LastWrite;
			watcher.Changed += delegate ( object sender, FileSystemEventArgs e ) {
				try {
					this.Reload ( );
				} catch ( Exception ex ) {
					this.LogWarn ( ex.Message, ex );
				}
			};
			watcher.EnableRaisingEvents = true;
			PluginSettings = new PluginSettings ( );
		}

		/// <summary>
		/// Gets or sets the plugin settings.
		/// </summary>
		/// <value>The plugin settings.</value>
		[XmlElement ( "PluginSettings" )]
		public PluginSettings PluginSettings { get; set; }

		/// <summary>
		/// Gets or sets the system settings.
		/// </summary>
		/// <value>The system settings.</value>
		[XmlIgnore]
		public ISystemStoredSettings SystemSettings { get; set; }

		/// <summary>
		/// Gets or sets the apk paths.
		/// </summary>
		/// <value>The apk paths.</value>
		[XmlArray ( "ApkPaths" ), XmlArrayItem ( "Path" )]
		public List<string> ApkPaths { get; set; }

		/// <summary>
		/// Gets or sets the window.
		/// </summary>
		/// <value>The window.</value>
		[XmlElement ( "Window" )]
		public WindowSettings Window { get; set; }

		/// <summary>
		/// Gets or sets the proxy.
		/// </summary>
		/// <value>The proxy.</value>
		[XmlElement ( "Proxy" )]
		public ProxyInfo Proxy { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether [warn when no adb root].
		/// </summary>
		/// <value>
		///   <c>true</c> if [warn when no adb root]; otherwise, <c>false</c>.
		/// </value>
		[XmlElement("WarnWhenNoAdbRoot")]
		public bool WarnWhenNoAdbRoot { get; set; }

		/// <summary>
		/// Reloads the setting values
		/// </summary>
		public void Reload ( ) {
			_instance = null;
		}

		/// <summary>
		/// Saves this instance.
		/// </summary>
		public void Save ( ) {
			try {
				using ( FileStream fs = new FileStream ( SettingsFile.FullName, FileMode.Create, FileAccess.Write, FileShare.Read ) ) {
					XmlSerializer<Settings>.Serialize ( fs, this );
					Logger.LogDebug ( typeof ( Settings ), "Settings saved to file '{0}'", SettingsFile.Name );
				}
			} catch ( Exception ex ) {
				this.LogError ( ex.Message, ex );
			}
		}

		/// <summary>
		/// Raises the <see cref="E:Loaded"/> event.
		/// </summary>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void OnLoaded ( EventArgs e ) {
			if ( Loaded != null ) {
				Loaded ( this, e );
			}
		}

		/// <summary>
		/// Raises the <see cref="E:Saved"/> event.
		/// </summary>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void OnSaved ( EventArgs e ) {
			if ( Saved != null ) {
				Saved ( this, e );
			}
		}

		/// <summary>
		/// Gets the user data directory.
		/// </summary>
		/// <value>The user data directory.</value>
		public string UserDataDirectory {
			get {
				return FolderManagement.UserDataFolder;
			}
		}

		public string ProgramDataDirectory {
			get {
				return FolderManagement.ProgramDataFolder;
			}
		}

		public string ToolsDirectory {
			get {
				return FolderManagement.BundledToolsFolder;
			}
		}


		/// <summary>
		/// Gets the settings file.
		/// </summary>
		/// <value>The settings file.</value>
		public static FileInfo SettingsFile { get { return _settingsFile; } }
		/// <summary>
		/// Gets the default settings file.
		/// </summary>
		/// <value>The default settings file.</value>
		public static FileInfo DefaultSettingsFile { get { return _defaultSettingsFile; } }

		/// <summary>
		/// Get instance of settings object.
		/// </summary>
		public static Settings Instance {
			get {
				try {
					if ( _instance == null ) {

						if ( !SettingsFile.Exists && DefaultSettingsFile.Exists ) {
							if ( !SettingsFile.Directory.Exists ) {
								SettingsFile.Directory.Create ( );
							}
							DefaultSettingsFile.CopyTo ( SettingsFile.FullName, true );
						}

						if ( SettingsFile.Exists ) {
							_instance = XmlSerializer<Settings>.Deserialize ( SettingsFile );
							Logger.LogDebug ( typeof ( Settings ), "Settings Loaded from file '{0}'", SettingsFile.FullName );
						} else {
							_instance = XmlSerializer<Settings>.Deserialize ( DefaultSettingsFile );
							Logger.LogDebug ( typeof ( Settings ), "Settings Loaded from file '{0}'", DefaultSettingsFile.FullName );
						}
						_instance.SystemSettings = RegistrySettings.Instance;
						_instance.OnLoaded ( EventArgs.Empty );
					}
				} catch ( XmlException xe ) {
					Logger.LogError ( typeof ( Settings ), xe.Message, xe );
					_instance = new Settings ( );
				} catch ( Exception ex ) {
					Logger.LogError ( typeof ( Settings ), ex.Message, ex );
					_instance = new Settings();
				}
				return _instance;
			}
		}
	}
}
