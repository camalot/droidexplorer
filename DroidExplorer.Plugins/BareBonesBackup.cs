using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Core.Plugins;
using DroidExplorer.Core;
using System.Globalization;
using System.Resources;
using System.Reflection;
using ICSharpCode.SharpZipLib.Zip;
using DroidExplorer.Plugins.UI;
using System.ComponentModel;
using System.Windows.Forms;

namespace DroidExplorer.Plugins {
	/// <summary>
	/// 
	/// </summary>
  public class BareBonesBackup : BasePlugin {
		/// <summary>
		/// Initializes a new instance of the <see cref="BareBonesBackup"/> class.
		/// </summary>
		/// <param name="host">The host.</param>
		public BareBonesBackup ( IPluginHost host ) : base(host) {

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
      get { return "http://de.codeplex.com/"; }
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
      get { return "Google Applications Backup"; }
    }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>The description.</value>
		public override string Description {
      get { return "Backup files in to an update.zip"; }
    }

		/// <summary>
		/// Gets a value indicating whether this instance has configuration.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance has configuration; otherwise, <c>false</c>.
		/// </value>
		public override bool HasConfiguration {
      get { return true; }
    }

		/// <summary>
		/// Gets the image.
		/// </summary>
		/// <value>The image.</value>
		public override System.Drawing.Image Image {
      get { return Properties.Resources.appengine; }
    }

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>The text.</value>
		public override string Text {
      get { return "Google Applications Backup"; }
    }

		/// <summary>
		/// Gets a value indicating whether [create tool button].
		/// </summary>
		/// <value><c>true</c> if [create tool button]; otherwise, <c>false</c>.</value>
		public override bool CreateToolButton {
      get { return true; }
    }


    private BareBonesBackupForm _bbb = null;
		/// <summary>
		/// Gets the dialog.
		/// </summary>
		/// <value>The dialog.</value>
    internal BareBonesBackupForm Dialog {
      get {
        if ( _bbb == null || _bbb.IsDisposed ) {
          _bbb = new BareBonesBackupForm ( );
        }
        return _bbb;
      }
    }

		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		/// <param name="args">The args.</param>
		public override void Execute ( IPluginHost pluginHost, DroidExplorer.Core.IO.LinuxDirectoryInfo currentDirectory, string[] args ) {
      if ( pluginHost != null && pluginHost.GetHostWindow() != null ) {
        Dialog.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        Dialog.Show ( pluginHost.GetHostWindow ( ) );
      } else {
        Dialog.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Dialog.ShowDialog ( );
      }
    }

    #endregion
  }
}
