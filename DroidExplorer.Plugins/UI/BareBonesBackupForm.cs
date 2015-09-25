using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ICSharpCode.SharpZipLib.Zip;
using DroidExplorer.Core;
using System.Diagnostics;
using System.Globalization;
using DroidExplorer.Core.IO;

namespace DroidExplorer.Plugins.UI {
  public partial class BareBonesBackupForm : Form {
    private delegate bool GetRadioCheckedValueDelegate ( RadioButton radio );
    private delegate void SetControlEnabledDelegate ( Control ctrl, bool enabled );
    private delegate void SetControlVisibleDelegate ( Control ctrl, bool visible );
    private delegate void SetControlTextDelegate ( Control ctrl, string text );
    private delegate void IncrementProgressDelegate ( ProgressBar pb, int val );


    public BareBonesBackupForm ( ) {
      InitializeComponent ( );
      try {
        // clean before running
        CommandRunner.Instance.TempPathCleanup ( );
        if ( !System.IO.Directory.Exists ( CommandRunner.Instance.TempDataPath ) ) {
          System.IO.Directory.CreateDirectory ( CommandRunner.Instance.TempDataPath );
        }
      } catch ( Exception ex ) {
        this.LogError ( ex.Message, ex );
      }
    }

    private Thread ProcessThread { get; set; }
    private string UpdateZip { get; set; }

    private string UpdateTempPath {
      get {
        string path =  System.IO.Path.Combine ( CommandRunner.Instance.TempDataPath, "gabupdate" );

        if ( !System.IO.Directory.Exists ( path ) ) {
          System.IO.Directory.CreateDirectory ( path );
        }
        return path;
      }
    }
    private bool UseDevice {
      get {
        if ( this.InvokeRequired ) {
          return ( bool )this.Invoke ( new GetRadioCheckedValueDelegate ( delegate ( RadioButton r ) {
            return r.Checked;
          } ), backupFromDevice );
        } else {
          return this.backupFromDevice.Checked;
        }
      }
    }

    private void ExtractTools ( ) {
      this.LogDebug ( "Extracting backup tools" );

      if ( this.InvokeRequired ) {
        this.Invoke ( new SetControlTextDelegate ( SetControlText ), status, "Extracting Backup Tools" );
      } else {
        SetControlText ( status, "Extracting Backup Tools" );
      }

      string zipFile = System.IO.Path.Combine ( CommandRunner.Instance.TempDataPath, "SignApk.zip" );
      using ( System.IO.FileStream fs = new System.IO.FileStream ( zipFile, System.IO.FileMode.Create, System.IO.FileAccess.Write ) ) {
        fs.Write ( Properties.Resources.SignApk_zip, 0, Properties.Resources.SignApk_zip.Length );
      }
      FastZip zip = new FastZip ( );
      zip.ExtractZip ( zipFile, CommandRunner.Instance.TempDataPath, string.Empty );
    }

    private void ExtractConfig ( ) {
      this.LogDebug ( "Extracting configuration file" );

      if ( this.InvokeRequired ) {
        this.Invoke ( new SetControlTextDelegate ( SetControlText ), status, "Extracting Configuration File" );
      } else {
        SetControlText ( status, "Extracting Configuration File" );
      }

      System.IO.FileInfo config = new System.IO.FileInfo ( System.IO.Path.Combine ( CommandRunner.Instance.TempDataPath, "BareBonesBackup.config" ) );
      using ( System.IO.FileStream fs = new System.IO.FileStream ( config.FullName, System.IO.FileMode.Create, System.IO.FileAccess.Write ) ) {
        byte[] bytes = Encoding.Default.GetBytes ( Properties.Resources.BareBonesBackup_config );
        fs.Write ( bytes, 0, bytes.Length );
        fs.Flush ( );
      }
    }

    private void PullBackupFiles ( ) {
      if ( UseDevice ) {
        PullFromDevice ( );
      } else {
        PullFromZip ( );
      }
    }

