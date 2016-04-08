using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Core;
using DroidExplorer.Components;
using System.Drawing.Imaging;
using System.Threading;
using DroidExplorer.Controls;
using System.Collections.Specialized;
using DroidExplorer.Core.Market;
using System.Net;
using System.Globalization;
using Vista.Controls;
using Microsoft.WindowsAPICodePack.Taskbar;
using System.Reflection;
using DroidExplorer.Plugins;
using DroidExplorer.Core.Plugins;
using DroidExplorer.Core.UI.Components;
using DroidExplorer.Core.UI;
using DroidExplorer.Core.UI.Exceptions;
using DroidExplorer.Tools;
using System.Drawing.Drawing2D;
using DroidExplorer.Configuration;
using DroidExplorer.Core.Net;
using DroidExplorer.ActiveButtons;
using DroidExplorer.Core.UI.Renderers.ToolStrip;
using System.Threading.Tasks;
using Camalot.Common.Extensions;
using DroidExplorer.Core.IO;
using Managed.Adb.IO;

namespace DroidExplorer.UI {
	public delegate void AddTreeNodeDelegate ( TreeNodeCollection parent, TreeNode node );
	public delegate void GenericDelegate ( );
	public delegate void SetListViewItemImageIndexDelegate ( ListViewItem lvi, int index );
	public delegate void AddListViewItemDelegate ( ListView lv, ListViewItem lvi );
	public delegate DialogResult MessageBoxDelegate ( string title, string message, string content, TaskDialogButtons buttons, SysIcons icon );

	public partial class MainForm : Form, IPluginHost {
		private delegate void SetWindowTitleDelegate ( string title );
		private delegate void SetSelectedTreeNodeDelegate ( TreeView tv, TreeNode tn );
		private delegate void NavigateToPathDelegate ( DroidExplorer.Core.IO.LinuxDirectoryInfo linuxDirectoryInfo );

		private delegate void SetToolStripItemEnabledDelegate ( ToolStripItem tsi, bool enabled );
		private delegate void SetToolStripEnabledDelegate ( ToolStrip tsi, bool enabled );
		private delegate int GetListViewItemsCountDelegate ( ListView lv );
		private delegate void SetStatusBarLabelTextDelegate ( ToolStripStatusLabel label, string text );
		private delegate int GetSystemIconIndexDelegate ( SystemImageList sysImgList, string file );
		private delegate Image GetSystemBitmapDelegate ( SystemImageList sysImgList, int index );


		private DirectoryTreeNode rootNode = null;
		private string currentDirectory = string.Empty;
		private Thread filesThread = null;
		private bool navigating = false;
		public MainForm ( ) {

			rootNode = new DirectoryTreeNode ( DroidExplorer.Core.IO.DirectoryInfo.CreateRoot ( ) );
			Program.SplashManager.SplashDialog.IncrementLoadStep ( 1 );
			CommandRunner.Instance.DeviceStateChanged += new EventHandler<DeviceEventArgs> ( CommandRunner_DeviceStateChanged );
			CommandRunner.Instance.Connected += new EventHandler<DeviceEventArgs> ( CommandRunner_Connected );
			CommandRunner.Instance.Disconnected += new EventHandler<DeviceEventArgs> ( CommandRunner_Disconnected );
			FileTypeActionHandlers = new FileTypeActionHandlers ( );
			FileTypeIconHandlers = new FileTypeIconHandlers ( );
			OpenWithItems = new List<ToolStripMenuItem> ( );


			InitializeComponent ( );

			askForHelpToolStripMenuItem.Image = DroidExplorer.Resources.Images.Bubble_16xLG;
			askForHelpToolStripMenuItem1.Image = DroidExplorer.Resources.Images.Bubble_16xLG;

			newFolderContextToolStripMenuItem.Image = DroidExplorer.Resources.Images.NewSolutionFolder_6289;
			newFileContextToolStripMenuItem.Image = DroidExplorer.Resources.Images.NewFile_6276;

			this.StickyWindow = new StickyWindow ( this );
			this.StickyWindow.StickToOther = true;
			this.StickyWindow.StickOnResize = true;
			this.StickyWindow.StickOnMove = true;

			this.LogDebug ( "Initializing the breadcumb bar" );
			SetupBreadcrumbBar ( );
			Program.SplashManager.SplashDialog.IncrementLoadStep ( 1 );

			// vista / win7 + themeing
			ToolStripManager.Renderer = new VisualStudio2012Renderer ( );
			this.directoryTree.SetVistaExplorerStyle ( false, false );
			this.itemsList.SetVistaExplorerStyle ( );
			this.itemsList.SetAllowDraggableColumns ( true );
			Program.SplashManager.SplashDialog.IncrementLoadStep ( 1 );

			// TODO: [workitem:17419]
			this.LogDebug ( "Loading toolstrip settings" );
			ToolStripManager.LoadSettings ( this, "DroidExplorer" );

			Program.SplashManager.SplashDialog.IncrementLoadStep ( 1 );

			this.LogDebug ( "Initializing the treeview images" );
			InitializeTreeViewImages ( );
			Program.SplashManager.SplashDialog.IncrementLoadStep ( 1 );

			this.LogDebug ( "Initializing the listview" );
			InitializeListView ( );
			Program.SplashManager.SplashDialog.IncrementLoadStep ( 1 );


			itemsList.MouseDoubleClick += new MouseEventHandler ( itemsList_MouseDoubleClick );
			itemsList.DragDrop += new DragEventHandler ( itemsList_DragDrop );
			itemsList.DragEnter += new DragEventHandler ( itemsList_DragEnter );
			itemsList.DragOver += new DragEventHandler ( itemsList_DragOver );
			itemsList.SelectedIndexChanged += new EventHandler ( itemsList_SelectedIndexChanged );
			itemsList.DragLeave += new EventHandler ( itemsList_DragLeave );
			itemsList.ItemDrag += new ItemDragEventHandler ( itemsList_ItemDrag );
			itemsList.ViewChanged += new EventHandler ( itemsList_ViewChanged );
			itemsList.AfterLabelEdit += new LabelEditEventHandler ( itemsList_AfterLabelEdit );
			itemsList.BeforeLabelEdit += new LabelEditEventHandler ( itemsList_BeforeLabelEdit );
			// begin edit when pressing f2
			itemsList.KeyUp += delegate ( object sender, KeyEventArgs e ) {
				if ( e.KeyCode == Keys.F2 && !InLabelEdit && itemsList.SelectedItems.Count > 0 ) {
					itemsList.SelectedItems[0].BeginEdit ( );
				}
			};
			itemsList.View = View.Details;

			directoryTree.BeforeExpand += new TreeViewCancelEventHandler ( directoryTree_BeforeExpand );
			directoryTree.AfterExpand += new TreeViewEventHandler ( directoryTree_AfterExpand );
			directoryTree.BeforeCollapse += new TreeViewCancelEventHandler ( directoryTree_BeforeCollapse );
			directoryTree.AfterCollapse += new TreeViewEventHandler ( directoryTree_AfterCollapse );
			directoryTree.AfterSelect += new TreeViewEventHandler ( directoryTree_AfterSelect );

			this.editToolStripMenuItem.DropDownOpening += delegate ( object sender, EventArgs e ) {
				fileListContextMenuStrip_Opening ( sender, new CancelEventArgs ( false ) );
			};

			this.imageLink.Click += delegate ( object s, EventArgs e ) {
				CommandRunner.Instance.LaunchProcessWindow ( Resources.Strings.DontateLink, string.Empty, true );
			};


			// initialize Active Menu buttons
			IActiveMenu menu = ActiveMenu.GetInstance ( this );
			// define a new button
			ActiveButton button = new ActiveButton ( );
			// todo: fix it so it renders with the icon correctly.
			button.Text = "        Ask for Help";
			button.TextAlign = ContentAlignment.MiddleRight;
			button.Image = DroidExplorer.Resources.Images.Bubble_16xLG;
			button.ImageAlign = ContentAlignment.MiddleLeft;
			button.Margin = new Padding ( 20, button.Margin.Top, button.Margin.Right, button.Margin.Bottom );
			button.TextImageRelation = TextImageRelation.ImageBeforeText;
			button.BackColor = Color.FromArgb ( 255, 38, 114, 236 );
			button.ForeColor = Color.White;
			menu.ToolTip.SetToolTip ( button, button.Text.Trim ( ) );
			button.Click += askForHelpToolStripMenuItem1_Click;

			// add the button to the menu
			menu.Items.Add ( button );
			this.askForHelpToolStripMenuItem.Visible = false;

			this.LogDebug ( "Loading application settings" );
			Program.SplashManager.SplashDialog.SetStepText ( "Loading Application Settings" );
			LoadSettings ( );
			Program.SplashManager.SplashDialog.IncrementLoadStep ( 1 );

			this.LogDebug ( "Connecting to device: {0}", this.Device );
			CommandRunner.Instance.Connect ( );
			Program.SplashManager.SplashDialog.IncrementLoadStep ( 1 );


			this.LogDebug ( "Loading Plugins" );
			Program.SplashManager.SplashDialog.SetStepText ( "Loading Droid Explorer Plugins" );
			LoadPlugins ( );
			Program.SplashManager.SplashDialog.IncrementLoadStep ( 1 );


			Program.SplashManager.CloseSplashDialog ( );
			//Task.Run(async () =>await new ApkCacheBuilder().RunAsync());
		}


		/// <summary>
		/// Checks if there is an update available on codeplex
		/// </summary>
		/// <remarks>
		/// The service that supplies the information is informed of a new version when the publish happens.
		/// </remarks>
		private void VersionCheck ( ) {

			VersionCheckService.Instance.BeginGetLatestVersion ( new VersionCheckService.AsyncVersionCheck ( delegate ( SoftwareRelease release ) {
				var currentVersion = VersionCheckService.Instance.GetVersion ( );
				if ( release.Version.CompareTo ( currentVersion ) >= 1 ) {
					// new version available
					Application.Run ( new NewVersionForm ( release ) );
				}
			} ) );
		}

		private StickyWindow StickyWindow { get; set; }
		private JumpList JumpList { get; set; }
		internal FileTypeActionHandlers FileTypeActionHandlers { get; set; }
		internal FileTypeIconHandlers FileTypeIconHandlers { get; set; }
		private bool InLabelEdit { get; set; }

