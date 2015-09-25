using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using DroidExplorer.Core;
using System.Globalization;
using System.Threading;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using DroidExplorer.Configuration;
using DroidExplorer.Tools;

namespace DroidExplorer.UI {
  public partial class SdkInstallDialog : Form {
    private delegate void UpdateLabelDelegate ( string text );
    private delegate void UpdateProgressBarValueDelegate ( int min, int max, int value );
    private delegate void SetControlEnabledDelegate ( Control ctrl, bool enabled );
    private delegate void SetControlVisibleDelegate ( Control ctrl, bool visible );
    private delegate void SetStepImagePositionDelegate ( Control ctrl );
    private delegate void GenericDelegate ( );

    public SdkInstallDialog ( ) {
      InitializeComponent ( );
      this.Size = new Size ( 569, 402 );
      WebClient = new WebClient ( );
      //ToolsZip = new FileInfo ( System.IO.Path.Combine ( Program.LocalPath, string.Format ( CultureInfo.InvariantCulture, "android-sdk-tools-{0}.zip", AndroidUsbDriverHelper.IsCupcakeDriver ? "cupcake" : "donut" ) ) );
      ZipFile = System.IO.Path.Combine ( CommandRunner.Instance.TempDataPath, string.Format ( CultureInfo.InvariantCulture, "android-sdk-tools-{0}.zip", AndroidUsbDriverHelper.IsCupcakeDriver ? "cupcake" : "donut" ) );
      // set to the default (user) path
      this.SdkToolsPath = CommandRunner.Instance.SdkPath;
      usbDriverVersion.Text = string.Format ( CultureInfo.InvariantCulture, usbDriverVersion.Text, AndroidUsbDriverHelper.IsCupcakeDriver ? "Cupcake" : "Donut" );
    }

    private bool Downloading { get; set; }
    private WebClient WebClient { get; set; }
    private string ZipFile { get; set; }
    private Thread RunningThread { get; set; }
    private Thread DownloadThread { get; set; }
    //private FileInfo ToolsZip { get; set; }
    private string SdkToolsPath { get; set; }

    private void perform_Click ( object sender, EventArgs e ) {
      try {

        SetControlEnabled ( this.perform, false );
        SetControlVisible ( this.progress, true );
        SetControlVisible ( stepImage, true );

          if ( DownloadThread != null && DownloadThread.IsAlive ) {
            try {
              DownloadThread.Abort ( );
            } catch ( ThreadAbortException taex ) {
              this.LogWarn ( "Aborted download thread." );
            }
          }

          DownloadThread = new Thread ( new ThreadStart ( delegate {
            FileInfo tfile = new FileInfo ( ZipFile );
            if ( tfile.Exists ) {
              tfile.Delete ( );
            }

            if ( !tfile.Directory.Exists ) {
              tfile.Directory.Create ( );
            }

            this.LogInfo ( "Checking USB Driver Version..." );
            bool isCupcake = AndroidUsbDriverHelper.IsCupcakeDriver;
            this.LogInfo ( "Downloading {0} SDK Tools", isCupcake ? "1.5" : "1.6" );

            HttpWebRequest req = HttpWebRequest.Create ( new Uri ( isCupcake ? Properties.Resources.AndroidCupcakeSdkDownloadUrl : Properties.Resources.AndroidDonutSdkDownloadUrl ) ) as HttpWebRequest;

            if ( Settings.Instance.Proxy.Enabled ) {
              req.Proxy = Settings.Instance.Proxy.CreateProxy ( );
            }

            HttpWebResponse resp = req.GetResponse ( ) as HttpWebResponse;

            using ( Stream reader = resp.GetResponseStream ( ) ) {
              Downloading = true;

              int bytesRead = 0;
              byte[] buffer = new byte[ 2048 ];
              long totalBytes = resp.ContentLength;
              long totalBytesRead = 0;

              using ( FileStream fs = new FileStream ( ZipFile, FileMode.Create, FileAccess.Write, FileShare.Read ) ) {
                while ( ( bytesRead = reader.Read ( buffer, 0, buffer.Length ) ) > 0 ) {
                  fs.Write ( buffer, 0, bytesRead );
                  totalBytesRead += bytesRead;
                  int percentage = ( int )( ( ( double )totalBytesRead / ( double )totalBytes ) * 100f );
                  if ( this.InvokeRequired ) {
                    this.Invoke ( new UpdateProgressBarValueDelegate ( this.UpdateProgressBarValue ), 0, 100, percentage );
                    this.Invoke ( new UpdateLabelDelegate ( this.UpdateStatusLabel ), string.Format ( new DroidExplorer.Core.IO.FileSizeFormatProvider ( ), "Downloading Android Tools: {0:fs} of {1:fs} - {2}%", totalBytesRead, totalBytes, percentage ) );
                  } else {
                    UpdateProgressBarValue ( 0, 100, percentage );
                    UpdateStatusLabel ( string.Format ( new DroidExplorer.Core.IO.FileSizeFormatProvider ( ), "Downloading Android Tools: {0:fs} of {1:fs} - {2}%", totalBytesRead, totalBytes, percentage ) );
                  }
                }

                if ( this.InvokeRequired ) {
                  this.Invoke ( new UpdateProgressBarValueDelegate ( this.UpdateProgressBarValue ), 0, 100, 0 );
                  this.Invoke ( new UpdateLabelDelegate ( this.UpdateStatusLabel ), "Download Complete" );
                } else {
                  UpdateProgressBarValue ( 0, 100, 0 );
                  UpdateStatusLabel ( "Download Complete" );
                }

                Downloading = false;
              }
            }

            if ( RunningThread != null && RunningThread.IsAlive ) {
              try {
                RunningThread.Abort ( );
              } catch { }
            }
            RunningThread = new Thread ( new ThreadStart ( delegate {
              Step2 ( );
            } ) );
            RunningThread.Start ( );

          } ) );
          DownloadThread.Start ( );
         
      } catch ( Exception ex ) {
        this.LogError ( ex.Message, ex );
        SetError ( ex );
        FinializeSetup ( );
      }
    }

