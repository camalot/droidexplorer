using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Bootstrapper.Configuration;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using DroidExplorer.Bootstrapper.Panels;
using Microsoft.Win32;
using System.Security.Policy;

namespace DroidExplorer.Bootstrapper.UI {
	public partial class WizardForm : Form, IWizard {

		public const String SDK_PATH_REGISTRY_VALUE = "SdkPath";
#if PLATFORMX86
		public const string ROOT_REGISTRY_KEY = @"SOFTWARE\DroidExplorer\InstallPath";
		public const string ROOT_REGISTRY_KEY32 = ROOT_REGISTRY_KEY;
#elif PLATFORMX64
		public const string ROOT_REGISTRY_KEY = @"SOFTWARE\DroidExplorer\InstallPath";
		public const string ROOT_REGISTRY_KEY32 = @"SOFTWARE\WOW6432Node\DroidExplorer\InstallPath";
#endif

		public event EventHandler CancelRequest;
		public event EventHandler NextClick;
		public event EventHandler BackClick;

		public const int CANCEL_STEP_INDEX = 1000;
		public const int ERROR_STEP_INDEX = 9000;


		public WizardForm ( ) {

			this.LogDebug ( "Initializing Wizard Form" );
			InitializeComponent ( );
			InstalledCache = Installed;
			base.CancelButton = this.cancel;
			base.AcceptButton = this.next;

			PromptExit = true;
			PromptCancel = true;
			StepIndex = 0;
			this.LogDebug ( "Loading Configuration" );
			LoadConfiguation ( );

			this.Text = Configuration.Title;
			this.LogDebug ( "Navigating to first panel" );
			Navigate ( 0 );

			bottomPanel.Paint += new PaintEventHandler ( bottomPanel_Paint );
			topPanel.Paint += new PaintEventHandler ( topPanel_Paint );
		}


		public bool InstalledCache { get; private set; }

		/// <summary>
		/// Loads the configuation.
		/// </summary>
		private void LoadConfiguation ( ) {
			try {
				using ( Stream stream = this.GetType ( ).Assembly.GetManifestResourceStream ( "DroidExplorer.Bootstrapper.Data.Wizard.config" ) ) {
					XmlSerializer ser = new XmlSerializer ( typeof ( WizardConfiguration ) );
					Configuration = ser.Deserialize ( stream ) as WizardConfiguration;
				}
			} catch ( Exception ex ) {
				this.LogFatal ( ex.Message, ex );
				MessageBox.Show ( ex.Message + Environment.NewLine + ex.ToString ( ), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}

		protected override void OnClosing ( CancelEventArgs e ) {
			base.OnClosing ( e );

			if ( PromptExit ) {
				this.Cancel ( );
				e.Cancel = true;
			}
		}

		/// <summary>
		/// Gets or sets the configuration.
		/// </summary>
		/// <value>The configuration.</value>
		private WizardConfiguration Configuration { get; set; }

		/// <summary>
		/// Handles the Paint event of the topPanel control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
		void topPanel_Paint ( object sender, PaintEventArgs e ) {
			Graphics g = e.Graphics;
			g.DrawLine ( SystemPens.ControlDark, new Point ( 0, topPanel.Height - 2 ), new Point ( topPanel.Width, topPanel.Height - 2 ) );
			g.DrawLine ( SystemPens.ControlLightLight, new Point ( 0, topPanel.Height - 1 ), new Point ( topPanel.Width, topPanel.Height - 1 ) );
		}

		/// <summary>
		/// Handles the Paint event of the bottomPanel control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
		private void bottomPanel_Paint ( object sender, PaintEventArgs e ) {
			Graphics g = e.Graphics;
			g.DrawLine ( SystemPens.ControlDark, new Point ( 0, 0 ), new Point ( bottomPanel.Width, 0 ) );
			g.DrawLine ( SystemPens.ControlLightLight, new Point ( 0, 1 ), new Point ( bottomPanel.Width, 1 ) );
		}

		/// <summary>
		/// Handles the Click event of the next control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void next_Click ( object sender, EventArgs e ) {
			Next ( );
		}
		/// <summary>
		/// Handles the Click event of the back control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void back_Click ( object sender, EventArgs e ) {
			Back ( );
		}


