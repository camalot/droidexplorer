using DroidExplorer.Core.UI.CConsole;
namespace DroidExplorer.Plugins.UI {
  partial class ShellConsole {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShellConsole));
			this.shell = new DroidExplorer.Core.UI.CConsole.ConsoleControl();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.rowLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.columnLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.deviceLabel = new System.Windows.Forms.ToolStripStatusLabel();

			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// shell
			// 
			this.shell.BackColor = System.Drawing.Color.Black;
			this.shell.CaretColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.shell.DefaultForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.shell.Dock = System.Windows.Forms.DockStyle.Fill;
			this.shell.ErrorColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.shell.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.shell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.shell.InputStart = 0;
			this.shell.IsInputEnabled = true;
			this.shell.Location = new System.Drawing.Point(0, 0);
			this.shell.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.shell.Name = "shell";
			this.shell.SelectionBackColor = System.Drawing.Color.Black;
			this.shell.SendKeyboardCommandsToProcess = false;
			this.shell.ShowDiagnostics = false;
			this.shell.Size = new System.Drawing.Size(598, 273);
			this.shell.TabIndex = 0;
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.BottomToolStripPanel
			// 
			this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.AutoScroll = true;
			this.toolStripContainer1.ContentPanel.Controls.Add(this.shell);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(598, 273);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(598, 320);
			this.toolStripContainer1.TabIndex = 1;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// statusStrip1
			// 
			this.statusStrip1.AutoSize = false;
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rowLabel,
            this.columnLabel,
						this.deviceLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(598, 22);
			this.statusStrip1.TabIndex = 0;
			// 
			// rowLabel
			// 
			this.rowLabel.AutoSize = false;
			this.rowLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.rowLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
			this.rowLabel.Margin = new System.Windows.Forms.Padding(1, 3, 0, 1);
			this.rowLabel.Name = "rowLabel";
			this.rowLabel.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.rowLabel.Size = new System.Drawing.Size(100, 18);
			this.rowLabel.Text = "Row: 0";
			this.rowLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.rowLabel.Visible = false;
			// 
			// columnLabel
			// 
			this.columnLabel.AutoSize = false;
			this.columnLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.columnLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
			this.columnLabel.Margin = new System.Windows.Forms.Padding(1, 3, 0, 1);
			this.columnLabel.Name = "columnLabel";
			this.columnLabel.Size = new System.Drawing.Size(100, 18);
			this.columnLabel.Text = "Column: 0";
			this.columnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.columnLabel.Visible = false;
			// 
			// deviceLabel
			// 
			this.deviceLabel.AutoSize = true;
			this.deviceLabel.Name = "deviceLabel";
			this.deviceLabel.Margin = new System.Windows.Forms.Padding(1, 3, 0, 1);
			this.deviceLabel.Size = new System.Drawing.Size(100, 18);
			this.deviceLabel.Spring = true;
			this.deviceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ShellConsole
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(598, 320);
			this.Controls.Add(this.toolStripContainer1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(0, 0);
			this.Name = "ShellConsole";
			this.Text = "Droid Explorer Shell";
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);

    }

    #endregion

    //private System.Windows.Forms.RichTextBox shell;
		private ConsoleControl shell;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel rowLabel;
    private System.Windows.Forms.ToolStripStatusLabel columnLabel;
		private System.Windows.Forms.ToolStripStatusLabel deviceLabel;
	}
}