    /*private void UseLocalPackage ( ) {
      UpdateStatusLabel ( "Using local package..." );

      // we have a local one, use it.
      ToolsZip.CopyTo ( ZipFile, true );
      if ( RunningThread != null && RunningThread.IsAlive ) {
        try {
          RunningThread.Abort ( );
        } catch { }
      }
      RunningThread = new Thread ( new ThreadStart ( delegate {
        Step2 ( );
      } ) );
      RunningThread.Start ( );
    }*/

    private void Step4 ( ) {
      if ( this.InvokeRequired ) {
        this.Invoke ( new SetStepImagePositionDelegate ( SetStepImagePosition ), step4 );
        this.Invoke ( new UpdateLabelDelegate ( this.UpdateStatusLabel ), "Cleaining up temporary files..." );
      } else {
        SetStepImagePosition ( step4 );
        this.UpdateStatusLabel ( "Cleaining up temporary files..." );
      }
      try {
        System.IO.File.Delete ( ZipFile );
      } catch ( Exception ex ) {
        this.LogError ( ex.Message, ex );
        SetError ( ex );
      } finally {
        Thread.Sleep ( 1000 );
        FinializeSetup ( );
      }
    }

    private void Step3 ( ) {
      if ( this.InvokeRequired ) {
        this.Invoke ( new SetStepImagePositionDelegate ( SetStepImagePosition ), step3 );
        this.Invoke ( new UpdateLabelDelegate ( this.UpdateStatusLabel ), "Registering Tools..." );
      } else {
        SetStepImagePosition ( step3 );
        this.UpdateStatusLabel ( "Registering Tools..." );
      }

      Settings.Instance.SdkPath = this.SdkToolsPath;
      CommandRunner.Instance.SdkPath = this.SdkToolsPath;

      if ( this.InvokeRequired ) {
        this.Invoke ( new UpdateLabelDelegate ( this.UpdateStatusLabel ), "Starting Android Debuging Bridge Server..." );
      } else {
        this.UpdateStatusLabel ( "Starting Android Debuging Bridge Server..." );
      }
      CommandRunner.Instance.StartServer ( );

      Step4 ( );
    }

