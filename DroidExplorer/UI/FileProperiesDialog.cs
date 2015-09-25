using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Core.IO;
using DroidExplorer.Core.UI;
using DroidExplorer.Core;
using System.Threading;
using System.Globalization;

namespace DroidExplorer.UI {
	public partial class FileProperiesDialog : Form {
		/// <summary>
		/// Initializes a new instance of the <see cref="FileProperiesDialog"/> class.
		/// </summary>
		internal FileProperiesDialog ( ) {
			InitializeComponent ( );
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FileProperiesDialog"/> class.
		/// </summary>
		/// <param name="fsi">The fsi.</param>
		public FileProperiesDialog ( FileSystemInfo fsi )
			: this ( ) {
			FileSystemInfo = fsi;
			this.permissionTypes.Items.Clear ( );
			ListViewItem lvi = new ListViewItem ( "User" );
			lvi.Tag = FileSystemInfo.UserPermissions;
			this.permissionTypes.Items.Add ( lvi );

			lvi = new ListViewItem ( "Group" );
			lvi.Tag = FileSystemInfo.GroupPermissions;
			this.permissionTypes.Items.Add ( lvi );

			lvi = new ListViewItem ( "Other" );
			lvi.Tag = FileSystemInfo.OtherPermissions;
			this.permissionTypes.Items.Add ( lvi );


			this.permissionTypes.SelectedIndices.Add ( 0 );

			this.isReadOnly.Visible = CommandRunner.Instance.IsMountPoint ( FileSystemInfo.FullPath );

			SetDialogValues ( );
		}

		/// <summary>
		/// Sets the dialog values.
		/// </summary>
		private void SetDialogValues ( ) {
			bool isApk = string.Compare ( System.IO.Path.GetExtension ( FileSystemInfo.Name ), ".apk", true ) == 0;
			this.filename.Text = FileSystemInfo.Name;

			this.filePathLabel.Text = string.Format ( CultureInfo.InvariantCulture, "{0}{1}", FileSystemInfo.FullPath,
				FileSystemInfo.IsLink ? string.Format ( CultureInfo.InvariantCulture, " -> {0}", ( FileSystemInfo as SymbolicLinkInfo ).Link ) : string.Empty );
			this.modifiedLabel.Text = FileSystemInfo.LastModificationDateTime.ToString ( "ddd, MMMM dd, yyyy, h:mm:ss tt" );
			this.fileSizeLabel.Text = string.Format ( new FileSizeFormatProvider ( ), "{0:fs} ({0} bytes)", FileSystemInfo.Size );
			Image img =
				FileSystemInfo.IsDirectory ?
					FileSystemInfo.IsLink ?
						DroidExplorer.Resources.Images.folder_Closed_32xLG_Link :
						DroidExplorer.Resources.Images.folder_Closed_32xLG :
					FileSystemInfo.IsLink ?
					/*FileSystemInfo.IsExecutable ?
						DroidExplorer.Resources.Images.GearSL32 :*/
						DroidExplorer.Resources.Images.document_32_Link :
					/*FileSystemInfo.IsExecutable ?
						DroidExplorer.Resources.Images.Gear :*/
						DroidExplorer.Resources.Images.document_32;
			if ( isApk ) {
				img = CommandRunner.Instance.GetApkIconImage ( FileSystemInfo.FullPath );
				if ( img == null ) {
					img = DroidExplorer.Resources.Images.apk32;
				}
			} else {
				if ( SystemImageListHost.Instance.SystemIcons.ContainsKey ( System.IO.Path.GetExtension ( FileSystemInfo.Name ) ) ) {
					img = SystemImageListHost.Instance.LargeImageList.Images[ SystemImageListHost.Instance.SystemIcons[ System.IO.Path.GetExtension ( FileSystemInfo.Name ) ] ];
				}
			}
			this.fileIcon.Image = img;

			this.fileTypeLabel.Text = FileSystemInfo.IsDirectory ?
				FileSystemInfo.IsLink ? "Directory Symbolic Link" :
				"Directory" :
				FileSystemInfo.IsLink ?
					/*FileSystemInfo.IsExecutable ? "Executable" :*/
					"Symbolic Link" : "File";
			this.isInstalled.Visible = isApk;

			if ( isApk ) {
				new Thread ( new ParameterizedThreadStart ( delegate ( object o ) {
					this.isInstalled.SetText ( "Installed (Checking...)" );
					AaptBrandingCommandResult apkInfo = CommandRunner.Instance.GetApkInformation ( FileSystemInfo.FullPath );
					this.isInstalled.SetChecked ( CommandRunner.Instance.IsPackageInstalled ( apkInfo.Package ) );
					this.isInstalled.SetText ( "Installed" );
				} ) ).Start ( );
			}

			this.isExecutable.Enabled = !FileSystemInfo.IsDirectory && !isApk;
			this.isExecutable.Checked = FileSystemInfo.IsExecutable;
			this.isLink.Checked = FileSystemInfo.IsLink;
			this.isPipe.Checked = FileSystemInfo.IsPipe;
			this.isSocket.Checked = FileSystemInfo.IsSocket;
		}

		/// <summary>
		/// Gets or sets the file system info.
		/// </summary>
		/// <value>The file system info.</value>
		private FileSystemInfo FileSystemInfo { get; set; }

		/// <summary>
		/// Handles the Paint event of the iconFileNamePanel control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
		private void iconFileNamePanel_Paint ( object sender, PaintEventArgs e ) {
			DrawSeparatorOnPanel ( e.Graphics, sender as Panel );
		}


		/// <summary>
		/// Handles the Paint event of the openInfoPanel control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
		private void openInfoPanel_Paint ( object sender, PaintEventArgs e ) {
			DrawSeparatorOnPanel ( e.Graphics, sender as Panel );
		}

		/// <summary>
		/// Handles the Paint event of the diskInfoPanel control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
		private void diskInfoPanel_Paint ( object sender, PaintEventArgs e ) {
			DrawSeparatorOnPanel ( e.Graphics, sender as Panel );
		}

		/// <summary>
		/// Handles the Paint event of the panel1 control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
		private void panel1_Paint ( object sender, PaintEventArgs e ) {
			DrawSeparatorOnPanel ( e.Graphics, sender as Panel );
		}

		/// <summary>
		/// Draws the separator on panel.
		/// </summary>
		/// <param name="g">The graphics object.</param>
		/// <param name="panel">The panel.</param>
		private void DrawSeparatorOnPanel ( Graphics g, Panel panel ) {
			g.DrawLine ( SystemPens.ControlDark, 5, panel.Height - 2, panel.Width - 10, panel.Height - 2 );
			g.DrawLine ( SystemPens.ControlLightLight, 5, panel.Height - 1, panel.Width - 10, panel.Height - 1 );
		}

		/// <summary>
		/// Handles the SelectedIndexChanged event of the permissionTypes control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void permissionTypes_SelectedIndexChanged ( object sender, EventArgs e ) {
			if ( permissionTypes.SelectedItems.Count > 0 ) {
				Permission perm = permissionTypes.SelectedItems[ 0 ].Tag as Permission;
				if ( perm == null ) {
					canExecute.Checked = canWrite.Checked = canRead.Checked = false;
				} else {
					canExecute.Checked = perm.CanExecute;
					canWrite.Checked = perm.CanWrite;
					canRead.Checked = perm.CanRead;
				}
			} else {
				canExecute.Checked = canWrite.Checked = canRead.Checked = false;
			}
		}
	}
}
