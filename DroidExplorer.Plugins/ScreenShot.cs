using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Core.Plugins;
using System.Drawing;
using System.Windows.Forms;
using DroidExplorer.Plugins.UI;
using DroidExplorer.Core;
using System.ComponentModel;

namespace DroidExplorer.Plugins {
	/// <summary>
	/// 
	/// </summary>
  public class ScreenShot : BasePlugin {

    ScreenShotForm screenShotForm = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="ScreenShot"/> class.
		/// </summary>
		/// <param name="host">The host.</param>
    public ScreenShot ( IPluginHost host ) : base(host) {
      CommandRunner.Instance.DeviceStateChanged += new EventHandler<DeviceEventArgs> ( CommandRunner_DeviceStateChanged );
    }

		/// <summary>
		/// Handles the DeviceStateChanged event of the CommandRunner control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="DroidExplorer.Core.DeviceEventArgs"/> instance containing the event data.</param>
    void CommandRunner_DeviceStateChanged ( object sender, DeviceEventArgs e ) {
      bool enabled = e.State != CommandRunner.DeviceState.Offline && e.State != CommandRunner.DeviceState.Unknown;
      if ( string.Compare ( e.Device, CommandRunner.Instance.DefaultDevice, true ) == 0 ) {
        if ( ToolStripMenuItem != null ) {
          ToolStripMenuItem.SetEnabled ( enabled );
        }
        if ( ToolStripButton != null ) {
          ToolStripButton.SetEnabled ( enabled );
        }
      }
    }

    #region IPlugin Members

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public override string Name {
      get { return "Screen Shot"; }
    }
		public override string Group { get { return "Tools"; } }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>The description.</value>
		public override string Description {
      get { return "Take screen shots of android device"; }
    }


		/// <summary>
		/// Gets the image.
		/// </summary>
		/// <value>The image.</value>
		public override System.Drawing.Image Image {
			get { return DroidExplorer.Resources.Images.screenshot; }
    }

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>The text.</value>
		public override string Text {
      get { return "Screen Shot"; }
    }


		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		public override void Execute ( IPluginHost pluginHost, DroidExplorer.Core.IO.LinuxDirectoryInfo currentDirectory, string[] args ) {
      if ( screenShotForm == null || screenShotForm.IsDisposed ) {
        screenShotForm = new ScreenShotForm ( pluginHost );
      }
      if ( pluginHost != null && pluginHost.GetHostWindow ( ) != null ) {
        Screen screen = Screen.FromControl ( pluginHost.GetHostControl ( ) );
        int r = screen.Bounds.Right - ( pluginHost.Left + pluginHost.Width );


        screenShotForm.StartPosition = FormStartPosition.Manual;
        if ( r > 100 ) {
          screenShotForm.Location = new Point ( pluginHost.Left + pluginHost.Width, pluginHost.Top );
        } else {
          screenShotForm.Top = Screen.PrimaryScreen.WorkingArea.Top;
          screenShotForm.Left = Screen.PrimaryScreen.WorkingArea.Left;
        }
        if ( !screenShotForm.Visible ) {
          screenShotForm.Show ( pluginHost.GetHostWindow ( ) );
        }
      } else {
				screenShotForm.Top = Screen.PrimaryScreen.WorkingArea.Top;
				screenShotForm.Left = Screen.PrimaryScreen.WorkingArea.Left;
				screenShotForm.ShowDialog ( );
      }
    }

		
		/// <summary>
		/// Gets a value indicating whether [create tool button].
		/// </summary>
		/// <value><c>true</c> if [create tool button]; otherwise, <c>false</c>.</value>
		public override bool CreateToolButton {
      get { return true; }
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

    #endregion

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

		/// <summary>
		/// Gets or sets the tool strip menu item.
		/// </summary>
		/// <value>The tool strip menu item.</value>
    private ToolStripMenuItem ToolStripMenuItem { get; set; }
		/// <summary>
		/// Gets or sets the tool strip button.
		/// </summary>
		/// <value>The tool strip button.</value>
    private ToolStripButton ToolStripButton { get; set; }

  }
}
