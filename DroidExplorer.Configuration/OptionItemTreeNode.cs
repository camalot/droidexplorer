using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Configuration.UI;

namespace DroidExplorer.Configuration {
  public class OptionItemTreeNode : TreeNode {

    public OptionItemTreeNode ( ) : base() {

    }

    public OptionItemTreeNode (string text ) : base(text) {

    }

    public IUIEditor UIEditor { get; set; }
    public IOptionNodeDataLoader DataLoader { get; set; }
  }
}
