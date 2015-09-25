using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DroidExplorer.Bootstrapper.Panels {
	public class ErrorPanel : WizardPanel {
		private TextBox additionalText;
		private Label label1;
		private PictureBox pictureBox2;
		private Label label2;

		/// <summary>
		/// Initializes a new instance of the <see cref="ErrorPanel"/> class.
		/// </summary>
		internal ErrorPanel ( ) : base() {

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ErrorPanel"/> class.
		/// </summary>
		/// <param name="wizard">The wizard.</param>
		public ErrorPanel ( IWizard wizard ) : base(wizard) {
			this.Controls.AddRange ( new Control[] { this.label1, this.label2, this.additionalText, this.pictureBox2 } );
		}

		/// <summary>
		/// Sets the additional text.
		/// </summary>
		/// <param name="text">The text.</param>
		public override void SetAdditionalText ( string text ) {
			this.additionalText.SetText ( text );
		}

		/// <summary>
		/// Initializes the component.
		/// </summary>
		protected override void InitializeComponent ( ) {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager ( typeof ( ErrorPanel ) );
			this.Size = new System.Drawing.Size ( 416, 315 );
			this.additionalText = new System.Windows.Forms.TextBox ( );
			this.label1 = new System.Windows.Forms.Label ( );
			this.pictureBox2 = new System.Windows.Forms.PictureBox ( );
			this.label2 = new System.Windows.Forms.Label ( );
			( (System.ComponentModel.ISupportInitialize)( this.pictureBox2 ) ).BeginInit ( );
			this.SuspendLayout ( );
			// 
			// additionalText
			// 
			this.additionalText.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
									| System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.additionalText.Location = new System.Drawing.Point ( 31, 139 );
			this.additionalText.Multiline = true;
			this.additionalText.Name = "additionalText";
			this.additionalText.ReadOnly = true;
			this.additionalText.Font = new System.Drawing.Font ( "Tahoma", 8 );
			this.additionalText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.additionalText.Size = new System.Drawing.Size ( 360, 158 );
			this.additionalText.TabIndex = 14;
			// 
			// label1
			// 
			this.label1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.label1.Font = new System.Drawing.Font ( "Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
			this.label1.Location = new System.Drawing.Point ( 80, 9 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size ( 311, 24 );
			this.label1.TabIndex = 11;
			this.label1.Text = "Droid Explorer Setup Wizard encountered an error";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = global::DroidExplorer.Bootstrapper.Properties.Resources.error;
			this.pictureBox2.Location = new System.Drawing.Point ( 26, 9 );
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size ( 48, 48 );
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox2.TabIndex = 13;
			this.pictureBox2.TabStop = false;
			// 
			// label2
			// 
			this.label2.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.label2.Location = new System.Drawing.Point ( 28, 72 );
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size ( 363, 47 );
			this.label2.TabIndex = 12;
			this.label2.Font = new System.Drawing.Font ( "Tahoma", 8 );
			this.label2.Text = resources.GetString ( "label2.Text" );
			( (System.ComponentModel.ISupportInitialize)( this.pictureBox2 ) ).EndInit ( );
			this.ResumeLayout ( false );

		}
	}
}