    private void Step2 ( ) { // extract file

      if ( this.InvokeRequired ) {
        this.Invoke ( new SetStepImagePositionDelegate ( SetStepImagePosition ), step2 );
        this.Invoke ( new SetControlVisibleDelegate ( SetControlVisible ), this.progress, true );
        this.Invoke ( new UpdateLabelDelegate ( this.UpdateStatusLabel ), "Extracting Tools..." );
      } else {
        SetStepImagePosition ( step2 );
        SetControlVisible ( progress, true );
        this.UpdateStatusLabel ( "Extracting Tools..." );
      }
      FileInfo zFile = new FileInfo ( ZipFile );
      if ( !zFile.Exists ) {
        SetError ( new FileNotFoundException ( "Unable to locate Tools package.", ZipFile ) );
        FinializeSetup ( );
        return;
      }

      try {
        long entriesCount = 0;
        using ( ZipFile z = new ZipFile ( zFile.FullName ) ) {
          entriesCount = z.Count;
        }

        using ( FileStream fs = new FileStream ( zFile.FullName, FileMode.Open, FileAccess.Read, FileShare.Read ) ) {
          using ( ZipInputStream zis = new ZipInputStream ( fs ) ) {
            ZipEntry entry = zis.GetNextEntry ( );
            int currentIndex = 0;
            while ( entry != null ) {

              string entryPath = entry.Name;
              string dir = string.Empty;
              if ( !string.IsNullOrEmpty ( entryPath ) ) {
                dir = Path.GetDirectoryName ( entryPath );
              }
              string file = Path.GetFileName ( entryPath );

              if ( currentIndex < entriesCount ) {
                int val = ( int )( ( ( double )currentIndex / ( double )entriesCount ) * 100f );
                if ( this.InvokeRequired ) {
                  this.Invoke ( new UpdateProgressBarValueDelegate ( this.UpdateProgressBarValue ), 0, 100, val );
                  this.Invoke ( new UpdateLabelDelegate ( this.UpdateStatusLabel ), string.Format ( "Extracting Tools. {0}%", val ) );
                } else {
                  UpdateProgressBarValue ( 0, 100, val );
                  this.UpdateStatusLabel ( string.Format ( "Extracting Tools. {0}%", val ) );
                }
              }


              // create the directory if it doesnt exist.
              if ( !string.IsNullOrEmpty ( dir ) && !Directory.Exists ( Path.Combine ( CommandRunner.Instance.SdkPath, dir ) ) ) {
                Directory.CreateDirectory ( Path.Combine ( CommandRunner.Instance.SdkPath, dir ) );
              }
              if ( !string.IsNullOrEmpty ( file ) ) {
                using ( FileStream nfs = new FileStream ( Path.Combine ( CommandRunner.Instance.SdkPath, entryPath ), FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read ) ) {
                  byte[] buffer = new byte[ 2048 ];
                  int dataLength = 0;
                  while ( ( dataLength = zis.Read ( buffer, 0, buffer.Length ) ) > 0 ) {
                    nfs.Write ( buffer, 0, dataLength );
                  }
                }
              }
              currentIndex++;
              entry = zis.GetNextEntry ( );
            }
          }
        }
      } catch ( Exception ex ) {
        this.LogError ( ex.Message, ex );
        SetError ( ex );
        FinializeSetup ( );
        return;
      }


      Step3 ( );

    }

    private void FinializeSetup ( ) {
      if ( this.InvokeRequired ) {
        this.Invoke ( new SetControlVisibleDelegate ( SetControlVisible ), this.progress, false );
        this.Invoke ( new SetControlVisibleDelegate ( SetControlVisible ), stepImage, false );
        this.Invoke ( new SetControlVisibleDelegate ( SetControlVisible ), this.finish, true );
        this.Invoke ( new SetControlVisibleDelegate ( SetControlVisible ), this.selectSourcePanel, false );
        this.Invoke ( new SetControlVisibleDelegate ( SetControlVisible ), this.cancel, false );
        this.Invoke ( new UpdateLabelDelegate ( this.UpdateStatusLabel ), string.Empty );
      } else {
        SetControlVisible ( this.progress, false );
        SetControlVisible ( stepImage, false );
        SetControlVisible ( finish, true );
        SetControlVisible ( this.selectSourcePanel, false );
        SetControlVisible ( cancel, false );
        this.UpdateStatusLabel ( string.Empty );
      }
    }

    private void SetErrorPanelDockTop ( ) {
      errorPanel.Dock = DockStyle.Top;
    }

