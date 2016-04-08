using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Core.UI.Components;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using System.Threading;
using DroidExplorer.Core.IO;
using Managed.Adb.IO;

namespace DroidExplorer.Core.UI {

	public abstract partial class FileDialog : Form {
		private string _filter;
		private string _initialDirectory;
		private DroidExplorer.Core.IO.LinuxDirectoryInfo _currentPath;

		/// <summary>
		/// Initializes a new instance of the <see cref="FileDialog"/> class.
		/// </summary>
		public FileDialog ( ) : this ( new string ( new char[] { LinuxPath.DirectorySeparatorChar } ) ) {

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FileDialog"/> class.
		/// </summary>
		/// <param name="initialDirectory">The initial directory.</param>
		public FileDialog ( string initialDirectory ) {
			InitializeComponent ( );

			InitialDirectory = initialDirectory;

			Title = "Open File";
			CheckFileExists = true;
			CheckPathExists = true;
			SelectedFilter = "*";

			InitializeCustomPlaces ( );


			// real icon setup
			backToolStripButton.Image = DroidExplorer.Resources.Images.back;
			parentToolStripButton.Image = DroidExplorer.Resources.Images.OneLevelUp_5834;
			newFolderToolStripButton.Image = DroidExplorer.Resources.Images.NewSolutionFolder_6289;
			viewModeToolStripDropDownButton.Image = DroidExplorer.Resources.Images.Views_7953;

			this.selectedPath.AddControl ( pathTree );
			this.fileTypes.SelectedIndexChanged += delegate ( object sender, EventArgs e ) {
				if ( fileTypes.SelectedItem == null ) {
					this.SelectedFilter = "*";
				} else {
					this.SelectedFilter = ( this.fileTypes.SelectedItem as FileTypeFilterItem ).Filter;
				}

				if ( this.CurrentPath != null ) {
					Navigate ( this.CurrentPath );
				}
			};

			this.files.SmallImageList = SystemImageListHost.Instance.SmallImageList;
			this.files.LargeImageList = SystemImageListHost.Instance.LargeImageList;
			this.files.SelectedIndexChanged += delegate ( object sender, EventArgs e ) {
				if ( this.files.SelectedItems.Count == 1 ) {
					FileSystemInfoListViewItem fsilvi = this.files.SelectedItems[0] as FileSystemInfoListViewItem;
					if ( !fsilvi.FileSystemInfo.IsDirectory ) {
						SetFileName ( fsilvi.FileSystemInfo.FullPath );
					}
				} else {
					if ( this.Multiselect ) {

					}
				}
			};

			this.files.DoubleClick += delegate ( object sender, EventArgs e ) {
				if ( this.files.SelectedItems.Count == 1 ) {
					FileSystemInfoListViewItem fsilvi = this.files.SelectedItems[0] as FileSystemInfoListViewItem;
					if ( fsilvi.FileSystemInfo.IsDirectory ) {
						Navigate ( new DroidExplorer.Core.IO.LinuxDirectoryInfo ( fsilvi.FileSystemInfo.FullPath ) );
					} else {
						OpenFileName ( fsilvi.FileSystemInfo.FullPath );
					}
				}
			};
		}

		/// <summary>
		/// Initializes the custom places.
		/// </summary>
		private void InitializeCustomPlaces ( ) {
			this.CustomPlaces.Clear ( );
			#region Custom Places buttons
			this.CustomPlaces.Add ( new FileDialogCustomPlace ( "/", DroidExplorer.Resources.Images.mobile_32xLG, delegate ( object sender, EventArgs e ) {
				this.Navigate ( new DroidExplorer.Core.IO.LinuxDirectoryInfo ( "/" ) );
			} ) );
			if ( !CustomPlaces.Any ( x => x.Path == "/sdcard/" ) ) {
				this.CustomPlaces.Add ( new FileDialogCustomPlace ( "/sdcard/", "SD Card", DroidExplorer.Resources.Images.sdcard, delegate ( object sender, EventArgs e ) {
					Navigate ( new DroidExplorer.Core.IO.LinuxDirectoryInfo ( "/sdcard/" ) );
				} ) );
			}
			if ( !CustomPlaces.Any ( x => x.Path == "/data/app/" ) ) {
				this.CustomPlaces.Add ( new FileDialogCustomPlace ( "/data/app/", "Installed Apps", DroidExplorer.Resources.Images.package32, delegate ( object sender, EventArgs e ) {
					Navigate ( new DroidExplorer.Core.IO.LinuxDirectoryInfo ( "/data/app/" ) );
				} ) );
			}
			if ( !CustomPlaces.Any ( x => x.Path == "/system/app/" ) ) {
				this.CustomPlaces.Add ( new FileDialogCustomPlace ( "/system/app/", "System Apps", DroidExplorer.Resources.Images.package32, delegate ( object sender, EventArgs e ) {
					Navigate ( new DroidExplorer.Core.IO.LinuxDirectoryInfo ( "/system/app/" ) );
				} ) );
			}

			#endregion
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.Load" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnLoad ( EventArgs e ) {
			base.OnLoad ( e );
			Navigate ( CurrentPath );
		}

		/// <summary>
		/// Opens the name of the file.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		private void OpenFileName ( string filePath ) {
			SetFileName ( filePath );
			if ( PerformChecks ( filePath ) ) {
				this.DialogResult = DialogResult.OK;
			}
		}

