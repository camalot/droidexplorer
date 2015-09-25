using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;
using DroidExplorer.Core.Plugins;

namespace DroidExplorer.Configuration {
	/// <summary>
	/// 
	/// </summary>
	public class PluginInfo {
		/// <summary>
		/// Initializes a new instance of the <see cref="PluginInfo"/> class.
		/// </summary>
		public PluginInfo ( ) {
			Enabled = true;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="PluginInfo"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		[XmlAttribute, Category ( "Plugin" ), DefaultValue ( true )]
		public bool Enabled { get; set; }
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		[XmlAttribute, Category ( "Plugin" ), DisplayName ( "(Name)" ), ReadOnly ( true )]
		public string Name { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether [execute on load].
		/// </summary>
		/// <value><c>true</c> if [execute on load]; otherwise, <c>false</c>.</value>
		[XmlAttribute, Category ( "Plugin" ), DefaultValue(false)]
		[Description("The plugin will run when the application is loading all plugins")]
		public bool ExecuteOnLoad { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether [execute on unload].
		/// </summary>
		/// <value><c>true</c> if [execute on unload]; otherwise, <c>false</c>.</value>
		[XmlAttribute, Category ( "Plugin" ), DefaultValue ( false )]
		[Description("The plugin will run when the application is unloading all plugins")]
		public bool ExecuteOnUnload { get; set; }
		/// <summary>
		/// Gets or sets the ID.
		/// </summary>
		/// <value>The ID.</value>
		[XmlAttribute, Browsable ( false )]
		public string ID { get; set; }


		[XmlIgnore,TypeConverter(typeof(ExpandableObjectConverter)),
		Category("Additional Information")]
		[DisplayName("(About)")]
		public IPlugin Plugin { get; internal set; }
		[XmlIgnore, TypeConverter(typeof(ExpandableObjectConverter)), Category("Additional Information")]

		[DisplayName("Settings")]
		public IPluginSettings Configuration { get { return Plugin != null ? Plugin.Configuration : null; } }
	}
}
