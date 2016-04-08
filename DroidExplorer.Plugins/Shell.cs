using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Core.Plugins;
using System.Globalization;
using DroidExplorer.Plugins.UI;
using DroidExplorer.Core;
using System.Windows.Forms;
using System.ComponentModel;
using DroidExplorer.Plugins.Configuration;
using Managed.Adb.IO;

namespace DroidExplorer.Plugins {
	/// <summary>
	/// 
	/// </summary>
	public class Shell : BasePlugin, IFileTypeHandler {

		/// <summary>
		/// Initializes a new instance of the <see cref="Shell"/> class.
		/// </summary>
		/// <param name="host">The host.</param>
		public Shell ( IPluginHost host ) : base(host) {

			if ( host != null ) {
				this.PluginHost.RegisterFileTypeIcon ( ".sh", DroidExplorer.Resources.Images.shellscript_16xLG, DroidExplorer.Resources.Images.shellscript_32xLG );
				this.PluginHost.RegisterFileTypeHandler ( ".sh", this );
			}

			this.Configuration = new ShellConfiguration();
		}

		#region IPluginExtendedInfo Members

		/// <summary>
		/// Gets the author.
		/// </summary>
		/// <value>The author.</value>
		public override string Author {
			get { return "Ryan Conrad"; }
		}

		/// <summary>
		/// Gets the URL.
		/// </summary>
		/// <value>The URL.</value>
		public override string Url {
			get { return DroidExplorer.Resources.Strings.ApplicationWebsiteUrl; }
		}

		/// <summary>
		/// Gets the contact.
		/// </summary>
		/// <value>The contact.</value>
		public override string Contact {
			get { return string.Empty; }
		}

		#endregion

		#region IPlugin Members

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public override string Name {
			get { return "Console"; }
		}
		public override string Group { get { return "Tools"; } }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>The description.</value>
		public override string Description {
			get { return "Console Shell Window"; }
		}

		/// <summary>
		/// Gets the image.
		/// </summary>
		/// <value>The image.</value>
		public override System.Drawing.Image Image {
			get { return DroidExplorer.Resources.Images.shell; }
		}

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>The text.</value>
		public override string Text {
			get { return "Console"; }
		}

		/// <summary>
		/// Gets a value indicating whether [create tool button].
		/// </summary>
		/// <value><c>true</c> if [create tool button]; otherwise, <c>false</c>.</value>
		public override bool CreateToolButton {
			get { return true; }
		}



		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		/// <param name="args">The args.</param>
		public override void Execute ( IPluginHost pluginHost, DroidExplorer.Core.IO.LinuxDirectoryInfo currentDirectory, string[] args ) {
			Arguments arguments = new Arguments ( args ?? new string[] { } );
			string file = string.Empty;
			if ( arguments.Contains( "file" ) ) {
				file = arguments[ "file" ];
			}
			ShellConsole console = new ShellConsole ( pluginHost );
			console.Top = Screen.PrimaryScreen.WorkingArea.Top;
			console.Left = Screen.PrimaryScreen.WorkingArea.Left;

			if ( !string.IsNullOrEmpty ( file ) ) {
				console.Run ( file );
			}
			if(pluginHost != null && pluginHost.GetHostWindow() != null) {
				console.Show();
			} else {
				console.ShowInTaskbar = true;
				console.ShowDialog();
			}
		}

		#endregion

		#region IFileTypeHandler Members

		/// <summary>
		/// Opens the specified file.
		/// </summary>
		/// <param name="file">The file.</param>
		public void Open ( DroidExplorer.Core.IO.FileInfo file ) {
			Execute ( this.PluginHost, new DroidExplorer.Core.IO.LinuxDirectoryInfo ( LinuxPath.GetPathWithoutFile ( file.FullPath ) ), new string[ ] { string.Format ( CultureInfo.InvariantCulture, "/file=sh {0}", file.FullPath ) } );
			//CommandRunner.Instance.LaunchShellWindow ( CommandRunner.Instance.DefaultDevice, string.Format ( CultureInfo.InvariantCulture, "sh {0}", file.FullPath ) );
		}

		#endregion
	}
}
