using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DroidExplorer.Bootstrapper.Panels {
	public class CancelPanel : WizardPanel {
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;

		internal CancelPanel ( ) : base(){

		}

		public CancelPanel ( IWizard wizard ) : base(wizard){
			this.Size = new System.Drawing.Size ( 416, 315 );
		}

		protected override void InitializeComponent ( ) {
			this.label2 = new System.Windows.Forms.Label ( );
			this.label1 = new System.Windows.Forms.Label ( );
			this.SuspendLayout ( );
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point ( 30, 86 );
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size ( 363, 72 );
			this.label2.TabIndex = 1;
			this.label2.Font = new System.Drawing.Font ( "Tahoma", 8 );
			this.label2.Text = "Droid Explorer setup was intrrupted. Your system has not been modified. To instal" +
					"l this program at a later time, please run the installation again. Click the Fin" +
					"ish button to exit the Setup Wizard.";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point ( 49, 26 );
			this.label1.Name = "label1";
			this.label1.Font = new System.Drawing.Font ( "Tahoma", 12 );
			this.label1.Size = new System.Drawing.Size ( 322, 20 );
			this.label1.TabIndex = 0;
			this.label1.Text = "Droid Explorer Setup Wizard was interrupted";
			this.Controls.AddRange ( new Control[] { label1, label2 } );
			this.ResumeLayout ( false );

		}
	}
}
