using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Core.Plugins;
using System.ComponentModel;
using System.Windows.Forms;
using DroidExplorer.Core;

namespace DroidExplorer.Plugins {
	/// <summary>
	/// 
	/// </summary>
  public class FlashRecovery : BasePlugin {

		/// <summary>
		/// Initializes a new instance of the <see cref="FlashRecovery" /> class.
		/// </summary>
		/// <param name="host">The host.</param>
    public FlashRecovery ( IPluginHost host ) : base(host) {
      this.PluginHost = host;

      CommandRunner.Instance.DeviceStateChanged += new EventHandler<DeviceEventArgs> ( CommandRunner_DeviceStateChanged );
    }

		/// <summary>
		/// Handles the DeviceStateChanged event of the CommandRunner control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="DeviceEventArgs" /> instance containing the event data.</param>
    void CommandRunner_DeviceStateChanged ( object sender, DeviceEventArgs e ) {
      bool enabled =  e.State != CommandRunner.DeviceState.Offline && e.State != CommandRunner.DeviceState.Unknown;
      if ( string.Compare ( e.Device, CommandRunner.Instance.DefaultDevice, true ) == 0 ) {
        if ( ToolStripMenuItem != null ) {
          ToolStripMenuItem.SetEnabled ( enabled );
        }
        if ( ToolStripButton != null ) {
          ToolStripButton.SetEnabled ( enabled );
        }
      }
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
      get { return "FlashRecovery"; }
    }
		/// <summary>
		/// Gets the group.
		/// </summary>
		/// <value>
		/// The group.
		/// </value>
		public override string Group { get { return "Tools"; } }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public override string Description {
      get { return "Flash a new recovery image to the device"; }
    }

		/// <summary>
		/// Gets the image.
		/// </summary>
		/// <value>
		/// The image.
		/// </value>
		public override System.Drawing.Image Image {
      get { return Properties.Resources.flash_icon; }
    }

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		public override string Text {
      get { return "Flash Recovery Image"; }
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
      Arguments arguments = new Arguments ( args ?? new string[ ] { } );
      string file = string.Empty;
      if ( arguments.Contains ( "file" ) ) {
        file = arguments[ "file" ];
      }

      if ( string.IsNullOrEmpty ( file ) ) {
        DroidExplorer.Core.UI.OpenFileDialog ofd = new DroidExplorer.Core.UI.OpenFileDialog ( );
        ofd.Title = "Select Recovery Image";
        ofd.Filter = "Recovery Image|*.img|All Files (*.*)|*.*";
        ofd.FilterIndex = 0;
        if ( ofd.ShowDialog ( ) == DialogResult.OK ) {
          file = ofd.FileName;
        }
      }

      if ( string.IsNullOrEmpty ( file ) ) {
        return;
      } else {
        CommandRunner.Instance.FlashImage ( file );
        if ( PluginHost != null ) {
          int result = PluginHost.ShowCommandBox ( "Reboot Now?", "Recovery image has been flashed to the device.", string.Empty, string.Empty, string.Empty, string.Empty,
            "Reboot Device|Reboot Device in Recovery mode|Do not reboot device", false, MessageBoxIcon.Question, MessageBoxIcon.None );

          switch ( result ) {
            case 0: // Reboot
              CommandRunner.Instance.Reboot ( );
              break;
            case 1: // reboot recovery
              CommandRunner.Instance.RebootRecovery ( );
              break;
          }
        }
      }
    }

		/// <summary>
		/// Gets a value indicating whether this <see cref="FlashRecovery" /> is runnable.
		/// </summary>
		/// <value>
		///   <c>true</c> if runnable; otherwise, <c>false</c>.
		/// </value>
		public override bool Runnable {
      get { return false; }
    }

		/// <summary>
		/// Creates the tool strip menu item.
		/// </summary>
		/// <returns></returns>
		public override ToolStripItem CreateToolStripMenuItem ( ) {
      ToolStripMenuItem = PluginHelper.CreateToolStripMenuItemForPlugin ( this );
      CommandRunner_DeviceStateChanged ( this, new DeviceEventArgs ( CommandRunner.Instance.DefaultDevice, CommandRunner.Instance.State ) );
      return ToolStripMenuItem;
    }

		/// <summary>
		/// Creates the tool strip button.
		/// </summary>
		/// <returns></returns>
		public override ToolStripItem CreateToolStripButton ( ) {
      ToolStripButton = PluginHelper.CreateToolStripButtonForPlugin ( this );
      CommandRunner_DeviceStateChanged ( this, new DeviceEventArgs ( CommandRunner.Instance.DefaultDevice, CommandRunner.Instance.State ) );
      return ToolStripButton;
    }

		/// <summary>
		/// Gets a value indicating whether [requires root].
		/// </summary>
		/// <value>
		/// <c>true</c> if [requires root]; otherwise, <c>false</c>.
		/// </value>
		public override bool RequiresRoot {
			get {
				return true;
			}
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
