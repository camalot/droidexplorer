using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using DroidExplorer.Core;
using System.Threading;
using DroidExplorer.Core.IO;
using System.Diagnostics;
using System.Globalization;

namespace DroidExplorer.Configuration.UI {
  public class ServiceControllerUIEditor : Control, IUIEditor {
    private delegate void SetControlVisibleOrEnabledDelegate ( Control ctrl, bool status );
    private delegate void SetControlTextDelegate ( Control ctrl, string text );
    private System.Windows.Forms.Button createService;
    private System.Windows.Forms.Button start;
    private System.Windows.Forms.Button pause;
    private System.Windows.Forms.Button stop;
    private System.Windows.Forms.Label serviceName;
    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.Label status;
    private System.Windows.Forms.Label serviceState;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;


    public ServiceControllerUIEditor ( ) {
      InitializeComponent ( );
      try {
        Controller = new ServiceController ( "DroidExplorerService" );
        serviceName.Text = Controller.DisplayName;
        createService.Enabled = false;
        progressBar.Visible = false;
        status.Visible = false;
        RefreshButtons ( );
      } catch ( InvalidOperationException ioe ) {
        if ( string.Compare ( "Service DroidExplorerService was not found on computer", ioe.Message, true ) == 0 ) {
          createService.Enabled = true;
          serviceName.Text = "Droid Explorer Service";
          stop.Enabled = start.Enabled = pause.Enabled = false;
          SetControlText ( serviceState, "Service is not installed" );

        } else {
          this.LogError ( ioe.Message, ioe );
        }
      } catch ( Exception ex ) {
        this.LogError ( ex.Message, ex );
      }
    }

    private ServiceController Controller { get; set; }

    #region IUIEditor Members

    public void SetSourceObject ( object obj ) {
    }

    #endregion

    private void createService_Click ( object sender, EventArgs e ) {
      string serviceFile = Path.Combine ( Path.GetDirectoryName ( typeof ( ServiceControllerUIEditor ).Assembly.Location ), "DroidExplorer.Service.exe" );
      string toolDir = Path.Combine ( Path.Combine ( Environment.GetFolderPath ( Environment.SpecialFolder.System ), ".." ), @"Microsoft.NET\Framework\v2.0.50727" );
      Process proc = new Process ( );

      ProcessStartInfo psi = new ProcessStartInfo ( "installutil.exe", string.Format ( CultureInfo.InvariantCulture, "/i \"{0}\"", serviceFile ) );
      psi.CreateNoWindow = true;
      psi.WorkingDirectory = toolDir;
      psi.WindowStyle = ProcessWindowStyle.Hidden;
      proc.StartInfo = psi;

      proc.Start ( );
      proc.WaitForExit ( );

      Controller = new ServiceController ( "DroidExplorerService" );
      serviceName.Text = Controller.DisplayName;
      createService.Enabled = false;
      RefreshButtons ( );

    }

    private void start_Click ( object sender, EventArgs e ) {
      if ( Controller.Status == ServiceControllerStatus.Stopped ) {
        new Thread ( new ThreadStart ( delegate {
          if ( this.InvokeRequired ) {
            this.Invoke ( new SetControlVisibleOrEnabledDelegate ( this.SetControlVisible ), progressBar, true );
            this.Invoke ( new SetControlVisibleOrEnabledDelegate ( this.SetControlVisible ), status, true );
            this.Invoke ( new SetControlTextDelegate ( this.SetControlText ), status, "Starting Service..." );
          } else {
            SetControlVisible ( progressBar, true );
            SetControlVisible ( status, true );
            SetControlText ( status, "Starting Service..." );
          }
          Controller.Start ( );
          Controller.WaitForStatus ( ServiceControllerStatus.Running, new TimeSpan ( 0, 0, 10 ) );
          RefreshButtons ( );
        } ) ).Start ( );

      }
    }