    private void PullFromZip ( ) {

      string statusString = string.Format ( CultureInfo.InvariantCulture, "Extracting {0}", System.IO.Path.GetFileName ( UpdateZip ) );
      this.LogDebug ( statusString );


      if ( this.InvokeRequired ) {
        this.Invoke ( new SetControlTextDelegate ( SetControlText ), status, statusString );
      } else {
        SetControlText ( status, statusString );
      }

      FastZip zip = new FastZip ( );
      zip.ExtractZip ( UpdateZip, CommandRunner.Instance.TempDataPath, string.Empty );

      System.IO.FileInfo systemImage = new System.IO.FileInfo ( System.IO.Path.Combine ( CommandRunner.Instance.TempDataPath, "system.img" ) );
      if ( systemImage.Exists ) {

        statusString = "Extracting system.img";
        if ( this.InvokeRequired ) {
          this.Invoke ( new SetControlTextDelegate ( SetControlText ), status, statusString );
        } else {
          SetControlText ( status, statusString );
        }

        this.LogDebug ( "Extracting system.img" );
        Process proc = new Process ( );
        ProcessStartInfo psi = new ProcessStartInfo ( "unyaffs.exe", "system.img" );
        psi.CreateNoWindow = true;
        psi.WindowStyle = ProcessWindowStyle.Hidden;
        psi.WorkingDirectory = CommandRunner.Instance.TempDataPath;

        proc.StartInfo = psi;
        proc.Start ( );
        proc.WaitForExit ( );
      }

      System.IO.FileInfo userdataImage = new System.IO.FileInfo ( System.IO.Path.Combine ( CommandRunner.Instance.TempDataPath, "userdata.img" ) );
      if ( userdataImage.Exists ) {
        statusString = "Extracting userdata.img";
        if ( this.InvokeRequired ) {
          this.Invoke ( new SetControlTextDelegate ( SetControlText ), status, statusString );
        } else {
          SetControlText ( status, statusString );
        }

        this.LogDebug ( "Extracting userdata.img" );
        Process proc = new Process ( );
        ProcessStartInfo psi = new ProcessStartInfo ( "unyaffs.exe", "userdata.img" );
        psi.CreateNoWindow = true;
        psi.WindowStyle = ProcessWindowStyle.Hidden;
        psi.WorkingDirectory = CommandRunner.Instance.TempDataPath;
        proc.StartInfo = psi;
        proc.Start ( );
        proc.WaitForExit ( );
      }

      this.LogDebug ( "Copying extracted files" );

      CopyExtractedFiles ( "/" );
      CopyExtractedFiles ( "/system/" );
    }

    private void CopyExtractedFiles ( string root ) {

      foreach ( string item in BareBonesBackupConfiguration.Instance.Files ) {
        string tstring = item;
        if ( tstring.StartsWith ( root ) ) {
          tstring = tstring.Remove ( 0, root.Length );
        }

        string tlocal = item;
        if ( tlocal.StartsWith ( "/" ) ) {
          tlocal = tlocal.Remove ( 0, 1 );
        }

        System.IO.FileInfo ofile = new System.IO.FileInfo ( System.IO.Path.Combine ( CommandRunner.Instance.TempDataPath, tstring ) );

        if ( ofile.Exists ) {
          System.IO.FileInfo nfile = new System.IO.FileInfo ( System.IO.Path.Combine ( UpdateTempPath, tlocal ) );
          if ( !nfile.Directory.Exists ) {
            nfile.Directory.Create ( );
          }

          string statusString = string.Format ( CultureInfo.InvariantCulture, "Copying {0}", nfile.Name );
          if ( this.InvokeRequired ) {
            this.Invoke ( new SetControlTextDelegate ( SetControlText ), status, statusString );
          } else {
            SetControlText ( status, statusString );
          }

          ofile.MoveTo ( nfile.FullName );
        }
      }
    }

    private void PullFromDevice ( ) {
      this.LogDebug ( "Pulling files off device" );


      foreach ( var item in BareBonesBackupConfiguration.Instance.Files ) {
        string tstring = item;
        if ( tstring.StartsWith ( "/" ) ) {
          tstring = tstring.Remove ( 0, 1 );
        }
        System.IO.FileInfo tfile = CommandRunner.Instance.PullFile ( item );
        if ( tfile != null && tfile.Exists ) {
          System.IO.FileInfo nfile = new System.IO.FileInfo ( System.IO.Path.Combine ( UpdateTempPath, tstring ) );
          if ( !nfile.Directory.Exists ) {
            nfile.Directory.Create ( );
          }

          string statusString = string.Format ( CultureInfo.InvariantCulture, "Copying {0}", nfile.Name );
          if ( this.InvokeRequired ) {
            this.Invoke ( new SetControlTextDelegate ( SetControlText ), status, statusString );
          } else {
            SetControlText ( status, statusString );
          }

          tfile.MoveTo ( nfile.FullName );
        }
      }
    }