		private void SetUpJumpList ( ) {
			if ( NativeApi.IsWindow7OrLater && NativeApi.IsRecentDocumentTrackingEnabled ( ) ) {
				var cat = new JumpListCustomCategory ( "Plugins" );

				var jumper = Microsoft.WindowsAPICodePack.Taskbar.JumpList.CreateJumpList ( );
				string appPath = System.IO.Path.Combine ( Program.LocalPath, "DroidExplorer.Runner.exe" );
				foreach ( var item in Settings.Instance.PluginSettings.Plugins ) {
					IPlugin iplug = Settings.Instance.PluginSettings.GetPlugin ( item.ID.Replace ( " ", string.Empty ) );
					if ( item.Enabled && iplug != null && iplug.Runnable ) {
						JumpListLink jll = new JumpListLink ( appPath, iplug.Text );
						jll.Arguments = string.Format ( CultureInfo.InvariantCulture, "/type={0}", item.ID.Replace ( " ", string.Empty ) );
						jll.IconReference = new Microsoft.WindowsAPICodePack.Shell.IconReference ( string.Format ( CultureInfo.InvariantCulture, "{0},0", appPath ) );

						cat.AddJumpListItems ( jll );
					}
				}
				jumper.AddCustomCategories ( cat );
				jumper.Refresh ( );
			}
		}

		private void LoadPlugins ( ) {
			PluginLoader.Load ( this, this.toolStripContainer1.TopToolStripPanel, this.pluginsToolStripMenuItem );
		}

		private void SetupBreadcrumbBar ( ) {
			if ( Areo.IsGlassSupported ) {
				this.breadcrumbBar.BackgroundAlpha = 255;
				this.ExtendFrameIntoClientArea ( glassArea );
			} else {
				//add some padding to left and right if we dont have areo
				this.breadcrumbBar.BackgroundAlpha = 255;
				glassArea.Paint += delegate ( object sender, PaintEventArgs e ) {
					Graphics g = e.Graphics;
					RenderBackgroundGradient ( g, glassArea, ProfessionalColors.ToolStripPanelGradientBegin, ProfessionalColors.ToolStripPanelGradientEnd, Orientation.Horizontal );
				};
				glassArea.Padding = new Padding ( 4, glassArea.Padding.Top, 4, glassArea.Padding.Right );
			}

			BreadcrumbBarButton refresh = new BreadcrumbBarButton ( );
			refresh.Image = Vista.Controls.Properties.Resources.refresh;
			refresh.Click += delegate ( object s, EventArgs e ) {
				this.NavigateToPath ( new DroidExplorer.Core.IO.LinuxDirectoryInfo ( this.breadcrumbBar.FullPath ) );
			};
			this.breadcrumbBar.Buttons.Add ( refresh );

			this.breadcrumbBar.Navigate += delegate ( object sender, EventArgs e ) {
				this.NavigateToPath ( new DroidExplorer.Core.IO.LinuxDirectoryInfo ( this.breadcrumbBar.FullPath ) );
			};

		}

		private void RenderBackgroundGradient ( Graphics g, Control control, Color beginColor, Color endColor, Orientation orientation ) {
			if ( control.RightToLeft == RightToLeft.Yes ) {
				Color color = beginColor;
				beginColor = endColor;
				endColor = color;
			}
			if ( orientation == Orientation.Horizontal ) {
				Control parent = control.Parent;
				if ( parent != null ) {
					Rectangle rectangle = new Rectangle ( Point.Empty, parent.Size );
					if ( rectangle.IsEmpty ) {
						return;
					}
					using ( LinearGradientBrush brush = new LinearGradientBrush ( rectangle, beginColor, endColor, LinearGradientMode.Horizontal ) ) {
						brush.TranslateTransform ( (float)( parent.Width - control.Location.X ), (float)( parent.Height - control.Location.Y ) );
						g.FillRectangle ( brush, new Rectangle ( Point.Empty, control.Size ) );
						return;
					}
				}
				Rectangle rectangle2 = new Rectangle ( Point.Empty, control.Size );
				if ( rectangle2.IsEmpty ) {
					return;
				}
				using ( LinearGradientBrush brush2 = new LinearGradientBrush ( rectangle2, beginColor, endColor, LinearGradientMode.Horizontal ) ) {
					g.FillRectangle ( brush2, rectangle2 );
					return;
				}
			}
			using ( Brush brush3 = new SolidBrush ( beginColor ) ) {
				g.FillRectangle ( brush3, new Rectangle ( Point.Empty, control.Size ) );
			}
		}

		protected override void OnHandleCreated ( EventArgs e ) {
			base.OnHandleCreated ( e );
		}