    private void pause_Click ( object sender, EventArgs e ) {
      if ( Controller.Status == ServiceControllerStatus.Paused && Controller.CanPauseAndContinue ) {
        new Thread ( new ThreadStart ( delegate {
          if ( this.InvokeRequired ) {
            this.Invoke ( new SetControlVisibleOrEnabledDelegate ( this.SetControlVisible ), progressBar, true );
            this.Invoke ( new SetControlVisibleOrEnabledDelegate ( this.SetControlVisible ), status, true );
            this.Invoke ( new SetControlTextDelegate ( this.SetControlText ), status, "Pausing Service..." );
          } else {
            SetControlVisible ( progressBar, true );
            SetControlVisible ( status, true );
            SetControlText ( status, "Pausing Service..." );
          }

          Controller.Continue ( );
          Controller.WaitForStatus ( ServiceControllerStatus.Running, new TimeSpan ( 0, 0, 10 ) );
          RefreshButtons ( );
        } ) ).Start ( );

      }
    }

    private void RefreshButtons ( ) {
      string serviceStateText = string.Format ( "{0} is {1}", Controller.DisplayName, Controller.Status );
      if ( this.InvokeRequired ) {
        this.Invoke ( new SetControlVisibleOrEnabledDelegate ( this.SetControlEnabled ), stop, Controller.Status == ServiceControllerStatus.Running && Controller.CanStop );
        this.Invoke ( new SetControlVisibleOrEnabledDelegate ( this.SetControlEnabled ), start, Controller.Status == ServiceControllerStatus.Stopped );
        this.Invoke ( new SetControlVisibleOrEnabledDelegate ( this.SetControlEnabled ), pause, Controller.Status == ServiceControllerStatus.Running && Controller.CanPauseAndContinue );
        this.Invoke ( new SetControlVisibleOrEnabledDelegate ( this.SetControlVisible ), progressBar, false );
        this.Invoke ( new SetControlVisibleOrEnabledDelegate ( this.SetControlVisible ), status, false );
        this.Invoke ( new SetControlTextDelegate ( this.SetControlText ), serviceState, serviceStateText );
      } else {
        SetControlEnabled ( stop, Controller.Status == ServiceControllerStatus.Running && Controller.CanStop );
        SetControlEnabled ( start, Controller.Status == ServiceControllerStatus.Stopped );
        SetControlEnabled ( pause, Controller.Status == ServiceControllerStatus.Running && Controller.CanPauseAndContinue );
        SetControlVisible ( progressBar, false );
        SetControlVisible ( status, false );
        SetControlText ( serviceState, serviceStateText );
      }
    }

    private void stop_Click ( object sender, EventArgs e ) {
      if ( Controller.Status == ServiceControllerStatus.Running && Controller.CanStop ) {
        new Thread ( new ThreadStart ( delegate {
          if ( this.InvokeRequired ) {
            this.Invoke ( new SetControlVisibleOrEnabledDelegate ( this.SetControlVisible ), progressBar, true );
            this.Invoke ( new SetControlVisibleOrEnabledDelegate ( this.SetControlVisible ), status, true );
            this.Invoke ( new SetControlTextDelegate ( this.SetControlText ), status, "Stopping Service..." );
          } else {
            SetControlVisible ( progressBar, true );
            SetControlVisible ( status, true );
            SetControlText ( status, "Stopping Service..." );
          }
          Controller.Stop ( );
          Controller.WaitForStatus ( ServiceControllerStatus.Stopped, new TimeSpan ( 0, 0, 10 ) );
          RefreshButtons ( );
        } ) ).Start ( );
      }
    }

    private void SetControlEnabled ( Control ctrl, bool enabled ) {
      ctrl.Enabled = enabled;
    }

    private void SetControlVisible ( Control ctrl, bool visible ) {
      ctrl.Visible = visible;
    }

