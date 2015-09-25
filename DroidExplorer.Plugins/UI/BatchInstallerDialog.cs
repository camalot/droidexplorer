using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Core.UI;
using DroidExplorer.Core;
using System.Threading;

namespace DroidExplorer.Plugins.UI {
  public partial class BatchInstallerDialog : Form {
    private delegate void IncrementDelegate ( int val );
    private delegate void SetIconDelegate ( Image img );
    private delegate void SetControlVisibleDelegate ( Control ctrl, bool visible );
    private delegate void SetControlTextDelegate ( Control ctrl, string text );

    public BatchInstallerDialog ( InstallDialog.InstallMode mode, List<string> apks ) {
      InitializeComponent ( );
      this.Mode = mode;
      this.ApkFiles = apks;
      this.perform.Text = this.Mode.ToString ( );
      this.progress.Maximum = ApkFiles.Count;
      this.progress.Minimum = 0;

      foreach ( var item in ApkFiles ) {
        this.files.Items.Add ( System.IO.Path.GetFileName ( item ) );
      }
    }
    private Thread InstallThread { get; set; }
    public List<string> ApkFiles { get; set; }
    public InstallDialog.InstallMode Mode { get; set; }

    private void perform_Click ( object sender, EventArgs e ) {
      this.perform.Enabled = false;
      this.progress.Visible = true;
      this.status.Visible = true;
      if ( InstallThread == null || !InstallThread.IsAlive ) {
        InstallThread = new Thread ( new ThreadStart ( delegate {
          foreach ( var item in ApkFiles ) {
            if ( this.InvokeRequired ) {
              this.Invoke ( new SetIconDelegate ( this.SetIcon ), CommandRunner.Instance.GetLocalApkIconImage ( item ) );
              this.Invoke ( new SetControlTextDelegate ( this.SetControlText ), status, System.IO.Path.GetFileName ( item ) );
            } else {
              this.SetIcon ( CommandRunner.Instance.GetLocalApkIconImage ( item ) );
              this.SetControlText ( status, System.IO.Path.GetFileName ( item ) );
            }

            if ( this.Mode == InstallDialog.InstallMode.Install ) {
              CommandRunner.Instance.InstallApk ( item );
            } else {
              AaptBrandingCommandResult result = CommandRunner.Instance.GetLocalApkInformation ( item );
              CommandRunner.Instance.UninstallApk ( result.Package );
            }

            if ( this.InvokeRequired ) {
              this.Invoke ( new IncrementDelegate ( this.progress.Increment ), 1 );
            } else {
              this.progress.Increment ( 1 );
            }
          }

          if ( this.InvokeRequired ) {
            this.Invoke ( new SetControlVisibleDelegate ( SetControlVisible ), finish, true );
            this.Invoke ( new SetControlVisibleDelegate ( SetControlVisible ), cancel, false );
          } else {
            SetControlVisible ( finish, true );
            SetControlVisible ( cancel, false );
          }
        } ) );
        InstallThread.Start ( );
      }
    }

    private void SetControlText ( Control ctrl, string text ) {
      ctrl.Text = text;
    }

    private void SetControlVisible ( Control ctrl, bool visible ) {
      ctrl.Visible = visible;
    }

    private void SetIcon ( Image img ) {
      this.icon.Image = img;
    }

    private void cancel_Click ( object sender, EventArgs e ) {
      if ( InstallThread != null && this.InstallThread.IsAlive ) {
        try {
          InstallThread.Abort ( );
        } catch {

        }
      }
    }

    private void finish_Click ( object sender, EventArgs e ) {
      this.DialogResult = DialogResult.OK;
    }

  }
}