    private void SetError ( Exception ex ) {
      if ( this.InvokeRequired ) {
        this.Invoke ( new SetControlVisibleDelegate ( SetControlVisible ), this.errorPanel, true );
        this.Invoke ( new SetControlVisibleDelegate ( SetControlVisible ), this.stepsPanel, false );
        this.Invoke ( new SetControlVisibleDelegate ( SetControlVisible ), this.selectSourcePanel, false );
        this.Invoke ( new SetControlEnabledDelegate ( SetControlVisible ), this.installPanel, false );
        this.Invoke ( new GenericDelegate ( SetErrorPanelDockTop ) );
        this.Invoke ( new UpdateLabelDelegate ( this.UpdateErrorLabel ), ex.ToString ( ) );
      } else {
        SetControlVisible ( this.errorPanel, true );
        SetControlVisible ( this.installPanel, false );
        SetControlVisible ( this.stepsPanel, false );
        SetControlVisible ( this.selectSourcePanel, false );
        SetErrorPanelDockTop ( );
        UpdateErrorLabel ( ex.ToString ( ) );
      }

      this.finish.DialogResult = DialogResult.Cancel;
    }

    private void UpdateErrorLabel ( string text ) {
      this.errorLabel.Text = text;
    }

    private void SetStepImagePosition ( Control control ) {
      stepImage.Visible = true;
      stepImage.Top = control.Top;
    }


    private void cancel_Click ( object sender, EventArgs e ) {
      if ( Downloading ) {
        try {
          if ( DownloadThread != null && DownloadThread.IsAlive ) {
            DownloadThread.Abort ( );
          }
        } catch ( ThreadAbortException ) {
          this.LogWarn ( "Abortting 'Download' Thread." );
        }
      }

      if ( RunningThread != null && RunningThread.IsAlive ) {
        try {
          RunningThread.Abort ( );
        } catch ( ThreadAbortException ) {
          this.LogWarn ( "Abortting 'Running' Thread." );
        }
      }

      this.DialogResult = DialogResult.Cancel;
    }

    private void UpdateStatusLabel ( string text ) {
      this.statusLabel.Text = text;
    }

    private void UpdateProgressBarValue ( int min, int max, int value ) {
      this.progress.Minimum = min;
      this.progress.Maximum = max;
      this.progress.Value = value;
    }

    private void SetControlEnabled ( Control ctrl, bool enabled ) {
      ctrl.Enabled = enabled;
    }

    private void SetControlVisible ( Control ctrl, bool visible ) {
      ctrl.Visible = visible;
    }

    private void finish_Click ( object sender, EventArgs e ) {
      this.DialogResult = DialogResult.OK;
    }

    private void next_Click ( object sender, EventArgs e ) {
      SetControlEnabled ( next, false );

      if ( androidTools.Checked ) {
        SetControlVisible ( next, false );
        SetControlVisible ( selectSourcePanel, false );
        SetControlVisible ( perform, true );
        SetControlVisible ( installPanel, true );
        installPanel.Dock = DockStyle.Top;
      } else {
        FolderBrowserDialog fbd = new FolderBrowserDialog ( );
        SetControlVisible ( statusLabel, false );

        fbd.ShowNewFolderButton = false;
        fbd.Description = "Select Android SDK Driectory";

        if ( fbd.ShowDialog ( this ) == DialogResult.OK ) {
          string dir = fbd.SelectedPath;
          if ( FoundSdkFile ( dir, CommandRunner.ADB_COMMAND ) && FoundSdkFile ( dir, CommandRunner.AAPT_COMMAND ) && FoundSdkFile ( dir, CommandRunner.DDMS_COMMAND ) ) {
            this.SdkToolsPath = dir;
            Step3 ( );
            SetControlVisible ( finishedPanel, true );
            finishedPanel.Dock = DockStyle.Top;
          } else {
            SetError ( new FileNotFoundException ( "Unable to locate required SDK tools in the selected directory" ) );
          }
        } else {
          SetError ( new Exception ( "Operation Canceled by user" ) );
        }

        FinializeSetup ( );
      }
    }

    private bool FoundSdkFile ( string path, string filename ) {
      string fullPath = Path.Combine ( path, filename );
      bool exists = File.Exists ( fullPath );
      this.LogDebug ( "Checking sdk file exists ({0}): {1}", fullPath, exists );
      return exists;
    }
  }
}