		/// <summary>
		/// Performs the checks.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <returns></returns>
		private bool PerformChecks ( string filePath ) {
			return !string.IsNullOrEmpty ( filePath );
		}

		/// <summary>
		/// Sets the name of the file.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		private void SetFileName ( string filePath ) {
			this.FileName = LinuxPath.GetFileName ( filePath );
		}

		/// <summary>
		/// Navigates the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		public void Navigate ( DroidExplorer.Core.IO.LinuxDirectoryInfo path ) {
			if ( NavigateThread != null && NavigateThread.IsAlive ) {
				try {
					NavigateThread.Abort ( );
				} catch ( ThreadAbortException taex ) {

				}
			}
			NavigateThread = new Thread ( new ParameterizedThreadStart ( delegate ( object state ) {
				if ( state is DroidExplorer.Core.IO.LinuxDirectoryInfo ) {
					DroidExplorer.Core.IO.LinuxDirectoryInfo dir = state as DroidExplorer.Core.IO.LinuxDirectoryInfo;
					PrivateNavigate ( dir );
				}
			} ) );
			NavigateThread.Start ( path );
		}

		/// <summary>
		/// Privates the navigate.
		/// </summary>
		/// <param name="path">The path.</param>
		private void PrivateNavigate ( DroidExplorer.Core.IO.LinuxDirectoryInfo path ) {
			this.CurrentPath = path;
			string pathName = LinuxPath.GetDirectoryName ( path.FullName );
			if ( string.IsNullOrEmpty ( pathName ) ) {
				pathName = CommandRunner.Instance.GetSerialNumber ( );
			}
			if ( this.InvokeRequired ) {
				this.Invoke ( new GenericDelegate ( this.files.Items.Clear ) );
				this.Invoke ( new SetComboBoxExDisplayValueDelegate ( this.SetComboBoxExDisplayValue ), this.selectedPath, pathName );
			} else {
				this.files.Items.Clear ( );
				SetComboBoxExDisplayValue ( this.selectedPath, pathName );
			}
			List<DroidExplorer.Core.IO.FileSystemInfo> fsis = CommandRunner.Instance.GetDirectoryContents ( path.FullName );
			foreach ( var item in fsis ) {
				FileSystemInfoListViewItem lvi = new FileSystemInfoListViewItem ( item );
				if ( ( item.IsDirectory && !item.IsPipe && !item.IsSocket ) || ( IsFilterMatch ( item, SelectedFilter ) && !item.IsPipe && !item.IsSocket ) ) {
					if ( !item.IsDirectory && !item.IsExecutable ) {
						string ext = System.IO.Path.GetExtension ( lvi.FileSystemInfo.Name );
						string keyName = ext.ToLower ( );
						if ( keyName.StartsWith ( "/" ) ) {
							keyName = keyName.Substring ( 1 );
						}
						keyName = keyName.Replace ( "/", "." );

						if ( !string.IsNullOrEmpty ( ext ) ) {



							if ( string.Compare ( ext, ".apk", true ) == 0 ) {

								AaptBrandingCommandResult result = CommandRunner.Instance.GetApkInformation ( item.FullPath );
								lvi = new ApkFileSystemInfoListViewItem ( item, result );

								keyName = lvi.FileSystemInfo.FullPath;
								if ( keyName.StartsWith ( "/" ) ) {
									keyName = keyName.Substring ( 1 );
								}
								keyName = keyName.Replace ( "/", "." );

								if ( !SystemImageListHost.Instance.SystemIcons.ContainsKey ( keyName ) ) {
									// get apk and extract the app icon
									Image img = CommandRunner.Instance.GetLocalApkIconImage ( result.LocalApk );

									if ( img == null ) {
										img = DroidExplorer.Resources.Images.package32;
									} else {
										using ( System.IO.MemoryStream stream = new System.IO.MemoryStream ( ) ) {
											string lpath = System.IO.Path.GetDirectoryName ( this.GetType ( ).Assembly.Location );
											string fileName = System.IO.Path.Combine ( System.IO.Path.Combine ( CommandRunner.Settings.UserDataDirectory, Cache.APK_IMAGE_CACHE ), string.Format ( "{0}.png", keyName ) );
											img.Save ( stream, ImageFormat.Png );
											stream.Position = 0;
											using ( System.IO.FileStream fs = new System.IO.FileStream ( fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write ) ) {
												byte[] buffer = new byte[2048];
												int readBytes = 0;
												while ( ( readBytes = stream.Read ( buffer, 0, buffer.Length ) ) != 0 ) {
													fs.Write ( buffer, 0, readBytes );
												}
											}
										}

									}
									SystemImageListHost.Instance.AddFileTypeImage ( keyName, img, img );
								}
								if ( this.InvokeRequired ) {
									this.Invoke ( new SetListViewItemImageIndexDelegate ( SetListViewItemImageIndex ), lvi, SystemImageListHost.Instance.SystemIcons[keyName] );
								} else {
									SetListViewItemImageIndex ( lvi, SystemImageListHost.Instance.SystemIcons[keyName] );
								}
							} else {
								if ( !SystemImageListHost.Instance.SystemIcons.ContainsKey ( ext.ToLower ( ) ) ) { // add index and icon
									Image sico;
									Image lico;
									if ( this.InvokeRequired ) {
										int iconIndex = (int)this.Invoke ( new GetSystemIconIndexDelegate ( GetSystemIconIndex ), new object[] { SystemImageListHost.Instance.SmallSystemImageList, item.Name } );
										sico = (Image)this.Invoke ( new GetSystemBitmapDelegate ( GetSystemBitmap ), new object[] { SystemImageListHost.Instance.SmallSystemImageList, iconIndex } );
										lico = (Image)this.Invoke ( new GetSystemBitmapDelegate ( GetSystemBitmap ), new object[] { SystemImageListHost.Instance.LargeSystemImageList, iconIndex } );
									} else {
										int iconIndex = SystemImageListHost.Instance.SmallSystemImageList.IconIndex ( item.Name, false );
										sico = GetSystemBitmap ( SystemImageListHost.Instance.SmallSystemImageList, iconIndex );
										lico = GetSystemBitmap ( SystemImageListHost.Instance.LargeSystemImageList, iconIndex );
									}
									SystemImageListHost.Instance.AddFileTypeImage ( ext.ToLower ( ), sico, lico );
								}
							}

							if ( this.InvokeRequired ) {
								this.Invoke ( new SetListViewItemImageIndexDelegate ( this.SetListViewItemImageIndex ), new object[] { lvi, SystemImageListHost.Instance.SystemIcons[keyName] } );
							} else {
								SetListViewItemImageIndex ( lvi, SystemImageListHost.Instance.SystemIcons[keyName] );
							}
						}
					}
					if ( this.InvokeRequired ) {
						this.Invoke ( new AddListViewItemDelegate ( AddListViewItem ), this.files, lvi );
					} else {
						AddListViewItem ( this.files, lvi );
					}
				}
			}

			if ( this.InvokeRequired ) {
				this.Invoke ( new AutoResizeColumnsDelegate ( AutoResizeColumns ), this.files, ColumnHeaderAutoResizeStyle.ColumnContent );
			} else {
				AutoResizeColumns ( this.files, ColumnHeaderAutoResizeStyle.ColumnContent );
			}
		}

		/// <summary>
		/// Automatics the resize columns.
		/// </summary>
		/// <param name="lv">The lv.</param>
		/// <param name="resizeStyle">The resize style.</param>
		private void AutoResizeColumns ( ListView lv, ColumnHeaderAutoResizeStyle resizeStyle ) {
			if ( lv.Items.Count == 0 ) {
				lv.AutoResizeColumns ( ColumnHeaderAutoResizeStyle.HeaderSize );
			} else {
				lv.AutoResizeColumns ( resizeStyle );
			}
		}
		/// <summary>
		/// Gets the system bitmap.
		/// </summary>
		/// <param name="sysImgList">The system img list.</param>
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
		/// <param name="sysImgList">The system img list.</param>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		private int GetSystemIconIndex ( SystemImageList sysImgList, string file ) {
			return sysImgList.IconIndex ( file, false );
		}

		/// <summary>
		/// Adds the ListView item.
		/// </summary>
		/// <param name="lv">The lv.</param>
		/// <param name="lvi">The lvi.</param>
		private void AddListViewItem ( ListView lv, ListViewItem lvi ) {
			lv.Items.Add ( lvi );
		}

		/// <summary>
		/// Sets the index of the ListView item image.
		/// </summary>
		/// <param name="lvi">The lvi.</param>
		/// <param name="index">The index.</param>
		private void SetListViewItemImageIndex ( ListViewItem lvi, int index ) {
			lvi.ImageIndex = index;
		}

		/// <summary>
		/// Determines whether [is filter match] [the specified fsi].
		/// </summary>
		/// <param name="fsi">The fsi.</param>
		/// <param name="filter">The filter.</param>
		/// <returns></returns>
		private bool IsFilterMatch ( DroidExplorer.Core.IO.FileSystemInfo fsi, string filter ) {
			string[] split = filter.Split ( ";".ToCharArray ( ), StringSplitOptions.RemoveEmptyEntries );
			foreach ( var item in split ) {
				if ( string.Compare ( item, "*", true ) == 0 || string.Compare ( item, "*.*", true ) == 0 ) {
					return true;
				}
				Regex regex = new Regex ( item.Replace ( ".", "\\." ).Replace ( "*", ".*" ).Replace ( "?", ".{1}" ), RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace );
				if ( regex.IsMatch ( fsi.Name ) ) {
					return true;
				}
			}
			return false;
		}


		/// <summary>
		/// Gets or sets the current path.
		/// </summary>
		/// <value>
		/// The current path.
		/// </value>
		internal DroidExplorer.Core.IO.LinuxDirectoryInfo CurrentPath {
			get { return this._currentPath; }
			set {
				this._currentPath = value;
				if ( this.InvokeRequired ) {
					this.Invoke ( new SetToolStripItemEnabledDelegate ( SetToolStripItemEnabled ), parentToolStripButton, this.CurrentPath != null && this.CurrentPath.Parent != null );
				} else {
					SetToolStripItemEnabled ( parentToolStripButton, this.CurrentPath != null && this.CurrentPath.Parent != null );
				}

			}
		}
		/// <summary>
		/// Gets or sets the selected filter.
		/// </summary>
		/// <value>
		/// The selected filter.
		/// </value>
		internal string SelectedFilter { get; set; }
		/// <summary>
		/// Gets or sets the navigate thread.
		/// </summary>
		/// <value>
		/// The navigate thread.
		/// </value>
		internal Thread NavigateThread { get; set; }

		/// <summary>
		/// Gets or sets the ok text.
		/// </summary>
		/// <value>
		/// The ok text.
		/// </value>
		protected string OkText { get { return this.ok.Text; } set { this.ok.Text = value; } }
		/// <summary>
		/// Gets or sets a value indicating whether [add extension].
		/// </summary>
		/// <value>
		///   <c>true</c> if [add extension]; otherwise, <c>false</c>.
		/// </value>
		public bool AddExtension { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether [check file exists].
		/// </summary>
		/// <value>
		///   <c>true</c> if [check file exists]; otherwise, <c>false</c>.
		/// </value>
		public bool CheckFileExists { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether [check path exists].
		/// </summary>
		/// <value>
		///   <c>true</c> if [check path exists]; otherwise, <c>false</c>.
		/// </value>
		public bool CheckPathExists { get; set; }
		/// <summary>
		/// Gets the custom places.
		/// </summary>
		/// <value>
		/// The custom places.
		/// </value>
		public FileDialogCustomPlacesCollection CustomPlaces { get { return this.customPlacesPanel.CustomPlaces; } }
		/// <summary>
		/// Gets or sets the default ext.
		/// </summary>
		/// <value>
		/// The default ext.
		/// </value>
		public string DefaultExt { get; set; }
		/// <summary>
		/// Gets or sets the filenames.
		/// </summary>
		/// <value>
		/// The filenames.
		/// </value>
		public string[] Filenames { get; set; }
		/// <summary>
		/// Gets or sets the initial directory.
		/// </summary>
		/// <value>
		/// The initial directory.
		/// </value>
		public string InitialDirectory {
			get { return this._initialDirectory; }
			set {
				this._initialDirectory = value;
				this.CurrentPath = new DroidExplorer.Core.IO.LinuxDirectoryInfo ( value );
				InitializeCustomPlaces ( );
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="FileDialog" /> is multiselect.
		/// </summary>
		/// <value>
		///   <c>true</c> if multiselect; otherwise, <c>false</c>.
		/// </value>
		public bool Multiselect { get { return this.files.MultiSelect; } set { this.files.MultiSelect = value; } }
		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		public string Title { get { return this.Text; } set { this.Text = value; } }
		/// <summary>
		/// Gets or sets a value indicating whether [validate names].
		/// </summary>
		/// <value>
		///   <c>true</c> if [validate names]; otherwise, <c>false</c>.
		/// </value>
		public bool ValidateNames { get; set; }
		/// <summary>
		/// Gets or sets the filter.
		/// </summary>
		/// <value>
		/// The filter.
		/// </value>
		public string Filter {
			get { return this._filter; }
			set {
				this._filter = value;
				this.SetFilters ( );
			}
		}
		/// <summary>
		/// Gets or sets the index of the filter.
		/// </summary>
		/// <value>
		/// The index of the filter.
		/// </value>
		public int FilterIndex {
			get { return this.fileTypes.SelectedIndex; }
			set { this.fileTypes.SelectedIndex = value; }
		}
		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>
		/// The name of the file.
		/// </value>
		public string FileName { get { return CurrentPath.FullName + this.fileName.Text; } set { this.fileName.Text = value; } }

		/// <summary>
		/// Sets the filters.
		/// </summary>
		private void SetFilters ( ) {
			this.fileTypes.Items.Clear ( );

			string[] split = this.Filter.Split ( "|".ToCharArray ( ) );

			for ( int i = 0; i < split.Length; i += 2 ) {
				this.fileTypes.Items.Add ( new FileTypeFilterItem ( split[i], split[i + 1] ) );
			}

		}

		/// <summary>
		/// Sets the tool strip item enabled.
		/// </summary>
		/// <param name="tsi">The tsi.</param>
		/// <param name="enabled">if set to <c>true</c> [enabled].</param>
		private void SetToolStripItemEnabled ( ToolStripItem tsi, bool enabled ) {
			tsi.Enabled = enabled;
		}

		/// <summary>
		/// Sets the ComboBox ex display value.
		/// </summary>
		/// <param name="cmbo">The cmbo.</param>
		/// <param name="text">The text.</param>
		private void SetComboBoxExDisplayValue ( ComboBoxEx cmbo, string text ) {
			cmbo.SetDisplayValue ( text );
		}

		/// <summary>
		/// Handles the Click event of the iconsToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void iconsToolStripMenuItem_Click ( object sender, EventArgs e ) {
			this.files.View = View.SmallIcon;
			foreach ( var item in viewModeToolStripDropDownButton.DropDownItems ) {
				( item as ToolStripMenuItem ).Checked = item.Equals ( sender );
			}
		}

		/// <summary>
		/// Handles the Click event of the listToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void listToolStripMenuItem_Click ( object sender, EventArgs e ) {
			this.files.View = View.List;
			foreach ( var item in viewModeToolStripDropDownButton.DropDownItems ) {
				( item as ToolStripMenuItem ).Checked = item.Equals ( sender );
			}
		}

		/// <summary>
		/// Handles the Click event of the detailsToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void detailsToolStripMenuItem_Click ( object sender, EventArgs e ) {
			this.files.View = View.Details;
			if ( this.files.Items.Count == 0 ) {
				this.files.AutoResizeColumns ( ColumnHeaderAutoResizeStyle.HeaderSize );
			} else {
				this.files.AutoResizeColumns ( ColumnHeaderAutoResizeStyle.ColumnContent );
			}
			foreach ( var item in viewModeToolStripDropDownButton.DropDownItems ) {
				( item as ToolStripMenuItem ).Checked = item.Equals ( sender );
			}
		}

		/// <summary>
		/// Handles the Click event of the largeIconsToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void largeIconsToolStripMenuItem_Click ( object sender, EventArgs e ) {
			this.files.View = View.LargeIcon;
			foreach ( var item in viewModeToolStripDropDownButton.DropDownItems ) {
				( item as ToolStripMenuItem ).Checked = item.Equals ( sender );
			}
		}

		/// <summary>
		/// Handles the Click event of the ok control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void ok_Click ( object sender, EventArgs e ) {
			if ( !string.IsNullOrEmpty ( this.FileName ) ) {
				OpenFileName ( this.FileName );
			}
		}

		/// <summary>
		/// Handles the Click event of the parentToolStripButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void parentToolStripButton_Click ( object sender, EventArgs e ) {
			if ( this.CurrentPath != null && this.CurrentPath.Parent != null ) {
				Navigate ( this.CurrentPath.Parent );
			}
		}

		/// <summary>
		/// Handles the SelectedIndexChanged event of the fileTypes control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void fileTypes_SelectedIndexChanged ( object sender, EventArgs e ) {

		}

	}
}

