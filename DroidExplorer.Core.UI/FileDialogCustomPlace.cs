using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DroidExplorer.Core.UI {
  public class FileDialogCustomPlace : ToolStripButton {
    public FileDialogCustomPlace ( )
      : this ( string.Empty, (EventHandler)null ) {

    }

    public FileDialogCustomPlace ( string path )
      : this ( path, null, (EventHandler)null ) {
    }

    public FileDialogCustomPlace ( string path, EventHandler click )
      : this ( path, null, click ) {
    }

    public FileDialogCustomPlace ( string path, Image image )
      : this ( path, image, null ) {

    }

    public FileDialogCustomPlace ( string path, Image image, EventHandler click )
      : this ( path, string.Empty, image, click ) {

    }

    public FileDialogCustomPlace ( string path, string text, Image image )
      : this ( path, text, image, null ) {

    }


    public FileDialogCustomPlace ( string path, string text, Image image, EventHandler click )
      : base ( text, image, click, path ) {
      this.Path = path;

      if ( string.IsNullOrEmpty ( text ) ) {
        this.Text = DroidExplorer.Core.IO.Path.GetDirectoryName ( Path );
      } else {
        this.Text = text;
      }

      if ( string.IsNullOrEmpty ( this.Text ) && this.Path == new string ( new char[ ] { DroidExplorer.Core.IO.Path.DirectorySeparatorChar } ) ) {
        this.Text = string.IsNullOrEmpty ( CommandRunner.Instance.DefaultDevice ) ? CommandRunner.Instance.GetSerialNumber ( ) : CommandRunner.Instance.DefaultDevice;
      }

      this.TextImageRelation = TextImageRelation.ImageAboveText;
      this.ImageAlign = ContentAlignment.TopCenter;
      this.TextAlign = ContentAlignment.BottomCenter;
      this.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

      this.Image = image;

      if ( this.Image == null ) {
        if ( this.Path == new string ( new char[ ] { DroidExplorer.Core.IO.Path.DirectorySeparatorChar } ) ) {
          this.Image = DroidExplorer.Resources.Images.mobile_32xLG;
        } else {
          this.Image = DroidExplorer.Resources.Images.folder_Closed_32xLG;
        }
      }
    }



    public string Path { get; set; }
  }
}
