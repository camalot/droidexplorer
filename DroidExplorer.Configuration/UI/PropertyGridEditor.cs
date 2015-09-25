using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DroidExplorer.Configuration.UI {
  public class PropertyGridEditor : PropertyGrid, IUIEditor {

    public PropertyGridEditor ( ) {
      this.Dock = DockStyle.Fill;
      this.Margin = new Padding ( 10 );
      this.Location = new System.Drawing.Point ( 0, 0 );
    }

    #region IUIEditor Members

    public virtual void SetSourceObject ( object obj ) {
      this.SelectedObject = obj;
    }

    #endregion
  }
}
