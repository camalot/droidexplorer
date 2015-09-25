using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DroidExplorer.Bootstrapper.Panels {
	public class StartPanel : WizardPanel {

		internal StartPanel ( ) : base() {

		}

		public StartPanel ( IWizard wizard ) : base(wizard) {

		}

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;

		protected override void InitializeComponent ( ) {
			this.label2 = new System.Windows.Forms.Label ( );
			this.label1 = new System.Windows.Forms.Label ( );
			this.SuspendLayout ( );
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point ( 34, 132 );
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size ( 352, 61 );
			this.label2.TabIndex = 1;
			this.label2.Text = "The Setup Wizard will install all the necessary components to get Droid Explorer " +
					"running on your computer. Click Next to continue or Cancel to exit the Setup Wiz" +
					"ard.";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font ( "Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
			this.label1.Location = new System.Drawing.Point ( 30, 33 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size ( 348, 56 );
			this.label1.TabIndex = 0;
			this.label1.Text = "Welcome to the Droid Explorer Setup Wizard";
			this.Controls.AddRange ( new Control[] { label1, label2 } );
			this.ResumeLayout ( false );

		}
	}
}
