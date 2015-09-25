using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Core.Plugins;

namespace DroidExplorer.Plugins.ShellExtension {
	public class ApkShellExtension : BasePlugin {

		public ApkShellExtension ( IPluginHost host ) : base(host) {

		}

    #region IPluginExtendedInfo Members

    public override string Author {
      get { return "Ryan Conrad"; }
    }

		public override string Url {
      get { return "http://de.codeplex.com"; }
    }

    public override string Contact {
      get { return string.Empty; }
    }

    #endregion

    #region IPlugin Members

    public override string Name {
      get { return "APKShellExt"; }
    }
		public override string Group { get { return "Tools"; } }

    public override string Description {
      get { return "Windows shell extension for APK files"; }
    }

    public override bool HasConfiguration {
      get { return false; }
    }

    public override System.Drawing.Image Image {
      get { return DroidExplorer.Resources.Images.apk16; }
    }

    public override string Text {
      get { return "APK File Windows Shell Extension"; }
    }

    public override bool CreateToolButton {
      get { return false; }
    }


    public override void Execute ( IPluginHost pluginHost, DroidExplorer.Core.IO.LinuxDirectoryInfo currentDirectory, string[ ] args ) {

    }

    public override bool Runnable {
      get { return false; }
    }

    #endregion
  }
}
