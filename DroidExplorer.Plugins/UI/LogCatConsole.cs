using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Core.Plugins;
using DroidExplorer.Core.UI;
using DroidExplorer.UI;
using DroidExplorer.Core;
using System.Threading;

namespace DroidExplorer.Plugins.UI {
  public partial class LogCatConsole :PluginForm,  IShellProcess {
    public LogCatConsole ( IPluginHost host) : base( host ) {
      InitializeComponent ( );
      this.StickyWindow = new StickyWindow ( this );
			this.shell.OnProcessExit += shell_OnProcessExit;
			Run("logcat");
    }

		void shell_OnProcessExit(object sender, Core.UI.CConsole.ConsoleEventArgs args) {
			Invoke((Action)(() => {
				this.Close();
			}));
		}
    protected override void OnClosing ( CancelEventArgs e ) {
			if(IsProcessRunning && !e.Cancel) {
				this.StopProcess();
			}
      base.OnClosing ( e );
    }

    private void Run ( string command ) {
      CommandRunner.Instance.LaunchRedirectedShellWindow ( CommandRunner.Instance.DefaultDevice, command, this );
    }


    #region IShellProcess Members

		public bool EchoInput {
			get { return false; }
		}

		public bool IsProcessRunning {
			get {
				return this.shell.IsProcessRunning;
			}
		}

		public void WriteInput(string input) {
			this.shell.WriteInput(input);
		}

		public void WriteOutput(string output) {
			this.shell.WriteOutput(output);
		}

		public void StartProcess(string command, string args) {
		this.shell.StartProcess(command, args);
		}

		public void StopProcess() {
			this.shell.StopProcess();
		}


    #endregion

    private void saveToolStripButton_Click ( object sender, EventArgs e ) {
      System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog ( );
      sfd.Title = "Save logcat";
      sfd.Filter = "Log file|*.log";
      sfd.FilterIndex = 0;
			if ( sfd.ShowDialog ( this ) == DialogResult.OK ) {
				this.shell.InternalRichTextBox.SaveFile ( sfd.FileName, RichTextBoxStreamType.PlainText );
			}
    }

    private void clearToolStripButton_Click ( object sender, EventArgs e ) {
			this.shell.InternalRichTextBox.Clear();
    }
  }
}
