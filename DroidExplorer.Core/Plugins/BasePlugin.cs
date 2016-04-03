using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroidExplorer.Core.Plugins {
	public abstract class BasePlugin : IPluginExtendedInfo {

		public BasePlugin ( IPluginHost host ) {
			this.PluginHost = host;
		}

		#region IPluginExtendedInfo Members

		public abstract string Author { get; }

		public abstract string Url { get; }

		public abstract string Contact { get; }

		public virtual string Copyright {
			get { return String.Format ( "Copyright © {1} {0}", DateTime.Now.Year, Author ); }
		}

		#endregion

		#region IPlugin Members

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public abstract string Name { get; }
		/// <summary>
		/// Gets the group.
		/// </summary>
		/// <value>
		/// The group.
		/// </value>
		public abstract string Group { get; }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public abstract string Description { get; }

		/// <summary>
		/// Gets a value indicating whether this instance has configuration.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance has configuration; otherwise, <c>false</c>.
		/// </value>
		public virtual bool HasConfiguration {
			get { return false; }
		}

		/// <summary>
		/// Gets the image.
		/// </summary>
		/// <value>
		/// The image.
		/// </value>
		public virtual System.Drawing.Image Image {
			get { return null; }
		}

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		public virtual string Text {
			get { return Name; }
		}

		/// <summary>
		/// Gets a value indicating whether [create tool button].
		/// </summary>
		/// <value>
		///   <c>true</c> if [create tool button]; otherwise, <c>false</c>.
		/// </value>
		public abstract bool CreateToolButton { get; }

		/// <summary>
		/// Gets the display style.
		/// </summary>
		/// <value>
		/// The display style.
		/// </value>
		public System.Windows.Forms.ToolStripItemDisplayStyle DisplayStyle {
			get { return System.Windows.Forms.ToolStripItemDisplayStyle.Image; }
		}

		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		public virtual void Execute ( IPluginHost pluginHost, IO.LinuxDirectoryInfo currentDirectory ) {
			Execute ( pluginHost, currentDirectory, null );
		}
		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		public virtual void Execute ( IPluginHost pluginHost ) {
			Execute ( pluginHost, new DroidExplorer.Core.IO.LinuxDirectoryInfo ( "/" ), null );
		}

		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		/// <param name="args">The arguments.</param>
		public abstract void Execute ( IPluginHost pluginHost, IO.LinuxDirectoryInfo currentDirectory, string[] args );

		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="args">The arguments.</param>
		public virtual void Execute ( IPluginHost pluginHost, string[] args ) {
			Execute ( pluginHost, new DroidExplorer.Core.IO.LinuxDirectoryInfo ( "/" ), args );
		}


		/// <summary>
		/// Executes the asynchronous.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		/// <returns></returns>
		public virtual async Task ExecuteAsync ( IPluginHost pluginHost, IO.LinuxDirectoryInfo currentDirectory ) {
			await ExecuteAsync ( pluginHost, currentDirectory, null );
		}

		/// <summary>
		/// Executes the asynchronous.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <returns></returns>
		public virtual async Task ExecuteAsync ( IPluginHost pluginHost ) {
			await ExecuteAsync ( pluginHost, new IO.LinuxDirectoryInfo ( "/" ), null );
		}

		/// <summary>
		/// Executes the asynchronous.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		/// <param name="args">The arguments.</param>
		/// <returns></returns>
		public virtual async Task ExecuteAsync ( IPluginHost pluginHost, IO.LinuxDirectoryInfo currentDirectory, string[] args ) {
			await Task.Run ( ( ) => { Execute ( pluginHost, currentDirectory, args ); } );
		}

		/// <summary>
		/// Executes the asynchronous.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="args">The arguments.</param>
		/// <returns></returns>
		public virtual async Task ExecuteAsync ( IPluginHost pluginHost, string[] args ) {
			await ExecuteAsync ( pluginHost, new IO.LinuxDirectoryInfo ( "/" ), args );
		}

		/// <summary>
		/// Initializes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		public virtual void Initialize ( IPluginHost pluginHost ) {
			this.LogDebug ( "Initializing Plugin: {0}", this.GetType ( ).Name );
			return;
		}

		//public virtual async Task InitializeAsync(IPluginHost pluginHost) {
		//	return;
		//}

		/// <summary>
		/// Gets or sets the plugin host.
		/// </summary>
		/// <value>
		/// The plugin host.
		/// </value>
		[Browsable ( false ), ReadOnly ( true )]
		public IPluginHost PluginHost { get; set; }

		/// <summary>
		/// Gets a value indicating whether this <see cref="IPlugin" /> is runnable.
		/// </summary>
		/// <value>
		///   <c>true</c> if runnable; otherwise, <c>false</c>.
		/// </value>
		public virtual bool Runnable {
			get { return true; }
		}

		/// <summary>
		/// Creates the tool strip menu item.
		/// </summary>
		/// <returns></returns>
		public virtual System.Windows.Forms.ToolStripItem CreateToolStripMenuItem ( ) {
			return PluginHelper.CreateToolStripMenuItemForPlugin ( this );
		}

		/// <summary>
		/// Creates the tool strip button.
		/// </summary>
		/// <returns></returns>
		public virtual System.Windows.Forms.ToolStripItem CreateToolStripButton ( ) {
			return PluginHelper.CreateToolStripButtonForPlugin ( this );
		}

		/// <summary>
		/// Indicates the minimum SDK Tools Version that is required for this plugin. If no requirement, then default the value to 0.
		/// </summary>
		public virtual int MinimumSDKToolsVersion {
			get { return 0; }
		}

		/// <summary>
		/// Indicates the minimum SDK Platform Tools Version that is required for this plugin. If no requirement, then default the value to 0.
		/// </summary>
		public virtual int MinimumSDKPlatformToolsVersion {
			get { return 0; }
		}

		/// <summary>
		/// Gets a value indicating whether [requires root].
		/// </summary>
		/// <value>
		///   <c>true</c> if [requires root]; otherwise, <c>false</c>.
		/// </value>
		public virtual bool RequiresRoot {
			get { return false; }
		}

		/// <summary>
		/// Gets the id of the plugin. Used to identify the plugin for launch.
		/// </summary>
		/// <value>
		/// The id of the plugin.
		/// </value>
		public virtual string Id {
			get {
				var t = this.GetType ( );
				return String.Format ( "{0},{1}", t.FullName, t.Assembly.GetName ( ).Name );
			}
		}

		/// <summary>
		/// Gets the configuration.
		/// </summary>
		/// <value>
		/// The configuration.
		/// </value>
		[TypeConverter ( typeof ( ExpandableObjectConverter ) )]
		[Browsable ( false )]
		public virtual IPluginSettings Configuration { get; protected set; }

		#endregion

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString ( ) {
			return this.Name;
		}
	}
}