		protected override void OnLoad ( EventArgs e ) {
			base.OnLoad ( e );

		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.Shown"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.EventArgs"/> that contains the event data.</param>
		protected override void OnShown ( EventArgs e ) {
			base.OnShown ( e );
			SetUpJumpList ( );



			// check for new version
			VersionCheck ( );

		}

		protected override void OnPaint ( PaintEventArgs e ) {
			base.OnPaint ( e );
		}

		protected override void WndProc ( ref Message m ) {
			base.WndProc ( ref m );
		}

		#region DirectoryTree Event Handlers
		void directoryTree_AfterExpand ( object sender, TreeViewEventArgs e ) {

		}

		void directoryTree_AfterSelect ( object sender, TreeViewEventArgs e ) {
			if ( e.Node is DirectoryTreeNode && !navigating ) {
				NavigateToPath ( new DroidExplorer.Core.IO.LinuxDirectoryInfo ( ( e.Node as DirectoryTreeNode ).LinuxPath ) );
			}
		}

		void directoryTree_AfterCollapse ( object sender, TreeViewEventArgs e ) {
			if ( e.Node is DirectoryTreeNode ) {
				( e.Node as DirectoryTreeNode ).OnAfterCollapse ( CommandRunner.Instance );
			}
		}

		void directoryTree_BeforeCollapse ( object sender, TreeViewCancelEventArgs e ) {
			if ( e.Node is DirectoryTreeNode ) {
				( e.Node as DirectoryTreeNode ).OnBeforeCollapse ( CommandRunner.Instance );
			} else {
				e.Node.SelectedImageIndex = e.Node.ImageIndex = 1;
			}
		}


		private DirectoryTreeNode LastExpandedNode { get; set; }

		void directoryTree_BeforeExpand ( object sender, TreeViewCancelEventArgs e ) {
			if ( e.Node is DirectoryTreeNode ) {
				if ( LastExpandedNode != e.Node || e.Node != rootNode ) {
					LastExpandedNode = e.Node as DirectoryTreeNode;
					( e.Node as DirectoryTreeNode ).OnBeforeExpand ( CommandRunner.Instance, true );
				} else {
					( e.Node as DirectoryTreeNode ).OnBeforeExpand ( CommandRunner.Instance, false );
				}
				if ( e.Node.Nodes.Count == 0 ) {
					( e.Node as DirectoryTreeNode ).OnBeforeCollapse ( CommandRunner.Instance );
					( e.Node as DirectoryTreeNode ).OnAfterCollapse ( CommandRunner.Instance );
					e.Cancel = true;
				}
			} else {
				e.Node.SelectedImageIndex = e.Node.ImageIndex = 0;
			}
		}
		#endregion

		#region ItemsList Event Handlers


		void itemsList_BeforeLabelEdit ( object sender, LabelEditEventArgs e ) {
			InLabelEdit = !e.CancelEdit;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <workitem id="10276" status="open">Renaming read-only file fails silently but appears to succeed</workitem>
		void itemsList_AfterLabelEdit ( object sender, LabelEditEventArgs e ) {
			if ( !e.CancelEdit ) {
				FileSystemInfoListViewItem lvi = itemsList.Items[e.Item] as FileSystemInfoListViewItem;
				if ( lvi != null ) {
					if ( !lvi.FileSystemInfo.IsDirectory ) {
						if ( lvi.FileSystemInfo.IsLink ) {
							CommandRunner.Rename ( ( lvi.FileSystemInfo as DroidExplorer.Core.IO.SymbolicLinkInfo ).FullPath, e.Label );
						} else if ( lvi.FileSystemInfo.IsPipe ) {
							e.CancelEdit = true;
						} else if ( lvi.FileSystemInfo.IsSocket ) {
							e.CancelEdit = true;
						} else {
							CommandRunner.RenameFile ( lvi.FileSystemInfo.FullPath, e.Label );
						}
					} else {
						CommandRunner.Rename ( lvi.FileSystemInfo.FullPath, e.Label );
					}
				}
			}

			InLabelEdit = false;
		}

		void itemsList_ViewChanged ( object sender, EventArgs e ) {
			if ( itemsList.Items.Count == 0 ) {
				if ( itemsList.View == View.Details ) {
					itemsList.AutoResizeColumns ( ColumnHeaderAutoResizeStyle.HeaderSize );
				}
			} else {
				if ( itemsList.View == View.Details ) {
					itemsList.AutoResizeColumns ( ColumnHeaderAutoResizeStyle.ColumnContent );
				}
			}
		}


		void itemsList_MouseDoubleClick ( object sender, MouseEventArgs e ) {
			if ( e.Button == MouseButtons.Right ) {
				return;
			}
			if ( itemsList.SelectedItems.Count == 1 ) {
				FileSystemInfoListViewItem lvi = itemsList.SelectedItems[0] as FileSystemInfoListViewItem;
				if ( lvi != null ) {
					if ( lvi.FileSystemInfo.IsDirectory ) {
						NavigateToPath ( new DroidExplorer.Core.IO.LinuxDirectoryInfo ( lvi.FileSystemInfo.FullPath ) );
						// } else if ( lvi.FileSystemInfo.IsExecutable ) { // all files seem to be executable now...
						// run?
						//CommandRunner.Instance.LaunchShellWindow ( lvi.FileSystemInfo.FullPath );
					} else if ( !lvi.FileSystemInfo.IsDirectory /*&& !lvi.FileSystemInfo.IsExecutable*/ ) {
						if ( lvi is ApkFileSystemInfoListViewItem ) {
							// todo : check if is an app dir, if it is, uninstall, otherwise, install?
						} else {
							string ext = LinuxPath.GetExtension ( lvi.FileSystemInfo.Name );
							if ( FileTypeActionHandlers.ContainsKey ( ext ) ) {
								FileTypeActionHandlers[ext].Open ( ( lvi.FileSystemInfo as DroidExplorer.Core.IO.FileInfo ) );
							} else {
								System.IO.FileInfo file = CommandRunner.Instance.PullFile ( lvi.FileSystemInfo.FullPath );
								CommandRunner.Instance.LaunchProcessWindow ( file.FullName, string.Empty, true );
							}
						}
					}
				}
			}
		}

		private void itemsList_SelectedIndexChanged ( object sender, EventArgs e ) {
			copyToLocalToolStripButton.Enabled = itemsList.SelectedItems.Count > 0;
		}

		void itemsList_DragOver ( object sender, DragEventArgs e ) {

		}


		void itemsList_DragLeave ( object sender, EventArgs e ) {

		}


		void itemsList_ItemDrag ( object sender, ItemDragEventArgs e ) {
			// this works, but hangs the copy process because it has to 
			// pull the files first.
			/*List<DroidExplorer.Core.IO.FileInfo> fifiles = new List<DroidExplorer.Core.IO.FileInfo> ( );
			List<string> files = new List<string> ( );
			List<string> copyFiles = new List<string> ( );
			foreach ( var lvi in this.itemsList.SelectedItems ) {
				if ( lvi is FileSystemInfoListViewItem ) {
					FileSystemInfoListViewItem flvi = lvi as FileSystemInfoListViewItem;
					fifiles.Add ( flvi.FileSystemInfo as DroidExplorer.Core.IO.FileInfo );
					files.Add ( flvi.FileSystemInfo.FullPath );
				}
			}

			//TransferForm tf = new TransferForm ( );
			//tf.Pull ( fifiles, new System.IO.DirectoryInfo ( CommandRunner.Instance.TempDataPath ) );

				List<System.IO.FileInfo> tfiles = CommandRunner.Instance.PullFiles ( files );
				foreach ( var item in tfiles ) {
					copyFiles.Add ( item.FullName );
				}
				DataObject dobj = new DataObject ( DataFormats.FileDrop, copyFiles.ToArray ( ) );
				itemsList.DoDragDrop ( dobj, DragDropEffects.Copy );


			*/
		}

		void itemsList_DragEnter ( object sender, DragEventArgs e ) {
			if ( e.Data.GetDataPresent ( "FileDrop" ) ) {
				e.Effect = DragDropEffects.Copy;
			} else {
				e.Effect = DragDropEffects.None;
			}
		}

		void itemsList_DragDrop ( object sender, DragEventArgs e ) {
			if ( e.Data.GetDataPresent ( DataFormats.FileDrop ) ) {
				string[] fsiItems = (string[])e.Data.GetData ( DataFormats.FileDrop );
				List<System.IO.FileInfo> files = new List<System.IO.FileInfo> ( );
				foreach ( var item in fsiItems ) {
					if ( System.IO.File.Exists ( item ) ) {
						files.Add ( new System.IO.FileInfo ( item ) );
					} else if ( System.IO.Directory.Exists ( item ) ) {
						// TODO: Handle copying entire directory over.
					}
				}

				var transfer = new TransferDialog ( );
				transfer.TransferComplete += delegate ( object s, EventArgs ea ) {
					if ( this.InvokeRequired ) {
						this.Invoke ( new NavigateToPathDelegate ( NavigateToPath ), new object[] { this.CurrentDirectory } );
					} else {
						NavigateToPath ( this.CurrentDirectory );
					}
				};
				transfer.TransferError += delegate ( object s, EventArgs ea ) {
					if ( transfer.TransferException != null ) { // was there an error while transfering?
						if ( this.InvokeRequired ) {
							this.Invoke ( new MessageBoxDelegate ( TaskDialog.MessageBox ), new object[] { "Transfer Error", transfer.TransferException.Message, string.Empty, TaskDialogButtons.OK, SysIcons.Error } );
						} else {
							TaskDialog.MessageBox ( "Transfer Error", transfer.TransferException.Message, string.Empty, TaskDialogButtons.OK, SysIcons.Error );
						}
					}
				};
				transfer.Push ( files, this.CurrentDirectory.FullName );
			}
		}

		#endregion

		#region CommandRunner Event Handlers
		void CommandRunner_DeviceStateChanged ( object sender, DeviceEventArgs e ) {
			string title = CommandRunner.Instance.State.ToString ( );

			if ( CommandRunner.Instance.State == CommandRunner.DeviceState.Device ) {
				title = KnownDeviceManager.Instance.GetDeviceFriendlyName ( string.IsNullOrEmpty ( CommandRunner.Instance.DefaultDevice ) ? CommandRunner.Instance.GetSerialNumber ( ) : CommandRunner.Instance.DefaultDevice );
			}

			if ( this.InvokeRequired ) {
				this.Invoke ( new SetWindowTitleDelegate ( SetWindowTitle ), new object[] { string.Format ( Resources.Strings.TitleFormat, title ) } );
			} else {
				SetWindowTitle ( string.Format ( Resources.Strings.TitleFormat, title ) );
			}
		}

		void CommandRunner_Connected ( object sender, DeviceEventArgs e ) {

			this.InvokeIfRequired ( ( ) => {
				directoryTree.Nodes.Clear ( );
				itemsList.Items.Clear ( );
				rootNode.Nodes.Clear ( );

				foreach ( Control p in this.toolStripContainer1.TopToolStripPanel.Controls ) {
					if ( !p.Equals ( menuStrip ) ) {
						p.Enabled = true;
					} else {
						foreach ( var msi in menuStrip.Items ) {
							if ( !msi.Equals ( connectToDeviceToolStripMenuItem ) && !msi.Is<ImageLinkToolStripItem> ( ) ) {
								( (ToolStripMenuItem)msi ).Enabled = true;
							}
						}
					}
				}
			} );

			BuildTree ( );

			// this fails because the BreadcrumbBar doesn't handle the invoke correctly.
			// this.NavigateToPath ( new LinuxDirectoryInfo ( initialPath ) );


		}

		void CommandRunner_Disconnected ( object sender, DeviceEventArgs e ) {

			this.InvokeIfRequired ( ( ) => {
				breadcrumbBar.Nodes.Clear ( );
				directoryTree.Nodes.Clear ( );
				itemsList.Items.Clear ( );
				rootNode.Nodes.Clear ( );
				foreach ( Control p in this.toolStripContainer1.TopToolStripPanel.Controls ) {
					if ( !p.Equals ( menuStrip ) ) {
						p.Enabled = false;
					} else {
						foreach ( var msi in menuStrip.Items ) {
							if ( !msi.Equals ( connectToDeviceToolStripMenuItem ) && !msi.Is<ImageLinkToolStripItem> ( ) ) {
								( (ToolStripMenuItem)msi ).Enabled = false;
							}
						}
					}
				}
				objectsToolStripStatusLabel.Text = string.Empty;
				sizeToolStripStatusLabel.Text = string.Empty;
			} );
		}
		#endregion

		private void InitializeTreeViewImages ( ) {
			AlphaImageList.AddFromImage ( (Image)DroidExplorer.Resources.Images.folder_Closed_16xLG, this.treeImages );
			AlphaImageList.AddFromImage ( (Image)DroidExplorer.Resources.Images.folder_Open_16xLG, this.treeImages );
			AlphaImageList.AddFromImage ( (Image)DroidExplorer.Resources.Images.folder_Closed_16xLG_Link, this.treeImages );
			AlphaImageList.AddFromImage ( (Image)DroidExplorer.Resources.Images.folder_Open_16xLG_Link, this.treeImages );
		}

		private void InitializeListView ( ) {
			itemsList.SmallImageList = SystemImageListHost.Instance.SmallImageList;
			itemsList.LargeImageList = SystemImageListHost.Instance.LargeImageList;

			itemsList.ContextMenuStrip = this.fileListContextMenuStrip;
		}

		private void ThreadedBuildListViewItems ( object fsiList ) {
			if ( fsiList.GetType ( ) == typeof ( List<DroidExplorer.Core.IO.FileSystemInfo> ) ) {
				BuildListViewItems ( fsiList as List<DroidExplorer.Core.IO.FileSystemInfo> );
			}
		}
		private void BuildListViewItems ( List<DroidExplorer.Core.IO.FileSystemInfo> fsiList ) {
			try {
				if ( this.InvokeRequired ) {
					this.Invoke ( new SetStatusBarLabelTextDelegate ( this.SetStatusBarLabelText ), new object[] { sizeToolStripStatusLabel, string.Empty } );
					this.Invoke ( new SetStatusBarLabelTextDelegate ( SetStatusBarLabelText ), new object[] { objectsToolStripStatusLabel, "Scanning for items..." } );
					this.Invoke ( new SetToolStripItemEnabledDelegate ( SetToolStripItemEnabled ), new object[] { copyToLocalToolStripButton, false } );
					this.Invoke ( new GenericDelegate ( itemsList.Items.Clear ) );
				} else {
					SetStatusBarLabelText ( objectsToolStripStatusLabel, "Scanning for items..." );
					SetStatusBarLabelText ( sizeToolStripStatusLabel, string.Empty );
					SetToolStripItemEnabled ( copyToLocalToolStripButton, false );
					itemsList.Items.Clear ( );
				}

				long totalFileSize = 0;
				foreach ( DroidExplorer.Core.IO.FileSystemInfo item in fsiList ) {
					try {

						// filesize info
						totalFileSize += item.Size;
						string sizeString = string.Format ( new DroidExplorer.Core.IO.FileSizeFormatProvider ( ), "{0:fs}", totalFileSize );
						if ( this.InvokeRequired ) {
							this.Invoke ( new SetStatusBarLabelTextDelegate ( this.SetStatusBarLabelText ), new object[] { sizeToolStripStatusLabel, sizeString } );
						} else {
							SetStatusBarLabelText ( sizeToolStripStatusLabel, sizeString );
						}

						ListViewItem listViewItem = new FileSystemInfoListViewItem ( item );
						var keyName = Cache.GetCacheKey ( item );
						if ( !item.IsDirectory ) {
							string ext = System.IO.Path.GetExtension ( item.Name );
							keyName = ext.ToLower ( );
							if ( keyName.StartsWith ( "/" ) ) {
								keyName = keyName.Substring ( 1 );
							}
							keyName = keyName.REReplace ( @"\/\:", "." );
							if ( !string.IsNullOrEmpty ( ext ) ) {
								// file type icon handlers
								if ( this.FileTypeIconHandlers.ContainsKey ( ext ) ) {
									var handler = this.FileTypeIconHandlers[ext];
									this.LogDebug ( "Loading image from Handler for {0}", ext );
									keyName = handler.GetKeyName ( item );
									if ( !SystemImageListHost.Instance.SystemIcons.ContainsKey ( keyName ) ) {
										var largeImage = handler.GetLargeImage ( item );
										var smallImage = largeImage.Resize ( 16, 16 );
										SystemImageListHost.Instance.AddFileTypeImage ( keyName, smallImage, largeImage );
									}
									// get the listview item from the handler
									listViewItem = handler.GetListViewItem ( item );
									var imageIndex = SystemImageListHost.Instance.SystemIcons[keyName];

									if ( this.InvokeRequired ) {
										this.Invoke ( (Action)( ( ) => {
											SetListViewItemImageIndex ( listViewItem, imageIndex );
										} ) );
									} else {
										SetListViewItemImageIndex ( listViewItem, imageIndex );
									}

								} else {
									listViewItem = new FileSystemInfoListViewItem ( item );
									if ( !SystemImageListHost.Instance.SystemIcons.ContainsKey ( ext.ToLower ( ) ) ) { // add index and icon
										Image sico;
										Image lico;
										if ( this.InvokeRequired ) {
											int iconIndex = (int)this.Invoke ( new GetSystemIconIndexDelegate ( GetSystemIconIndex ), SystemImageListHost.Instance.SmallSystemImageList, item.Name );
											sico = (Image)this.Invoke ( new GetSystemBitmapDelegate ( GetSystemBitmap ), SystemImageListHost.Instance.SmallSystemImageList, iconIndex );
											lico = (Image)this.Invoke ( new GetSystemBitmapDelegate ( GetSystemBitmap ), SystemImageListHost.Instance.LargeSystemImageList, iconIndex );
										} else {
											int iconIndex = SystemImageListHost.Instance.SmallSystemImageList.IconIndex ( item.Name, false );
											sico = GetSystemBitmap ( SystemImageListHost.Instance.SmallSystemImageList, iconIndex );
											lico = GetSystemBitmap ( SystemImageListHost.Instance.LargeSystemImageList, iconIndex );
										}
										SystemImageListHost.Instance.AddFileTypeImage ( ext.ToLower ( ), sico, lico );
									}

									if ( this.InvokeRequired ) {
										this.Invoke ( new SetListViewItemImageIndexDelegate ( this.SetListViewItemImageIndex ), listViewItem, SystemImageListHost.Instance.SystemIcons[keyName] );
									} else {
										SetListViewItemImageIndex ( listViewItem, SystemImageListHost.Instance.SystemIcons[keyName] );
									}
								}
							}
						}
						if ( this.InvokeRequired ) {
							this.Invoke ( new AddListViewItemDelegate ( this.AddListViewItem ), itemsList, listViewItem );
							int lvCount = (int)this.Invoke ( new GetListViewItemsCountDelegate ( GetListViewItemsCount ), itemsList );
							this.Invoke ( new SetStatusBarLabelTextDelegate ( SetStatusBarLabelText ), objectsToolStripStatusLabel, string.Format ( "{0} object{1}", lvCount, lvCount != 1 ? "s" : string.Empty ) );
						} else {
							AddListViewItem ( itemsList, listViewItem );
							int lvCount = GetListViewItemsCount ( itemsList );
							SetStatusBarLabelText ( objectsToolStripStatusLabel, string.Format ( "{0} object{1}", lvCount, lvCount != 1 ? "s" : string.Empty ) );
						}
					} catch ( ThreadAbortException taex ) {
						// Ignore
					} catch ( Exception ex ) {
						this.LogError ( ex.Message, ex );
					}
				}

				if ( this.InvokeRequired ) {
					int lvCount = (int)this.Invoke ( new GetListViewItemsCountDelegate ( GetListViewItemsCount ), itemsList );
					if ( lvCount == 0 ) {
						this.Invoke ( new SetStatusBarLabelTextDelegate ( SetStatusBarLabelText ), objectsToolStripStatusLabel, "0 objects" );
						this.Invoke ( new AutoResizeColumnsDelegate ( AutoResizeListViewColumnHeaders ), this.itemsList, ColumnHeaderAutoResizeStyle.HeaderSize );
					} else {
						this.Invoke ( new AutoResizeColumnsDelegate ( AutoResizeListViewColumnHeaders ), this.itemsList, ColumnHeaderAutoResizeStyle.ColumnContent );
					}
				} else {
					int lvCount = GetListViewItemsCount ( itemsList );
					if ( lvCount == 0 ) {
						SetStatusBarLabelText ( objectsToolStripStatusLabel, "0 objects" );
						AutoResizeListViewColumnHeaders ( this.itemsList, ColumnHeaderAutoResizeStyle.HeaderSize );
					} else {
						AutoResizeListViewColumnHeaders ( this.itemsList, ColumnHeaderAutoResizeStyle.ColumnContent );
					}
				}
			} catch ( Exception mex ) {
				this.LogError ( mex.Message, mex );
			}
		}

		private void AutoResizeListViewColumnHeaders ( ListView lv, ColumnHeaderAutoResizeStyle resizeStyle ) {
			lv.AutoResizeColumns ( resizeStyle );
		}

		private void BuildTree ( ) {
			if ( this.InvokeRequired ) {
				this.Invoke ( new GenericDelegate ( directoryTree.Nodes.Clear ) );
				this.Invoke ( new AddTreeNodeDelegate ( AddTreeNode ), directoryTree.Nodes, rootNode );
			} else {
				directoryTree.Nodes.Clear ( );
				AddTreeNode ( directoryTree.Nodes, rootNode );
			}

			foreach ( DroidExplorer.Core.IO.FileSystemInfo dir in CommandRunner.Instance.ListDirectories ( rootNode.LinuxPath ) ) {
				DirectoryTreeNode tn = new DirectoryTreeNode ( dir );
				if ( dir is DroidExplorer.Core.IO.SymbolicLinkInfo ) {
					tn.SelectedImageIndex = tn.ImageIndex = 2;
				}
				if ( this.InvokeRequired ) {
					this.Invoke ( new AddTreeNodeDelegate ( AddTreeNode ), rootNode.Nodes, tn );
				} else {
					AddTreeNode ( rootNode.Nodes, tn );
				}
			}

			if ( this.InvokeRequired ) {
				this.Invoke ( new SetSelectedTreeNodeDelegate ( SetSelectedTreeNode ), directoryTree, rootNode );
				this.Invoke ( new GenericDelegate ( rootNode.Expand ) );
			} else {
				SetSelectedTreeNode ( directoryTree, rootNode );
				rootNode.Expand ( );
			}

		}

		private void NavigateToPath ( DroidExplorer.Core.IO.LinuxDirectoryInfo path ) {
			NavigateToPath ( path, true );
		}

		private void NavigateToPath ( DroidExplorer.Core.IO.LinuxDirectoryInfo path, bool addHistory ) {
			if ( !navigating ) {
				if ( path.FullName.StartsWith ( "./" ) ) {
					path = new DroidExplorer.Core.IO.LinuxDirectoryInfo ( path.FullName.Substring ( 1 ) );
				}
				//directoryTree.ExpandToPath ( path.FullName );
				DirectoryTreeNode dtn = FindNodeFromPath ( path );
				if ( dtn != null ) {
					navigating = true;
					PluginLoader.CurrentPath = path;
					if ( InvokeRequired ) {
						Invoke ( ( (Action)( ( ) => {
							directoryTree.SelectedNode = dtn;
						} ) ) );
					} else {
						directoryTree.SelectedNode = dtn;
					}

					this.breadcrumbBar.ViewMode = BreadcrumbBar.ViewModes.Nodes;

					this.breadcrumbBar.Nodes.Clear ( );

					string name = LinuxPath.GetDirectoryName ( path.FullName );
					string device = KnownDeviceManager.Instance.GetDeviceFriendlyName ( string.IsNullOrEmpty ( CommandRunner.Instance.DefaultDevice ) ? CommandRunner.Instance.GetSerialNumber ( ) : CommandRunner.Instance.DefaultDevice );
					Image itemImage = null;
					if ( string.IsNullOrEmpty ( name ) ) {
						itemImage = DroidExplorer.Resources.Images.device_16x16;
						name = device;
					}

					this.breadcrumbBar.Nodes.Add ( new BreadcrumbBarNode ( device, string.Empty, delegate ( object sender, EventArgs e ) {
						BreadcrumbBarNode item = sender as BreadcrumbBarNode;

					}, this.breadcrumbBar.PathSeparator ) );

					StringBuilder tpath = new StringBuilder ( );
					foreach ( string s in path.FullName.Split ( new string[] { this.breadcrumbBar.PathSeparator }, StringSplitOptions.RemoveEmptyEntries ) ) {
						tpath.AppendFormat ( "{0}{1}", s, this.breadcrumbBar.PathSeparator );
						this.breadcrumbBar.Nodes.Add ( new BreadcrumbBarNode ( s, s, delegate ( object sender, EventArgs e ) {
							BreadcrumbBarNode item = sender as BreadcrumbBarNode;

						}, tpath ) );
					}


					if ( addHistory ) {
						if ( !explorerNavigation.History.ContainsKey ( path.FullName ) ) {
							this.explorerNavigation.AddHistory (
								new ExplorerNavigationHistoryItem ( name, path.FullName, itemImage,
									delegate ( object sender, EventArgs e ) {
										ExplorerNavigationHistoryItem item = ( sender as ExplorerNavigationHistoryItem );
										if ( item.Tag is DroidExplorer.Core.IO.LinuxDirectoryInfo ) {
											NavigateToPath ( item.Tag as DroidExplorer.Core.IO.LinuxDirectoryInfo, false );
										}
									}, path ), true );
						} else {
							this.explorerNavigation.HistoryGo ( this.explorerNavigation.History.IndexOf ( path.FullName ) );
						}
					}

					//this.breadcrumbBar.FullPath = string.Format ( "({0}){1}", device, path.FullName );
					this.breadcrumbBar.Root.Image = DroidExplorer.Resources.Images.device_16x16;

					//dtn.EnsureVisible ( );
					List<DroidExplorer.Core.IO.FileSystemInfo> fsiList = dtn.OnAfterSelect ( CommandRunner.Instance );

					folderUpToolStripButton.Enabled = path.Parent != null;
					CurrentDirectory = new DroidExplorer.Core.IO.LinuxDirectoryInfo ( dtn.LinuxPath );

					if ( filesThread != null && ( filesThread.IsAlive || filesThread.ThreadState == ThreadState.Running ) ) {
						filesThread.Abort ( );
					}

					filesThread = new Thread ( new ParameterizedThreadStart ( ThreadedBuildListViewItems ) );
					filesThread.Start ( fsiList );
					if ( !dtn.IsExpanded ) {
						dtn.Expand ( );
					}
				} else {
					TaskDialog.MessageBox ( "Address Bar", string.Format ( CultureInfo.InvariantCulture, Resources.Strings.AddressBarPathNotFoundMessage, path.FullName ), string.Empty,
						TaskDialogButtons.OK, SysIcons.Error );
					this.breadcrumbBar.ViewMode = BreadcrumbBar.ViewModes.Text;
				}
				navigating = false;
			}
		}

		private DirectoryTreeNode FindNodeFromPath ( DroidExplorer.Core.IO.LinuxDirectoryInfo path ) {
			directoryTree.ExpandToPath ( path.FullName );
			DirectoryTreeNode dtn = directoryTree.FindNodeFromPath ( path.FullName );
			return dtn;
		}


		private void SaveSettings ( ) {
			Settings.Instance.Save ( );
		}

		private void LoadSettings ( ) {
			switch ( Settings.Instance.Window.FolderView ) {
				case View.Details:
					this.detailsToolStripMenuItem.PerformClick ( );
					break;
				case View.LargeIcon:
					this.largeIconToolStripMenuItem.PerformClick ( );
					break;
				case View.List:
					this.listToolStripMenuItem.PerformClick ( );
					break;
				case View.SmallIcon:
					this.smallIconToolStripMenuItem.PerformClick ( );
					break;
				case View.Tile:
					this.tileToolStripMenuItem.PerformClick ( );
					break;
				default:
					break;
			}

			this.WindowState = Settings.Instance.Window.WindowState;

			if ( !Settings.Instance.Window.Size.IsEmpty ) {
				this.Size = Settings.Instance.Window.Size;
			}

			if ( !Settings.Instance.Window.Location.IsEmpty ) {
				int maxX = 0;
				int maxY = 0;

				foreach ( var item in Screen.AllScreens ) {
					maxX += item.Bounds.Width;
					if ( maxY < item.Bounds.Bottom ) {
						maxY += item.Bounds.Bottom;
					}
				}

				if ( Settings.Instance.Window.Location.X < 0 || Settings.Instance.Window.Location.Y < 0 ||
					Settings.Instance.Window.Location.X > maxX || Settings.Instance.Window.Location.Y > maxY ) {
					this.StartPosition = FormStartPosition.WindowsDefaultLocation;
				} else {
					this.StartPosition = FormStartPosition.Manual;
					this.Location = Settings.Instance.Window.Location;
				}
			} else {
				this.StartPosition = FormStartPosition.WindowsDefaultLocation;
			}

		}

		protected override void OnClosing ( CancelEventArgs e ) {
			Settings.Instance.Window.WindowState = this.WindowState;
			if ( this.WindowState == FormWindowState.Normal ) {
				Settings.Instance.Window.Location = this.Location;
			}

			ToolStripManager.SaveSettings ( this, "DroidExplorer" );

			if ( CommandRunner.Instance != null ) {
				this.LogDebug ( "Disconnecting" );
				CommandRunner.Instance.Disconnect ( );
			}

			// Save Settings
			SaveSettings ( );
			base.OnClosing ( e );
		}

		/// <summary>
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		protected override void OnResize ( EventArgs e ) {
			base.OnResize ( e );
			if ( this.WindowState == FormWindowState.Normal ) {
				Settings.Instance.Window.Size = this.Size;
			}
		}

		protected override void OnMove ( EventArgs e ) {
			base.OnMove ( e );
		}

		#region Delegate Handlers
		/// <summary>
		/// Sets the tool strip item enabled.
		/// </summary>
		/// <param name="tsi">The tsi.</param>
		/// <param name="enabled">if set to <c>true</c> [enabled].</param>
		private void SetToolStripItemEnabled ( ToolStripItem tsi, bool enabled ) {
			tsi.Enabled = enabled;
		}

		private void SetToolStripEnabled ( ToolStrip ts, bool enabled ) {
			ts.Enabled = enabled;
		}

		/// <summary>
		/// Sets the status bar label text.
		/// </summary>
		/// <param name="label">The label.</param>
		/// <param name="text">The text.</param>
		private void SetStatusBarLabelText ( ToolStripStatusLabel label, string text ) {
			label.Text = text;
		}

		/// <summary>
		/// Gets the list view items count.
		/// </summary>
		/// <param name="lv">The lv.</param>
		/// <returns></returns>
		private int GetListViewItemsCount ( ListView lv ) {
			return lv.Items.Count;
		}

		/// <summary>
		/// Gets the system bitmap.
		/// </summary>
		/// <param name="sysImgList">The sys img list.</param>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		private Image GetSystemBitmap ( SystemImageList sysImgList, int index ) {
			System.Drawing.Icon ico = sysImgList.Icon ( index );
			if ( ico != null ) {
				return ico.ToBitmap ( );
			} else {
				return null;
			}
		}

		/// <summary>
		/// Gets the index of the system icon.
		/// </summary>
		/// <param name="sysImgList">The sys img list.</param>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		private int GetSystemIconIndex ( SystemImageList sysImgList, string file ) {
			return sysImgList.IconIndex ( file, false );
		}

