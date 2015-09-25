using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DroidExplorer.Core.UI {
	public partial class TransferDialog : Form {
		public event EventHandler TransferError;
		public event EventHandler TransferComplete;

		private delegate void SetLabelStatusDelegate ( string text );
		private delegate void SetFromStatusLabelDelegate ( string from, string ffile, string to, string tfile );

		private struct PushState {
			public System.IO.FileInfo File { get; set; }
			public string RemotePath { get; set; }
		}

		private int kbs = 0;
		private System.Threading.Timer timeRemainingTimer = null;
		public TransferDialog ( ) {
			InitializeComponent ( );

		}

		private new DialogResult ShowDialog ( IWin32Window owner ) {
			return base.ShowDialog ( owner );
		}

		private new DialogResult ShowDialog ( ) {
			return base.ShowDialog ( null );
		}


		private new void Show ( ) {
			base.Show ( );
		}

		protected void OnTransferError ( EventArgs e ) {
			if ( this.TransferError != null ) {
				this.TransferError ( this, e );
			}
		}

		protected void OnTransferComplete ( EventArgs e ) {
			if ( this.TransferComplete != null ) {
				this.TransferComplete ( this, e );
			}
		}

		public Exception TransferException {
			get;
			private set;
		}

		private void PrivatePull ( DroidExplorer.Core.IO.FileInfo remoteFile, System.IO.FileInfo destFile ) {
			this.TotalItems = 1;
			this.TotalSize = remoteFile.Size;
			SetFromStatus ( CommandRunner.Instance.DefaultDevice, remoteFile.FullPath, Environment.MachineName, destFile.Directory.Name );
			SpeedTest ( );
			SetItemsRemainingStatus ( this.TotalItems.ToString ( ) );
			SetCopyInfoLabel ( );
			SetTitle ( );

			new Thread ( delegate ( ) {
				try {
					System.IO.FileInfo result = CommandRunner.Instance.PullFile ( remoteFile.FullPath );
					if ( !destFile.Directory.Exists ) {
						destFile.Directory.Create ( );
					}
					if ( string.Compare ( destFile.FullName, result.FullName, true ) != 0 ) {
						result.CopyTo ( destFile.FullName, true );
					}
					this.DialogResult = DialogResult.OK;
					this.OnTransferComplete ( EventArgs.Empty );
				} catch ( Exception ex ) {
					TransferException = ex;
					this.DialogResult = DialogResult.Abort;
					this.OnTransferError ( EventArgs.Empty );
				} finally {

					try {
						if ( !this.IsDisposed ) {
							if ( this.InvokeRequired ) {
								this.Invoke ( new GenericDelegate ( this.Close ) );
							} else {
								this.Close ( );
							}
						}
					} catch (Exception) { }
				}
			} ).Start ( );

		}

		private void PrivatePull ( List<string> files, System.IO.DirectoryInfo destPath ) {
			List<DroidExplorer.Core.IO.FileInfo> tfiles = new List<DroidExplorer.Core.IO.FileInfo> ( );
			foreach ( string item in files ) {
				DroidExplorer.Core.IO.FileInfo tf = DroidExplorer.Core.IO.FileInfo.Create ( System.IO.Path.GetFileName ( item ), 0, null,
					null, null, DateTime.Now, false, item );
				tfiles.Add ( tf );
			}
			PrivatePull ( tfiles, destPath );
		}

		private void PrivatePull ( List<DroidExplorer.Core.IO.FileInfo> files, System.IO.DirectoryInfo destPath ) {
			this.TotalItems = files.Count;
			if ( this.TotalItems == 0 ) {
				this.DialogResult = DialogResult.Cancel;
			}
			foreach ( var fi in files ) {
				this.TotalSize += fi.Size;
			}
			SpeedTest ( );
			SetItemsRemainingStatus ( this.TotalItems.ToString ( ) );
			SetCopyInfoLabel ( );
			SetTitle ( );
			DroidExplorer.Core.Threading.ThreadPool pool = new DroidExplorer.Core.Threading.ThreadPool ( 1 );

			foreach ( var fi in files ) {
				PushState ps = new PushState ( );
				ps.RemotePath = fi.FullPath;
				ps.File = new System.IO.FileInfo ( System.IO.Path.Combine ( destPath.FullName, fi.ToSafeFileName ( ) ) );

				pool.Queue<PushState> ( delegate ( object o ) {
					PushState pushState = (PushState)o;
					try {
						System.IO.FileInfo resultFile = CommandRunner.Instance.PullFile ( pushState.RemotePath );
						if ( !pushState.File.Directory.Exists ) {
							pushState.File.Directory.Create ( );
						}
						if ( string.Compare ( pushState.File.FullName, resultFile.FullName, true ) != 0 ) {
							resultFile.CopyTo ( pushState.File.FullName, true );
						}
						if ( this.InvokeRequired ) {
							this.Invoke ( new SetFromStatusLabelDelegate ( this.SetFromStatus ), new object[] { Environment.MachineName, ps.File.Name,
							CommandRunner.Instance.DefaultDevice, ps.RemotePath } );
							this.Invoke ( new SetLabelStatusDelegate ( this.SetItemsRemainingStatus ), new object[] { ( --this.TotalItems ).ToString ( ) } );
						} else {
							SetFromStatus ( Environment.MachineName, ps.File.Name, CommandRunner.Instance.DefaultDevice, ps.RemotePath );
							SetItemsRemainingStatus ( ( --this.TotalItems ).ToString ( ) );
						}
					} catch ( Exception ex ) {
						this.LogError ( ex.Message, ex );
						TransferException = ex;
						this.DialogResult = DialogResult.Abort;
						this.OnTransferError ( EventArgs.Empty );
					}

					if ( this.TotalItems == 0 ) {
						this.DialogResult = DialogResult.OK;
						this.OnTransferComplete ( EventArgs.Empty );
						if ( !this.IsDisposed ) {
							if ( this.InvokeRequired ) {
								this.Invoke ( new GenericDelegate ( this.Close ) );
							} else {
								this.Close ( );
							}
						}
					}
				}, ps );
			}

			if ( files.Count > 0 ) {
				SetFromStatus ( CommandRunner.Instance.DefaultDevice, System.IO.Path.Combine ( destPath.FullName, files[0].Name ), Environment.MachineName, files[0].Name );
			}
			pool.Start ( );
		}

		private void PrivatePush ( System.IO.FileInfo file, string remote ) {
			this.TotalItems = 1;
			this.TotalSize = file.Length;
			SetFromStatus ( Environment.MachineName, file.Directory.FullName, CommandRunner.Instance.DefaultDevice, remote );
			SpeedTest ( );
			SetItemsRemainingStatus ( this.TotalItems.ToString ( ) );
			SetCopyInfoLabel ( );
			SetTitle ( );
			new Thread ( delegate ( ) {
				try {
					CommandRunner.Instance.PushFile ( file.FullName, remote );
					this.DialogResult = DialogResult.OK;
					this.OnTransferComplete ( EventArgs.Empty );
				} catch ( Exception ex ) {
					TransferException = ex;
					this.DialogResult = DialogResult.Abort;
					this.OnTransferError ( EventArgs.Empty );
					try {
						if ( this.InvokeRequired ) {
							this.Invoke ( new GenericDelegate ( this.Close ) );
						} else {
							this.Close ( );
						}
					} catch {

					}
				}
			} ).Start ( );
		}

		private void PrivatePush ( List<System.IO.FileInfo> files, string destPath ) {
			this.TotalItems = files.Count;
			if ( this.TotalItems == 0 ) {
				this.DialogResult = DialogResult.Cancel;
			}
			foreach ( var fi in files ) {
				this.TotalSize += fi.Length;
			}
			SpeedTest ( );
			SetItemsRemainingStatus ( this.TotalItems.ToString ( ) );
			SetCopyInfoLabel ( );
			SetTitle ( );
			DroidExplorer.Core.Threading.ThreadPool pool = new DroidExplorer.Core.Threading.ThreadPool ( 1 );

			foreach ( var fi in files ) {
				PushState ps = new PushState ( );
				string remotePath = System.IO.Path.Combine ( destPath, fi.Name );
				ps.File = fi;
				ps.RemotePath = remotePath;

				pool.Queue<PushState> ( delegate ( object o ) {
					PushState pushState = (PushState)o;
					try {
						CommandRunner.Instance.PushFile ( pushState.File.FullName, pushState.RemotePath );
						if ( this.InvokeRequired ) {
							this.Invoke ( new SetFromStatusLabelDelegate ( this.SetFromStatus ), new object[] { Environment.MachineName, ps.File.Name,
							CommandRunner.Instance.DefaultDevice, ps.RemotePath } );
							this.Invoke ( new SetLabelStatusDelegate ( this.SetItemsRemainingStatus ), new object[] { ( --this.TotalItems ).ToString ( ) } );
						} else {
							SetFromStatus ( Environment.MachineName, ps.File.Name, CommandRunner.Instance.DefaultDevice, ps.RemotePath );
							SetItemsRemainingStatus ( ( --this.TotalItems ).ToString ( ) );
						}
					} catch ( Exception ex ) {
						this.LogError ( ex.Message, ex );
						TransferException = ex;
						this.DialogResult = DialogResult.Abort;
						this.OnTransferError ( EventArgs.Empty );
					}

					if ( this.TotalItems == 0 ) {
						if ( this.InvokeRequired ) {
							this.Invoke ( new GenericDelegate ( this.Close ) );
						} else {
							this.Close ( );
						}
						this.DialogResult = DialogResult.OK;
						this.OnTransferComplete ( EventArgs.Empty );
					}
				}, ps );
			}

			SetFromStatus ( Environment.MachineName, files[0].Name, CommandRunner.Instance.DefaultDevice, System.IO.Path.Combine ( destPath, files[0].Name ) );

			pool.Start ( );
		}

		public void Pull ( DroidExplorer.Core.IO.FileInfo remoteFile, System.IO.FileInfo destFile ) {
			PrivatePull ( remoteFile, destFile );
			this.Show ( );
		}

		public void Pull ( List<string> files, System.IO.DirectoryInfo destPath ) {
			PrivatePull ( files, destPath );
			this.Show ( );
		}

		public void Pull ( List<DroidExplorer.Core.IO.FileInfo> files, System.IO.DirectoryInfo destPath ) {
			PrivatePull ( files, destPath );
			this.Show ( );
		}

		public DialogResult PullDialog ( DroidExplorer.Core.IO.FileInfo remoteFile, System.IO.FileInfo destFile ) {
			PrivatePull ( remoteFile, destFile );
			return this.ShowDialog ( );
		}

		public DialogResult PullDialog ( List<string> files, System.IO.DirectoryInfo destPath ) {
			PrivatePull ( files, destPath );
			return this.ShowDialog ( );
		}

		public DialogResult PullDialog ( List<DroidExplorer.Core.IO.FileInfo> files, System.IO.DirectoryInfo destPath ) {
			PrivatePull ( files, destPath );
			return this.ShowDialog ( );
		}

		public void Push ( List<System.IO.FileInfo> files, string destPath ) {
			PrivatePush ( files, destPath );
			this.Show ( );
		}

		public void Push ( System.IO.FileInfo file, string remote ) {
			PrivatePush ( file, remote );
		}

		public DialogResult PushDialog ( System.IO.FileInfo file, string remote ) {
			PrivatePush ( file, remote );
			return this.ShowDialog ( null );
		}


		public DialogResult PushDialog ( List<System.IO.FileInfo> files, string destPath ) {
			PrivatePush ( files, destPath );
			return this.ShowDialog ( null );
		}

		private void SpeedTest ( ) {
			SetTimeStatus ( "Calculating..." );
			try {
				if ( kbs <= 0 ) {
					string path = System.IO.Path.GetDirectoryName ( this.GetType ( ).Assembly.Location );
					DroidExplorer.Core.TransferCommandResult tcr = CommandRunner.Instance.PushFile ( System.IO.Path.Combine ( path, @"Data\speedtest.png" ), "/sdcard/speedtest.png" ) as DroidExplorer.Core.TransferCommandResult;
					kbs = tcr.KillobytesPerSecond;
					CommandRunner.Instance.DeleteFile ( "/sdcard/speedtest.png" );
				}

				int secs = 0;
				string s = ( ( this.TotalSize / 1024 ) / kbs ).ToString ( );
				int.TryParse ( s, out secs );
				TimeSpan ts = new TimeSpan ( 0, 0, secs );
				SetTimeStatus ( ts.ToString ( ) );
				timeRemainingTimer = new System.Threading.Timer ( delegate ( object o ) {
					try {
						if ( ts.TotalSeconds > 0 ) {
							ts = ts.Subtract ( new TimeSpan ( 0, 0, 1 ) );
							if ( this.InvokeRequired ) {
								this.Invoke ( new SetLabelStatusDelegate ( this.SetTimeStatus ), new object[] { ts.ToString ( ) } );
							} else {
								SetTimeStatus ( ts.ToString ( ) );
							}
						} else {
							timeRemainingTimer = null;
						}
					} catch ( Exception ex ) {
						this.LogError ( ex.Message, ex );
					}
				}, null, new TimeSpan ( 0, 0, 1 ), new TimeSpan ( 0, 0, 1 ) );
			} catch ( Exception ex ) {
				SetTimeStatus ( "Unknown" );
				this.LogError ( ex.Message, ex );
			}

		}


		public int TotalItems { get; private set; }
		public long TotalSize { get; private set; }

		private void SetCopyInfoLabel ( ) {
			this.copyStatus.Text = String.Format ( new DroidExplorer.Core.IO.FileSizeFormatProvider ( ), Resources.Strings.TransferFormCopyLabel, this.TotalItems, this.TotalSize, this.TotalItems != 1 ? "s" : string.Empty );
		}

		public void SetTitle ( ) {
			this.Text = string.Format ( Resources.Strings.TransferFormTitle, this.TotalItems != 1 ? "s" : string.Empty );
		}

		public void SetFromStatus ( string from, string fromPath, string to, string toPath ) {
			this.from.Text = string.Format ( Resources.Strings.TransferFormFromLabel, from, fromPath, to, toPath );
		}

		public void SetTimeStatus ( string text ) {
			this.timeRemaining.Text = string.Format ( Resources.Strings.TransferFormTimeRemainingLabel, text );
		}

		public void SetItemsRemainingStatus ( string text ) {
			this.itemsRemaining.Text = string.Format ( Resources.Strings.TransferFormItemsRemainingLabel, text );
		}
	}
}
