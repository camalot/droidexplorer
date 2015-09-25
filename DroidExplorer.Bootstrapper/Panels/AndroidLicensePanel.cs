using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DroidExplorer.Bootstrapper.Panels {
	public class AndroidLicensePanel : WizardPanel {
		private System.Windows.Forms.CheckBox acceptEula;
		private System.Windows.Forms.TextBox eula;
		/// <summary>
		/// Initializes a new instance of the <see cref="AndroidLicensePanel"/> class.
		/// </summary>
		internal AndroidLicensePanel ( )
			: base ( ) {

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AndroidLicensePanel"/> class.
		/// </summary>
		/// <param name="wizard">The wizard.</param>
		public AndroidLicensePanel ( IWizard wizard )
			: base ( wizard ) {
			this.Size = new System.Drawing.Size ( 573, 257 );
			this.Controls.AddRange ( new Control[] { this.acceptEula, this.eula } );
		}

		public override void InitializeWizardPanel ( ) {
			this.eula.SetText ( Properties.Resources.AndroidSdkEULA );
			this.acceptEula.CheckedChanged += new EventHandler ( acceptEula_CheckedChanged );
		}

		/// <summary>
		/// Handles the CheckedChanged event of the acceptEula control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void acceptEula_CheckedChanged ( object sender, EventArgs e ) {
			Wizard.NextButton.Enabled = acceptEula.Checked;
		}

		/// <summary>
		/// Initializes the component.
		/// </summary>
		protected override void InitializeComponent ( ) {
			this.acceptEula = new System.Windows.Forms.CheckBox ( );
			this.eula = new System.Windows.Forms.TextBox ( );
			this.SuspendLayout ( );
			// 
			// acceptEula
			// 
			this.acceptEula.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.acceptEula.AutoSize = true;
			this.acceptEula.Location = new System.Drawing.Point ( 26, 219 );
			this.acceptEula.Name = "acceptEula";
			this.acceptEula.Size = new System.Drawing.Size ( 234, 17 );
			this.acceptEula.TabIndex = 1;
			this.acceptEula.Text = "I &accept the terms in the License Agreement";
			this.acceptEula.UseVisualStyleBackColor = true;
			this.acceptEula.Font = new System.Drawing.Font ( "Tahoma", 8 );
			// 
			// eula
			// 
			this.eula.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
									| System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.eula.Location = new System.Drawing.Point ( 26, 22 );
			this.eula.Multiline = true;
			this.eula.Name = "eula";
			this.eula.Font = new System.Drawing.Font ( "Tahoma", 8 );
			this.eula.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.eula.Size = new System.Drawing.Size ( 523, 178 );
			this.eula.TabIndex = 0;
			this.ResumeLayout ( false );

		}

	}
}
