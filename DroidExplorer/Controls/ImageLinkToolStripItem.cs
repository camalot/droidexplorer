using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DroidExplorer.Controls {
  public class ImageLinkToolStripItem : ToolStripControlHost {
    public ImageLinkToolStripItem ( )
      : base ( new PictureBox ( ), "image" ) {
      ImageHost = base.Control as PictureBox;
      ImageHost.Name = "linkImage";
			ImageHost.Image = DroidExplorer.Resources.Images.donate_menu;
      ImageHost.Cursor = Cursors.Hand;
    }

    private PictureBox ImageHost { get; set; }

    public Image Image { get { return ImageHost.Image; } set { ImageHost.Image = value; } }

    protected override void OnClick ( EventArgs e ) {
      base.OnClick ( e );
    }
  }
}
