using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DroidExplorer.Core;
using DroidExplorer.Components;
using DroidExplorer.Core.IO;
using System.Drawing.Imaging;
using DroidExplorer.Controls;
using DroidExplorer.Core.UI;
using DroidExplorer.Core.Plugins;

namespace DroidExplorer.UI {
  public partial class PackageManagerForm : Form {
    private delegate void SetControlVisibleDelegate ( Control ctrl, bool visible );
    private delegate void RemoveListViewItemDelegate ( System.Windows.Forms.ListView.ListViewItemCollection parent, ListViewItem lvi );
    public PackageManagerForm ( ) {
      InitializeComponent ( );
      this.packagesList.SmallImageList = SystemImageListHost.Instance.SmallImageList;
			this.packagesList.LargeImageList = SystemImageListHost.Instance.LargeImageList;
      this.packagesList.SelectedIndexChanged += new EventHandler ( packagesList_SelectedIndexChanged );
      new Thread ( delegate ( ) {
        BuildListView ( );
      } ).Start ( );
    }

    void packagesList_SelectedIndexChanged ( object sender, EventArgs e ) {
      this.uninstallToolStripButton.Enabled = this.packagesList.SelectedItems.Count > 0;
    }

    private void BuildListView ( ) {
      try {
        if ( this.InvokeRequired ) {
          this.Invoke ( new SetControlVisibleDelegate ( SetControlVisible ), new object[ ] { loadingPanel, true } );
          this.Invoke ( new DroidExplorer.UI.GenericDelegate ( packagesList.Items.Clear ) );
        } else {
          SetControlVisible ( loadingPanel, true );
          packagesList.Items.Clear ( );
        }
        List<DroidExplorer.Core.AaptBrandingCommandResult> apks = CommandRunner.Instance.GetInstalledPackagesApkInformation ( );

        foreach ( var item in apks ) {
          ApkPackageListViewItem lvi = new ApkPackageListViewItem ( item );

          // cant uninstall if we dont know the package
          if ( string.IsNullOrEmpty ( lvi.ApkInformation.Package ) ) {
            continue;
          }

          string keyName = lvi.ApkInformation.DevicePath;
          if ( keyName.StartsWith ( "/" ) ) {
            keyName = keyName.Substring ( 1 );
          }
          keyName = keyName.Replace ( "/", "." );

          if ( !Program.SystemIcons.ContainsKey ( keyName ) ) {
            // get apk and extract the app icon
            Image img = CommandRunner.Instance.GetLocalApkIconImage ( item.LocalApk );

            if ( img == null ) {
							img = DroidExplorer.Resources.Images.package32;
            } else {
              using ( System.IO.MemoryStream stream = new System.IO.MemoryStream ( ) ) {
								string fileName = System.IO.Path.Combine ( System.IO.Path.Combine ( CommandRunner.Settings.UserDataDirectory, Cache.APK_IMAGE_CACHE ), string.Format ( "{0}.png", keyName ) );
                img.Save ( stream, ImageFormat.Png );
                stream.Position = 0;
                using ( System.IO.FileStream fs = new System.IO.FileStream ( fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write ) ) {
                  byte[] buffer = new byte[ 2048 ];
                  int readBytes = 0;
                  while ( ( readBytes = stream.Read ( buffer, 0, buffer.Length ) ) != 0 ) {
                    fs.Write ( buffer, 0, readBytes );
                  }
                }
              }

            }
						SystemImageListHost.Instance.AddFileTypeImage ( keyName, img, img );
          }

          if ( this.InvokeRequired ) {
            this.Invoke ( new SetListViewItemImageIndexDelegate ( this.SetListViewItemImageIndex ), new object[ ] { lvi, Program.SystemIcons[ keyName ] } );
            this.Invoke ( new AddListViewItemDelegate ( this.AddListViewItem ), new object[ ] { packagesList, lvi } );
          } else {
            SetListViewItemImageIndex ( lvi, Program.SystemIcons[ keyName ] );
            AddListViewItem ( packagesList, lvi );
          }
        }
      } catch ( Exception ex ) {
        this.LogError( ex.Message,ex );
      } finally {
        if ( this.InvokeRequired ) {
          this.Invoke ( new SetControlVisibleDelegate ( SetControlVisible ), new object[ ] { loadingPanel, false } );
        } else {
          SetControlVisible ( loadingPanel, false );
        }
      }
    }

