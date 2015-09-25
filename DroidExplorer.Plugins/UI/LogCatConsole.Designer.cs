using DroidExplorer.Core.UI.CConsole;

namespace DroidExplorer.Plugins.UI {
  partial class LogCatConsole {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogCatConsole));
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.shell = new ConsoleControl();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.clearToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.AutoScroll = true;
			this.toolStripContainer1.ContentPanel.Controls.Add(this.shell);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(606, 307);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(606, 332);
			this.toolStripContainer1.TabIndex = 2;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
			// 
			// shell
			// 
			this.shell.BackColor = System.Drawing.Color.Black;
			this.shell.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.shell.Dock = System.Windows.Forms.DockStyle.Fill;
			this.shell.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.shell.Location = new System.Drawing.Point(0, 0);
			this.shell.Name = "shell";
			this.shell.Size = new System.Drawing.Size(606, 307);
			this.shell.TabIndex = 0;
			this.shell.Text = "";
			this.shell.OutputProcessor = new DebugConsoleOutputProcessor(this.shell);
			this.shell.IsInputEnabled = false;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripButton,
            this.clearToolStripButton});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(606, 25);
			this.toolStrip1.Stretch = true;
			this.toolStrip1.TabIndex = 0;
			// 
			// saveToolStripButton
			// 
			this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveToolStripButton.Image = global::DroidExplorer.Resources.Images.saveHS;
			this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripButton.Name = "saveToolStripButton";
			this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.saveToolStripButton.Text = "Save";
			this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
			// 
			// clearToolStripButton
			// 
			this.clearToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.clearToolStripButton.Image = global::DroidExplorer.Resources.Images.Symbols_Critical_16xLG;
			this.clearToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.clearToolStripButton.Name = "clearToolStripButton";
			this.clearToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.clearToolStripButton.Text = "Clear Console";
			this.clearToolStripButton.Click += new System.EventHandler(this.clearToolStripButton_Click);
			// 
			// LogCatConsole
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(606, 332);
			this.Controls.Add(this.toolStripContainer1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LogCatConsole";
			this.Text = "LogCat Console";
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton saveToolStripButton;
    private System.Windows.Forms.ToolStripButton clearToolStripButton;
		private ConsoleControl shell;
  }
}