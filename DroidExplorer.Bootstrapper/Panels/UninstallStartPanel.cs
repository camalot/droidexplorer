using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DroidExplorer.Bootstrapper.Panels {
	public class UninstallStartPanel : WizardPanel {
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		
		internal UninstallStartPanel ( ) {

		}

		public UninstallStartPanel ( IWizard wizard ) : base(wizard) {

		}

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
			this.label2.Font = new System.Drawing.Font ( "Tahoma", 8 );
			this.label2.Text = Properties.Resources.UninstallStartMessage;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font ( "Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
			this.label1.Location = new System.Drawing.Point ( 30, 33 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size ( 348, 56 );
			this.label1.TabIndex = 0;
			this.label1.Text = "Welcome to the Droid Explorer Setup Wizard";
			this.Controls.AddRange ( new Control[] { label1, label2 } );
			this.ResumeLayout ( false );
		}

		public override void InitializeWizardPanel ( ) {
			base.InitializeWizardPanel ( );
		}
	}
}