		/// <summary>
		/// Sets the index of the list view item image.
		/// </summary>
		/// <param name="lvi">The lvi.</param>
		/// <param name="index">The index.</param>
		private void SetListViewItemImageIndex ( ListViewItem lvi, int index ) {
			lvi.ImageIndex = index;
		}

		/// <summary>
		/// Sets the selected tree node.
		/// </summary>
		/// <param name="tv">The tv.</param>
		/// <param name="tn">The tn.</param>
		private void SetSelectedTreeNode ( TreeView tv, TreeNode tn ) {
			tv.SelectedNode = tn;
		}

		/// <summary>
		/// Sets the window title.
		/// </summary>
		/// <param name="title">The title.</param>
		private void SetWindowTitle ( string title ) {
			this.Text = title;
		}

		/// <summary>
		/// Adds the tree node.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <param name="tn">The tn.</param>
		private void AddTreeNode ( TreeNodeCollection parent, TreeNode tn ) {
			parent.Add ( tn );
		}

		/// <summary>
		/// Adds the list view item.
		/// </summary>
		/// <param name="lv">The lv.</param>
		/// <param name="lvi">The lvi.</param>
		private void AddListViewItem ( ListView lv, ListViewItem lvi ) {
			lv.Items.Add ( lvi );
		}
		#endregion

