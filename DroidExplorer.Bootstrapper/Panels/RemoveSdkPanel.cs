using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.ServiceProcess;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Win32;
using DroidExplorer.Bootstrapper.UI;

namespace DroidExplorer.Bootstrapper.Panels {
	public class RemoveSdkPanel : WizardPanel {
		private System.Windows.Forms.Label status;
		private System.Windows.Forms.ProgressBar progress;
		private System.Windows.Forms.Label label1;
		private Thread runThread = null;

		internal RemoveSdkPanel ( ) : base ( ) { }
		public RemoveSdkPanel ( IWizard wizard )
			: base ( wizard ) {
			this.Wizard.CancelRequest += new EventHandler ( Wizard_CancelRequest );
			this.Size = new System.Drawing.Size ( 573, 257 );
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
				} catch ( ThreadAbortException tae ) {

				}
			}
		}

		protected override void InitializeComponent ( ) {
			this.status = new System.Windows.Forms.Label ( );
			this.progress = new System.Windows.Forms.ProgressBar ( );
			this.label1 = new System.Windows.Forms.Label ( );
			this.SuspendLayout ( );
			// 
			// status
			// 
			this.status.Location = new System.Drawing.Point ( 24, 156 );
			this.status.Name = "status";
			this.status.Size = new System.Drawing.Size ( 521, 21 );
			this.status.TabIndex = 2;
			this.status.Text = "[status]";
			this.status.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// progress
			// 
			this.progress.Location = new System.Drawing.Point ( 24, 180 );
			this.progress.Name = "progress";
			this.progress.Size = new System.Drawing.Size ( 521, 23 );
			this.progress.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point ( 21, 34 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size ( 384, 13 );
			this.label1.TabIndex = 0;
			this.label1.Text = "Removing SDK components from your system. This may take a couple minutes.";
			this.Controls.AddRange ( new Control[] { this.label1, this.status, this.progress } );
			this.ResumeLayout ( false );

		}

		/// <summary>
		/// Initializes the wizard panel.
		/// </summary>
		public override void InitializeWizardPanel ( ) {
			runThread = new Thread ( RunRemoval );
			runThread.Start ( );
		}

		private void RunRemoval ( ) {
			this.progress.SetValue ( 0 );
			this.progress.SetMinimum ( 0 );
			status.SetText ( "Gathering removal information (adb processes)..." );

			DirectoryInfo sdkPath = new DirectoryInfo ( Wizard.GetSdkPath ( ) );
			Process[] procs = new Process[] { };

			try {
				procs = Process.GetProcessesByName ( "adb" );
			} catch ( Exception ex ) {
				this.LogWarning ( ex.Message, ex );
			}

			FileInfo[] files = new FileInfo[0];
			if ( sdkPath.Exists && !Wizard.UseExistingSdk ) {
				status.SetText ( "Gathering removal information (sdk files)..." );
				files = sdkPath.GetFiles ( "*", SearchOption.AllDirectories );
			}

			this.progress.SetMaximum ( files.Length + procs.Length + 3 );

			this.LogDebug ( "Stopping service" );
			status.SetText ( "Killing ADB Server..." );
			try {
				Process proc = new Process ( );
				proc.StartInfo = new ProcessStartInfo ( "adb.exe", "kill-server" );
				proc.StartInfo.CreateNoWindow = true;
				proc.StartInfo.ErrorDialog = false;
				proc.Start ( );
			} catch ( Exception ex ) {
				this.LogWarning ( ex.Message, ex );
			}
			progress.IncrementExt ( 1 );

			try {
				this.LogDebug ( "Stopping service" );
				status.SetText ( "Stopping Service..." );
				ServiceController controller = new ServiceController ( "DroidExplorerService" );
				if ( controller.Status != ServiceControllerStatus.Stopped && controller.Status != ServiceControllerStatus.StopPending ) {
					controller.Stop ( );
					controller.WaitForStatus ( ServiceControllerStatus.Stopped );
				}
			} catch ( InvalidOperationException ioe ) {
				if ( !ioe.Message.Contains ( "not found on computer" ) ) {
					// its ok if it doesnt exist, but lets log it.
					this.LogWarning ( ioe.Message, ioe );
				}
			} catch ( Exception ex ) {
				this.LogFatal ( ex.Message, ex );
				Wizard.Error ( ex );
			} finally {
				progress.IncrementExt ( 1 );
			}

			status.SetText ( "Killing all ADB processes" );
			this.LogDebug ( "Killing all running adb processes" );
			foreach ( Process proc in procs ) {
				try {
					proc.Kill ( );
					progress.IncrementExt ( 1 );
				} catch ( Exception ex ) {
					this.LogFatal ( ex.Message, ex );
					Wizard.Error ( ex );
				}
			}

			// only delete if we are using a "local" sdk
			if ( !Wizard.UseExistingSdk ) {
				status.SetText ( "Deleting files..." );
				this.LogDebug ( "Deleting all sdk files" );
				foreach ( FileInfo file in files ) {
					try {
						DirectoryInfo dir = file.Directory;
						status.SetText ( string.Format ( CultureInfo.InvariantCulture, "Deleting {0}...", file.FullName.Substring ( sdkPath.FullName.Length ) ) );
						file.Delete ( );
						this.progress.IncrementExt ( 1 );
						if ( dir.GetFiles ( "*" ).Length == 0 ) {
							dir.Delete ( );
						}
					} catch ( Exception ex ) {

					}
				}

				try {
					if ( sdkPath.Exists ) {
						sdkPath.Delete ( true );
					}
				} catch ( Exception ex ) { }
			}

			try {
				this.LogDebug ( "Removing registry settings" );
				status.SetText ( "Removing Registry Keys..." );
				Registry.LocalMachine.DeleteSubKey ( WizardForm.ROOT_REGISTRY_KEY );
			} catch {
			} finally {
				this.progress.IncrementExt ( 1 );
			}
			Wizard.Next ( );
		}
	}
}
