using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using DroidExplorer.Core;
using System.Globalization;
using DroidExplorer.Core.Xml.Serialization;

namespace DroidExplorer.Configuration {
	[XmlRoot("OptionTree")]
	public class OptionTreeSettings {
		public event EventHandler<EventArgs> Loaded;
		public event EventHandler<EventArgs> Saved;

		private static FileInfo _settingsFile = null;
		private static OptionTreeSettings _instance = null;

		static OptionTreeSettings ( ) {
			_settingsFile = new FileInfo ( Path.Combine ( Path.GetDirectoryName ( typeof ( OptionTreeSettings ).Assembly.Location ), @"Data\OptionsTree.config" ) );
		}

		[Obsolete ( "Use the Instance Propery, this is for the xml serializer", false )]
		public OptionTreeSettings ( ) {
			FileSystemWatcher watcher = new FileSystemWatcher ( Settings.SettingsFile.DirectoryName, OptionTreeSettings.SettingsFile.Name );
			watcher.NotifyFilter = NotifyFilters.LastWrite;
			watcher.Changed += delegate ( object sender, FileSystemEventArgs e ) {
				try {
					this.Reload ( );
				} catch ( Exception ) {

				}
			};
			watcher.EnableRaisingEvents = true;
			Nodes = new List<OptionNode> ( );
		}

		[XmlElement("OptionNode")]
		public List<OptionNode> Nodes { get; set; }

		/// <summary>
		/// Reloads the setting values
		/// </summary>
		public void Reload ( ) {
			_instance = null;
		}

		public void Save ( ) {
			try {
				using ( FileStream fs = new FileStream ( SettingsFile.FullName, FileMode.Create , FileAccess.Write, FileShare.Read ) ) {
					XmlSerializer<OptionTreeSettings>.Serialize ( fs, this );
				}
			} catch ( Exception ex ) {
				this.LogError ( ex.Message, ex );
			}
		}

		protected void OnLoaded ( EventArgs e ) {
			if ( Loaded != null ) {
				Loaded ( this, e );
			}
		}

		protected void OnSaved ( EventArgs e ) {
			if ( Saved != null ) {
				Saved ( this, e );
			}
		}


		public static FileInfo SettingsFile { get { return _settingsFile; } }

		/// <summary>
		/// Get instance of settings object.
		/// </summary>
		public static OptionTreeSettings Instance {
			get {
				try {
					if ( _instance == null ) {
						if ( SettingsFile.Exists ) {
							_instance = XmlSerializer<OptionTreeSettings>.Deserialize ( SettingsFile );
						} else {
							_instance = new OptionTreeSettings ( );
							Logger.LogError ( typeof ( OptionTreeSettings ), "Unable to load settings file.", new FileNotFoundException ( string.Format ( CultureInfo.InvariantCulture, "The settings file ({0}) was not found.", SettingsFile.FullName ) ) );
						}
						_instance.OnLoaded ( EventArgs.Empty );
					}
				} catch ( Exception ex ) {
					Logger.LogError ( typeof ( OptionTreeSettings ), ex.Message, ex );
				}
				return _instance;
			}
		}
	}
}
