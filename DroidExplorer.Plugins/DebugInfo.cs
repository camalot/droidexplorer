using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Core.Plugins;
using DroidExplorer.UI;
using System.Windows.Forms;
using DroidExplorer.Plugins.UI;
using System.ComponentModel;

namespace DroidExplorer.Plugins {
	/// <summary>
	/// 
	/// </summary>
  public class DebugInfo : BasePlugin {

		/// <summary>
		/// Initializes a new instance of the <see cref="DebugInfo" /> class.
		/// </summary>
		/// <param name="host">The host.</param>
    public DebugInfo ( IPluginHost host ) : base(host){
    }

    #region IPlugin Members

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public override string Name {
      get { return "Debug Console"; }
    }
		public override string Group { get { return "Tools"; } }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public override string Description {
      get { return "Debug Console Window"; }
    }

		/// <summary>6
		/// Gets the image.
		/// </summary>
		/// <value>
		/// The image.
		/// </value>
		public override System.Drawing.Image Image {
      get { return Resources.Images.bug; }
    }

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		public override string Text {
      get { return "Debug Console"; }
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
    private DebugConsole _console = null;
		/// <summary>
		/// Gets the console window.
		/// </summary>
		/// <value>
		/// The console window.
		/// </value>
    internal DebugConsole ConsoleWindow {
      get {
        if ( _console == null || _console.IsDisposed ) {
          _console = new DebugConsole ( this.PluginHost );
        }
        return _console;
      }
    }



		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		/// <param name="args">The args.</param>
		public override void Execute ( IPluginHost pluginHost, DroidExplorer.Core.IO.LinuxDirectoryInfo currentDirectory, string[] args ) {
			ConsoleWindow.StartPosition = FormStartPosition.Manual;
			if ( pluginHost != null && pluginHost.GetHostWindow() != null ) {
        int h = Screen.FromControl ( this.PluginHost.GetHostControl ( ) ).Bounds.Bottom - this.PluginHost.Bottom;
        if ( h > 100 ) {
          ConsoleWindow.Top = this.PluginHost.Bottom;
          ConsoleWindow.Left = this.PluginHost.Left;
          ConsoleWindow.Width = this.PluginHost.Width;
          ConsoleWindow.Height = Screen.FromControl ( this.PluginHost.GetHostControl ( ) ).WorkingArea.Bottom - this.PluginHost.Bottom;
        } else {
          ConsoleWindow.Top = Screen.PrimaryScreen.WorkingArea.Top;
        }
				if ( !this.ConsoleWindow.Visible ) {
					ConsoleWindow.Show ( pluginHost.GetHostWindow ( ) );
				}
			} else {
				ConsoleWindow.ShowDialog ( );
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
