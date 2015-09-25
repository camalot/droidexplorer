using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Core.UI {
  public class SaveFileDialog : FileDialog {
    public SaveFileDialog ( )
      : this ( "/" ) {
    }

		public SaveFileDialog ( string initialDirectory ) : base( initialDirectory ) {
      this.OkText = "&Save";
      this.Title = "Open File";

		}
  }
}
