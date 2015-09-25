using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Core.Plugins;
using System.ComponentModel;
using System.Windows.Forms;
using DroidExplorer.Plugins.UI;

namespace DroidExplorer.Plugins {
	/// <summary>
	/// 
	/// </summary>
  public class LogCat : BasePlugin {
		/// <summary>
		/// Initializes a new instance of the <see cref="LogCat" /> class.
		/// </summary>
		/// <param name="host">The host.</param>
    public LogCat ( IPluginHost host ) : base(host) {

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
      get { return "LogCat"; }
    }
		public override string Group { get { return "SDK Tools"; } }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public override string Description {
      get { return "LogCat Console Window"; }
    }

		/// <summary>
		/// Gets a value indicating whether this instance has configuration.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance has configuration; otherwise, <c>false</c>.
		/// </value>
		public override bool HasConfiguration {
      get { return false; }
    }

		/// <summary>
		/// Gets the image.
		/// </summary>
		/// <value>
		/// The image.
		/// </value>
		public override System.Drawing.Image Image {
      get { return Resources.Images.PickAxe_16xLG; }
    }

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		public override string Text {
      get { return "LogCat Console"; }
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
      LogCatConsole console = new LogCatConsole ( pluginHost );
      console.Top = Screen.PrimaryScreen.WorkingArea.Top;
      console.Left = Screen.PrimaryScreen.WorkingArea.Left;
			if ( pluginHost.GetHostWindow ( ) == null ) {
				Application.Run ( console );
			} else {
				console.Show ( );
			}
    }

		public override bool Runnable {
      get { return false; }
    }
    #endregion
  }
}