    private void SetListViewItemImageIndex ( ListViewItem lvi, int index ) {
      lvi.ImageIndex = index;
    }

    private void AddListViewItem ( ListView lv, ListViewItem lvi ) {
      lv.Items.Add ( lvi );
    }

    private void SetControlVisible ( Control ctrl, bool visible ) {
      ctrl.Visible = visible;
    }

    private void installToolStripButton_Click ( object sender, EventArgs e ) {
			System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog ();
      ofd.Title = "Select application to install";
      ofd.Filter = "Android Application|*.apk|All Files (*.*)|*.*";
      ofd.FilterIndex = 0;
      ofd.InitialDirectory = Environment.GetFolderPath ( Environment.SpecialFolder.Desktop );
      ofd.RestoreDirectory = true;
      ofd.Multiselect = false;
      if ( ofd.ShowDialog ( this ) == DialogResult.OK ) {
        AaptBrandingCommandResult apkInfo = CommandRunner.Instance.GetLocalApkInformation ( ofd.FileName );
        apkInfo.LocalApk = ofd.FileName;
        InstallDialog install = new InstallDialog ((IPluginHost)this.ParentForm, InstallDialog.InstallMode.Install, apkInfo );
        install.ShowDialog ( this );


        /*if ( CommandRunner.Instance.InstallApk ( ofd.FileName ) ) {
          try {
          TaskDialog.MessageBox ( "Install Complete", string.Format ( "Successfully installed {0}", System.IO.Path.GetFileName ( ofd.FileName ) ),
            string.Empty, TaskDialogButtons.OK, SysIcons.Information );
          } catch ( Exception ex ) {
            Console.WriteLine ( "[{0}] {1}", this.GetType ( ).Name, ex.ToString ( ) );

            TaskDialog.MessageBox ( "Install Error", string.Format ( Properties.Resources.InstallErrorMessage, System.IO.Path.GetFileName ( ofd.FileName ) ),
              ex.Message, TaskDialogButtons.OK, SysIcons.Error );
          }
        } else {
          TaskDialog.MessageBox ( "Install Error", string.Format ( Properties.Resources.InstallErrorMessage, System.IO.Path.GetFileName ( ofd.FileName ) ),
            Properties.Resources.InstallErrorGenericMessage, TaskDialogButtons.OK, SysIcons.Error );
        }*/


      }
        }

    private void uninstallToolStripButton_Click ( object sender, EventArgs e ) {
      if ( this.packagesList.SelectedItems.Count == 1 ) {
        ApkPackageListViewItem lvi = packagesList.SelectedItems[ 0 ] as ApkPackageListViewItem;
        if ( lvi != null && lvi.ApkInformation != null && !string.IsNullOrEmpty ( lvi.ApkInformation.Package ) ) {
          InstallDialog install = new InstallDialog ( (IPluginHost)this.ParentForm, InstallDialog.InstallMode.Uninstall, lvi.ApkInformation );
          if ( install.ShowDialog ( this ) == DialogResult.OK ) {
            RemoveListViewItem ( packagesList.Items, lvi );
      }
          
          /*string package = lvi.ApkInformation.Package;
          string name = string.IsNullOrEmpty ( lvi.ApkInformation.Label ) ? lvi.ApkInformation.Package : lvi.ApkInformation.Label;

          if ( CommandRunner.Instance.UninstallApk ( package ) ) {
            try {
                RemoveListViewItem ( packagesList.Items, lvi );
                TaskDialog.MessageBox ( "Uninstall Complete", string.Format ( "Successfully uninstalled {0}", name ),
                string.Empty, TaskDialogButtons.OK, SysIcons.Information );
            } catch ( Exception ex ) {
              Console.WriteLine ( "[{0}] {1}", this.GetType ( ).Name, ex.ToString ( ) );

              TaskDialog.MessageBox ( "Uninstall Error", string.Format ( Properties.Resources.UninstallErrorMessage, name ),
                ex.Message, TaskDialogButtons.OK, SysIcons.Error );
            }
          } else {
            TaskDialog.MessageBox ( "Install Error", string.Format ( Properties.Resources.UninstallErrorMessage, name ),
              Properties.Resources.UninstallErrorGenericMessage, TaskDialogButtons.OK, SysIcons.Error );
          }*/
        }
      }
    }

    private void RemoveListViewItem ( ListView.ListViewItemCollection lvic, ListViewItem lvi ) {
      lvic.Remove ( lvi );
    }
  }
}
