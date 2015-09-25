using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DroidExplorer.Core.UI {
  public class CustomPlacesPanel : ToolStrip {
    public CustomPlacesPanel ( ) {
      this.SetStyle ( ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserMouse, true );
      this.Dock = DockStyle.Fill;
      this.CustomPlaces = new FileDialogCustomPlacesCollection ( );
      this.GripStyle = ToolStripGripStyle.Hidden;
      this.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
      this.CanOverflow = false;
      this.RenderMode = ToolStripRenderMode.System;

      CustomPlaces.ItemAdded += new EventHandler ( CustomPlaces_ItemAdded );
      CustomPlaces.ItemRemoved += new EventHandler ( CustomPlaces_ItemRemoved );
      CustomPlaces.ItemsSorted += new EventHandler ( CustomPlaces_ItemsSorted );

      this.ImageScalingSize = new Size ( 32, 32 );
    }

    public FileDialogCustomPlacesCollection CustomPlaces { get; private set; }

    void CustomPlaces_ItemsSorted ( object sender, EventArgs e ) {
      RedrawPlaces ( );
    }

    void CustomPlaces_ItemRemoved ( object sender, EventArgs e ) {
      RedrawPlaces ( );
    }

    void CustomPlaces_ItemAdded ( object sender, EventArgs e ) {
      RedrawPlaces ( );
    }

    void RedrawPlaces ( ) {
      this.Items.Clear ( );

      for ( int i = 0; i < this.CustomPlaces.Count; i++ ) {
        FileDialogCustomPlace place = this.CustomPlaces[ i ];

        this.Items.Add ( place );
      }
    }
  }
}