    private bool SetZipFile ( ) {
      OpenFileDialog ofd = new OpenFileDialog ( );
      ofd.Title = "Select update.zip";
      ofd.FileName = "update.zip";
      ofd.Filter = "Update Files|*.zip|All Files|*.*";
      if ( ofd.ShowDialog ( this ) == DialogResult.OK ) {
        UpdateZip = ofd.FileName;
        return true;
      } else {
        return false;
      }
    }

    private void CreateUpdateZip ( ) {
      string statusString = "Creating update.zip";
      this.LogDebug ( statusString );
      if ( this.InvokeRequired ) {
        this.Invoke ( new SetControlTextDelegate ( SetControlText ), status, statusString );
      } else {
        SetControlText ( status, statusString );
      }
      System.IO.DirectoryInfo updatescriptDir = System.IO.Directory.CreateDirectory ( System.IO.Path.Combine ( UpdateTempPath, @"META-INF\com\google\android" ) );

      using ( System.IO.FileStream fs = new System.IO.FileStream ( System.IO.Path.Combine ( updatescriptDir.FullName, "update-script" ), System.IO.FileMode.Create, System.IO.FileAccess.Write ) ) {
        fs.Write ( Properties.Resources.gab_update_script, 0, Properties.Resources.gab_update_script.Length );
      }

      FastZip zip = new FastZip ( );
      string outputZip = System.IO.Path.Combine ( CommandRunner.Instance.TempDataPath, "update_unsigned.zip" );
      zip.CreateZip ( outputZip, UpdateTempPath, true, string.Empty );
    }

    private void SignZip ( ) {
      string statusString = "Signing update.zip";
      this.LogDebug ( statusString );
      if ( this.InvokeRequired ) {
        this.Invoke ( new SetControlTextDelegate ( SetControlText ), status, statusString );
      } else {
        SetControlText ( status, statusString );
      }

      string outputZip = System.IO.Path.Combine ( CommandRunner.Instance.TempDataPath, string.Format ( CultureInfo.InvariantCulture, BareBonesBackupConfiguration.Instance.OutputFile, DateTime.Today.ToString ( "MMddyyyy" ) ) );

      Process proc = new Process ( );
      ProcessStartInfo psi = new ProcessStartInfo ( "java.exe", string.Format ( CultureInfo.InvariantCulture, "-jar signapk.jar testkey.x509.pem testkey.pk8 update_unsigned.zip {0}", System.IO.Path.GetFileName ( outputZip ) ) );
      psi.WorkingDirectory = CommandRunner.Instance.TempDataPath;
      psi.CreateNoWindow = true;
      psi.WindowStyle = ProcessWindowStyle.Hidden;
      proc.StartInfo = psi;
      proc.Start ( );
      proc.WaitForExit ( );

      System.IO.FileInfo newFile = new System.IO.FileInfo ( System.IO.Path.Combine ( Environment.GetFolderPath ( Environment.SpecialFolder.Desktop ), System.IO.Path.GetFileName ( outputZip ) ) );
      if ( newFile.Exists ) {
        newFile.Delete ( );
      }
      System.IO.File.Move ( outputZip, newFile.FullName );
      this.LogInfo ( "Update file saved to {0}", newFile.FullName );
    }

