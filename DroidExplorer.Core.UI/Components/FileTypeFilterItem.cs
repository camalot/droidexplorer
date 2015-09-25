using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Core.UI.Components {
  internal class FileTypeFilterItem {
    public FileTypeFilterItem ( )
      : this ( string.Empty, string.Empty ) {

    }

    public FileTypeFilterItem ( string text, string filter ) {
      this.Text = text;
      this.Filter = filter;
    }
    public string Text { get; set; }
    public string Filter { get; set; }
    public override string ToString ( ) {
      return Text;
    }
  }
}
