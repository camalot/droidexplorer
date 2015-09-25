using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DroidExplorer.Core.Plugins;
using System.Reflection;
using DroidExplorer.Core;
using System.Globalization;

namespace DroidExplorer.Configuration {
	/// <summary>
	/// 
	/// </summary>
	[XmlRoot ( "PluginSettings" )]
	public class PluginSettings {
		public readonly string[] DefaultPlaths = new string[] { "Plugins" };

		/// <summary>
		/// Initializes a new instance of the <see cref="PluginSettings"/> class.
		/// </summary>
		public PluginSettings ( ) {
			Plugins = new List<PluginInfo> ( );
			Paths = new List<string> ( );
		}

		/// <summary>
		/// Initializes the <see cref="PluginSettings"/> class.
		/// </summary>
		static PluginSettings ( ) {
			LoadedPlugins = new List<IPlugin> ( );
		}

		/// <summary>
		/// Gets or sets the loaded plugins.
		/// </summary>
		/// <value>The loaded plugins.</value>
		public static List<IPlugin> LoadedPlugins { get; private set; }

		/// <summary>
		/// Gets or sets the paths.
		/// </summary>
		/// <value>The paths.</value>
		[XmlArray ( "Paths" ), XmlArrayItem ( "Path" )]
		public List<string> Paths { get; set; }
		/// <summary>
		/// Gets or sets the plugins.
		/// </summary>
		/// <value>The plugins.</value>
		[XmlArray ( "Plugins" ), XmlArrayItem ( "Plugin" )]
		public List<PluginInfo> Plugins { get; set; }


		/// <summary>
		/// Gets the plugins.
		/// </summary>
		/// <param name="host">The host.</param>
		/// <returns></returns>
		public List<IPlugin> GetPlugins ( IPluginHost host ) {
			string localPath = System.IO.Path.GetDirectoryName ( this.GetType ( ).Assembly.Location );
			List<IPlugin> plugins = new List<IPlugin> ( );
			var merged = Paths.Union(Settings.Instance.PluginSettings.DefaultPlaths);
			foreach(string pathName in merged) {

				string tpathName = System.IO.Path.IsPathRooted ( pathName ) ? pathName : System.IO.Path.Combine ( localPath, pathName );

				System.IO.DirectoryInfo path = new System.IO.DirectoryInfo ( tpathName );
				foreach ( var item in path.GetFiles ( "*.dll" ) ) {
					try {
						Assembly tasm = AppDomain.CurrentDomain.Load ( AssemblyName.GetAssemblyName ( item.FullName ) );
						List<Type> types = tasm.GetTypes ( ).ToList<Type> ( );
						types.Sort ( new Comparison<Type> ( delegate ( Type x, Type y ) {
							return x.Name.CompareTo ( y.Name );
						} ) );
						foreach ( var type in types ) {
							if ( type.GetInterface ( typeof ( IPlugin ).FullName ) != null && !type.IsInterface ) {
								try {
									ConstructorInfo phCtor = type.GetConstructor ( new Type[] { typeof ( IPluginHost ) } );
									IPlugin plugin = null;

									PluginInfo pi = GetPluginInfo ( type );

									if ( pi == null ) {
										pi = new PluginInfo ( );
										pi.Enabled = true;
										pi.ID = string.Format ( CultureInfo.InvariantCulture, "{0}, {1}", type.FullName, tasm.GetName ( ).Name );
										pi.ExecuteOnLoad = false;
										pi.ExecuteOnUnload = false;
										pi.Name = type.Name;
										this.Plugins.Add ( pi );
									}

									if ( pi.Enabled ) {
										if ( !plugins.Contains ( plugin ) ) {
											if ( phCtor != null ) {
												plugin = Activator.CreateInstance ( type, host ) as IPlugin;
											} else {
												plugin = Activator.CreateInstance ( type ) as IPlugin;
											}
											LoadedPlugins.Add ( plugin );
											plugins.Add ( plugin );
										}
									}

								} catch ( Exception ex ) {
									this.LogError ( string.Format ( CultureInfo.InvariantCulture, "Unable to load type {0}: {1}", type.FullName ), ex );
								}
							}
						}
					} catch ( Exception ex ) {
						this.LogError ( string.Format ( CultureInfo.InvariantCulture, "Unable to load {0}", item ), ex );
					}
				}
			}

			return plugins;
		}

		/// <summary>
		/// Gets the plugin info.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public PluginInfo GetPluginInfo ( Type type ) {
			foreach ( var item in this.Plugins ) {
				string name = string.Format ( CultureInfo.InvariantCulture, "{0}, {1}", type.FullName, type.Assembly.GetName ( ).Name );
				if ( string.Compare ( item.ID, name, true ) == 0 ) {
					return item;
				}
			}
			return null;
		}

		/// <summary>
		/// Gets the plugin info.
		/// </summary>
		/// <param name="plugin">The plugin.</param>
		/// <returns></returns>
		public PluginInfo GetPluginInfo ( IPlugin plugin ) {
			return GetPluginInfo ( plugin.GetType ( ) );
		}

		/// <summary>
		/// Gets the plugin.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public IPlugin GetPlugin ( string id ) {
			foreach ( var item in LoadedPlugins ) {
				string tid = string.Format ( CultureInfo.InvariantCulture, "{0},{1}", item.GetType ( ).FullName, item.GetType ( ).Assembly.GetName ( ).Name );
				if ( string.Compare ( tid, id, true ) == 0 ) {
					return item;
				}
			}
			return null;
		}
	}
}
