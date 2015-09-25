using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Threading;

namespace DroidExplorer.Bootstrapper.Panels {
	public class StartServicePanel : WizardPanel {
		public const string SERVICE_NAME = "DroidExplorerService";
		public const int SERVICE_ACTION_TIMEOUT = 60;
		private Label label2;
		private Label label1;
		private Label startStatus;
		private ProgressBar startProgress;
		private Thread runThread = null;

		internal StartServicePanel ( ) {

		}

		public StartServicePanel ( IWizard wizard )
			: base ( wizard ) {
			this.Controls.AddRange ( new Control[] { this.label1, this.label2, this.startProgress, this.startStatus } );
			Wizard.CancelRequest += new EventHandler ( Wizard_CancelRequest );
		}

		void Wizard_CancelRequest ( object sender, EventArgs e ) {
			if ( runThread != null && runThread.IsAlive ) {
				try {
					runThread.Abort ( );
				} catch ( ThreadAbortException tae ) { }
			}
		}

		public override void InitializeWizardPanel ( ) {
			runThread = new Thread ( new ThreadStart ( StartService ) );
			runThread.Start ( );
		}

		private void StartService ( ) {
			try {
				Thread.Sleep ( 200 );
				Controller = new ServiceController ( StartServicePanel.SERVICE_NAME );
				if ( Controller.Status == ServiceControllerStatus.Running ) {
					this.LogDebug ( "Attempting to stop service." );
					this.startStatus.SetText ( "Attempting to stop service..." );
					Controller.Stop ( );
					Controller.WaitForStatus ( ServiceControllerStatus.Stopped, new TimeSpan ( 0, 0, SERVICE_ACTION_TIMEOUT ) );
				}

				if ( Controller.Status == ServiceControllerStatus.Stopped ) {
					this.startStatus.SetText ( "Attempting to start service..." );
					this.LogDebug ( "Attempting to start service." );
					Controller.Start ( );
					Controller.WaitForStatus ( ServiceControllerStatus.StartPending, new TimeSpan ( 0, 0, SERVICE_ACTION_TIMEOUT ) );
          Controller.WaitForStatus( ServiceControllerStatus.Running, new TimeSpan ( 0, 0, SERVICE_ACTION_TIMEOUT ) );
				}

				Wizard.Next ( );
			} catch ( InvalidOperationException ioe ) {
				if ( ioe.Message.Contains ( "not found on computer" ) ) {
					try {
						// the service was not installed so we can skip this
						Wizard.Next ( );
						return;
					} catch ( Exception ex1 ) {
						this.LogError ( ex1.Message, ex1 );
						Wizard.Error ( ex1 );
					}
				} else {
					this.LogError ( ioe.Message, ioe );
					Wizard.Error ( ioe );
				}
			} catch ( Exception ex ) {
				this.LogError ( ex.Message, ex );
				Wizard.Error ( ex );
			}
		}

		protected override void InitializeComponent ( ) {
			this.label2 = new System.Windows.Forms.Label ( );
			this.label1 = new System.Windows.Forms.Label ( );
			this.startStatus = new System.Windows.Forms.Label ( );
			this.startProgress = new System.Windows.Forms.ProgressBar ( );
			this.SuspendLayout ( );
			// 
			// label2
			// 
			this.label2.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.label2.Location = new System.Drawing.Point ( 25, 72 );
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size ( 522, 35 );
			this.label2.Font = new System.Drawing.Font ( "Tahoma", 8 );
			this.label2.TabIndex = 3;
			this.label2.Text = "Please wait while the Droid Explorer service is configured and started. Once star" +
					"ted, click the Next button to continue.";
			// 
			// label1
			// 
			this.label1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.label1.Font = new System.Drawing.Font ( "Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
			this.label1.Location = new System.Drawing.Point ( 25, 12 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size ( 525, 38 );
			this.label1.TabIndex = 2;
			this.label1.Text = "Starting Droid Explorer Service";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// startStatus
			// 
			this.startStatus.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.startStatus.Location = new System.Drawing.Point ( 22, 170 );
			this.startStatus.Name = "startStatus";
			this.startStatus.Size = new System.Drawing.Size ( 525, 16 );
			this.startStatus.TabIndex = 1;
			this.startStatus.Font = new System.Drawing.Font ( "Tahoma", 8 );
			this.startStatus.Text = "Attempting to start service...";
			// 
			// startProgress
			// 
			this.startProgress.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.startProgress.Location = new System.Drawing.Point ( 22, 189 );
			this.startProgress.Style = ProgressBarStyle.Marquee;
			this.startProgress.Name = "startProgress";
			this.startProgress.Size = new System.Drawing.Size ( 528, 23 );
			this.startProgress.TabIndex = 0;
			this.ResumeLayout ( false );

		}

		private ServiceController Controller { get; set; }
	}
}
