using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Core.Plugins;
using System.Windows.Forms;
using DroidExplorer.Core.IO;
using DroidExplorer.Core;
using ICSharpCode.SharpZipLib.Zip;
using System.Globalization;
using System.ComponentModel;

namespace DroidExplorer.Plugins {
	/// <summary>
	/// 
	/// </summary>
	/// <seealso cref="DroidExplorer.Core.Plugins.BasePlugin" />
	public class ApplicationBackup : BasePlugin {

		/// <summary>
		/// Initializes a new instance of the <see cref="ApplicationBackup"/> class.
		/// </summary>
		/// <param name="host">The host.</param>
		public ApplicationBackup ( IPluginHost host ) : base(host) {

		}

		#region IPlugin Members
		/// <summary>
		/// Gets the group.
		/// </summary>
		/// <value>
		/// The group.
		/// </value>
		public override string Group { get { return "Applications and Data"; } }

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public override string Name {
      get { return "ApplicationBackup"; }
    }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public override string Description {
      get { return "Backup device applications to local computer"; }
    }

		/// <summary>
		/// Gets the image.
		/// </summary>
		/// <value>
		/// The image.
		/// </value>
		public override System.Drawing.Image Image {
      get { return Properties.Resources.backup; }
    }

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		public override string Text {
      get { return "Application Backup"; }
    }

		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		/// <param name="args">The arguments.</param>
		public override void Execute ( IPluginHost pluginHost, DroidExplorer.Core.IO.LinuxDirectoryInfo currentDirectory, string[] args ) {
      string backupPath = System.IO.Path.Combine ( FolderManagement.TempFolder, "Backup" );
			System.IO.DirectoryInfo appdir = pluginHost.CommandRunner.PullDirectory ( CommandRunner.APP_PUBLIC_PATH, backupPath );
			System.IO.DirectoryInfo appdir2 = pluginHost.CommandRunner.PullDirectory ( CommandRunner.APP_SD_PUBLIC_PATH, backupPath );
			System.IO.DirectoryInfo pappdir = pluginHost.CommandRunner.PullDirectory ( CommandRunner.APP_PRIVATE_PATH, backupPath );
			System.IO.DirectoryInfo pappdir2 = pluginHost.CommandRunner.PullDirectory ( CommandRunner.APP_SD_PRIVATE_PATH, backupPath );

			FastZip zip = new FastZip ( );
      string path = System.IO.Path.Combine ( System.IO.Path.GetDirectoryName ( this.GetType ( ).Assembly.Location ), "AppBackups" );

      if ( !System.IO.Directory.Exists ( path ) ) {
        System.IO.Directory.CreateDirectory ( path );
      }

      zip.CreateZip ( System.IO.Path.Combine ( path, string.Format ( CultureInfo.InvariantCulture, "AB{0}.zip", DateTime.Now.ToString ( "yyyyMMdd", CultureInfo.InvariantCulture ) ) ), backupPath, true, string.Empty );


    }
		/// <summary>
		/// Gets a value indicating whether this <see cref="T:DroidExplorer.Core.Plugins.IPlugin" /> is runnable.
		/// </summary>
		/// <value>
		/// <c>true</c> if runnable; otherwise, <c>false</c>.
		/// </value>
		public override bool Runnable { get { return false; } }

		/// <summary>
		/// Gets a value indicating whether [create tool button].
		/// </summary>
		/// <value>
		/// <c>true</c> if [create tool button]; otherwise, <c>false</c>.
		/// </value>
		public override bool CreateToolButton {
      get { return false; }
    }


    #endregion

    #region IPluginExtendedInfo Members

		public override string Author {
      get { return "Ryan Conrad"; }
    }

		public override string Url {
			get { return DroidExplorer.Resources.Strings.ApplicationWebsiteUrl; }
    }

		public override string Contact {
      get { return string.Empty; }
    }

    #endregion


	}
}
