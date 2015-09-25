using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Core.Plugins;
using DroidExplorer.Plugins.UI;
using System.Windows.Forms;
using System.ComponentModel;

namespace DroidExplorer.Plugins {
  public class ProcessExplorer : BasePlugin {
    public ProcessExplorer ( IPluginHost host ) : base(host){

		}

    #region IPlugin Members

    public override  string Name {
      get { return "Process Explorer"; }
    }
		public override string Group { get { return "Tools"; } }

		public override string Description {
      get { return this.Name; }
    }

    public override  System.Drawing.Image Image {
      get { return DroidExplorer.Resources.Images.DialogGroup_5846_16x; }
    }

		public override bool CreateToolButton {
      get { return true; }
    }

		public override void Execute ( IPluginHost pluginHost, DroidExplorer.Core.IO.LinuxDirectoryInfo currentDirectory, string[] args ) {
      ProcessViewer.StartPosition = FormStartPosition.Manual;
      ProcessViewer.Left = this.PluginHost.Right;
      ProcessViewer.Top = this.PluginHost.Top;
      if ( !ProcessViewer.Visible ) {
        ProcessViewer.Show ( this.PluginHost.GetHostWindow ( ) );
      }
		}

		public override bool Runnable { get { return false; } }


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
    private ProcessViewerForm _processViewer = null;
    internal ProcessViewerForm ProcessViewer {
      get {
        if ( _processViewer == null || _processViewer.IsDisposed ) {
          _processViewer = new ProcessViewerForm (  this.PluginHost );
        }
        return _processViewer;
      }
    }
  }
}
