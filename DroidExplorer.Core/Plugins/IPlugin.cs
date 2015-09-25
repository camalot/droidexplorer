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
    string Name { get; }
    string Description { get; }

    bool HasConfiguration { get; }
    Image Image { get; }
    string Text { get; }
    bool CreateToolButton { get; }

		string Group { get; }

    System.Windows.Forms.ToolStripItemDisplayStyle DisplayStyle { get; }

    void Execute ( IPluginHost pluginHost, LinuxDirectoryInfo currentDirectory );
    void Execute ( IPluginHost pluginHost );
		void Execute ( IPluginHost pluginHost, LinuxDirectoryInfo currentDirectory, string[] args );
		void Execute ( IPluginHost pluginHost, string[] args );

		Task ExecuteAsync(IPluginHost pluginHost, LinuxDirectoryInfo currentDirectory);
		Task ExecuteAsync(IPluginHost pluginHost);
		Task ExecuteAsync(IPluginHost pluginHost, LinuxDirectoryInfo currentDirectory, string[] args);
		Task ExecuteAsync(IPluginHost pluginHost, string[] args);

		void Initialize ( IPluginHost pluginHost );
		//Task InitializeAsync ( IPluginHost pluginHost );

		[Browsable(false), ReadOnly(true)]
		IPluginHost PluginHost { get; set; }
		bool Runnable { get; }

    ToolStripItem CreateToolStripMenuItem ( );
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
		/// Gets the id of the plugin. Used to identify the plugin for launch.
		/// </summary>
		/// <value>
		/// The id of the plugin.
		/// </value>
		String Id { get; }

		IPluginSettings Configuration { get; }
	}
}
