using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Core.Plugins;
using System.Windows.Forms;
using DroidExplorer.Core.UI;
using DroidExplorer.Core;
using System.IO;
using System.ComponentModel;

namespace DroidExplorer.Plugins {
	/// <summary>
	/// 
	/// </summary>
	public class Installer : BasePlugin {

		
		/// <summary>
		/// Initializes a new instance of the <see cref="Installer" /> class.
		/// </summary>
		/// <param name="host">The host.</param>
		public Installer ( IPluginHost host ) : base(host){
			this.PluginHost = host;
      this.PluginHost.CommandRunner.DeviceStateChanged += new EventHandler<DeviceEventArgs> ( CommandRunner_DeviceStateChanged );
    }

		/// <summary>
		/// Handles the DeviceStateChanged event of the CommandRunner control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="DeviceEventArgs" /> instance containing the event data.</param>
    void CommandRunner_DeviceStateChanged ( object sender, DeviceEventArgs e ) {
      if ( string.Compare ( e.Device, this.PluginHost.CommandRunner.DefaultDevice, true ) == 0 ) {
        if ( ToolStripMenuItem != null ) {
          ToolStripMenuItem.Enabled = e.State != CommandRunner.DeviceState.Offline && e.State != CommandRunner.DeviceState.Unknown;
        }
        if ( ToolStripButton != null ) {
          ToolStripButton.Enabled = e.State == CommandRunner.DeviceState.Offline && e.State != CommandRunner.DeviceState.Unknown;
        }
      }
    }

		#region IPlugin Members

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public override string Name {
			get { return "APKInstaller"; }
		}
		public override string Group { get { return "Application"; } }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public override string Description {
			get { return "Installs an APK from the local machine to your android device"; }
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
			get { return "APK Installer"; }
		}

		/// <summary>
		/// Gets a value indicating whether [create tool button].
		/// </summary>
		/// <value>
		///   <c>true</c> if [create tool button]; otherwise, <c>false</c>.
		/// </value>
		public override bool CreateToolButton {
			get { return false; }
		}




		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		/// <param name="args">The args.</param>
		public override void Execute ( IPluginHost pluginHost, DroidExplorer.Core.IO.LinuxDirectoryInfo currentDirectory, string[] args ) {
			Arguments arguments = new Arguments ( args ?? new string[] { "/install" } );
			string apkFile = string.Empty;
			var apkArg = arguments.Contains ( "apk" ) ? arguments["apk"] : string.Empty;

			// check that we have a file, and that it exists.
			if ( !string.IsNullOrWhiteSpace(apkArg) && System.IO.File.Exists(apkArg) ) {
				apkFile = apkArg;
			} else {
				System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog ( );
				ofd.Title = "Select Android Application";
				ofd.Filter = "Android Applications|*.apk";
				ofd.CheckFileExists = true;
				ofd.Multiselect = false;
				if ( ofd.ShowDialog ( ) == DialogResult.OK ) {
					apkFile = ofd.FileName;
				} else {
					return;
				}

			}

			if ( File.Exists ( apkFile ) ) {
				try {
					var apkInfo = this.PluginHost.CommandRunner.GetLocalApkInformation ( apkFile );
					var id = new InstallDialog ( this.PluginHost, arguments.Contains( "uninstall" ) ? InstallDialog.InstallMode.Uninstall : InstallDialog.InstallMode.Install, apkInfo );
					if ( pluginHost != null && pluginHost.GetHostWindow() != null ) { 
						id.Show ( );
					} else {
						id.ShowInTaskbar = true;
						id.ShowDialog ( );
					}
				} catch ( Exception ex ) {
					MessageBox.Show ( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1 );
					this.LogError ( ex.Message, ex );
				}
			}
		}


		/// <summary>
		/// Creates the tool strip menu item.
		/// </summary>
		/// <returns></returns>
		public override ToolStripItem CreateToolStripMenuItem ( ) {
      ToolStripMenuItem = PluginHelper.CreateToolStripMenuItemForPlugin ( this );
      CommandRunner_DeviceStateChanged ( this, new DeviceEventArgs ( this.PluginHost.CommandRunner.DefaultDevice, this.PluginHost.CommandRunner.State ) );
      return ToolStripMenuItem;
    }

		/// <summary>
		/// Creates the tool strip button.
		/// </summary>
		/// <returns></returns>
		public override ToolStripItem CreateToolStripButton ( ) {
      ToolStripButton = PluginHelper.CreateToolStripButtonForPlugin ( this );
      CommandRunner_DeviceStateChanged ( this, new DeviceEventArgs ( this.PluginHost.CommandRunner.DefaultDevice, this.PluginHost.CommandRunner.State ) );
      return ToolStripButton;
    }


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

		/// <summary>
		/// Gets or sets the tool strip menu item.
		/// </summary>
		/// <value>
		/// The tool strip menu item.
		/// </value>
    private ToolStripMenuItem ToolStripMenuItem { get; set; }
		/// <summary>
		/// Gets or sets the tool strip button.
		/// </summary>
		/// <value>
		/// The tool strip button.
		/// </value>
    private ToolStripButton ToolStripButton { get; set; }

	}
}
