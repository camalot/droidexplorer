using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;
using System.ServiceProcess;

namespace DroidExplorer.Bootstrapper.Panels {
	public class RunningProcessesPanel : WizardPanel {
		private System.Windows.Forms.Label status;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label label1;


		/// <summary>
		/// Initializes a new instance of the <see cref="RunningProcessesPanel"/> class.
		/// </summary>
		internal RunningProcessesPanel ( )
			: base ( ) {

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RunningProcessesPanel"/> class.
		/// </summary>
		/// <param name="wizard">The wizard.</param>
		public RunningProcessesPanel ( IWizard wizard )
			: base ( wizard ) {

			Wizard.CancelRequest += new EventHandler ( Wizard_CancelRequest );

		}

		private Thread RunningThread { get; set; }

		void Wizard_CancelRequest ( object sender, EventArgs e ) {
			if ( RunningThread != null && RunningThread.IsAlive ) {
				try {
					RunningThread.Abort ( );
				} catch ( ThreadAbortException ) { }
			}
		}

		public override void InitializeWizardPanel ( ) {

			if ( this.GetRunningAdbProcesses ( ).Count != 0 ) {
				this.Wizard.Next ( );
			} else {
				RunningThread = new Thread ( new ThreadStart ( KillAdb ) );
				RunningThread.Start ( );
			}
		}

		private void KillAdb ( ) {
			Thread.Sleep ( 200 );
			try {
				ServiceController Controller = new ServiceController ( StartServicePanel.SERVICE_NAME );
				if ( Controller.Status == ServiceControllerStatus.Running ) {
					this.status.SetText ( "Attempting to stop service..." );
					Controller.Stop ( );
					Controller.WaitForStatus ( ServiceControllerStatus.Stopped, new TimeSpan ( 0, 0, StartServicePanel.SERVICE_ACTION_TIMEOUT ) );
				}
			} catch ( InvalidOperationException ioe ) {
				this.LogWarning ( ioe.Message, ioe );
			}

			int tryCount = 0;
			bool isRunning = this.GetRunningAdbProcesses ( ).Count > 0;
			while ( isRunning && tryCount < 5 ) {
				List<Process> procs = this.GetRunningAdbProcesses ( );
				foreach ( Process proc in procs ) {
					if ( !proc.HasExited ) {
						try {
							this.status.SetText ( "Stopping ADB" );
							Process pkill = Process.Start ( proc.StartInfo.FileName, "kill-server" );
							pkill.WaitForExit ( );
							this.status.SetText ( "Killing ADB process" );
							proc.Kill ( );
							this.status.SetText ( "Wating for ADB process to exit" );
							proc.WaitForExit ( );
						} catch ( Win32Exception ) {
							throw;
						} catch ( Exception ex ) {
							this.LogWarning ( ex.Message, ex );
						}
					}
				}
				tryCount++;
				isRunning = this.GetRunningAdbProcesses ( ).Count > 0;
				if ( isRunning && tryCount < 5 ) {
					this.SetText ( "ADB still running, attempting to kill the processes." );
				}
			}

			Thread.Sleep ( 1000 );
			isRunning = this.GetRunningAdbProcesses ( ).Count > 0;
			if ( isRunning && tryCount > 5 ) {
				Wizard.Error ( new InvalidOperationException ( "Unable to stop ADB processes." ) );
			} else {
				Wizard.Next ( );
			}
		}

		private List<Process> GetRunningAdbProcesses ( ) {
			List<Process> procs = new List<Process> ( );
			foreach ( var item in Process.GetProcessesByName ( "adb.exe" ) ) {
				if ( !item.HasExited ) {
					procs.Add ( item );
				}
			}
			return procs;
		}

		protected override void InitializeComponent ( ) {
			this.status = new System.Windows.Forms.Label ( );
			this.progressBar1 = new System.Windows.Forms.ProgressBar ( );
			this.label1 = new System.Windows.Forms.Label ( );
			this.SuspendLayout ( );
			// 
			// status
			// 
			this.status.AutoSize = true;
			this.status.Location = new System.Drawing.Point ( 15, 135 );
			this.status.Name = "status";
			this.status.Size = new System.Drawing.Size ( 41, 13 );
			this.status.TabIndex = 2;
			this.status.Text = "";
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.progressBar1.Location = new System.Drawing.Point ( 13, 154 );
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size ( 548, 23 );
			this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar1.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.label1.Location = new System.Drawing.Point ( 12, 13 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size ( 549, 50 );
			this.label1.TabIndex = 0;

			this.label1.Text = "There are adb processes still running. The setup must stop these processes before " +
				"installation can continue. Please wait while adb is stopped.";

			this.Controls.AddRange ( new System.Windows.Forms.Control[] { this.label1, this.progressBar1, this.status } );
			this.ResumeLayout ( false );

		}
	}
}