    private void Finished ( ) {
      string statusString = "Performing Data Cleanup";
      this.LogDebug ( statusString );
      if ( this.InvokeRequired ) {
        this.Invoke ( new SetControlTextDelegate ( SetControlText ), status, statusString );
      } else {
        SetControlText ( status, statusString );
      }

      //CommandRunner.Instance.TempPathCleanup ( );

      if ( this.InvokeRequired ) {
        this.Invoke ( new SetControlEnabledDelegate ( SetControlEnabled ), perform, false );
        this.Invoke ( new SetControlVisibleDelegate ( SetControlVisible ), cancel, false );
        this.Invoke ( new SetControlEnabledDelegate ( SetControlEnabled ), finish, true );
        this.Invoke ( new SetControlVisibleDelegate ( SetControlVisible ), finish, true );
        this.Invoke ( new SetControlVisibleDelegate ( SetControlVisible ), backupComplete, true );
        this.Invoke ( new SetControlVisibleDelegate ( SetControlVisible ), backupFromDevice, false );
        this.Invoke ( new SetControlVisibleDelegate ( SetControlVisible ), backupFromZip, false );
        this.Invoke ( new SetControlVisibleDelegate ( SetControlVisible ), progress, false );
        this.Invoke ( new SetControlVisibleDelegate ( SetControlVisible ), status, false );
      } else {
        backupComplete.Visible = true;
        backupFromZip.Visible = false;
        backupFromDevice.Visible = false;
        perform.Enabled = false;
        cancel.Visible = false;
        finish.Visible = finish.Enabled = true;
        progress.Visible = false;
        status.Visible = false;
      }
    }

    private void SetControlEnabled ( Control ctrl, bool enabled ) {
      ctrl.Enabled = enabled;
    }

    private void SetControlVisible ( Control ctrl, bool visible ) {
      ctrl.Visible = visible;
    }

    private void SetControlText ( Control ctrl, string txt ) {
      ctrl.Text = txt;
    }

    private void IncrementProgress ( ProgressBar pb, int val ) {
      pb.Increment ( val );
    }

    private void perform_Click ( object sender, EventArgs e ) {
      if ( ProcessThread != null && ProcessThread.IsAlive ) {
        try {
          ProcessThread.Abort ( );
        } catch { }
      }

      perform.Enabled = false;
      cancel.Enabled = true;
      progress.Visible = true;
      progress.Minimum = 0;
      progress.Maximum = 5;
      progress.Value = 0;
      backupFromDevice.Enabled = backupFromZip.Enabled = false;

      if ( !UseDevice ) {
        if ( !SetZipFile ( ) ) {
          perform.Enabled = true;
          cancel.Enabled = false;
          progress.Visible = false;
          backupFromDevice.Enabled = true;
          backupFromZip.Enabled = true;
          return;
        }
      }

      ProcessThread = new Thread ( new ThreadStart ( StartBackup ) );
      ProcessThread.Start ( );
    }

    private void StartBackup ( ) {

      ExtractConfig ( );

      if ( this.InvokeRequired ) {
        this.Invoke ( new IncrementProgressDelegate ( IncrementProgress ), progress, 1 );
      } else {
        progress.Increment ( 1 );
      }

      ExtractTools ( );

      if ( this.InvokeRequired ) {
        this.Invoke ( new IncrementProgressDelegate ( IncrementProgress ), progress, 1 );
      } else {
        progress.Increment ( 1 );
      }

      PullBackupFiles ( );

      if ( this.InvokeRequired ) {
        this.Invoke ( new IncrementProgressDelegate ( IncrementProgress ), progress, 1 );
      } else {
        progress.Increment ( 1 );
      }

      CreateUpdateZip ( );

      if ( this.InvokeRequired ) {
        this.Invoke ( new IncrementProgressDelegate ( IncrementProgress ), progress, 1 );
      } else {
        progress.Increment ( 1 );
      }

      SignZip ( );

      if ( this.InvokeRequired ) {
        this.Invoke ( new IncrementProgressDelegate ( IncrementProgress ), progress, 1 );
      } else {
        progress.Increment ( 1 );
      }

      Finished ( );
    }

    private void finish_Click ( object sender, EventArgs e ) {
      DialogResult = DialogResult.OK;
      this.Close ( );
    }

    private void cancel_Click ( object sender, EventArgs e ) {
      if ( this.ProcessThread != null && this.ProcessThread.IsAlive ) {
        try {
          this.ProcessThread.Abort ( );
        } catch { }
      }

      this.DialogResult = DialogResult.Cancel;
      this.Close ( );
    }
  }
}