		/// <summary>
		/// Unchecks the view menu items.
		/// </summary>
		/// <param name="tsmi">The tsmi.</param>
		private void UncheckViewMenuItems ( ToolStripMenuItem tsmi ) {
			foreach ( ToolStripMenuItem var in viewStyleToolStripButton.DropDownItems ) {
				var.Checked = tsmi == var;
			}
		}
		private void largeIconToolStripMenuItem_Click ( object sender, EventArgs e ) {
			UncheckViewMenuItems ( sender as ToolStripMenuItem );
			itemsList.View = View.LargeIcon;
			Settings.Instance.Window.FolderView = itemsList.View;
		}

		private void smallIconToolStripMenuItem_Click ( object sender, EventArgs e ) {
			UncheckViewMenuItems ( sender as ToolStripMenuItem );
			itemsList.View = View.SmallIcon;
			Settings.Instance.Window.FolderView = itemsList.View;
		}

		private void listToolStripMenuItem_Click ( object sender, EventArgs e ) {
			UncheckViewMenuItems ( sender as ToolStripMenuItem );
			itemsList.View = View.List;
			Settings.Instance.Window.FolderView = itemsList.View;
		}

		private void tileToolStripMenuItem_Click ( object sender, EventArgs e ) {
			UncheckViewMenuItems ( sender as ToolStripMenuItem );
			itemsList.View = View.Tile;
			Settings.Instance.Window.FolderView = itemsList.View;
		}

		private void detailsToolStripMenuItem_Click ( object sender, EventArgs e ) {
			UncheckViewMenuItems ( sender as ToolStripMenuItem );
			itemsList.View = View.Details;
			Settings.Instance.Window.FolderView = itemsList.View;
		}

		private void foldersToolStripButton_Click ( object sender, EventArgs e ) {
			foldersToolStripButton.Checked = !foldersToolStripButton.Checked;
			splitContainer1.Panel1Collapsed = !foldersToolStripButton.Checked;
		}


		private void folderUpToolStripButton_Click ( object sender, EventArgs e ) {
			if ( this.CurrentDirectory.Parent != null ) {
				NavigateToPath ( this.CurrentDirectory.Parent );
			}
		}

		private void connectToDeviceToolStripMenuItem_Click ( object sender, EventArgs e ) {


			GenericDeviceSelectionForm dsf = new GenericDeviceSelectionForm ( );
			if ( dsf.ShowDialog ( this ) == DialogResult.OK && !string.IsNullOrEmpty ( dsf.SelectedDevice ) ) {
				CommandRunner.Instance.Disconnect ( );
				CommandRunner.Instance.DefaultDevice = dsf.SelectedDevice;
				CommandRunner.Instance.Connect ( );
			}
		}

		private void updateRomToolStripButton_Click ( object sender, EventArgs e ) {
			applyROMUpdateToolStripMenuItem.PerformClick ( );
		}

		private void copyToDeviceToolStripButton_Click ( object sender, EventArgs e ) {
			System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog ( );
			ofd.Title = "Select files to copy";
			ofd.Filter = "All Files (*.*)|*.*";
			ofd.FilterIndex = 0;
			ofd.CheckFileExists = true;
			ofd.Multiselect = true;
			//ofd.InitialDirectory = Environment.GetFolderPath ( Environment.SpecialFolder.Desktop );
			if ( ofd.ShowDialog ( this ) == DialogResult.OK ) {
				if ( ofd.FileNames != null && ofd.FileNames.Length > 0 ) {
					List<System.IO.FileInfo> files = new List<System.IO.FileInfo> ( );
					foreach ( var item in ofd.FileNames ) {
						files.Add ( new System.IO.FileInfo ( item ) );
					}
					var tf = new TransferDialog ( );
					if ( tf.PushDialog ( files, this.CurrentDirectory.FullName ) != DialogResult.OK ) {
						if ( tf.TransferException != null ) { // was there an error while transfering?
							TaskDialog.MessageBox ( "Transfer Error", tf.TransferException.Message, string.Empty, TaskDialogButtons.OK, SysIcons.Error );
						}
					} else {
						NavigateToPath ( this.CurrentDirectory );
					}
				}
			}
		}


