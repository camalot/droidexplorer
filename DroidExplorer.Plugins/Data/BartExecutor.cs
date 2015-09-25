using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Core;
using System.Globalization;
using DroidExplorer.Core.Plugins;
using DroidExplorer.Core.IO;
using System.Threading;

namespace DroidExplorer.Plugins.Data {
	/// <summary>
	/// 
	/// </summary>
	public enum BartMode {
		/// <summary>
		/// 
		/// </summary>
		List,
		/// <summary>
		/// 
		/// </summary>
		Backup,
		/// <summary>
		/// 
		/// </summary>
		Restore,
		/// <summary>
		/// 
		/// </summary>
		Delete
	}

	/// <summary>
	/// 
	/// </summary>
	public enum CompleteTask {
		/// <summary>
		/// 
		/// </summary>
		None,
		/// <summary>
		/// 
		/// </summary>
		Shutdown,
		/// <summary>
		/// 
		/// </summary>
		Reboot
	}

	/// <summary>
	/// 
	/// </summary>
	public enum BackupMode {
		/// <summary>
		/// 
		/// </summary>
		Default,
		/// <summary>
		/// 
		/// </summary>
		Apps,
		/// <summary>
		/// 
		/// </summary>
		Base,
		/// <summary>
		/// 
		/// </summary>
		ExtOnly,
		/// <summary>
		/// 
		/// </summary>
		NandroidOnly
	}

	/// <summary>
	/// 
	/// </summary>
	public class BartExecutor : IShellProcess {
		public event EventHandler<EventArgs> Complete;
		private const string BARTDE = "/system/sd/bartde.sh";


