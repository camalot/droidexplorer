using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Tools;
using DroidExplorer.Controls;
using DroidExplorer.Core.UI;
using DroidExplorer.Core;
using DroidExplorer.Configuration;
using DroidExplorer.Configuration.UI;
using System.Globalization;
using System.Threading;
using Camalot.Common.Extensions;

namespace DroidExplorer.UI {
  public partial class OptionsForm : Form {
    
    public OptionsForm ( ) {
      InitializeComponent ( );

      categories.SetVistaExplorerStyle ( false, true );

      CreateTree ( );
    }

    private void CreateTree ( ) {
      categories.Nodes.Clear ( );
			foreach ( OptionNode on in OptionTreeSettings.Instance.Nodes ) {
        OptionItemTreeNode oitn = on.CreateTreeNode ( );
				oitn.ExpandAll ( );
        categories.Nodes.Add ( oitn );
      }

      categories.AfterSelect += delegate ( object sender, TreeViewEventArgs e ) {
        if ( e.Node != null && e.Node is OptionItemTreeNode ) {
          OptionItemTreeNode tn = e.Node as OptionItemTreeNode;
          new Thread ( new ParameterizedThreadStart ( delegate ( object o ) {
            OptionItemTreeNode oitn = ( o as OptionItemTreeNode );
            if ( oitn != null && oitn.UIEditor is SdkUIEditor ) {
              ( oitn.UIEditor as SdkUIEditor ).SetSourceObject ( string.Format ( CultureInfo.InvariantCulture, "{0} ({1})", AndroidUsbDriverHelper.DriverVersion.Or(new Version(0,0,0,0)).ToString ( ), AndroidUsbDriverHelper.IsRevision1Driver ? "Revision 1" : "Revision 2" ) );
            }
          } ) ).Start ( tn );
          SetSettingsControl(tn.UIEditor);

        }
      };
    }

    private void SetSettingsControl ( IUIEditor uiEditor ) {
      this.contentPanel.Controls.Clear ( );

      if ( uiEditor is Control ) {
        this.contentPanel.Controls.Add ( uiEditor as Control );
      }

    }

    private void registerExplorer_Click ( object sender, EventArgs e ) {
      try {
        ExplorerRegistrationUtility.Instance.Register ( );
        TaskDialog.MessageBox ( DroidExplorer.Resources.Strings.ExtensionRegisterSuccessTitle, DroidExplorer.Resources.Strings.ExtensionRegisterSuccessMessage, 
					string.Empty, TaskDialogButtons.OK, SysIcons.Information );
      } catch ( Exception ex ) {
        TaskDialog.MessageBox (DroidExplorer.Resources.Strings.ExtensionRegisterErrorTitle, DroidExplorer.Resources.Strings.ExtensionRegisterErrorMessage, 
					ex.Message, TaskDialogButtons.OK, SysIcons.Information );
      }
    }

    private void unregisterExplorer_Click ( object sender, EventArgs e ) {
      try {
        ExplorerRegistrationUtility.Instance.Unregister ( );
        TaskDialog.MessageBox ( DroidExplorer.Resources.Strings.ExtensionUnregisterSuccessTitle, DroidExplorer.Resources.Strings.ExtensionUnregisterSuccessMessage, 
					string.Empty, TaskDialogButtons.OK, SysIcons.Information );
      } catch ( Exception ex ) {
        TaskDialog.MessageBox ( DroidExplorer.Resources.Strings.ExtensionUnregisterErrorTitle, DroidExplorer.Resources.Strings.ExtensionUnregisterErrorMessage, 
					ex.Message, TaskDialogButtons.OK, SysIcons.Information );
      }
    }

    private void bottomPanel_Paint ( object sender, PaintEventArgs e ) {
      Graphics g = e.Graphics;

      Point leftPoint = new Point ( this.contentPanel.Left + this.contentPanel.Padding.Left, 0 );
      Point rightPoint = new Point ( this.contentPanel.Right - this.contentPanel.Padding.Right, 0 );
      
      using ( Pen pen = new Pen ( SystemColors.ControlDark, 1 ) ) {
        g.DrawLine ( pen, leftPoint, rightPoint );
      }

      leftPoint.Offset ( 0, 1 );
      rightPoint.Offset ( 0, 1 );

      using ( Pen pen = new Pen ( SystemColors.ControlLightLight, 1 ) ) {
        g.DrawLine ( pen, leftPoint, rightPoint );
      }
    }

    private void ok_Click ( object sender, EventArgs e ) {
      Settings.Instance.Save ( );
      this.DialogResult = DialogResult.OK;
    }

  }
}
