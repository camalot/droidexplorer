using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Core.Plugins;
using DroidExplorer.Plugins.UI;
using System.ComponentModel;
using System.Windows.Forms;
using DroidExplorer.Core;

namespace DroidExplorer.Plugins {
	public class BatchInstaller : BasePlugin {
		public BatchInstaller ( IPluginHost host ) : base(host) {

		}

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

		#region IPlugin Members

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public override string Name {
			get { return "BatchInstaller"; }
		}

		public override string Group { get { return "Applications and Data"; } }


		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public override string Description {
			get { return "Install multiple APK's at once"; }
		}

		/// <summary>
		/// Gets the image.
		/// </summary>
		/// <value>
		/// The image.
		/// </value>
		public override System.Drawing.Image Image {
			get { return DroidExplorer.Resources.Images.apk16; }
		}

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		public override string Text {
			get { return "Batch Installer"; }
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


		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		/// <param name="args">The args.</param>
		public override void Execute ( IPluginHost pluginHost, DroidExplorer.Core.IO.LinuxDirectoryInfo currentDirectory, string[] args ) {
			Arguments arguments = new Arguments ( args ?? new string[] { "/install" } );
			string[] apkFiles = new string[] { };

			if ( arguments.Contains ( "apk" ) ) {
				apkFiles = arguments["apk"].Split ( new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries );
			} else {
				System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog ( );
				ofd.Title = "Select Android Applications";
				ofd.Filter = "Android Applications|*.apk";
				ofd.Multiselect = true;
				if ( ofd.ShowDialog ( ) == System.Windows.Forms.DialogResult.OK ) {
					apkFiles = ofd.FileNames;
          new BatchInstallerDialog ( DroidExplorer.Core.UI.InstallDialog.InstallMode.Install, new List<string> ( apkFiles ) ).ShowDialog ( );
				} else {
					return;
				}
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="BatchInstaller" /> is runnable.
		/// </summary>
		/// <value>
		///   <c>true</c> if runnable; otherwise, <c>false</c>.
		/// </value>
		public override bool Runnable {
			get { return true; }
		}
		#endregion
	}
}
