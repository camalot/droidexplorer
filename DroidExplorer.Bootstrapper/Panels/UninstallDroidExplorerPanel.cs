using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace DroidExplorer.Bootstrapper.Panels {
	public class UninstallDroidExplorerPanel : WizardPanel {
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ProgressBar extractProgress;
		private Thread runThread = null;
		/// <summary>
		/// Initializes a new instance of the <see cref="UninstallDroidExplorerPanel"/> class.
		/// </summary>
		internal UninstallDroidExplorerPanel ( ) {

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UninstallDroidExplorerPanel"/> class.
		/// </summary>
		/// <param name="wizard">The wizard.</param>
		public UninstallDroidExplorerPanel ( IWizard wizard )
			: base ( wizard ) {
			this.Controls.AddRange ( new Control[] { this.label2, this.label3, this.extractProgress } );
			this.Wizard.CancelRequest += new EventHandler ( Wizard_CancelRequest );
		}

		/// <summary>
		/// Handles the CancelRequest event of the Wizard control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void Wizard_CancelRequest ( object sender, EventArgs e ) {
			if ( runThread != null && runThread.IsAlive ) {
				try {
					runThread.Abort ( );
				} catch ( ThreadAbortException ) {

				}
			}
		}

		/// <summary>
		/// Initializes the wizard panel.
		/// </summary>
		public override void InitializeWizardPanel ( ) {
			InstallersPath = Path.Combine ( Path.GetTempPath ( ), "DroidExplorer" );
			if ( !Directory.Exists ( InstallersPath ) ) {
				Directory.CreateDirectory ( InstallersPath );
			}
			runThread = new Thread ( new ThreadStart ( delegate ( ) {
				try {
					ExtractInstallers ( );
				} catch ( Exception ex ) {
					Wizard.Error ( ex );
				}
			} ) );
			runThread.Start ( );
		}

		/// <summary>
		/// Gets or sets the installers path.
		/// </summary>
		/// <value>The installers path.</value>
		private string InstallersPath { get; set; }

		/// <summary>
		/// Extracts the installers.
		/// </summary>
		private void ExtractInstallers ( ) {
			this.LogDebug ( "Extracting installer" );

			using ( Stream strm = this.GetType ( ).Assembly.GetManifestResourceStream ( "DroidExplorer.Bootstrapper.Installs.Setup.msi" ) ) {

				if ( strm != null ) {
					extractProgress.SetMaximum ( (int)strm.Length );
					extractProgress.SetMinimum ( 0 );
					extractProgress.SetValue ( 0 );
					byte[] buffer = new byte[ 2048 ];
					int bread = 0;
					using ( FileStream fs = new FileStream ( Path.Combine ( InstallersPath, "Setup.msi" ), FileMode.Create, FileAccess.Write ) ) {
						while ( ( bread = strm.Read ( buffer, 0, buffer.Length ) ) > 0 ) {
							fs.Write ( buffer, 0, bread );
							extractProgress.IncrementExt ( bread );
						}
						fs.Close ( );
					}
					strm.Close ( );
				}
			}

			LaunchInstaller ( );
			Wizard.Next ( );
		}


		/// <summary>
		/// Launches the installer.
		/// </summary>
		private void LaunchInstaller ( ) {
			this.LogDebug ( "Running installer in \"uninstall\" mode" );

			DirectoryInfo installDir = new DirectoryInfo ( InstallersPath );
			foreach ( var item in installDir.GetFiles ( "*.msi" ) ) {
				Process proc = new Process ( );
				ProcessStartInfo psi = new ProcessStartInfo ( "msiexec.exe", string.Format ( CultureInfo.InvariantCulture, "/x \"{0}\" /passive", item.FullName ) );
				proc.StartInfo = psi;
				proc.Start ( );
				proc.WaitForExit ( );
			}
		}

		/// <summary>
		/// Initializes the component.
		/// </summary>
		protected override void InitializeComponent ( ) {
			this.label3 = new System.Windows.Forms.Label ( );
			this.label2 = new System.Windows.Forms.Label ( );
			this.extractProgress = new System.Windows.Forms.ProgressBar ( );
			this.SuspendLayout ( );
			// 
			// label3
			// 
			this.label3.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
									| System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.label3.Location = new System.Drawing.Point ( 29, 14 );
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size ( 518, 40 );
			this.label3.TabIndex = 3;
			this.label3.Font = new System.Drawing.Font ( "Tahoma", 8 );
			this.label3.Text = "Please wait while Droid Explorer is removed from your machine.";
			// 
			// label2
			// 
			this.label2.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point ( 26, 175 );
			this.label2.Name = "label2";
			this.label2.Font = new System.Drawing.Font ( "Tahoma", 8 );
			this.label2.Size = new System.Drawing.Size ( 94, 13 );
			this.label2.TabIndex = 2;
			this.label2.Text = "Extracting Setup...";
			// 
			// extractProgress
			// 
			this.extractProgress.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.extractProgress.Location = new System.Drawing.Point ( 26, 194 );
			this.extractProgress.Name = "extractProgress";
			this.extractProgress.Size = new System.Drawing.Size ( 521, 23 );
			this.extractProgress.TabIndex = 1;
			this.ResumeLayout ( false );

		}
	}
}
