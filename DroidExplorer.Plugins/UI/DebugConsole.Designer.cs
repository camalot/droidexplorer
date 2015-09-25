using DroidExplorer.Core.UI.CConsole;
namespace DroidExplorer.Plugins.UI {
  partial class DebugConsole {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose ( bool disposing ) {
      if ( disposing && ( components != null ) ) {
        components.Dispose ( );
      }
      base.Dispose ( disposing );
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent ( ) {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager ( typeof ( DebugConsole ) );
      this.console = new ConsoleControl ( );
      this.toolStrip1 = new System.Windows.Forms.ToolStrip ( );
      this.debugToolStripButton = new System.Windows.Forms.ToolStripButton ( );
      this.infoToolStripButton = new System.Windows.Forms.ToolStripButton ( );
      this.warnToolStripButton = new System.Windows.Forms.ToolStripButton ( );
      this.errorToolStripButton = new System.Windows.Forms.ToolStripButton ( );
      this.clearToolStripButton = new System.Windows.Forms.ToolStripButton ( );
      this.saveToolStripButton = new System.Windows.Forms.ToolStripButton ( );
      this.toolStrip1.SuspendLayout ( );
      this.SuspendLayout ( );
      // 
      // console
      // 
      this.console.BackColor = System.Drawing.Color.Black;
      this.console.Dock = System.Windows.Forms.DockStyle.Fill;
      this.console.Font = new System.Drawing.Font ( "Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0 );
      this.console.ForeColor = System.Drawing.Color.FromArgb (0,0,192,0);
      this.console.Location = new System.Drawing.Point ( 0, 25 );
      this.console.Name = "console";
      this.console.Size = new System.Drawing.Size ( 646, 282 );
      this.console.TabIndex = 0;
			this.console.IsInputEnabled = false;
			this.console.OutputProcessor = new DebugConsoleOutputProcessor(this.console);
      // 
      // toolStrip1
      // 
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange ( new System.Windows.Forms.ToolStripItem[ ] {
            this.errorToolStripButton,
            this.warnToolStripButton,
            this.infoToolStripButton,
            this.debugToolStripButton,
            this.saveToolStripButton,
            this.clearToolStripButton} );
      this.toolStrip1.Location = new System.Drawing.Point ( 0, 0 );
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new System.Windows.Forms.Padding ( 5, 0, 5, 0 );
      this.toolStrip1.Size = new System.Drawing.Size ( 646, 25 );
      this.toolStrip1.TabIndex = 1;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // debugToolStripButton
      // 
      this.debugToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.debugToolStripButton.Checked = true;
      this.debugToolStripButton.CheckOnClick = true;
      this.debugToolStripButton.CheckState = System.Windows.Forms.CheckState.Checked;
      this.debugToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.debugToolStripButton.Image = global::DroidExplorer.Resources.Images.bug;
      this.debugToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.debugToolStripButton.Margin = new System.Windows.Forms.Padding ( 5, 1, 0, 2 );
      this.debugToolStripButton.Name = "debugToolStripButton";
      this.debugToolStripButton.Size = new System.Drawing.Size ( 23, 22 );
      this.debugToolStripButton.Text = "Debug";
      this.debugToolStripButton.CheckedChanged += new System.EventHandler ( this.filters_CheckedChanged );
      // 
      // infoToolStripButton
      // 
      this.infoToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.infoToolStripButton.Checked = true;
      this.infoToolStripButton.CheckOnClick = true;
      this.infoToolStripButton.CheckState = System.Windows.Forms.CheckState.Checked;
      this.infoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.infoToolStripButton.Image = global::DroidExplorer.Resources.Images.Information_blue_6227_16x16_exp;
      this.infoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.infoToolStripButton.Margin = new System.Windows.Forms.Padding ( 3, 1, 0, 2 );
      this.infoToolStripButton.Name = "infoToolStripButton";
      this.infoToolStripButton.Size = new System.Drawing.Size ( 23, 22 );
      this.infoToolStripButton.Text = "Info";
      this.infoToolStripButton.CheckedChanged += new System.EventHandler ( this.filters_CheckedChanged );
      // 
      // warnToolStripButton
      // 
      this.warnToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.warnToolStripButton.Checked = true;
      this.warnToolStripButton.CheckOnClick = true;
      this.warnToolStripButton.CheckState = System.Windows.Forms.CheckState.Checked;
      this.warnToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.warnToolStripButton.Image = global::DroidExplorer.Resources.Images.Warning_yellow_7231_16x16_exp;
      this.warnToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.warnToolStripButton.Margin = new System.Windows.Forms.Padding ( 3, 1, 0, 2 );
      this.warnToolStripButton.Name = "warnToolStripButton";
      this.warnToolStripButton.Size = new System.Drawing.Size ( 23, 22 );
      this.warnToolStripButton.Text = "Warning";
      this.warnToolStripButton.CheckedChanged += new System.EventHandler ( this.filters_CheckedChanged );
      // 
      // errorToolStripButton
      // 
      this.errorToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.errorToolStripButton.Checked = true;
      this.errorToolStripButton.CheckOnClick = true;
      this.errorToolStripButton.CheckState = System.Windows.Forms.CheckState.Checked;
      this.errorToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.errorToolStripButton.Image = global::DroidExplorer.Resources.Images.Error_red_16x16_exp;
      this.errorToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.errorToolStripButton.Margin = new System.Windows.Forms.Padding ( 3, 1, 0, 2 );
      this.errorToolStripButton.Name = "errorToolStripButton";
      this.errorToolStripButton.Size = new System.Drawing.Size ( 23, 22 );
      this.errorToolStripButton.Text = "Errors";
      this.errorToolStripButton.CheckedChanged += new System.EventHandler ( this.filters_CheckedChanged );
      // 
      // clearToolStripButton
      // 
      this.clearToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.clearToolStripButton.Image = global::DroidExplorer.Resources.Images.Symbols_Critical_16xLG;
      this.clearToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.clearToolStripButton.Name = "clearToolStripButton";
      this.clearToolStripButton.Size = new System.Drawing.Size ( 23, 22 );
      this.clearToolStripButton.Text = "Clear Log";
      this.clearToolStripButton.Click += new System.EventHandler ( this.clearToolStripButton_Click );
      // 
      // saveToolStripButton
      // 
      this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveToolStripButton.Image = global::DroidExplorer.Resources.Images.saveHS;
      this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.saveToolStripButton.Name = "saveToolStripButton";
      this.saveToolStripButton.Size = new System.Drawing.Size ( 23, 22 );
      this.saveToolStripButton.Text = "Save";
      this.saveToolStripButton.Click += new System.EventHandler ( this.saveToolStripButton_Click );
      // 
      // DebugConsole
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size ( 646, 307 );
      this.Controls.Add ( this.console );
      this.Controls.Add ( this.toolStrip1 );
      this.Icon = ( ( System.Drawing.Icon )( resources.GetObject ( "$this.Icon" ) ) );
      this.Location = new System.Drawing.Point ( 0, 0 );
      this.Name = "DebugConsole";
      this.ShowInTaskbar = false;
      this.Text = "Droid Explorer Debug Console";
      this.toolStrip1.ResumeLayout ( false );
      this.toolStrip1.PerformLayout ( );
      this.ResumeLayout ( false );
      this.PerformLayout ( );

    }

    #endregion

    //private System.Windows.Forms.RichTextBox console;
		private ConsoleControl console;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton debugToolStripButton;
    private System.Windows.Forms.ToolStripButton infoToolStripButton;
    private System.Windows.Forms.ToolStripButton warnToolStripButton;
    private System.Windows.Forms.ToolStripButton errorToolStripButton;
    private System.Windows.Forms.ToolStripButton clearToolStripButton;
    private System.Windows.Forms.ToolStripButton saveToolStripButton;
  }
}