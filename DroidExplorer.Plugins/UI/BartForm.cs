using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Plugins.Data;

namespace DroidExplorer.Plugins.UI {
	/// <summary>
	/// 
	/// </summary>
	public partial class BartForm : Form {

		ConsoleWriter cwOut = null;
		ConsoleWriter cwError = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="BartForm"/> class.
		/// </summary>
		public BartForm ( ) {
			InitializeComponent ( );
		  cwOut = new ConsoleWriter ( ref this.console );
			cwError = new ConsoleWriter ( ref this.console );

			this.console.ReadOnly = true;

			BartExecutor.Instance.ConsoleOutput = cwOut;
			BartExecutor.Instance.ConsoleError = cwError;
			BartExecutor.Instance.Complete += delegate ( object sender, EventArgs e ) {
				backups.DataSource = BartExecutor.Instance.List ( );
			};

			backups.DataSource = BartExecutor.Instance.List ( );

		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.Closing"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs"/> that contains the event data.</param>
		protected override void OnClosing ( CancelEventArgs e ) {
			if ( BartExecutor.Instance.IsRunning ) {
				if ( !BartExecutor.Instance.BaseProcess.HasExited ) {
					BartExecutor.Instance.BaseProcess.Kill ( );
				}
				BartExecutor.Instance.BaseProcess = null;
			}
			base.OnClosing ( e );

		}

		/// <summary>
		/// Handles the Click event of the create control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void create_Click ( object sender, EventArgs e ) {
			if ( string.IsNullOrEmpty ( this.name.Text ) ) {
				this.errorProvider.SetError ( this.name, "Name is required" );
				this.errorProvider.SetIconAlignment ( this.name, ErrorIconAlignment.MiddleRight );
				this.errorProvider.SetIconPadding ( this.name, 5 );
				return;
			}
			this.errorProvider.Clear ( );

			BartExecutor.Instance.Verbose = verbose.Checked;
			BartExecutor.Instance.Compress = compress.Checked;
			BartExecutor.Instance.IncludeBoot = includeBoot.Checked;
			BartExecutor.Instance.IncludeData = includeData.Checked;
			BartExecutor.Instance.IncludeRecovery = includeRecovery.Checked;
			BartExecutor.Instance.IncludeSystem = includeSystem.Checked;

			BartExecutor.Instance.CompletionTask = reboot.Checked ? CompleteTask.Reboot : shutdown.Checked ? CompleteTask.Shutdown : CompleteTask.None;

			BartExecutor.Instance.Backup ( this.name.Text );
			tabs.SelectedIndex = 2;

		}

		/// <summary>
		/// Handles the Click event of the restore control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void restore_Click ( object sender, EventArgs e ) {

			if ( backups.SelectedItem == null ) {
				this.errorProvider.SetError ( this.restore, "You must select a ROM to restore" );
				this.errorProvider.SetIconAlignment ( this.restore, ErrorIconAlignment.MiddleLeft );
				this.errorProvider.SetIconPadding ( this.restore, 5 );

				return;
			}

			this.errorProvider.Clear ( );


			BartExecutor.Instance.Verbose = verbose.Checked;
			BartExecutor.Instance.IncludeBoot = includeBoot.Checked;
			BartExecutor.Instance.IncludeData = includeData.Checked;
			BartExecutor.Instance.IncludeRecovery = includeRecovery.Checked;
			BartExecutor.Instance.IncludeSystem = includeSystem.Checked;

			BartExecutor.Instance.CompletionTask = reboot.Checked ? CompleteTask.Reboot : shutdown.Checked ? CompleteTask.Shutdown : CompleteTask.None;

			BartExecutor.Instance.Restore ( (string)backups.SelectedItem );
			tabs.SelectedIndex = 2;

		}

		/// <summary>
		/// Handles the Click event of the gotoCreate control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void gotoCreate_Click ( object sender, EventArgs e ) {
			tabs.SelectedIndex = 1;
		}

		/// <summary>
		/// Handles the Click event of the delete control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void delete_Click ( object sender, EventArgs e ) {
			if ( backups.SelectedItem == null ) {
				this.errorProvider.SetError ( this.delete, "You must select a ROM to delete" );
				this.errorProvider.SetIconAlignment ( this.delete, ErrorIconAlignment.MiddleRight );
				this.errorProvider.SetIconPadding ( this.delete, 5 );

				return;
			}

			this.errorProvider.Clear ( );
			DialogResult dr = MessageBox.Show ( this, string.Format ( "Are you sure you want to remove '{0}'? This can not be undone.", (string)backups.SelectedItem ), "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question );
			switch ( dr ) {
				case DialogResult.Cancel:
				case DialogResult.No:
					return;
				case DialogResult.Yes:
					BartExecutor.Instance.Delete ( (string)backups.SelectedItem );
					tabs.SelectedIndex = 2;
					break;
			}
				
		}

		/// <summary>
		/// Handles the Click event of the close control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void close_Click ( object sender, EventArgs e ) {
			this.Close ( );
		}


	}
}
