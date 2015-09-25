using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Core.UI {
  public partial class PluginForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose ( bool disposing ) {
			try {
				if ( disposing && ( components != null ) ) {
					components.Dispose ( );
				}
				base.Dispose ( disposing );
			} catch {
			}
    }

    private void InitializeComponent ( ) {
      this.SuspendLayout ( );
      // 
      // PluginForm
      // 
      this.ClientSize = new System.Drawing.Size ( 429, 257 );
      this.Name = "PluginForm";
      this.ResumeLayout ( false );

    }
  }
}