		private void copyToLocalToolStripButton_Click ( object sender, EventArgs e ) {
			if ( itemsList.SelectedItems.Count > 0 ) {
				FolderBrowserDialog fbd = new FolderBrowserDialog ( );
				fbd.RootFolder = Environment.SpecialFolder.Desktop;
				fbd.Description = "Select destination for selected files";
				fbd.ShowNewFolderButton = true;
				if ( fbd.ShowDialog ( this ) == DialogResult.OK ) {
					string selPath = fbd.SelectedPath;
					List<DroidExplorer.Core.IO.FileInfo> files = new List<DroidExplorer.Core.IO.FileInfo> ( );
					foreach ( FileSystemInfoListViewItem item in itemsList.SelectedItems ) {
						if ( item.FileSystemInfo.IsDirectory ) {

						} else if ( item.FileSystemInfo.IsLink ) {

						} else if ( item.FileSystemInfo.IsPipe ) {

						} else if ( item.FileSystemInfo.IsSocket ) {

						} else { // file
							files.Add ( item.FileSystemInfo as DroidExplorer.Core.IO.FileInfo );
						}
					}

					var transfer = new TransferDialog ( );
					transfer.TransferComplete += delegate ( object s, EventArgs ea ) {

					};
					transfer.TransferError += delegate ( object s, EventArgs ea ) {
						if ( transfer.TransferException != null ) { // was there an error while transfering?
							TaskDialog.MessageBox ( "Transfer Error", transfer.TransferException.Message, string.Empty, TaskDialogButtons.OK, SysIcons.Error );
						}
					};
					transfer.Pull ( files, new System.IO.DirectoryInfo ( selPath ) );
				}
			}
		}


		private void installToolStripButton_Click ( object sender, EventArgs e ) {
			Program.PackageManager.Show ( );
		}

		#region Edit / Context Menu Event Handlers
		private void fileListContextMenuStrip_Opening ( object sender, CancelEventArgs e ) {
			if ( itemsList.SelectedItems.Count == 0 ) {
				// show "no files" mode
				newFileToolStripMenuItem.Visible = true;
				newFolderToolStripMenuItem.Visible = true;
				openToolStripMenuItem.Visible = false;
				openWithToolStripMenuItem.Visible = false;
				installToolStripMenuItem.Visible = false;
				uninstallToolStripMenuItem.Visible = false;

				copyToolStripMenuItem.Enabled = false;
				cutToolStripMenuItem.Enabled = false;

				deleteToolStripMenuItem.Enabled = false;

				newFileContextToolStripMenuItem.Visible = true;
				newFolderContextToolStripMenuItem.Visible = true;
				openContextToolStripMenuItem.Visible = false;
				openWithContextToolStripMenuItem.Visible = false;
				installContextToolStripMenuItem.Visible = false;
				uninstallContextToolStripMenuItem.Visible = false;

				copyContextToolStripMenuItem.Enabled = false;
				cutContextToolStripMenuItem.Enabled = false;

				deleteContextToolStripMenuItem.Enabled = true;

				propertiesToolStripMenuItem.Enabled = false;

			} else {
				bool hasFolder = false;
				foreach ( FileSystemInfoListViewItem item in itemsList.SelectedItems ) {
					if ( item.FileSystemInfo.IsDirectory ) {
						hasFolder = true;
						break;
					}
				}

				propertiesToolStripMenuItem.Enabled = true;

				newFileToolStripMenuItem.Visible = false;
				newFolderToolStripMenuItem.Visible = false;
				openWithToolStripMenuItem.Visible = !hasFolder;
				openToolStripMenuItem.Visible = true;

				copyToolStripMenuItem.Enabled = !hasFolder;
				cutToolStripMenuItem.Enabled = !hasFolder;

				newFileContextToolStripMenuItem.Visible = false;
				newFolderContextToolStripMenuItem.Visible = false;
				openWithContextToolStripMenuItem.Visible = !hasFolder;
				openContextToolStripMenuItem.Visible = true;

				copyContextToolStripMenuItem.Enabled = !hasFolder;
				cutContextToolStripMenuItem.Enabled = !hasFolder;

				if ( itemsList.SelectedItems.Count == 1 ) {
					bool isApk = itemsList.SelectedItems[0] is ApkFileSystemInfoListViewItem;

					installToolStripMenuItem.Visible = isApk;
					uninstallToolStripMenuItem.Visible = isApk;
					installContextToolStripMenuItem.Visible = isApk;
					uninstallContextToolStripMenuItem.Visible = isApk;

					BuildOpenMenu ( itemsList.SelectedItems[0] as FileSystemInfoListViewItem );

					//deleteToolStripMenuItem.Enabled = false;
				} else {
					installToolStripMenuItem.Visible = false;
					uninstallToolStripMenuItem.Visible = false;
					installContextToolStripMenuItem.Visible = false;
					uninstallContextToolStripMenuItem.Visible = false;
				}
			}

			pasteToolStripMenuItem.Enabled = Clipboard.ContainsFileDropList ( );
			selectAllToolStripMenuItem.Enabled = true;
			pasteContextToolStripMenuItem.Enabled = Clipboard.ContainsFileDropList ( );
			selectAllContextToolStripMenuItem.Enabled = true;

		}


		private List<ToolStripMenuItem> OpenWithItems { get; set; }

		private void BuildOpenMenu ( FileSystemInfoListViewItem item ) {

			foreach ( var x in OpenWithItems ) {
				fileListContextMenuStrip.Items.Remove ( x );
			}

			string ext = LinuxPath.GetExtension ( item.FileSystemInfo.Name );
			if ( FileTypeActionHandlers.ContainsKey ( ext ) ) {
				IFileTypeHandler ifth = FileTypeActionHandlers[ext];
				FileTypeHandlerToolStripMenuItem mi = new FileTypeHandlerToolStripMenuItem ( item, ifth );
				OpenWithItems.Add ( mi );
				int beforeIndex = fileListContextMenuStrip.Items.IndexOf ( openWithContextToolStripMenuItem );
				fileListContextMenuStrip.Items.Insert ( beforeIndex, mi );
			}
		}

		// TODO: async
		private void uninstallToolStripMenuItem_Click ( object sender, EventArgs e ) {
			if ( itemsList.SelectedItems.Count == 1 && itemsList.SelectedItems[0] is ApkFileSystemInfoListViewItem ) {
				ApkFileSystemInfoListViewItem apkFile = itemsList.SelectedItems[0] as ApkFileSystemInfoListViewItem;
				/*string package = apkFile.ApkInfo.Package;
				if ( string.IsNullOrEmpty ( package ) ) {
					// display error that we can not uninstall because we don't know the package name
					TaskDialog.MessageBox ( "Uninstall Error", string.Format ( Properties.Resources.UninstallErrorMessage, apkFile.Label ),
						Properties.Resources.UninstallErrorNoPackageMessage, TaskDialogButtons.OK, SysIcons.Error );
				} else {
					// uninstall and refresh if successful, otherwise display error message
					if ( CommandRunner.Instance.UninstallApk ( package ) ) {
						NavigateToPath ( this.CurrentDirectory );
					} else {
						TaskDialog.MessageBox ( "Uninstall Error", string.Format ( Properties.Resources.UninstallErrorMessage, apkFile.Label ),
							Properties.Resources.UninstallErrorGenericMessage, TaskDialogButtons.OK, SysIcons.Error );
					}
				}*/
				InstallDialog install = new InstallDialog ( this, InstallDialog.InstallMode.Uninstall, apkFile.ApkInfo );
				if ( install.ShowDialog ( this ) == DialogResult.OK ) {
					NavigateToPath ( this.CurrentDirectory );
				}
			}
		}

		// TODO: async
		private void installToolStripMenuItem_Click ( object sender, EventArgs e ) {
			if ( itemsList.SelectedItems.Count == 1 && itemsList.SelectedItems[0] is ApkFileSystemInfoListViewItem ) {
				ApkFileSystemInfoListViewItem apkFile = itemsList.SelectedItems[0] as ApkFileSystemInfoListViewItem;
				System.IO.FileInfo apk = CommandRunner.Instance.PullFile ( apkFile.FileSystemInfo.FullPath );
				apkFile.ApkInfo.LocalApk = apk.FullName;
				/*if ( apk.Exists ) {
					if ( CommandRunner.Instance.InstallApk ( apk.FullName ) ) {
						NavigateToPath ( this.CurrentDirectory );
					} else {
						TaskDialog.MessageBox ( "Install Error", string.Format ( Properties.Resources.InstallErrorMessage, apkFile.Label ),
							Properties.Resources.InstallErrorGenericMessage, TaskDialogButtons.OK, SysIcons.Error );
					}
				}*/

				InstallDialog install = new InstallDialog ( this, InstallDialog.InstallMode.Install, apkFile.ApkInfo );
				if ( install.ShowDialog ( this ) == DialogResult.OK ) {
					NavigateToPath ( this.CurrentDirectory );
				}
			}
		}

		private void openToolStripMenuItem_Click ( object sender, EventArgs e ) {
			if ( itemsList.SelectedItems.Count >= 0 ) {
				foreach ( var item in itemsList.SelectedItems ) {
					if ( item is FileSystemInfoListViewItem ) {
						FileSystemInfoListViewItem lvi = item as FileSystemInfoListViewItem;
						if ( lvi.FileSystemInfo.IsDirectory ) {
							NavigateToPath ( new DroidExplorer.Core.IO.LinuxDirectoryInfo ( lvi.FileSystemInfo.FullPath ) );
						} else {
							System.IO.FileInfo file = CommandRunner.Instance.PullFile ( lvi.FileSystemInfo.FullPath );
							CommandRunner.Instance.LaunchProcessWindow ( file.FullName, string.Empty, true );
						}
					}
				}
			}
		}

		private void openWithToolStripMenuItem_Click ( object sender, EventArgs e ) {
			if ( itemsList.SelectedItems.Count >= 0 ) {
				foreach ( var item in itemsList.SelectedItems ) {
					if ( item is FileSystemInfoListViewItem ) {
						FileSystemInfoListViewItem lvi = item as FileSystemInfoListViewItem;
						System.IO.FileInfo file = CommandRunner.Instance.PullFile ( lvi.FileSystemInfo.FullPath );
						OpenWith.ShowDialog ( this.Icon, file.FullName );
					}
				}
			}
		}