		private void cancel_Click ( object sender, EventArgs e ) {
			Cancel ( );
		}

		/// <summary>
		/// Navigates the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		private void Navigate ( int index ) {
			try {
				if ( Program.Mode == InstallMode.Install || Program.Mode == InstallMode.SdkOnly ) {
					if ( index < this.Configuration.InstallSteps.Count && index >= 0 ) {
						BaseNavigate ( this.Configuration.InstallSteps[index] );
					} else {
						if ( index > 0 ) {
							PromptExit = false;
							this.CloseExt ( );
						}
					}
				} else if ( Program.Mode == InstallMode.Uninstall ) {
					if ( index < this.Configuration.UninstallSteps.Count && index >= 0 ) {
						BaseNavigate ( this.Configuration.UninstallSteps[index] );
					} else {
						if ( index > 0 ) {
							PromptExit = false;
							this.CloseExt ( );
						}
					}
				}
			} catch ( Exception ex ) {
				this.LogFatal ( ex.Message, ex );
				Error ( ex );
				//MessageBox.Show ( ex.ToString ( ), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}

		/// <summary>
		/// Shows the error.
		/// </summary>
		private void ShowError ( Exception ex ) {
			try {
				BaseNavigate ( this.Configuration.Error, ex.ToString ( ) );
			} catch ( Exception tex ) {
				this.LogError ( tex.Message, tex );
				//MessageBox.Show ( tex.ToString ( ), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}

		/// <summary>
		/// Shows the cancel.
		/// </summary>
		private void ShowCancel ( ) {
			try {
				DialogResult result = DialogResult.Yes;
				if ( PromptCancel ) {
					result = MessageBox.Show ( this, "Are you sure you want to cancel Droid Explorer installation?",
						"Droid Explorer Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Information );
				}

				if ( result == DialogResult.Yes ) {
					BaseNavigate ( this.Configuration.Cancel );
					OnCancelRequest ( EventArgs.Empty );
					PromptExit = false;
				}
			} catch ( Exception ex ) {
				this.LogError ( ex.Message, ex );
				//MessageBox.Show ( ex.ToString ( ), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}

		protected void OnCancelRequest ( EventArgs e ) {
			if ( CancelRequest != null ) {
				CancelRequest ( this, e );
			}
		}

		protected void OnNextClick ( EventArgs e ) {
			if ( this.NextClick != null ) {
				this.NextClick ( this, e );
			}
		}

		protected void OnBackClick ( EventArgs e ) {
			if ( this.BackClick != null ) {
				this.BackClick ( this, e );
			}
		}

		private void BaseNavigate ( WizardStep step ) {
			BaseNavigate ( step, string.Empty );
		}

		/// <summary>
		/// Base navigate method
		/// </summary>
		/// <param name="step">The step.</param>
		private void BaseNavigate ( WizardStep step, string additionalText ) {

			string[] asmType = step.Content.Split ( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
			string asm = this.GetType ( ).Assembly.GetName ( ).FullName;
			string type = asm.Length > 0 ? asmType[0] : string.Empty;
			if ( asmType.Length == 2 ) {
				asm = asmType[1];
			}
			Type createdType = Type.GetType ( type, false, true );
			WizardPanel panel = Activator.CreateInstance ( createdType, this ) as WizardPanel;

			this.LogDebug ( "Navigating to step {0}", createdType.FullName );

			if ( panel != null ) {
				contentPanel.ClearControls ( );
				panel.SetDock ( DockStyle.Fill );
				contentPanel.SetBackColor ( step.ContentBackColor );
				next.SetEnabled ( step.NextEnabled );
				next.SetText ( step.NextText );
				back.SetEnabled ( step.BackEnabled );
				cancel.SetEnabled ( step.CancelEnabled );
				leftPanel.SetVisible ( step.DialogVisible );
				topPanel.SetVisible ( step.BannerVisible );
				title.SetText ( step.Title );
				subtitle.SetText ( step.Subtitle );
				contentPanel.AddControl ( panel );
				panel.SetAdditionalText ( additionalText );
				panel.InitializeWizardPanel ( );

			} else {
				throw new ArgumentException ( "Invalid content object defined in the configuration" );
			}
		}



		#region IWizard Members

		public int StepIndex { get; protected set; }
		public bool PromptExit { get; set; }
		public bool PromptCancel { get; set; }

		public string GetInstallPath ( ) {

			using ( RegistryKey key = Registry.LocalMachine.OpenSubKey ( ROOT_REGISTRY_KEY ) ) {
				this.LogDebug ( "Looking for key: {0}", ROOT_REGISTRY_KEY );
				if ( key != null ) {
					return (string)key.GetValue ( string.Empty, string.Empty );
				} else {
					return string.Empty;
				}
			}
		}
		public void Next ( ) {
			this.OnNextClick ( EventArgs.Empty );
			Navigate ( ++StepIndex );
		}

		public void Back ( ) {
			this.OnBackClick ( EventArgs.Empty );
			Navigate ( --StepIndex );
		}

		public void Error ( Exception ex ) {
			StepIndex = ERROR_STEP_INDEX;

			if ( Program.Mode == InstallMode.Install ) {

			}

			ShowError ( ex );
		}

		public void Cancel ( ) {
			StepIndex = CANCEL_STEP_INDEX;
			ShowCancel ( );
		}

		public Button NextButton {
			get { return next; }
		}

		public Button BackButton {
			get { return back; }
		}

		public new Button CancelButton {
			get { return cancel; }
		}

		/// <summary>
		/// Gets the SDK path.
		/// </summary>
		/// <returns></returns>
		public string GetSdkPath ( ) {
			using ( RegistryKey key = Registry.LocalMachine.OpenSubKey ( WizardForm.ROOT_REGISTRY_KEY ) ) {
				this.LogDebug ( "Looking for key: {0}", ROOT_REGISTRY_KEY );
				if ( key != null ) {
					return (string)key.GetValue ( WizardForm.SDK_PATH_REGISTRY_VALUE, string.Empty );
				} else {
					using ( RegistryKey key32 = Registry.LocalMachine.OpenSubKey ( WizardForm.ROOT_REGISTRY_KEY32 ) ) {
						if ( key32 != null ) {
							return (string)key32.GetValue ( WizardForm.SDK_PATH_REGISTRY_VALUE, string.Empty );
						} else {
							throw new ArgumentException ( "Not Installed" );
						}
					}
				}
			}
		}

		/// <summary>
		/// Gets a value indicating whether the product is installed.
		/// </summary>
		/// <value><c>true</c> if installed; otherwise, <c>false</c>.</value>
		public bool Installed {
			get {
				RegistryKey key = Registry.LocalMachine.OpenSubKey ( WizardForm.ROOT_REGISTRY_KEY );
				if ( key == null ) {
					this.LogWarning ( "Registry key '{0}' was not found on the system: {1}", WizardForm.ROOT_REGISTRY_KEY, Environment.OSVersion.VersionString );
					return false;
				} else {
					return !string.IsNullOrEmpty ( (string)key.GetValue ( string.Empty, string.Empty ) );
					// dont check for sdk path here, it is not set in the installer
					/*&& !string.IsNullOrEmpty ( (string)key.GetValue ( "SdkPath", string.Empty ) )*/
				}
			}
		}

		public bool UseExistingSdk {
			get {
				// you can now only use an existing SDK
				return Installed && true;
			}
			set {
				// you can now only use an existing SDK
				return;
			}
		}
		#endregion
	}
}
