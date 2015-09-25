using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;

namespace DroidExplorer.Bootstrapper.Panels {
	public class FinishPanel : WizardPanel {
		private CheckBox startDroidExplorer;
		private Label message;
		private Label title;
		private bool initializedPanel = false;
		internal FinishPanel ( ) {

		}

		public FinishPanel ( IWizard wizard )
			: base ( wizard ) {
			this.Size = new System.Drawing.Size ( 416, 315 );
			this.Wizard.NextClick += new EventHandler ( Wizard_NextClick );
			this.Controls.AddRange ( new Control[] { this.title, this.message, this.startDroidExplorer } );
		}

		public override void InitializeWizardPanel ( ) {
			this.title.SetText ( Program.Mode == InstallMode.Uninstall ? Properties.Resources.UninstallFinishedTitle : Properties.Resources.InstallFinishedTitle );
			this.message.SetText ( Program.Mode == InstallMode.Uninstall ? Properties.Resources.UninstallFinshedMessage : Properties.Resources.InstallFinishedMessage );
			initializedPanel = true;
			string file = Path.Combine ( Wizard.GetInstallPath ( ), "DroidExplorer.exe" );
			startDroidExplorer.SetVisible ( File.Exists ( file ) && Program.Mode != InstallMode.Uninstall );
		}

		void Wizard_NextClick ( object sender, EventArgs e ) {
			if ( initializedPanel ) {
				if ( this.startDroidExplorer.Checked ) {
					string file = Path.Combine ( Wizard.GetInstallPath ( ), "DroidExplorer.exe" );
					if ( File.Exists ( file ) ) {
						Process proc = new Process ( );
						ProcessStartInfo psi = new ProcessStartInfo ( file );
						proc.StartInfo = psi;
						proc.Start ( );
					}
				}
			}
		}

		protected override void InitializeComponent ( ) {
			this.startDroidExplorer = new System.Windows.Forms.CheckBox ( );
			this.message = new System.Windows.Forms.Label ( );
			this.title = new System.Windows.Forms.Label ( );
			this.SuspendLayout ( );
			// 
			// startDroidExplorer
			// 
			this.startDroidExplorer.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.startDroidExplorer.AutoSize = true;
			this.startDroidExplorer.Checked = true;
			this.startDroidExplorer.CheckState = System.Windows.Forms.CheckState.Checked;
			this.startDroidExplorer.Location = new System.Drawing.Point ( 28, 284 );
			this.startDroidExplorer.Name = "startDroidExplorer";
			this.startDroidExplorer.Size = new System.Drawing.Size ( 131, 17 );
			this.startDroidExplorer.Font = new System.Drawing.Font ( "Tahoma", 8 );
			this.startDroidExplorer.TabIndex = 5;
			this.startDroidExplorer.Text = "Launch Droid Explorer";
			this.startDroidExplorer.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.message.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.message.Location = new System.Drawing.Point ( 25, 76 );
			this.message.Name = "label2";
			this.message.Size = new System.Drawing.Size ( 370, 53 );
			this.message.Font = new System.Drawing.Font ( "Tahoma", 8 );
			this.message.TabIndex = 4;
			this.message.Text = "";
			// 
			// label1
			// 
			this.title.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.title.Font = new System.Drawing.Font ( "Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
			this.title.Location = new System.Drawing.Point ( 21, 13 );
			this.title.Name = "label1";
			this.title.Size = new System.Drawing.Size ( 374, 24 );
			this.title.TabIndex = 3;
			this.title.Text = "Installation Completed Successfully";
			this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.ResumeLayout ( false );

		}
	}
}
