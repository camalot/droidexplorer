namespace DroidExplorer.Bootstrapper.UI {
	partial class WizardForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager ( typeof ( WizardForm ) );
			this.bottomPanel = new System.Windows.Forms.Panel ( );
			this.back = new System.Windows.Forms.Button ( );
			this.next = new System.Windows.Forms.Button ( );
			this.cancel = new System.Windows.Forms.Button ( );
			this.leftPanel = new System.Windows.Forms.Panel ( );
			this.contentPanel = new System.Windows.Forms.Panel ( );
			this.topPanel = new System.Windows.Forms.Panel ( );
			this.subtitle = new System.Windows.Forms.Label ( );
			this.title = new System.Windows.Forms.Label ( );
			this.pictureBox1 = new System.Windows.Forms.PictureBox ( );
			this.bottomPanel.SuspendLayout ( );
			this.topPanel.SuspendLayout ( );
			( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).BeginInit ( );
			this.SuspendLayout ( );
			// 
			// bottomPanel
			// 
			this.bottomPanel.Controls.Add ( this.back );
			this.bottomPanel.Controls.Add ( this.next );
			this.bottomPanel.Controls.Add ( this.cancel );
			this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomPanel.Font = new System.Drawing.Font ( "Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
			this.bottomPanel.Location = new System.Drawing.Point ( 0, 315 );
			this.bottomPanel.Name = "bottomPanel";
			this.bottomPanel.Size = new System.Drawing.Size ( 573, 46 );
			this.bottomPanel.TabIndex = 0;
			// 
			// back
			// 
			this.back.Enabled = false;
			this.back.Font = new System.Drawing.Font ( "Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
			this.back.Location = new System.Drawing.Point ( 303, 11 );
			this.back.Name = "back";
			this.back.Size = new System.Drawing.Size ( 75, 23 );
			this.back.TabIndex = 2;
			this.back.Text = "&Back";
			this.back.UseVisualStyleBackColor = true;
			this.back.Click += new System.EventHandler ( this.back_Click );
			// 
			// next
			// 
			this.next.Font = new System.Drawing.Font ( "Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
			this.next.Location = new System.Drawing.Point ( 384, 11 );
			this.next.Name = "next";
			this.next.Size = new System.Drawing.Size ( 75, 23 );
			this.next.TabIndex = 1;
			this.next.Text = "&Next";
			this.next.UseVisualStyleBackColor = true;
			this.next.Click += new System.EventHandler ( this.next_Click );
			// 
			// cancel
			// 
			this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancel.Font = new System.Drawing.Font ( "Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
			this.cancel.Location = new System.Drawing.Point ( 486, 11 );
			this.cancel.Name = "cancel";
			this.cancel.Size = new System.Drawing.Size ( 75, 23 );
			this.cancel.TabIndex = 0;
			this.cancel.Text = "&Cancel";
			this.cancel.UseVisualStyleBackColor = true;
			this.cancel.Click += new System.EventHandler ( this.cancel_Click );
			// 
			// leftPanel
			// 
			this.leftPanel.BackgroundImage = global::DroidExplorer.Bootstrapper.Properties.Resources.UIDialog;
			this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.leftPanel.Font = new System.Drawing.Font ( "Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
			this.leftPanel.Location = new System.Drawing.Point ( 0, 58 );
			this.leftPanel.Name = "leftPanel";
			this.leftPanel.Size = new System.Drawing.Size ( 157, 257 );
			this.leftPanel.TabIndex = 1;
			this.leftPanel.Visible = false;
			// 
			// contentPanel
			// 
			this.contentPanel.BackColor = System.Drawing.SystemColors.Control;
			this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.contentPanel.Font = new System.Drawing.Font ( "Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
			this.contentPanel.Location = new System.Drawing.Point ( 157, 58 );
			this.contentPanel.Name = "contentPanel";
			this.contentPanel.Size = new System.Drawing.Size ( 416, 257 );
			this.contentPanel.TabIndex = 2;
			// 
			// topPanel
			// 
			this.topPanel.BackColor = System.Drawing.SystemColors.Window;
			this.topPanel.Controls.Add ( this.subtitle );
			this.topPanel.Controls.Add ( this.title );
			this.topPanel.Controls.Add ( this.pictureBox1 );
			this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.topPanel.Font = new System.Drawing.Font ( "Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
			this.topPanel.Location = new System.Drawing.Point ( 0, 0 );
			this.topPanel.Name = "topPanel";
			this.topPanel.Padding = new System.Windows.Forms.Padding ( 0, 1, 0, 1 );
			this.topPanel.Size = new System.Drawing.Size ( 573, 58 );
			this.topPanel.TabIndex = 3;
			// 
			// subtitle
			// 
			this.subtitle.Font = new System.Drawing.Font ( "Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
			this.subtitle.Location = new System.Drawing.Point ( 37, 35 );
			this.subtitle.Name = "subtitle";
			this.subtitle.Size = new System.Drawing.Size ( 451, 18 );
			this.subtitle.TabIndex = 2;
			this.subtitle.Text = "[step sub title]";
			// 
			// title
			// 
			this.title.Font = new System.Drawing.Font ( "Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
			this.title.Location = new System.Drawing.Point ( 21, 7 );
			this.title.Name = "title";
			this.title.Size = new System.Drawing.Size ( 484, 23 );
			this.title.TabIndex = 1;
			this.title.Text = "[Step Title]";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::DroidExplorer.Bootstrapper.Properties.Resources.setup_icon;
			this.pictureBox1.Location = new System.Drawing.Point ( 511, 1 );
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size ( 62, 54 );
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// WizardForm
			// 
			this.AcceptButton = this.next;
			this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size ( 573, 361 );
			this.Controls.Add ( this.contentPanel );
			this.Controls.Add ( this.leftPanel );
			this.Controls.Add ( this.bottomPanel );
			this.Controls.Add ( this.topPanel );
			this.Font = new System.Drawing.Font ( "Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ( (System.Drawing.Icon)( resources.GetObject ( "$this.Icon" ) ) );
			this.MaximizeBox = false;
			this.Name = "WizardForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Droid Explorer Setup";
			this.bottomPanel.ResumeLayout ( false );
			this.topPanel.ResumeLayout ( false );
			( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).EndInit ( );
			this.ResumeLayout ( false );

		}

		#endregion

		private System.Windows.Forms.Panel bottomPanel;
		private System.Windows.Forms.Panel leftPanel;
		private System.Windows.Forms.Panel contentPanel;
		private System.Windows.Forms.Button back;
		private System.Windows.Forms.Button next;
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.Panel topPanel;
		private System.Windows.Forms.Label subtitle;
		private System.Windows.Forms.Label title;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}

