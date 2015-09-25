using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DroidExplorer.Core.UI {
  [Serializable]
  public class ComboBoxEx : ComboBox {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #region Component Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent ( ) {
      components = new System.ComponentModel.Container ( );
    }

    #endregion

    #region Constants

    private const int TEXTBOX_PADDING = 3;
    private const int WM_USER = 0x0400;
    private const int WM_REFLECT = WM_USER + 0x1C00;
    private const int WM_COMMAND = 0x0111;
    private const int WM_CLICK = 513;

    private const int CBN_DROPDOWN = 7;
    private const int CBN_CLOSEUP = 8;

    #endregion

    #region Instance Member

    private ToolStripControlHost treeViewHost;
    protected ToolStripDropDown dropDown;
    protected IComboBoxExtender childControl;
    //protected TreeNode _intiallySelectedNode = null;
    private bool opened = false;

    #endregion

    #region Constructors

    public ComboBoxEx ( ) { }

    #endregion

    #region Methods

    public void AddControl ( IComboBoxExtender child ) {
      try {
        childControl = child;
        childControl.SetUserInterface ( );
        treeViewHost = new ToolStripControlHost ( childControl as Control );
        treeViewHost.Visible = false;
        CloseComboBoxExtenderHandler closeCombo = new CloseComboBoxExtenderHandler ( CloseComboBox );
        childControl.CloseComboBoxExtenderDelegate = closeCombo;

        dropDown = new ToolStripDropDown ( );
        dropDown.Items.Add ( treeViewHost );
        dropDown.AutoClose = true;

        this.DropDownStyle = ComboBoxStyle.DropDownList;

        
        dropDown.Closed += new ToolStripDropDownClosedEventHandler ( DropDownClosed );
        this.EnabledChanged += new EventHandler ( ExtenderCombo_EnabledChanged );

        closeCombo ( );

      } catch ( Exception ex ) {
        MessageBox.Show ( ex.Message );
      }
    }

    protected override void OnResize ( EventArgs e ) {
      base.OnResize ( e );
    }

    private void ShowDropDown ( ) {
      LoadChildControl ( );
      opened = true;
    }

    public void LoadChildControl ( ) {
      treeViewHost.Visible = true;
      treeViewHost.Width = DropDownWidth;
      treeViewHost.Height = DropDownHeight;
      ( ( Control )childControl ).Width = DropDownWidth;
      dropDown.Show ( this, 0, this.Height );
    }


    private void CloseComboBox ( ) {
      dropDown.Close ( );
    }

    #endregion

    #region Events Handlers

    /*private void txtTextBox_KeyDown ( object sender, KeyEventArgs e ) {
      try {
        if ( e.KeyCode != Keys.Down )
          return;
        ShowDropDown ( );
      } catch ( Exception ex ) {
        MessageBox.Show ( ex.Message );
      }
    }*/

    void ExtenderCombo_EnabledChanged ( object sender, EventArgs e ) {
      try {

			} catch ( Exception ex ) {
        MessageBox.Show ( ex.Message );
      }
    }

		/// <summary>
		/// Handles the Closed event of the cmbDropDown control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.ToolStripDropDownClosedEventArgs"/> instance containing the event data.</param>
    protected virtual void DropDownClosed ( object sender, ToolStripDropDownClosedEventArgs e ) {
      try {
        //this.textBox.Text = childControl.DisplayText;
				this.Text = childControl.DisplayText;
      } catch ( Exception ex ) {
        MessageBox.Show ( ex.Message );
      }
    }

		protected override void OnSelectedIndexChanged ( EventArgs e ) {
			//base.OnSelectedIndexChanged ( e );
		}

		protected override void OnSelectedItemChanged ( EventArgs e ) {
			//base.OnSelectedItemChanged ( e );
		}

		public override string Text {
			get {
				return base.Text;
				
			}
			set {
				//this.Items.Clear ();
				//this.Items.Add ( value );
				//this.SelectedItem = value;
				base.Text = value;
			}
		}

		public void SetDisplayValue ( string value ) {
			this.Items.Clear ();
			this.Items.Add ( value );
			this.SelectedItem = value;
		}

    private void txtTextBox_Click ( object sender, EventArgs e ) {
      try {
        if ( this.DropDownStyle == ComboBoxStyle.DropDownList ) {
          if ( opened ) {
            opened = false;
            return;
          }
          ShowDropDown ( );
        }
      } catch ( Exception ex ) {
        MessageBox.Show ( ex.Message );
      }
    }

    #endregion

    #region Windowproc Overrides
    /// <summary>
    /// This will return hi word (highest 16 bits)
    /// </summary>
    public static int HIWORD ( int n ) {
      return ( n >> 16 ) & 0xffff;
    }
    /// <summary>
    /// Overrided win proc event handler
    /// </summary>
    protected override void WndProc ( ref Message m ) {
      if ( m.Msg == ( WM_REFLECT + WM_COMMAND ) ) {
        if ( HIWORD ( ( int )m.WParam ) == CBN_DROPDOWN ) {
          if ( !opened )
            ShowDropDown ( );
          return;
        }
      } else if ( m.Msg == WM_CLICK ) {
        if ( !opened )
          ShowDropDown ( );
        else
          opened = false;
        return;
      }
      base.WndProc ( ref m );
    }

    protected override void Dispose ( bool disposing ) {
      if ( disposing ) {
        if ( dropDown != null ) {
          dropDown.Dispose ( );
          dropDown = null;
        }
      }
      base.Dispose ( disposing );
    }
    #endregion
  }
}
