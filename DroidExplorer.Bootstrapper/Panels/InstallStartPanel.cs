using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DroidExplorer.Bootstrapper.Panels {
	public class InstallStartPanel : WizardPanel {

		internal InstallStartPanel ( ) : base() {

		}

		public InstallStartPanel ( IWizard wizard ) : base(wizard) {
			// if sdk only, but not installed, switch to install mode
			if ( Program.Mode == InstallMode.SdkOnly ) {
				string sdkPath = Wizard.GetSdkPath ( );
				if ( string.IsNullOrEmpty ( sdkPath ) ) {
					this.LogWarning ( "Cannot install SDK only, Droid Explorer is not currently installed." );
					this.LogInfo ( "Switching mode to Install" );
					Program.Mode = InstallMode.Install;
				}
			}
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
			this.label2.Font = new System.Drawing.Font ( "Tahoma", 8 );
			this.label2.Text = Properties.Resources.InstallStartMessage;
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
	}
}
