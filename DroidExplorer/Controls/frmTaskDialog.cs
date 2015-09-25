using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DroidExplorer.Controls {

  /// <summary>
  /// 
  /// </summary>
  public partial class TaskDialogForm : Form {
    #region PRIVATE members
    //--------------------------------------------------------------------------------
    SysIcons _mainIcon = SysIcons.Question;
    SysIcons _footerIcon = SysIcons.Warning;

    string _mainInstruction = "Main Instruction Text";
    int _mainInstructionHeight = 0;
    Font _mainInstructionFont = new Font ( "Arial", 11.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( byte ) 0 );

    List<RadioButton> _radioButtonCtrls = new List<RadioButton> ( );
    string _radioButtons = "";
    int _initialRadioButtonIndex = 0;

    List<Button> _cmdButtons = new List<Button> ( );
    string _commandButtons = "";
    int _commandButtonClicked = -1;

    TaskDialogButtons _buttons = TaskDialogButtons.YesNoCancel;

    bool _expanded = false;
    bool _isVista = false;
    #endregion

    #region PROPERTIES
    /// <summary>
    /// Gets or sets the main icon.
    /// </summary>
    /// <value>The main icon.</value>
    public SysIcons MainIcon { get { return _mainIcon; } set { _mainIcon = value; } }
    /// <summary>
    /// Gets or sets the footer icon.
    /// </summary>
    /// <value>The footer icon.</value>
    public SysIcons FooterIcon { get { return _footerIcon; } set { _footerIcon = value; } }

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>The title.</value>
    public string Title { get { return this.Text; } set { this.Text = value; } }
    /// <summary>
    /// Gets or sets the main instruction.
    /// </summary>
    /// <value>The main instruction.</value>
    public string MainInstruction { get { return _mainInstruction; } set { _mainInstruction = value; this.Invalidate ( ); } }
    /// <summary>
    /// Gets or sets the content.
    /// </summary>
    /// <value>The content.</value>
    public string Content { get { return lbContent.Text; } set { lbContent.Text = value; } }
    /// <summary>
    /// Gets or sets the expanded info.
    /// </summary>
    /// <value>The expanded info.</value>
    public string ExpandedInfo { get { return lbExpandedInfo.Text; } set { lbExpandedInfo.Text = value; } }
    /// <summary>
    /// Gets or sets the footer.
    /// </summary>
    /// <value>The footer.</value>
    public string Footer { get { return lbFooter.Text; } set { lbFooter.Text = value; } }

    /// <summary>
    /// Gets or sets the radio buttons.
    /// </summary>
    /// <value>The radio buttons.</value>
    public string RadioButtons { get { return _radioButtons; } set { _radioButtons = value; } }
    /// <summary>
    /// Gets or sets the initial index of the radio button.
    /// </summary>
    /// <value>The initial index of the radio button.</value>
    public int InitialRadioButtonIndex { get { return _initialRadioButtonIndex; } set { _initialRadioButtonIndex = value; } }
    /// <summary>
    /// Gets the index of the radio button.
    /// </summary>
    /// <value>The index of the radio button.</value>
    public int RadioButtonIndex {
      get {
        foreach ( RadioButton rb in _radioButtonCtrls )
          if ( rb.Checked )
            return ( int ) rb.Tag;
        return -1;
      }
    }

    /// <summary>
    /// Gets or sets the command buttons.
    /// </summary>
    /// <value>The command buttons.</value>
    public string CommandButtons { get { return _commandButtons; } set { _commandButtons = value; } }
    /// <summary>
    /// Gets the index of the command button clicked.
    /// </summary>
    /// <value>The index of the command button clicked.</value>
    public int CommandButtonClickedIndex { get { return _commandButtonClicked; } }

    /// <summary>
    /// Gets or sets the buttons.
    /// </summary>
    /// <value>The buttons.</value>
    public TaskDialogButtons Buttons { get { return _buttons; } set { _buttons = value; } }

    /// <summary>
    /// Gets or sets the verification text.
    /// </summary>
    /// <value>The verification text.</value>
    public string VerificationText { get { return cbVerify.Text; } set { cbVerify.Text = value; } }
    /// <summary>
    /// Gets or sets a value indicating whether [verification check box checked].
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [verification check box checked]; otherwise, <c>false</c>.
    /// </value>
    public bool VerificationCheckBoxChecked { get { return cbVerify.Checked; } set { cbVerify.Checked = value; } }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="TaskDialogForm"/> is expanded.
    /// </summary>
    /// <value><c>true</c> if expanded; otherwise, <c>false</c>.</value>
    public bool Expanded { get { return _expanded; } set { _expanded = value; } }
    #endregion

    #region CONSTRUCTOR
    /// <summary>
    /// Initializes a new instance of the <see cref="frmTaskDialog"/> class.
    /// </summary>
    public TaskDialogForm ( ) {
      InitializeComponent ( );

      _isVista = VistaTaskDialog.IsAvailableOnThisOS;
      if ( !_isVista && TaskDialog.UseToolWindowOnXP ) // <- shall we use the smaller toolbar?
        this.FormBorderStyle = FormBorderStyle.FixedToolWindow;

      MainInstruction = "Main Instruction";
      Content = "";
      ExpandedInfo = "";
      Footer = "";
      VerificationText = "";
    }
    #endregion

    #region BuildForm
    bool _formBuilt = false;
    /// <summary>
    /// Builds the form.
    /// </summary>
    public void BuildForm ( ) {
      int form_height = 0;

      // Setup Main Instruction
      switch ( _mainIcon ) {
        case SysIcons.Information:
          imgMain.Image = SystemIcons.Information.ToBitmap ( );
          break;
        case SysIcons.Question:
          imgMain.Image = SystemIcons.Question.ToBitmap ( );
          break;
        case SysIcons.Warning:
          imgMain.Image = SystemIcons.Warning.ToBitmap ( );
          break;
        case SysIcons.Error:
          imgMain.Image = SystemIcons.Error.ToBitmap ( );
          break;
      }

      //AdjustLabelHeight(lbMainInstruction);
      //pnlMainInstruction.Height = Math.Max(41, lbMainInstruction.Height + 16);
      if ( _mainInstructionHeight == 0 )
        GetMainInstructionTextSizeF ( );
      pnlMainInstruction.Height = Math.Max ( 41, _mainInstructionHeight + 16 );

      form_height += pnlMainInstruction.Height;

      // Setup Content
      pnlContent.Visible = ( Content != "" );
      if ( Content != "" ) {
        AdjustLabelHeight ( lbContent );
        pnlContent.Height = lbContent.Height + 4;
        form_height += pnlContent.Height;
      }

      bool show_verify_checkbox = ( cbVerify.Text != "" );
      cbVerify.Visible = show_verify_checkbox;

      // Setup Expanded Info and Buttons panels
      if ( ExpandedInfo == "" ) {
        pnlExpandedInfo.Visible = false;
        lbShowHideDetails.Visible = false;
        cbVerify.Top = 12;
        pnlButtons.Height = 40;
      } else {
        AdjustLabelHeight ( lbExpandedInfo );
        pnlExpandedInfo.Height = lbExpandedInfo.Height + 4;
        pnlExpandedInfo.Visible = _expanded;
        lbShowHideDetails.Text = ( _expanded ? "        Hide details" : "        Show details" );
        lbShowHideDetails.ImageIndex = ( _expanded ? 0 : 3 );
        if ( !show_verify_checkbox )
          pnlButtons.Height = 40;
        if ( _expanded )
          form_height += pnlExpandedInfo.Height;
      }

      // Setup RadioButtons
      pnlRadioButtons.Visible = ( _radioButtons != "" );
      if ( _radioButtons != "" ) {
        string[ ] arr = _radioButtons.Split ( new char[ ] { '|' } );
        int pnl_height = 12;
        for ( int i = 0; i < arr.Length; i++ ) {
          RadioButton rb = new RadioButton ( );
          rb.Parent = pnlRadioButtons;
          rb.Location = new Point ( 60, 4 + ( i * rb.Height ) );
          rb.Text = arr[ i ];
          rb.Tag = i;
          rb.Checked = ( _initialRadioButtonIndex == i );
          rb.Width = this.Width - rb.Left - 15;
          pnl_height += rb.Height;
          _radioButtonCtrls.Add ( rb );
        }
        pnlRadioButtons.Height = pnl_height;
        form_height += pnlRadioButtons.Height;
      }

      // Setup CommandButtons
      pnlCommandButtons.Visible = ( _commandButtons != "" );
      if ( _commandButtons != "" ) {
        string[ ] arr = _commandButtons.Split ( new char[ ] { '|' } );
        int t = 8;
        int pnl_height = 16;
        for ( int i = 0; i < arr.Length; i++ ) {
          CommandButton btn = new CommandButton ( );
          btn.Parent = pnlCommandButtons;
          btn.Location = new Point ( 50, t );
          if ( _isVista )  // <- tweak font if vista
            btn.Font = new Font ( btn.Font, FontStyle.Regular );
          btn.Text = arr[ i ];
          btn.Size = new Size ( this.Width - btn.Left - 15, btn.GetBestHeight ( ) );
          t += btn.Height;
          pnl_height += btn.Height;
          btn.Tag = i;
          btn.Click += new EventHandler ( CommandButton_Click );
        }
        pnlCommandButtons.Height = pnl_height;
        form_height += pnlCommandButtons.Height;
      }

      // Setup Buttons
      switch ( _buttons ) {
        case TaskDialogButtons.YesNo:
          bt1.Visible = false;
          bt2.Text = "&Yes";
          bt2.DialogResult = DialogResult.Yes;
          bt3.Text = "&No";
          bt3.DialogResult = DialogResult.No;
          this.AcceptButton = bt2;
          this.CancelButton = bt3;
          break;
        case TaskDialogButtons.YesNoCancel:
          bt1.Text = "&Yes";
          bt1.DialogResult = DialogResult.Yes;
          bt2.Text = "&No";
          bt2.DialogResult = DialogResult.No;
          bt3.Text = "&Cancel";
          bt3.DialogResult = DialogResult.Cancel;
          this.AcceptButton = bt1;
          this.CancelButton = bt3;
          break;
        case TaskDialogButtons.OKCancel:
          bt1.Visible = false;
          bt2.Text = "&OK";
          bt2.DialogResult = DialogResult.OK;
          bt3.Text = "&Cancel";
          bt3.DialogResult = DialogResult.Cancel;
          this.AcceptButton = bt2;
          this.CancelButton = bt3;
          break;
        case TaskDialogButtons.OK:
          bt1.Visible = false;
          bt2.Visible = false;
          bt3.Text = "&OK";
          bt3.DialogResult = DialogResult.OK;
          this.AcceptButton = bt3;
          this.CancelButton = bt3;
          break;
        case TaskDialogButtons.Close:
          bt1.Visible = false;
          bt2.Visible = false;
          bt3.Text = "&Close";
          bt3.DialogResult = DialogResult.Cancel;
          this.CancelButton = bt3;
          break;
        case TaskDialogButtons.Cancel:
          bt1.Visible = false;
          bt2.Visible = false;
          bt3.Text = "&Cancel";
          bt3.DialogResult = DialogResult.Cancel;
          this.CancelButton = bt3;
          break;
        case TaskDialogButtons.None:
          bt1.Visible = false;
          bt2.Visible = false;
          bt3.Visible = false;
          break;
      }

      this.ControlBox = ( Buttons == TaskDialogButtons.Cancel ||
                         Buttons == TaskDialogButtons.Close ||
                         Buttons == TaskDialogButtons.OKCancel ||
                         Buttons == TaskDialogButtons.YesNoCancel );

      if ( !show_verify_checkbox && ExpandedInfo == "" && _buttons == TaskDialogButtons.None )
        pnlButtons.Visible = false;
      else
        form_height += pnlButtons.Height;

      pnlFooter.Visible = ( Footer != "" );
      if ( Footer != "" ) {
        AdjustLabelHeight ( lbFooter );
        pnlFooter.Height = Math.Max ( 28, lbFooter.Height + 16 );
        switch ( _footerIcon ) {
          case SysIcons.Information:
            imgFooter.Image = SystemIcons.Information.ToBitmap ( ).GetThumbnailImage ( 16, 16, null, IntPtr.Zero );
            break;
          case SysIcons.Question:
            imgFooter.Image = SystemIcons.Question.ToBitmap ( ).GetThumbnailImage ( 16, 16, null, IntPtr.Zero );
            break;
          case SysIcons.Warning:
            imgFooter.Image = SystemIcons.Warning.ToBitmap ( ).GetThumbnailImage ( 16, 16, null, IntPtr.Zero );
            break;
          case SysIcons.Error:
            imgFooter.Image = SystemIcons.Error.ToBitmap ( ).GetThumbnailImage ( 16, 16, null, IntPtr.Zero );
            break;
        }
        form_height += pnlFooter.Height;
      }

      this.ClientSize = new Size ( ClientSize.Width, form_height );

      _formBuilt = true;
    }

    /// <summary>
    /// Adjusts the height of the label.
    /// </summary>
    /// <param name="lb">The lb.</param>
    void AdjustLabelHeight ( Label lb ) {
      string text = lb.Text;
      Font textFont = lb.Font;
      SizeF layoutSize = new SizeF ( lb.ClientSize.Width, 5000.0F );
      Graphics g = Graphics.FromHwnd ( lb.Handle );
      SizeF stringSize = g.MeasureString ( text, textFont, layoutSize );
      lb.Height = ( int ) stringSize.Height + 4;
      g.Dispose ( );
    }
    #endregion

    #region EVENTS
    /// <summary>
    /// Handles the Click event of the CommandButton control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    void CommandButton_Click ( object sender, EventArgs e ) {
      _commandButtonClicked = ( int ) ( ( CommandButton ) sender ).Tag;
      this.DialogResult = DialogResult.OK;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
    protected override void OnLoad ( EventArgs e ) {
      base.OnLoad ( e );
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Form.Shown"/> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.EventArgs"/> that contains the event data.</param>
    protected override void OnShown ( EventArgs e ) {
      if ( !_formBuilt )
        throw new Exception ( "frmTaskDialog : Please call .BuildForm() before showing the TaskDialog" );
      base.OnShown ( e );
    }

    /// <summary>
    /// Handles the MouseEnter event of the lbDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private void lbDetails_MouseEnter ( object sender, EventArgs e ) {
      lbShowHideDetails.ImageIndex = ( _expanded ? 1 : 4 );
    }

    /// <summary>
    /// Handles the MouseLeave event of the lbDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private void lbDetails_MouseLeave ( object sender, EventArgs e ) {
      lbShowHideDetails.ImageIndex = ( _expanded ? 0 : 3 );
    }

    /// <summary>
    /// Handles the MouseUp event of the lbDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
    private void lbDetails_MouseUp ( object sender, MouseEventArgs e ) {
      lbShowHideDetails.ImageIndex = ( _expanded ? 1 : 4 );
    }

    /// <summary>
    /// Handles the MouseDown event of the lbDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
    private void lbDetails_MouseDown ( object sender, MouseEventArgs e ) {
      lbShowHideDetails.ImageIndex = ( _expanded ? 2 : 5 );
    }

    /// <summary>
    /// Handles the Click event of the lbDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private void lbDetails_Click ( object sender, EventArgs e ) {
      _expanded = !_expanded;
      pnlExpandedInfo.Visible = _expanded;
      lbShowHideDetails.Text = ( _expanded ? "        Hide details" : "        Show details" );
      if ( _expanded )
        this.Height += pnlExpandedInfo.Height;
      else
        this.Height -= pnlExpandedInfo.Height;
    }

    /// <summary>
    /// 
    /// </summary>
    const int MAIN_INSTRUCTION_LEFT_MARGIN = 46;
    /// <summary>
    /// 
    /// </summary>
    const int MAIN_INSTRUCTION_RIGHT_MARGIN = 8;

    /// <summary>
    /// Gets the main instruction text size F.
    /// </summary>
    /// <returns></returns>
    SizeF GetMainInstructionTextSizeF ( ) {
      SizeF mzSize = new SizeF ( pnlMainInstruction.Width - MAIN_INSTRUCTION_LEFT_MARGIN - MAIN_INSTRUCTION_RIGHT_MARGIN, 5000.0F );
      Graphics g = Graphics.FromHwnd ( this.Handle );
      SizeF textSize = g.MeasureString ( _mainInstruction, _mainInstructionFont, mzSize );
      _mainInstructionHeight = ( int ) textSize.Height;
      return textSize;
    }

    /// <summary>
    /// Handles the Paint event of the pnlMainInstruction control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
    private void pnlMainInstruction_Paint ( object sender, PaintEventArgs e ) {
      SizeF szL = GetMainInstructionTextSizeF ( );
      e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
      e.Graphics.DrawString ( _mainInstruction, _mainInstructionFont, new SolidBrush ( Color.DarkBlue ), new RectangleF ( new PointF ( MAIN_INSTRUCTION_LEFT_MARGIN, 10 ), szL ) );
    }

    /// <summary>
    /// Handles the Shown event of the frmTaskDialog control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private void frmTaskDialog_Shown ( object sender, EventArgs e ) {
      if ( TaskDialog.PlaySystemSounds ) {
        switch ( _mainIcon ) {
          case SysIcons.Error:
            System.Media.SystemSounds.Hand.Play ( );
            break;
          case SysIcons.Information:
            System.Media.SystemSounds.Asterisk.Play ( );
            break;
          case SysIcons.Question:
            System.Media.SystemSounds.Asterisk.Play ( );
            break;
          case SysIcons.Warning:
            System.Media.SystemSounds.Exclamation.Play ( );
            break;
        }
      }
    }

    #endregion
  }
}