    private void SetControlText ( Control ctrl, string text ) {
      ctrl.Text = text;
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent ( ) {
      this.createService = new System.Windows.Forms.Button ( );
      this.start = new System.Windows.Forms.Button ( );
      this.pause = new System.Windows.Forms.Button ( );
      this.stop = new System.Windows.Forms.Button ( );
      this.serviceName = new System.Windows.Forms.Label ( );
      this.progressBar = new System.Windows.Forms.ProgressBar ( );
      this.status = new System.Windows.Forms.Label ( );
      this.serviceState = new System.Windows.Forms.Label ( );
      this.SuspendLayout ( );
      // 
      // createService
      // 
      this.createService.Location = new System.Drawing.Point ( 287, 9 );
      this.createService.Name = "createService";
      this.createService.Size = new System.Drawing.Size ( 111, 23 );
      this.createService.TabIndex = 0;
      this.createService.Text = "&Create Service";
      this.createService.UseVisualStyleBackColor = true;
      this.createService.Click += new System.EventHandler ( this.createService_Click );
      // 
      // start
      // 
      this.start.Location = new System.Drawing.Point ( 13, 88 );
      this.start.Name = "start";
      this.start.Size = new System.Drawing.Size ( 75, 23 );
      this.start.TabIndex = 1;
      this.start.Text = "&Start";
      this.start.UseVisualStyleBackColor = true;
      this.start.Click += new System.EventHandler ( this.start_Click );
      // 
      // pause
      // 
      this.pause.Location = new System.Drawing.Point ( 94, 88 );
      this.pause.Name = "pause";
      this.pause.Size = new System.Drawing.Size ( 75, 23 );
      this.pause.TabIndex = 2;
      this.pause.Text = "&Pause";
      this.pause.UseVisualStyleBackColor = true;
      this.pause.Click += new System.EventHandler ( this.pause_Click );
      // 
      // stop
      // 
      this.stop.Location = new System.Drawing.Point ( 175, 88 );
      this.stop.Name = "stop";
      this.stop.Size = new System.Drawing.Size ( 75, 23 );
      this.stop.TabIndex = 3;
      this.stop.Text = "Sto&p";
      this.stop.UseVisualStyleBackColor = true;
      this.stop.Click += new System.EventHandler ( this.stop_Click );
      // 
      // serviceName
      // 
      this.serviceName.AutoSize = true;
      this.serviceName.Font = new System.Drawing.Font ( "Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
      this.serviceName.Location = new System.Drawing.Point ( 13, 9 );
      this.serviceName.Name = "serviceName";
      this.serviceName.Size = new System.Drawing.Size ( 124, 20 );
      this.serviceName.TabIndex = 4;
      this.serviceName.Text = "[ServiceName]";
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point ( 13, 166 );
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new System.Drawing.Size ( 385, 23 );
      this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
      this.progressBar.TabIndex = 5;
      // 
      // status
      // 
      this.status.AutoEllipsis = true;
      this.status.Location = new System.Drawing.Point ( 14, 147 );
      this.status.Name = "status";
      this.status.Size = new System.Drawing.Size ( 384, 16 );
      this.status.TabIndex = 6;
      // 
      // serviceState
      // 
      this.serviceState.AutoSize = true;
      this.serviceState.Location = new System.Drawing.Point ( 13, 47 );
      this.serviceState.Name = "serviceState";
      this.serviceState.Size = new System.Drawing.Size ( 124, 13 );
      this.serviceState.TabIndex = 7;
      this.serviceState.Text = "ServiceName is Running";
      // 
      // this
      // 
      this.ClientSize = new System.Drawing.Size ( 411, 263 );
      this.Dock = DockStyle.Fill;
      this.Controls.Add ( this.serviceState );
      this.Controls.Add ( this.status );
      this.Controls.Add ( this.progressBar );
      this.Controls.Add ( this.serviceName );
      this.Controls.Add ( this.stop );
      this.Controls.Add ( this.pause );
      this.Controls.Add ( this.start );
      this.Controls.Add ( this.createService );
      this.ResumeLayout ( false );
      this.PerformLayout ( );

    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose ( bool disposing ) {
      if ( disposing && ( components != null ) ) {
        components.Dispose ( );
      }
      base.Dispose ( disposing );
    }
  }
}
