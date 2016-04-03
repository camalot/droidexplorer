using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DroidExplorer.Core.IO;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace DroidExplorer.Core.Plugins {
  public interface IPlugin {
		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		string Name { get; }
		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		string Description { get; }

		/// <summary>
		/// Gets a value indicating whether this instance has configuration.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance has configuration; otherwise, <c>false</c>.
		/// </value>
		bool HasConfiguration { get; }
		/// <summary>
		/// Gets the image.
		/// </summary>
		/// <value>
		/// The image.
		/// </value>
		Image Image { get; }
		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		string Text { get; }
		/// <summary>
		/// Gets a value indicating whether [create tool button].
		/// </summary>
		/// <value>
		///   <c>true</c> if [create tool button]; otherwise, <c>false</c>.
		/// </value>
		bool CreateToolButton { get; }

		/// <summary>
		/// Gets the group.
		/// </summary>
		/// <value>
		/// The group.
		/// </value>
		string Group { get; }

		/// <summary>
		/// Gets the display style.
		/// </summary>
		/// <value>
		/// The display style.
		/// </value>
		System.Windows.Forms.ToolStripItemDisplayStyle DisplayStyle { get; }

		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		void Execute ( IPluginHost pluginHost, LinuxDirectoryInfo currentDirectory );
		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		void Execute ( IPluginHost pluginHost );
		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		/// <param name="args">The arguments.</param>
		void Execute ( IPluginHost pluginHost, LinuxDirectoryInfo currentDirectory, string[] args );
		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="args">The arguments.</param>
		void Execute ( IPluginHost pluginHost, string[] args );

		/// <summary>
		/// Executes the asynchronous.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		/// <returns></returns>
		Task ExecuteAsync (IPluginHost pluginHost, LinuxDirectoryInfo currentDirectory);
		/// <summary>
		/// Executes the asynchronous.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <returns></returns>
		Task ExecuteAsync (IPluginHost pluginHost);
		/// <summary>
		/// Executes the asynchronous.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		/// <param name="args">The arguments.</param>
		/// <returns></returns>
		Task ExecuteAsync (IPluginHost pluginHost, LinuxDirectoryInfo currentDirectory, string[] args);
		/// <summary>
		/// Executes the asynchronous.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="args">The arguments.</param>
		/// <returns></returns>
		Task ExecuteAsync (IPluginHost pluginHost, string[] args);

		/// <summary>
		/// Initializes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		void Initialize ( IPluginHost pluginHost );
		//Task InitializeAsync ( IPluginHost pluginHost );

		/// <summary>
		/// Gets or sets the plugin host.
		/// </summary>
		/// <value>
		/// The plugin host.
		/// </value>
		[Browsable (false), ReadOnly(true)]
		IPluginHost PluginHost { get; set; }
		/// <summary>
		/// Gets a value indicating whether this <see cref="IPlugin"/> is runnable.
		/// </summary>
		/// <value>
		///   <c>true</c> if runnable; otherwise, <c>false</c>.
		/// </value>
		bool Runnable { get; }

		/// <summary>
		/// Creates the tool strip menu item.
		/// </summary>
		/// <returns></returns>
		ToolStripItem CreateToolStripMenuItem ( );
		/// <summary>
		/// Creates the tool strip button.
		/// </summary>
		/// <returns></returns>
		ToolStripItem CreateToolStripButton ( );

		/// <summary>
		/// Indicates the minimum SDK Tools Version that is required for this plugin. If no requirement, then default the value to 0.
		/// </summary>
		int MinimumSDKToolsVersion { get; }
		/// <summary>
		/// Indicates the minimum SDK Platform Tools Version that is required for this plugin. If no requirement, then default the value to 0.
		/// </summary>
		int MinimumSDKPlatformToolsVersion { get; }

		/// <summary>
		/// Gets a value indicating whether [requires root].
		/// </summary>
		/// <value>
		///   <c>true</c> if [requires root]; otherwise, <c>false</c>.
		/// </value>
		bool RequiresRoot { get; }

		/// <summary>
		/// Gets the id of the plugin. Used to identify the plugin for launch.
		/// </summary>
		/// <value>
		/// The id of the plugin.
		/// </value>
		String Id { get; }

		/// <summary>
		/// Gets the configuration.
		/// </summary>
		/// <value>
		/// The configuration.
		/// </value>
		IPluginSettings Configuration { get; }
	}
}
