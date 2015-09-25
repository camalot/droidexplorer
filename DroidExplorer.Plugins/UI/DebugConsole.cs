using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using DroidExplorer.Core.UI;
using DroidExplorer.UI;
using DroidExplorer.Core;
using DroidExplorer.Core.UI.CConsole;
using DroidExplorer.Core.Plugins;

namespace DroidExplorer.Plugins.UI {

  public partial class DebugConsole : PluginForm {
    ConsoleWriter cwOut = null;
    ConsoleWriter cwError = null;

    public DebugConsole ( IPluginHost pluginHost ): base(pluginHost) {
      InitializeComponent ( );

			this.console.InternalRichTextBox.ReadOnly = true;
			cwOut = new ConsoleWriter(ref this.console, Logger.Levels.Debug);
      cwError = new ConsoleWriter ( ref this.console, Logger.Levels.Error );

      System.Console.SetOut ( cwOut );
      System.Console.SetError ( cwError );

    }

    protected override void OnClosing ( CancelEventArgs e ) {
      cwOut.Close ( );
      cwError.Close ( );
      base.OnClosing ( e );
    }

    

    private void filters_CheckedChanged ( object sender, EventArgs e ) {
      Logger.Level = Logger.Levels.None;

      if ( debugToolStripButton.Checked ) {
        Logger.Level |= Logger.Levels.Debug;
      }

      if ( infoToolStripButton.Checked ) {
        Logger.Level |= Logger.Levels.Info;
      }

      if ( warnToolStripButton.Checked ) {
        Logger.Level |= Logger.Levels.Warn;
      }

      if ( errorToolStripButton.Checked ) {
        Logger.Level |= Logger.Levels.Error;
      }
    }

    private void clearToolStripButton_Click ( object sender, EventArgs e ) {
			this.console.ClearOutput();
    }

    private void saveToolStripButton_Click ( object sender, EventArgs e ) {
      System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog ( );
      sfd.Title = "Save log file";
      sfd.Filter = "Log File|*.log|All Files (*.*)|*.*";
      sfd.FilterIndex = 0;
      if ( sfd.ShowDialog ( this ) == DialogResult.OK ) {
        this.console.InternalRichTextBox.SaveFile ( sfd.FileName, RichTextBoxStreamType.UnicodePlainText );
        this.LogInfo ( "Log file saved to {0}", sfd.FileName );
      }
    }

  }
}