		/// <summary>
		/// Initializes a new instance of the <see cref="BartExecutor"/> class.
		/// </summary>
		private BartExecutor ( ) {
			BackupMode = BackupMode.Default;
			Mode = BartMode.List;
			CompletionTask = CompleteTask.None;
			IncludeRecovery = false;
			IncludeBoot = true;
			IncludeData = true;
			IncludeSystem = true;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="BartExecutor"/> is verbose.
		/// </summary>
		/// <value><c>true</c> if verbose; otherwise, <c>false</c>.</value>
		public bool Verbose { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="BartExecutor"/> is compress.
		/// </summary>
		/// <value><c>true</c> if compress; otherwise, <c>false</c>.</value>
		public bool Compress { get; set; }
		/// <summary>
		/// Gets or sets the mode.
		/// </summary>
		/// <value>The mode.</value>
		private BartMode Mode { get; set; }
		/// <summary>
		/// Gets or sets the backup mode.
		/// </summary>
		/// <value>The backup mode.</value>
		public BackupMode BackupMode { get; set; }
		/// <summary>
		/// Gets or sets the completion task.
		/// </summary>
		/// <value>The completion task.</value>
		public CompleteTask CompletionTask { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [include recovery].
		/// </summary>
		/// <value><c>true</c> if [include recovery]; otherwise, <c>false</c>.</value>
		public bool IncludeRecovery { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether [include data].
		/// </summary>
		/// <value><c>true</c> if [include data]; otherwise, <c>false</c>.</value>
		public bool IncludeData { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether [include boot].
		/// </summary>
		/// <value><c>true</c> if [include boot]; otherwise, <c>false</c>.</value>
		public bool IncludeBoot { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether [include system].
		/// </summary>
		/// <value><c>true</c> if [include system]; otherwise, <c>false</c>.</value>
		public bool IncludeSystem { get; set; }

		/// <summary>
		/// Lists this instance.
		/// </summary>
		/// <returns></returns>
		public List<string> List ( ) {
			Mode = BartMode.List;
			CopyBart ( );
			string command = string.Format ( CultureInfo.InvariantCulture, "sh {0} {1}", BARTDE, CreateArgs ( ) );
			GenericCommandResult result = CommandRunner.Instance.ShellRun ( command ) as GenericCommandResult;
			string data = result.Output.ToString ( );
			string[] folders = data.Split ( new string[] { " ", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries );
			this.LogDebug ( "Bart List: {0}", string.Join ( ",", folders ) );
			//Cleanup ( );

			return new List<string> ( folders );
		}

		/// <summary>
		/// Backups the specified rom name.
		/// </summary>
		/// <param name="romName">Name of the rom.</param>
		public void Backup ( string romName ) {
			Mode = BartMode.Backup;
			ExecuteBart ( string.Format ( CultureInfo.InvariantCulture, "sh {0} {1}{2}", BARTDE, CreateArgs ( ), romName ) );
		}

		/// <summary>
		/// Restores the specified rom name.
		/// </summary>
		/// <param name="romName">Name of the rom.</param>
		public void Restore ( string romName ) {
			Mode = BartMode.Restore;
			ExecuteBart ( string.Format ( CultureInfo.InvariantCulture, "sh {0} {1}{2}", BARTDE, CreateArgs ( ), romName ) );
		}

		/// <summary>
		/// Deletes the specified rom name.
		/// </summary>
		/// <param name="romName">Name of the rom.</param>
		public void Delete ( string romName ) {
			Mode = BartMode.Delete;
			ExecuteBart ( string.Format ( CultureInfo.InvariantCulture, "sh {0} {1}{2}", BARTDE, CreateArgs ( ), romName ) );
		}

		/// <summary>
		/// Executes the bart command.
		/// </summary>
		/// <param name="command">The command.</param>
		private void ExecuteBart ( string command ) {
			CopyBart ( );
			this.LogDebug ( command );
			CommandRunner.Instance.LaunchRedirectedShellWindow ( CommandRunner.Instance.DefaultDevice, command, this );
		}

		/// <summary>
		/// Deletes the bart file.
		/// </summary>
		public void Cleanup ( ) {
			CommandRunner.Instance.MakeReadWrite ( "/system" );
			CommandRunner.Instance.DeleteFile ( BARTDE );
			CommandRunner.Instance.MakeReadOnly ( "/system" );
		}

		/// <summary>
		/// Copies the bart file to the device.
		/// </summary>
		private void CopyBart ( ) {
			CommandRunner.Instance.MakeReadWrite ( "/system" );

			System.IO.FileInfo bartFile = new System.IO.FileInfo ( System.IO.Path.Combine ( CommandRunner.Instance.TempDataPath, "bartde.sh" ) );
			byte[] bartData = Properties.Resources.bart_sh;
			using ( System.IO.FileStream fs = new System.IO.FileStream ( bartFile.FullName, System.IO.FileMode.Create, System.IO.FileAccess.Write ) ) {
				fs.Write ( bartData, 0, bartData.Length );
			}

			CommandRunner.Instance.PushFile ( bartFile.FullName, BARTDE );
			CommandRunner.Instance.Chmod ( 755, BARTDE );
			CommandRunner.Instance.MakeReadOnly ( "/system" );

		}

		/// <summary>
		/// Creates the args.
		/// </summary>
		/// <returns></returns>
		private string CreateArgs ( ) {
			if ( Mode == BartMode.List ) {
				this.LogInfo ( "Bart Arguments: -l" );
				return "-l";
			} else {
				StringBuilder args = new StringBuilder ( );
				args.Append ( "--noninteractive " );
				if ( Mode == BartMode.Delete ) {
					if ( BackupMode == BackupMode.ExtOnly ) {
						args.Append ( "-e " );
					} else if ( BackupMode == BackupMode.NandroidOnly ) {
						args.Append ( "-n " );
					}

					args.Append ( "-d " );
					this.LogInfo ( "Bart Arguments: {0}", args.ToString ( ) );
					return args.ToString ( );
				} else {
					switch ( BackupMode ) {
						case BackupMode.Apps:
							args.Append ( "-a " );
							break;
						case BackupMode.Base:
							args.Append ( "-b " );
							break;
						case BackupMode.ExtOnly:
							args.Append ( "-e " );
							break;
						case BackupMode.NandroidOnly:
							args.Append ( "-n " );
							break;
					}
					if ( Verbose ) {
						args.Append ( "--verbose " );
					}

					if ( !IncludeSystem ) {
						args.Append ( "--nosystem " );
					}

					if ( !IncludeBoot ) {
						args.Append ( "--noboot " );
					}

					if ( !IncludeRecovery ) {
						args.Append ( "--norecovery " );
					}

					if ( !IncludeData ) {
						args.Append ( "--nodata " );
					}

					if ( CompletionTask != CompleteTask.None ) {
						args.AppendFormat ( "--{0} ", CompletionTask.ToString ( ).ToLower ( ) );
					}

					switch ( Mode ) {
						case BartMode.Backup:
							if ( Compress ) {
								args.Append ( "-c " );
							}
							args.Append ( "-s " );
							break;
						case BartMode.Restore:
							args.Append ( "-r " );
							break;
					}
				}

				this.LogInfo ( "Bart Arguments: {0}", args.ToString ( ) );
				return args.ToString ( );
			}
		}


		/// <summary>
		/// 
		/// </summary>
		private static BartExecutor _instance;
		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static BartExecutor Instance {
			get {
				if ( _instance == null ) {
					_instance = new BartExecutor ( );
				}
				return _instance;
			}
		}


		/// <summary>
		/// Gets a value indicating whether this instance is running.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is running; otherwise, <c>false</c>.
		/// </value>
		public bool IsRunning { get { return this.BaseProcess != null; } }
		/// <summary>
		/// Gets or sets the console output.
		/// </summary>
		/// <value>The console output.</value>
		public ConsoleWriter ConsoleOutput { get; set; }
		/// <summary>
		/// Gets or sets the console error.
		/// </summary>
		/// <value>The console error.</value>
		public ConsoleWriter ConsoleError { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether [outputing data].
		/// </summary>
		/// <value><c>true</c> if [outputing data]; otherwise, <c>false</c>.</value>
		private bool OutputingData { get; set; }

		#region IShellProcess Members

		/// <summary>
		/// Gets or sets the output stream.
		/// </summary>
		/// <value>The output stream.</value>
		public System.IO.StreamReader OutputStream { get; private set; }

		/// <summary>
		/// Gets or sets the error stream.
		/// </summary>
		/// <value>The error stream.</value>
		public System.IO.StreamReader ErrorStream { get; private set; }

		/// <summary>
		/// Gets or sets the input stream.
		/// </summary>
		/// <value>The input stream.</value>
		public System.IO.StreamWriter InputStream { get; private set; }

		/// <summary>
		/// Gets or sets the base process.
		/// </summary>
		/// <value>The base process.</value>
		public System.Diagnostics.Process BaseProcess { get; internal set; }

		/// <summary>
		/// Sets the base process.
		/// </summary>
		/// <param name="process">The process.</param>
		public void SetBaseProcess ( System.Diagnostics.Process process ) {
			if ( process != null ) {
				BaseProcess = process;
			}
		}

		/// <summary>
		/// Sets the output stream.
		/// </summary>
		/// <param name="reader">The reader.</param>
		public void SetOutputStream ( System.IO.StreamReader reader ) {
			OutputStream = reader;
			new Thread ( new ThreadStart ( delegate {
				while ( IsRunning ) {
					while ( !OutputStream.EndOfStream ) {
						string line = OutputStream.ReadLine ( );
						OutputingData = true;
						if ( !string.IsNullOrEmpty ( line ) && ConsoleOutput != null ) {
							ConsoleOutput.WriteLine ( line );
						}
					}
				}
				OnComplete ( EventArgs.Empty );
				Cleanup ( );
			} ) ).Start ( );
		}

		/// <summary>
		/// Sets the error stream.
		/// </summary>
		/// <param name="reader">The reader.</param>
		public void SetErrorStream ( System.IO.StreamReader reader ) {
			ErrorStream = reader;
			new Thread ( new ThreadStart ( delegate {
				while ( IsRunning ) {
					while ( !ErrorStream.EndOfStream ) {
						string line = ErrorStream.ReadLine ( );
						OutputingData = true;
						if ( !string.IsNullOrEmpty ( line ) && ConsoleError != null ) {
							ConsoleError.WriteLine ( line );
						}
					}
				}
			} ) ).Start ( );
		}

		/// <summary>
		/// Sets the input stream.
		/// </summary>
		/// <param name="writer">The writer.</param>
		public void SetInputStream ( System.IO.StreamWriter writer ) {
			InputStream = writer;
		}

		#endregion

		/// <summary>
		/// Raises the <see cref="E:RestoreComplete"/> event.
		/// </summary>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void OnComplete ( EventArgs e ) {
			if ( Complete != null ) {
				Complete ( this, e );
			}
		}
	}
}