		private void newFolderToolStripMenuItem_Click ( object sender, EventArgs e ) {

		}

		private void newFileToolStripMenuItem_Click ( object sender, EventArgs e ) {

		}

		private void copyToolStripMenuItem_Click ( object sender, EventArgs e ) {
			List<string> lfiles = new List<string> ( );
			List<DroidExplorer.Core.IO.FileInfo> rfiles = new List<DroidExplorer.Core.IO.FileInfo> ( );
			foreach ( FileSystemInfoListViewItem item in itemsList.SelectedItems ) {
				if ( item.FileSystemInfo.IsLink ) {
					// links CAN NOT use ADB Pull/Push, the link needs to be resolved and then 
					// ADB Pull/Push can be used
					if ( !item.FileSystemInfo.IsDirectory ) {
						DroidExplorer.Core.IO.SymbolicLinkInfo lnk = item.FileSystemInfo as DroidExplorer.Core.IO.SymbolicLinkInfo;

						string linkFullPath = System.IO.Path.Combine ( CurrentDirectory.FullName, lnk.Link );
						this.LogDebug ( "Copying {0} link from {1}", lnk.Name, linkFullPath );
						rfiles.Add ( DroidExplorer.Core.IO.FileInfo.Create ( item.FileSystemInfo.Name,
							item.FileSystemInfo.Size, item.FileSystemInfo.UserPermissions,
							item.FileSystemInfo.GroupPermissions, item.FileSystemInfo.OtherPermissions,
							item.FileSystemInfo.LastModificationDateTime,/* item.FileSystemInfo.IsExecutable */ false,
							linkFullPath ) );
						lfiles.Add ( System.IO.Path.Combine ( FolderManagement.TempFolder, item.FileSystemInfo.Name ) );
					}
				} else if ( item.FileSystemInfo.IsDirectory ) {
					TaskDialog.MessageBox ( "Not Yet Implemented", "Can not copy directory to clipboard", "This feature will be added soon.", TaskDialogButtons.OK, SysIcons.Information );
				} else if ( item.FileSystemInfo.IsPipe ) {
					// do nothing as they are not yet visible
				} else if ( item.FileSystemInfo.IsSocket ) {
					// do nothing as they are not yet visible
				} else { // file
					rfiles.Add ( item.FileSystemInfo as DroidExplorer.Core.IO.FileInfo );
					lfiles.Add ( System.IO.Path.Combine ( FolderManagement.TempFolder, item.FileSystemInfo.Name ) );
				}
			}
			if ( rfiles.Count > 0 ) {
				var transfer = new TransferDialog ( );
				if ( transfer.PullDialog ( rfiles, new System.IO.DirectoryInfo ( FolderManagement.TempFolder ) ) == DialogResult.OK ) {
					this.Cursor = Cursors.WaitCursor;
					Clipboard.SetData ( DataFormats.FileDrop, lfiles.ToArray ( ) );
					this.Cursor = Cursors.Default;
				} else {
					if ( transfer.TransferException != null ) {
						TaskDialog.MessageBox ( "Copy error", transfer.TransferException.Message, string.Empty, TaskDialogButtons.OK, SysIcons.Error );
					}
				}
			}
		}

		private void cutToolStripMenuItem_Click ( object sender, EventArgs e ) {

		}

		private void pasteToolStripMenuItem_Click ( object sender, EventArgs e ) {
			if ( Clipboard.ContainsFileDropList ( ) ) {
				StringCollection files = Clipboard.GetFileDropList ( );
				List<System.IO.FileInfo> lfiles = new List<System.IO.FileInfo> ( );
				foreach ( var item in files ) {
					System.IO.FileInfo file = new System.IO.FileInfo ( item );
					if ( file.Exists ) {
						lfiles.Add ( file );
					}
				}
				var transfer = new TransferDialog ( );
				transfer.TransferComplete += delegate ( object s, EventArgs ea ) {
					if ( this.InvokeRequired ) {
						this.Invoke ( new NavigateToPathDelegate ( this.NavigateToPath ), new object[] { this.CurrentDirectory } );
					} else {
						this.NavigateToPath ( this.CurrentDirectory );
					}
				};
				transfer.TransferError += delegate ( object s, EventArgs ea ) {
					if ( transfer.TransferException != null ) {
						if ( this.InvokeRequired ) {
							this.Invoke ( new MessageBoxDelegate ( TaskDialog.MessageBox ), new object[] { "Paste error", transfer.TransferException.Message, string.Empty, TaskDialogButtons.OK, SysIcons.Error } );
						} else {
							TaskDialog.MessageBox ( "Paste error", transfer.TransferException.Message, string.Empty, TaskDialogButtons.OK, SysIcons.Error );
						}
					}
				};
				transfer.Push ( lfiles, this.CurrentDirectory.FullName );
			}
		}

		private void selectAllToolStripMenuItem_Click ( object sender, EventArgs e ) {
			foreach ( ListViewItem item in itemsList.Items ) {
				item.Selected = true;
			}
		}

		private void deleteToolStripMenuItem_Click ( object sender, EventArgs e ) {
			if ( itemsList.SelectedItems.Count > 0 && !InLabelEdit ) {

				TaskDialog.ShowTaskDialogBox ( "Confirm Delete", string.Format ( "Are you sure you want to delete the selected item{0}?", itemsList.SelectedItems.Count == 1 ? string.Empty : "s" ), string.Empty, string.Empty,
						string.Empty, string.Empty, string.Empty,
						string.Format ( "Delete selected item{0}|Do not delete selected item{0}", itemsList.SelectedItems.Count == 1 ? string.Empty : "s" ), TaskDialogButtons.None,
						SysIcons.Warning, SysIcons.Information );

				if ( TaskDialog.CommandButtonResult == 0 ) {
					foreach ( ListViewItem item in itemsList.SelectedItems ) {
						if ( item is FileSystemInfoListViewItem ) {
							FileSystemInfoListViewItem lvi = item as FileSystemInfoListViewItem;
							if ( lvi.FileSystemInfo.IsLink ) {
								// todo: remove sysmbolic link
							} else if ( !lvi.FileSystemInfo.IsDirectory ) {
								CommandRunner.Instance.DeleteFile ( lvi.FileSystemInfo.FullPath );
							} else if ( lvi.FileSystemInfo.IsDirectory ) {
								CommandRunner.Instance.DeleteDirectory ( lvi.FileSystemInfo.FullPath );
							}
						}
					}
					NavigateToPath ( this.CurrentDirectory );
				}
			}
		}


		private void propertiesToolStripMenuItem_Click ( object sender, EventArgs e ) {
			if ( itemsList.SelectedItems.Count > 0 && !InLabelEdit ) {
				FileSystemInfoListViewItem lvi = itemsList.SelectedItems[0] as FileSystemInfoListViewItem;
				FileProperiesDialog fpd = new FileProperiesDialog ( lvi.FileSystemInfo );
				if ( fpd.ShowDialog ( this ) == DialogResult.OK ) {
					// todo: refresh
				}
			}
		}
		#endregion

		private void installPackageToolStripMenuItem_Click ( object sender, EventArgs e ) {
			System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog ( );
			ofd.Filter = "Android Application|*.apk";
			ofd.FilterIndex = 0;
			ofd.Title = "Select Android Application to Install";
			if ( ofd.ShowDialog ( this ) == DialogResult.OK ) {
				AaptBrandingCommandResult apkInfo = CommandRunner.Instance.GetLocalApkInformation ( ofd.FileName );
				InstallDialog install = new InstallDialog ( this, InstallDialog.InstallMode.Install, apkInfo );
				install.ShowDialog ( this );
			}
		}

		#region IPluginHost Members
		public DroidExplorer.Core.IO.LinuxDirectoryInfo CurrentDirectory {
			get { return new DroidExplorer.Core.IO.LinuxDirectoryInfo ( this.currentDirectory ); }
			private set { this.currentDirectory = value.FullName; }
		}

		public void Pull ( Core.IO.FileInfo remoteFile, System.IO.FileInfo destFile ) {
			var transfer = new TransferDialog ( );
			transfer.PullDialog ( remoteFile, destFile );
		}

		public void Pull ( List<string> files, System.IO.DirectoryInfo destPath ) {
			var transfer = new TransferDialog ( );
			transfer.PullDialog ( files, destPath );
		}

		public void Pull ( List<Core.IO.FileInfo> files, System.IO.DirectoryInfo destPath ) {
			var transfer = new TransferDialog ( );
			transfer.PullDialog ( files, destPath );
		}

		public void Push ( List<System.IO.FileInfo> files, string destPath ) {
			var transfer = new TransferDialog ( );
			transfer.PushDialog ( files, destPath );
		}

		public void Push ( System.IO.FileInfo file, string remote ) {
			var transfer = new TransferDialog ( );
			transfer.PushDialog ( file, remote );
		}


		public void Navigate ( DroidExplorer.Core.IO.LinuxDirectoryInfo path ) {

		}

		public void Install ( System.IO.FileInfo file ) {

		}

		public void Uninstall ( string package ) {

		}

		public CommandRunner CommandRunner {
			get { return CommandRunner.Instance; }
		}


		public void RegisterFileTypeHandler ( string ext, IFileTypeHandler handler ) {
			if ( !this.FileTypeActionHandlers.ContainsKey ( ext ) ) {
				this.FileTypeActionHandlers.Add ( ext, handler );
			} else {
				this.LogWarn ( "Extension already contains a file type handler" );
			}
		}

		public void UnregisterFileTypeHandler ( string ext, IFileTypeHandler handler ) {
			this.FileTypeActionHandlers.Remove ( ext );
		}

		public void RegisterFileTypeIcon ( string ext, Image smallImage, Image largeImage ) {
			SystemImageListHost.Instance.AddFileTypeImage ( ext, smallImage, largeImage );
		}

		public void RegisterFileTypeIconHandler ( string ext, IFileTypeIconHandler handler ) {
			if ( !this.FileTypeIconHandlers.ContainsKey ( ext ) ) {
				this.FileTypeIconHandlers.Add ( ext, handler );
			} else {
				this.LogWarn ( "Attempted to register a second icon handler for {0}.", ext );
			}
		}

		public IWin32Window GetHostWindow ( ) {
			return this;
		}

		public Control GetHostControl ( ) {
			return this;
		}

		public string GetDeviceFriendlyName ( string device ) {
			return KnownDeviceManager.Instance.GetDeviceFriendlyName ( device );
		}

