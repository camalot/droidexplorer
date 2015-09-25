using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Core;
using System.Threading;
using DroidExplorer.Core.UI.Exceptions;
using System.Globalization;
using DroidExplorer.Core.Plugins;
using Camalot.Common.Extensions;

namespace DroidExplorer.Core.UI {
	public partial class InstallDialog : PluginForm {
		private delegate void SetControlVisibleStateDelegate ( Control ctrl, bool visible );

		public enum InstallMode {
			Install,
			Uninstall
		}

		public InstallDialog (IPluginHost pluginHost, InstallMode mode, AaptBrandingCommandResult apkInfo ) : base(pluginHost) {
			InitializeComponent ( );
			this.Mode = mode;
			this.ApkInformation = apkInfo;
			string name = string.Format ( "{0}{1}", !string.IsNullOrEmpty ( ApkInformation.Label ) ? ApkInformation.Label : ApkInformation.Package,
				string.IsNullOrEmpty ( ApkInformation.Version ) ? string.Empty : string.Format ( " {0}", ApkInformation.Version ) );
			this.title.Text = string.Format ( Properties.Resources.InstallDialogWelcomeTitle, name, Mode );
			information.Text = string.Format ( Properties.Resources.InstallDialogInformationLabel, name, mode );
			this.Text = string.Format ( "{0} {1}", mode, name );
			this.perform.Text = string.Format ( "&{0}", Mode.ToString ( ) );

			this.permissions.Visible = this.permissionsLabel.Visible = mode == InstallMode.Install;
			if ( mode == InstallMode.Install ) {
				this.permissions.DataSource = PluginHost.CommandRunner.GetLocalApkPermissions ( string.IsNullOrEmpty ( apkInfo.LocalApk ) ? apkInfo.DevicePath : apkInfo.LocalApk );
			}

			this.icon.Image = PluginHost.CommandRunner.GetLocalApkIconImage ( apkInfo.LocalApk );
		}

		public AaptBrandingCommandResult ApkInformation { get; private set; }
		public InstallMode Mode { get; private set; }
		private void perform_Click ( object sender, EventArgs e ) {
			this.progress.Visible = true;
			this.cancel.Enabled = false;
			this.perform.Enabled = false;
			try {
				new Thread ( delegate ( ) {
					bool success = true;
					Exception error = null;
					try {
						if ( Mode == InstallMode.Install ) {
							success = PluginHost.CommandRunner.InstallApk ( PluginHost.CommandRunner.DefaultDevice, ApkInformation.LocalApk );
						} else {
							success = PluginHost.CommandRunner.UninstallApk ( PluginHost.CommandRunner.DefaultDevice, ApkInformation.Package );
						}
					} catch ( Exception ex ) {
						success = false;
						error = ex;
					}

					string name = !string.IsNullOrEmpty ( ApkInformation.Label ) ? ApkInformation.Label : ApkInformation.Package;
					if ( success ) {
						this.DialogResult = DialogResult.OK;
					} else {
						string errorMessage = string.Empty;
						if ( error != null ) {
							errorMessage = error.Message;
						}

						this.DialogResult = DialogResult.Abort;
						/*if ( this.InvokeRequired ) {
							this.Invoke ( new GenericDelegate ( this.Close ) );
						} else {
							this.Close ();
						}*/

						throw new InstallUninstallException ( Mode, errorMessage, string.Format ( CultureInfo.InvariantCulture, Properties.Resources.InstallDialogGenericErrorMessage, Mode.ToString ( ), name ) );

					}
				} ).Start ( );
			} catch ( InstallUninstallException ex ) {
				MessageBox.Show ( this, string.Format ( "There was an error while {0}ing.", Mode.ToString ( ).ToLower ( ) ), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}

		private void SetControlVisibleState ( Control ctrl, bool visible ) {
			ctrl.Visible = visible;
		}
	}
}
