using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DroidExplorer.Core.IO;
using DroidExplorer.Core.Plugins;
using DroidExplorer.Plugins.UI;

namespace DroidExplorer.Plugins {
	public class PortManager : BasePlugin {

		public PortManager ( IPluginHost host ) : base( host ) {
		}
		#region IPlugin Members

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public override string Name {
			get { return "Port Manager"; }
		}
		public override string Group { get { return "Tools"; } }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public override string Description {
			get { return "Manage forwarded ports on your device"; }
		}

		/// <summary>6
		/// Gets the image.
		/// </summary>
		/// <value>
		/// The image.
		/// </value>
		public override System.Drawing.Image Image {
			get { return Resources.Images.portmanager_16; }
		}

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		public override string Text {
			get { return "Port Manager"; }
		}

		/// <summary>
		/// Gets a value indicating whether [create tool button].
		/// </summary>
		/// <value>
		///   <c>true</c> if [create tool button]; otherwise, <c>false</c>.
		/// </value>
		public override bool CreateToolButton {
			get { return true; }
		}

		private PortManagerForm PortManagerWindow { get; set; }

		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		/// <param name="args">The args.</param>
		public override void Execute ( IPluginHost pluginHost, DroidExplorer.Core.IO.LinuxDirectoryInfo currentDirectory, string[] args ) {
			if(PortManagerWindow == null ) {
				PortManagerWindow = new PortManagerForm ( pluginHost );
			}

			if ( pluginHost != null && pluginHost.GetHostWindow ( ) != null ) {
				if ( !this.PortManagerWindow.Visible ) {
					PortManagerWindow.Show ( pluginHost.GetHostWindow ( ) );
				}
			} else {
				PortManagerWindow.ShowDialog ( );
			};

		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="DebugInfo" /> is runnable.
		/// </summary>
		/// <value>
		///   <c>true</c> if runnable; otherwise, <c>false</c>.
		/// </value>
		public override bool Runnable { get { return false; } }

		#endregion

		#region IPluginExtendedInfo Members

		/// <summary>
		/// Gets the author.
		/// </summary>
		/// <value>
		/// The author.
		/// </value>
		public override string Author {
			get { return "Ryan Conrad"; }
		}

		/// <summary>
		/// Gets the URL.
		/// </summary>
		/// <value>
		/// The URL.
		/// </value>
		public override string Url {
			get { return DroidExplorer.Resources.Strings.ApplicationWebsiteUrl; }
		}

		/// <summary>
		/// Gets the contact.
		/// </summary>
		/// <value>
		/// The contact.
		/// </value>
		public override string Contact {
			get { return string.Empty; }
		}

		#endregion

	}
}