		public void SetDeviceFriendlyName ( string device, string name ) {
			KnownDeviceManager.Instance.SetDeviceFriendlyName ( device, name );
		}

		public int ShowCommandBox ( string title, string main, string content, string expandedInfo, string footer, string verification, string buttons, bool showCancel, MessageBoxIcon icon, MessageBoxIcon footerIcon ) {
			SysIcons sicon = SysIcons.Information;
			SysIcons ficon = SysIcons.Information;
			switch ( icon ) {
				case MessageBoxIcon.Asterisk:
				case MessageBoxIcon.Error:
					sicon = SysIcons.Error;
					break;
				case MessageBoxIcon.Question:
					sicon = SysIcons.Question;
					break;
				case MessageBoxIcon.Warning:
					sicon = SysIcons.Warning;
					break;
			}

			switch ( footerIcon ) {
				case MessageBoxIcon.Asterisk:
				case MessageBoxIcon.Error:
					ficon = SysIcons.Error;
					break;
				case MessageBoxIcon.Question:
					ficon = SysIcons.Question;
					break;
				case MessageBoxIcon.Warning:
					ficon = SysIcons.Warning;
					break;
			}

			return TaskDialog.ShowCommandBox ( title, main, content, expandedInfo, footer, verification, buttons, showCancel, sicon, ficon );
		}

		public DialogResult MessageBox ( string title, string main, string content, MessageBoxButtons buttons, MessageBoxIcon icon ) {
			TaskDialogButtons tdb = TaskDialogButtons.Close;
			SysIcons sicon = SysIcons.Information;
			switch ( icon ) {
				case MessageBoxIcon.Asterisk:
				case MessageBoxIcon.Error:
					sicon = SysIcons.Error;
					break;
				case MessageBoxIcon.Question:
					sicon = SysIcons.Question;
					break;
				case MessageBoxIcon.Warning:
					sicon = SysIcons.Warning;
					break;
			}
			switch ( buttons ) {
				case MessageBoxButtons.OK:
					tdb = TaskDialogButtons.OK;
					break;
				case MessageBoxButtons.OKCancel:
					tdb = TaskDialogButtons.OKCancel;
					break;
				case MessageBoxButtons.RetryCancel:
					tdb = TaskDialogButtons.OKCancel;
					break;
				case MessageBoxButtons.YesNo:
					tdb = TaskDialogButtons.YesNo;
					break;
				case MessageBoxButtons.YesNoCancel:
				case MessageBoxButtons.AbortRetryIgnore:
					tdb = TaskDialogButtons.YesNoCancel;
					break;
				default:
					tdb = TaskDialogButtons.Close;
					break;
			}

			return TaskDialog.MessageBox ( title, main, content, tdb, sicon );
		}

		public String Device {
			get {
				return CommandRunner.Instance.DefaultDevice;
			}
		}
		#endregion

		#region Tools Menu

		private void optionsToolStripMenuItem_Click ( object sender, EventArgs e ) {
			new OptionsForm ( ).ShowDialog ( this );
		}

		private void aboutDroidExplorerToolStripMenuItem_Click ( object sender, EventArgs e ) {
			new AboutDialog ( ).ShowDialog ( this );
		}

		private void reportABugToolStripMenuItem_Click ( object sender, EventArgs e ) {
			CommandRunner.Instance.LaunchProcessWindow ( DroidExplorer.Resources.Strings.IssueTrackerCreateUrl, string.Empty, true );
		}

		private void askForHelpToolStripMenuItem1_Click ( object sender, EventArgs e ) {
			CommandRunner.Instance.LaunchProcessWindow ( DroidExplorer.Resources.Strings.SupportUrl, string.Empty, true );
		}



		private void applyROMUpdateToolStripMenuItem_Click ( object sender, EventArgs e ) {
			System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog ( );
			ofd.Title = "Select ROM Update .zip";
			//ofd.RestoreDirectory = true;
			ofd.Multiselect = false;
			//ofd.InitialDirectory = Environment.GetFolderPath ( Environment.SpecialFolder.Desktop );
			ofd.Filter = "Zip Files|*.zip";
			ofd.FilterIndex = 0;
			if ( ofd.ShowDialog ( this ) == DialogResult.OK ) {
				var transfer = new TransferDialog ( );
				if ( transfer.PushDialog ( new System.IO.FileInfo ( ofd.FileName ), "/sdcard/update.zip" ) == DialogResult.OK ) {
					TaskDialog.ShowTaskDialogBox ( "Apply Update?", "Do you want to apply the uploaded update now?", string.Empty, string.Empty,
						string.Empty, string.Empty, string.Empty, "Reboot device to apply update now|Reboot in recovery but do not apply|Do not apply update now", TaskDialogButtons.None,
						SysIcons.Question, SysIcons.Information );
					if ( TaskDialog.CommandButtonResult == 0 ) {
						// apply now
						CommandRunner.Instance.ApplyUpdate ( );
					} else if ( TaskDialog.CommandButtonResult == 1 ) {
						CommandRunner.Instance.RebootRecovery ( );
					}
				} else {
					if ( transfer.TransferException != null ) { // was there an error while transfering?
						TaskDialog.MessageBox ( "Transfer Error", transfer.TransferException.Message, string.Empty, TaskDialogButtons.OK, SysIcons.Error );
					}
				}
			}
		}

		private void flashRecoveryImageToolStripMenuItem_Click ( object sender, EventArgs e ) {
			System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog ( );
			ofd.Title = "Select Recovery Image";
			ofd.Filter = "Recovery Images|*.img|All Files (*.*)|*.*";
			ofd.FilterIndex = 0;
			//ofd.InitialDirectory = Environment.GetFolderPath ( Environment.SpecialFolder.Desktop );
			//ofd.RestoreDirectory = true;
			ofd.Multiselect = false;
			if ( ofd.ShowDialog ( this ) == DialogResult.OK ) {
				string recoveryImage = string.Format ( "/sdcard/{0}", System.IO.Path.GetFileName ( ofd.FileName ) );
				var tf = new TransferDialog ( );
				if ( tf.PushDialog ( new System.IO.FileInfo ( ofd.FileName ), recoveryImage ) != DialogResult.OK ) {
					TaskDialog.ShowTaskDialogBox ( "Flash Recovery Image?", "Recovery image has been transfered to device.", string.Empty, string.Empty,
						string.Empty, string.Empty, string.Empty, "Flash image now|Do not do not flash image now", TaskDialogButtons.None,
						SysIcons.Question, SysIcons.Information );
					if ( TaskDialog.CommandButtonResult == 0 ) {
						// apply now
						try {
							CommandRunner.Instance.FlashImage ( recoveryImage );
							TaskDialog.MessageBox ( "Flash Complete", "The recovery image was successfully flashed to the device.", string.Empty, TaskDialogButtons.OK, SysIcons.Information );
						} catch ( Exception ex ) {
							TaskDialog.MessageBox ( "Flash Error", ex.Message, string.Empty, TaskDialogButtons.OK, SysIcons.Error );
						}
					}
				} else {
					if ( tf.TransferException != null ) { // was there an error while transfering?
						TaskDialog.MessageBox ( "Transfer Error", tf.TransferException.Message, string.Empty, TaskDialogButtons.OK, SysIcons.Error );
					}
				}
			}
		}

		private void eraseFlashRecoveryImageToolStripMenuItem_Click ( object sender, EventArgs e ) {
			System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog ( );
			ofd.Title = "Select Recovery Image";
			ofd.Filter = "Recovery Images|*.img|All Files (*.*)|*.*";
			ofd.FilterIndex = 0;
			//ofd.InitialDirectory = Environment.GetFolderPath ( Environment.SpecialFolder.Desktop );
			//ofd.RestoreDirectory = true;
			ofd.Multiselect = false;
			if ( ofd.ShowDialog ( this ) == DialogResult.OK ) {
				string recoveryImage = string.Format ( "/sdcard/{0}", System.IO.Path.GetFileName ( ofd.FileName ) );
				//CommandRunner.Instance.PushFile ( ofd.FileName, recoveryImage );
				var tf = new TransferDialog ( );
				if ( tf.PushDialog ( new System.IO.FileInfo ( ofd.FileName ), recoveryImage ) != DialogResult.OK ) {
					TaskDialog.ShowTaskDialogBox ( "Flash Recovery Image?", "Recovery image has been transfered to device.", string.Empty, string.Empty,
						string.Empty, string.Empty, string.Empty, "Flash image now|Do not do not flash image now", TaskDialogButtons.None,
						SysIcons.Question, SysIcons.Information );
					if ( TaskDialog.CommandButtonResult == 0 ) {
						// apply now
						try {
							CommandRunner.Instance.FastbootEraseRecovery ( );
							CommandRunner.Instance.FastbootFlashImage ( recoveryImage );
							TaskDialog.MessageBox ( "Flash Complete", "The recovery image was successfully flashed to the device.", string.Empty, TaskDialogButtons.OK, SysIcons.Information );
						} catch ( Exception ex ) {
							TaskDialog.MessageBox ( "Flash Error", ex.Message, string.Empty, TaskDialogButtons.OK, SysIcons.Error );
						}
					}
				} else {
					if ( tf.TransferException != null ) { // was there an error while transfering?
						TaskDialog.MessageBox ( "Transfer Error", tf.TransferException.Message, string.Empty, TaskDialogButtons.OK, SysIcons.Error );
					}
				}
			}
		}

		private void rebootToolStripMenuItem_Click ( object sender, EventArgs e ) {
			TaskDialog.ShowTaskDialogBox ( "Confirm reboot", "Do you want to reboot the device?", string.Empty, string.Empty,
						string.Empty, string.Empty, string.Empty, "Reboot device|Do not reboot device", TaskDialogButtons.None,
						SysIcons.Question, SysIcons.Information );
			if ( TaskDialog.CommandButtonResult == 0 ) {
				CommandRunner.Instance.Reboot ( );
			}
		}

		private void rebootInToRecoveryToolStripMenuItem_Click ( object sender, EventArgs e ) {
			TaskDialog.ShowTaskDialogBox ( "Confirm reboot", "Do you want to reboot the device in to recovery mode?", string.Empty, string.Empty,
						string.Empty, string.Empty, string.Empty, "Reboot device in to recovery mode|Do not reboot device", TaskDialogButtons.None,
						SysIcons.Question, SysIcons.Information );
			if ( TaskDialog.CommandButtonResult == 0 ) {
				CommandRunner.Instance.RebootRecovery ( );
			}
		}
		#endregion





	}
}