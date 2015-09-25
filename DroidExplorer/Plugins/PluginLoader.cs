using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Globalization;
using DroidExplorer.Core.Plugins;
using System.Drawing;
using DroidExplorer.Core;
using DroidExplorer.Configuration;
using Camalot.Common.Extensions;

namespace DroidExplorer.Plugins {
	public static class PluginLoader {

		public static DroidExplorer.Core.IO.LinuxDirectoryInfo CurrentPath { get; set; }
		public static IPluginHost PluginHost { get; private set; }
		public static void Load ( IPluginHost host, ToolStripPanel panel, ToolStripMenuItem pluginMenu ) {

			PluginHost = host;
			Program.LoadedPlugins.Clear ( );
			var asm = typeof ( PluginLoader ).Assembly;

			var plugins = Settings.Instance.PluginSettings.GetPlugins ( host );
			var rows = panel.Rows.Length;
			foreach ( IPlugin plugin in plugins ) {
				var pluginInfo = Settings.Instance.PluginSettings.GetPluginInfo ( plugin );
				if ( pluginInfo.Enabled ) {
					pluginInfo.LogDebug ( "Loading: {0}", pluginInfo.Name );
					Program.LoadedPlugins.Add ( plugin );

					plugin.Initialize ( host );

					if ( !plugin.CreateToolButton ) {
						continue;
					}
					var tsb = plugin.CreateToolStripButton ( );
					var tsm = plugin.CreateToolStripMenuItem ( );
					var group = plugin.Group.Or ( "[DEFAULT]" );

					var added = false;
					foreach ( var c in panel.Controls ) {
						if ( c.Is<ToolStrip> ( ) && string.Compare ( ( (ToolStrip)c ).Name, group, true ) == 0 ) {
							( (ToolStrip)c ).Items.Add ( tsb );
							added = true;
							break;
						}
					}
					if ( !added ) {
						// need to create the toolstrip and add the item
						var strip = new ToolStrip ( tsb ) {
							Name = group,
						};
						panel.Join ( strip, rows );
					}

					// menu items
					var parentMenu = default ( ToolStripMenuItem );
					foreach ( ToolStripMenuItem mi in pluginMenu.DropDownItems ) {
						if ( mi.Text == group ) {
							parentMenu = (ToolStripMenuItem)mi;
						}
					}
					if ( parentMenu == null ) {
						parentMenu = new ToolStripMenuItem ( group );
						pluginMenu.DropDownItems.Add ( parentMenu );
					}

					parentMenu.DropDownItems.Add ( tsm );


					if ( pluginInfo.ExecuteOnLoad && pluginInfo.Enabled ) {
						plugin.Execute ( host );
					}
				}
			}
		}
	}
}
