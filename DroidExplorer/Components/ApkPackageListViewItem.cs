using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Core;

namespace DroidExplorer.Components {
  public class ApkPackageListViewItem : ListViewItem {
    public ApkPackageListViewItem ( AaptBrandingCommandResult apkInfo ) {
      ApkInformation = apkInfo;
      if ( !string.IsNullOrEmpty ( this.ApkInformation.Label ) ) {
        this.Text = ApkInformation.Label;
      } else {
        this.Text = ApkInformation.Package;
      }

      this.SubItems.Add ( ApkInformation.Package );
      this.SubItems.Add ( ApkInformation.DevicePath );
      this.SubItems.Add ( ApkInformation.Version );
    }

    public AaptBrandingCommandResult ApkInformation { get; private set; }
  }
}
