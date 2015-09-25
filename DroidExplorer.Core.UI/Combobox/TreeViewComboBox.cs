using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DroidExplorer.Core.UI {
  public class TreeViewComboBox : TreeView, IComboBoxExtender {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private CloseComboBoxExtenderHandler CloseComboBoxExtender;

    protected override void OnDoubleClick ( EventArgs e ) {
      if ( CloseComboBoxExtender != null ) {
        CloseComboBoxExtender ( );
      }
      base.OnDoubleClick ( e );
    }

    #region IComboBoxExtender Members

    public void SetUserInterface ( ) {
      this.BorderStyle = BorderStyle.None;
      this.HideSelection = false;
      this.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
    }

    public CloseComboBoxExtenderHandler CloseComboBoxExtenderDelegate { get; set; }

    public string DisplayText {
      get {
        if ( this.SelectedNode == null ) {
          return string.Empty;
        } else {
          return this.SelectedNode.FullPath;
        }
      }
    }

    #endregion

    #region Component Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent ( ) {
      components = new System.ComponentModel.Container ( );
    }

    #endregion
  }
}
