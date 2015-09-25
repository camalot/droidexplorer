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
  public class ApplicationBackup : BasePlugin {
    
    public ApplicationBackup ( IPluginHost host ) : base(host) {

		}

    #region IPlugin Members
		public override string Group { get { return "Applications and Data"; } }

		public override string Name {
      get { return "ApplicationBackup"; }
    }

		public override string Description {
      get { return "Backup device applications to local computer"; }
    }

		public override System.Drawing.Image Image {
      get { return Properties.Resources.backup; }
    }

		public override string Text {
      get { return "Application Backup"; }
    }

		public override void Execute ( IPluginHost pluginHost, DroidExplorer.Core.IO.LinuxDirectoryInfo currentDirectory, string[] args ) {
      string backupPath = System.IO.Path.Combine ( FolderManagement.TempFolder, "Backup" );
			System.IO.DirectoryInfo appdir = pluginHost.CommandRunner.PullDirectory ( CommandRunner.APP_PUBLIC_PATH, backupPath );
			System.IO.DirectoryInfo appdir2 = pluginHost.CommandRunner.PullDirectory ( CommandRunner.APP_SD_PUBLIC_PATH, backupPath );
			System.IO.DirectoryInfo pappdir = pluginHost.CommandRunner.PullDirectory ( CommandRunner.APP_PRIVATE_PATH, backupPath );
			System.IO.DirectoryInfo pappdir2 = pluginHost.CommandRunner.PullDirectory ( CommandRunner.APP_SD_PRIVATE_PATH, backupPath );

			FastZip zip = new FastZip ( );
      string path = Path.Combine ( System.IO.Path.GetDirectoryName ( this.GetType ( ).Assembly.Location ), "AppBackups" );

      if ( !System.IO.Directory.Exists ( path ) ) {
        System.IO.Directory.CreateDirectory ( path );
      }

      zip.CreateZip ( System.IO.Path.Combine ( path, string.Format ( CultureInfo.InvariantCulture, "AB{0}.zip", DateTime.Now.ToString ( "yyyyMMdd", CultureInfo.InvariantCulture ) ) ), backupPath, true, string.Empty );


    }
		public override bool Runnable { get { return false; } }

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
