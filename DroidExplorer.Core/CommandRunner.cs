using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using DroidExplorer.Components;
using System.Drawing;
using ICSharpCode.SharpZipLib.Zip;
using DroidExplorer.Core.IO;
using System.Threading;
using System.Globalization;
using DroidExplorer.Core.Components;
using DroidExplorer.Core.Exceptions;
using System.ComponentModel;
using DroidExplorer.Core.Plugins;
using DroidExplorer.Core.Collections;
using System.Xml.Serialization;
using System.Drawing.Imaging;
using DroidExplorer.Core.Configuration;
using DroidExplorer.Core.Adb;
using System.Linq;
using Camalot.Common.Extensions;

namespace DroidExplorer.Core {
	/// <summary>
	/// 
	/// </summary>
	public class CommandRunner {
		private static CommandRunner _commandRunner;

		/// <summary>
		/// Occurs when [device state changed].
		/// </summary>
		public event EventHandler<DeviceEventArgs> DeviceStateChanged;
		/// <summary>
		/// Occurs when [connected].
		/// </summary>
		public event EventHandler<DeviceEventArgs> Connected;
		/// <summary>
		/// Occurs when [disconnected].
		/// </summary>
		public event EventHandler<DeviceEventArgs> Disconnected;
		/// <summary>
		/// Occurs when [top process updated].
		/// </summary>
		public event EventHandler<ProcessInfoEventArgs> TopProcessUpdated;
		/// <summary>
		/// Occurs when [top process update complete].
		/// </summary>
		public event EventHandler TopProcessUpdateComplete;
		/// <summary>
		/// Occurs when [top process update started].
		/// </summary>
		public event EventHandler TopProcessUpdateStarted;
		/// <summary>
		/// 
		/// </summary>
		public enum DeviceState {
			/// <summary>
			/// 
			/// </summary>
			Unknown,
			/// <summary>
			/// 
			/// </summary>
			Device,
			/// <summary>
			/// 
			/// </summary>
			Offline,
			/// <summary>
			/// 
			/// </summary>
			Bootloader,
			/// <summary>
			/// 
			/// </summary>
			Recovery,
			/// <summary>
			/// 
			/// </summary>
			Unauthorized
		}

		/// <summary>
		/// 
		/// </summary>
		public enum AaptCommand {
			/// <summary>
			/// 
			/// </summary>
			Dump_Badging,
			/// <summary>
			/// 
			/// </summary>
			Dump_Permissions,
			/// <summary>
			/// 
			/// </summary>
			Dump_Resources,
			/// <summary>
			/// 
			/// </summary>
			Dump_Configuration,
			/// <summary>
			/// 
			/// </summary>
			Dump_XmlTree,
			/// <summary>
			/// 
			/// </summary>
			Dump_XmlStrings,
			/// <summary>
			/// 
			/// </summary>
			List,
			/// <summary>
			/// 
			/// </summary>
			Package,
			/// <summary>
			/// 
			/// </summary>
			Remove,
			/// <summary>
			/// 
			/// </summary>
			Add,
			/// <summary>
			/// 
			/// </summary>
			Version,
		}

		/// <summary>
		/// 
		/// </summary>
		public enum AdbCommand {
			/// <summary>
			/// 
			/// </summary>
			Devices,
			/// <summary>
			/// 
			/// </summary>
			Help,
			/// <summary>
			/// 
			/// </summary>
			Root,
			/// <summary>
			/// 
			/// </summary>
			Remount,
			/// <summary>
			/// 
			/// </summary>
			Status_Window,
			/// <summary>
			/// 
			/// </summary>
			Get_SerialNo,
			/// <summary>
			/// 
			/// </summary>
			Get_State,
			/// <summary>
			/// 
			/// </summary>
			Kill_Server,
			/// <summary>
			/// 
			/// </summary>
			Start_Server,
			/// <summary>
			/// 
			/// </summary>
			Wait_For_Device,
			/// <summary>
			/// 
			/// </summary>
			Version,
			/// <summary>
			/// 
			/// </summary>
			Bugreport,
			/// <summary>
			/// 
			/// </summary>
			Uninstall,
			/// <summary>
			/// 
			/// </summary>
			Install,
			/// <summary>
			/// 
			/// </summary>
			Jdwp,
			/// <summary>
			/// 
			/// </summary>
			Forward,
			Reverse,
			/// <summary>
			/// 
			/// </summary>
			Logcat,
			/// <summary>
			/// 
			/// </summary>
			Emu,
			/// <summary>
			/// 
			/// </summary>
			Shell,
			Hell,
			/// <summary>
			/// 
			/// </summary>
			Sync,
			/// <summary>
			/// 
			/// </summary>
			Push,
			/// <summary>
			/// 
			/// </summary>
			Pull,
			/// <summary>
			/// 
			/// </summary>
			PPP,
			/// <summary>
			/// 
			/// </summary>
			ShellLS,
			/// <summary>
			/// 
			/// </summary>
			ShellPackageManager,
			/// <summary>
			/// screen capture
			/// </summary>
			ShellScreenCapture,
			/// <summary>
			/// Reboots the device
			/// </summary>
			Reboot,

			/// <summary>
			/// Backups a device (ICS+)
			/// </summary>
			Backup,
			/// <summary>
			/// Restore device data (ICS+)
			/// </summary>
			Restore,
			/// <summary>
			/// 
			/// </summary>
			Connect
		}

		/// <summary>
		/// 
		/// </summary>
		public enum InstallFailure {
			/// <summary>
			/// 
			/// </summary>
			UNKNOWN,
			/// <summary>
			/// 
			/// </summary>
			INSTALL_FAILED_ALREADY_EXISTS,
		}

#if PLATFORM_LINUX
		public const string ADB_COMMAND = @"adb";
		public const string HIERARCHYVIEWER_COMMAND = @"hierarchyviewer";
		public const string DDMS_COMMAND = @"ddms";
		public const string AAPT_COMMAND = @"aapt";
		public const string SDKMANAGER_COMMAND = @"SDKManager";
		public const string AVDMANAGER_COMMAND = @"AVDManager";
#elif PLATFORM_OSX
		public const string ADB_COMMAND = @"adb";
		public const string HIERARCHYVIEWER_COMMAND = @"hierarchyviewer";
		public const string DDMS_COMMAND = @"ddms";
		public const string AAPT_COMMAND = @"aapt";
		public const string SDKMANAGER_COMMAND = @"SDKManager";
		public const string AVDMANAGER_COMMAND = @"AVDManager";
#else // PLATFORM_WINDOWS
		public const string ADB_COMMAND = @"adb.exe";
		public const string HIERARCHYVIEWER_COMMAND = @"hierarchyviewer.bat";
		public const string DDMS_COMMAND = @"ddms.bat";
		public const string AAPT_COMMAND = @"aapt.exe";
		public const string SDKMANAGER_COMMAND = @"SDK Manager.exe";
		public const string AVDMANAGER_COMMAND = @"AVD Manager.exe";
#endif
		/// <summary>
		/// 
		/// </summary>
		public const string SYSTEM_APP_PATH = "/system/app/";
		/// <summary>
		/// 
		/// </summary>
		public const string APP_PUBLIC_PATH = "/data/app/";

		/// <summary>
		/// 
		/// </summary>
		public const string APP_SD_PUBLIC_PATH = "/sd-ext/app/";

		/// <summary>
		/// 
		/// </summary>
		public const string APP_PRIVATE_PATH = "/data/app-private/";

		/// <summary>
		/// 
		/// </summary>
		public const string APP_SD_PRIVATE_PATH = "/sd-ext/app-private/";


		private DeviceState _deviceState = DeviceState.Unknown;
		private BatteryInfo lastBatteryInfo = null;
		private DateTime lastBatteryCheckTime = DateTime.MinValue;

		/// <summary>
		/// Initializes a new instance of the <see cref="CommandRunner"/> class.
		/// </summary>
		public CommandRunner ( ) {
			if ( Settings == null ) {
				throw new ArgumentNullException ( "Settings not set. Must initialize settings before getting CommandRunner instance." );
			}
			String tsdk = Settings.SystemSettings.SdkPath;
			if ( String.IsNullOrEmpty ( tsdk ) || !System.IO.Directory.Exists ( tsdk ) ) {
				throw new System.IO.FileNotFoundException ( "Unable to locate sdk in path: '{0}'.".With ( tsdk ) );
      } else {
				SdkPath = tsdk;
			}
		}

		/// <summary>
		/// Starts the server.
		/// </summary>
		public void StartServer ( ) {
			var result = CommandRun ( string.Empty, AdbCommand.Start_Server, string.Empty );
			this.LogDebug ( result.Output.ToString ( ) );
		}

		public void StopServer ( ) {
			var result = CommandRun ( string.Empty, AdbCommand.Kill_Server, string.Empty );
			this.LogDebug ( result.Output.ToString ( ) );
		}

		public void RestartServer() {
			StopServer ( );
			StartServer ( );
		}


		/// <summary>
		/// Starts the adb service as root on the device.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <exception cref="AdbRootException"></exception>
		public void StartAdbAsRoot ( string device ) {
			var regexOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline;
			var result = CommandRun ( device, AdbCommand.Root, string.Empty );
			var rOutput = result.Output.ToString ( );
			// checks if we were able to launch adb as root.

			if ( rOutput.IsMatch ( DroidExplorer.Resources.Strings.CanAdbRootRegex, regexOptions ) ) {
				throw new AdbRootException ( );
			}
			this.LogDebug ( result.Output.ToString ( ) );
		}		

		/// <summary>
		/// Change file system modes of files and directories. The modes include permissions and special modes.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="group">The group.</param>
		/// <param name="other">The other.</param>
		/// <param name="file">The file.</param>
		public void Chmod ( Permission user, Permission group, Permission other, string file ) {
			Chmod ( DefaultDevice, user, group, other, file );
		}

		/// <summary>
		/// Change file system modes of files and directories. The modes include permissions and special modes.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="user">The user.</param>
		/// <param name="group">The group.</param>
		/// <param name="other">The other.</param>
		/// <param name="file">The file.</param>
		public void Chmod ( string device, Permission user, Permission group, Permission other, string file ) {
			string command = string.Format ( CultureInfo.InvariantCulture, "chmod {0}{1}{2} \"{3}\"", (int)user.ToChmod ( ),
				(int)group.ToChmod ( ), (int)other.ToChmod ( ), file );
			this.LogDebug ( command );
			ShellRun ( device, command );
		}

		/// <summary>
		/// Change file system modes of files and directories. The modes include permissions and special modes.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="mode">The mode.</param>
		/// <param name="file">The file.</param>
		public void Chmod ( string device, int mode, string file ) {
			string command = string.Format ( CultureInfo.InvariantCulture, "chmod {0} \"{1}\"", mode, file );
			this.LogDebug ( command );
			ShellRun ( device, command );
		}

		/// <summary>
		/// Change file system modes of files and directories. The modes include permissions and special modes.
		/// </summary>
		/// <param name="mode">The mode.</param>
		/// <param name="file">The file.</param>
		public void Chmod ( int mode, string file ) {
			Chmod ( DefaultDevice, mode, file );
		}

		/// <summary>
		/// Reads the file.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		public GenericCommandResult ReadFile ( string device, string file ) {
			return new GenericCommandResult ( RunAdbCommand ( device, AdbCommand.Shell, string.Format ( "cat {0}", file ) ) );
		}

		/// <summary>
		/// Reads the file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		public GenericCommandResult ReadFile ( string file ) {
			return ReadFile ( this.DefaultDevice, file );
		}

		/// <summary>
		/// Determines whether the specified path is a mount point on the default device.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>
		/// 	<c>true</c> if the specified path is a mount point on the default device; otherwise, <c>false</c>.
		/// </returns>
		public bool IsMountPoint ( string path ) {
			return IsMountPoint ( this.DefaultDevice, path );
		}

		/// <summary>
		/// Determines whether the specified path is a mount point on the specified device.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="path">The path.</param>
		/// <returns>
		/// 	<c>true</c> if the specified path is a mount point on the specified device; otherwise, <c>false</c>.
		/// </returns>
		public bool IsMountPoint ( string device, string path ) {
			string data = ReadFile ( device, "/etc/fstab" ).Output.ToString ( );
			Regex regex = new Regex ( Properties.Resources.FSTabRegexPattern, RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline );
			Match m = regex.Match ( data );
			while ( m.Success ) {
				if ( string.Compare ( m.Groups[2].Value, path, false ) == 0 ) {
					return true;
				}
				m = m.NextMatch ( );
			}
			return false;
		}

		/// <summary>
		/// Launches the activity.
		/// </summary>
		/// <param name="package">The package.</param>
		/// <param name="classFullName">Full name of the class.</param>
		public void LaunchActivity ( string package, string classFullName ) {
			LaunchActivity ( this.DefaultDevice, package, classFullName );
		}

		/// <summary>
		/// Launches the activity.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="package">The package.</param>
		/// <param name="classFullName">Full name of the class.</param>
		public void LaunchActivity ( string device, string package, string classFullName ) {
			ShellRun ( device, string.Format ( CultureInfo.InvariantCulture, "am start -a android.intent.action.MAIN -n {0}/{1}", package, classFullName ) );
		}

		/// <summary>
		/// Makes the mount point read write.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="mountPoint">The mount point.</param>
		public void MakeReadWrite ( string device, string mountPoint ) {
			if ( !mountPoint.StartsWith ( "/" ) ) {
				throw new AdbException ( "Invalid mount point" );
			}
			ShellRun ( device, string.Format ( CultureInfo.InvariantCulture, "busybox mount -o rw,remount {0}", mountPoint ) );
		}

		/// <summary>
		/// Makes the mount point read write.
		/// </summary>
		/// <param name="mountPoint">The mount point.</param>
		public void MakeReadWrite ( string mountPoint ) {
			MakeReadWrite ( this.DefaultDevice, mountPoint );
		}

		/// <summary>
		/// Makes the mount point read only.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="mountPoint">The mount point.</param>
		public void MakeReadOnly ( string device, string mountPoint ) {
			if ( !mountPoint.StartsWith ( "/" ) ) {
				throw new AdbException ( "Invalid mount point" );
			}
			ShellRun ( device, string.Format ( CultureInfo.InvariantCulture, "busybox mount -o ro,remount {0}", mountPoint ) );
		}

		/// <summary>
		/// Makes the mount point read only.
		/// </summary>
		/// <param name="mountPoint">The mount point.</param>
		public void MakeReadOnly ( string mountPoint ) {
			MakeReadOnly ( this.DefaultDevice, mountPoint );
		}

		/// <summary>
		/// Mounts the specified device.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="mount">The mount.</param>
		public void Mount ( string device, string mount ) {
			if ( string.IsNullOrEmpty ( mount ) ) {
				return;
			}

			ShellRun ( device, string.Format ( CultureInfo.InvariantCulture, "mount {0}", mount ) );
		}

		/// <summary>
		/// Mounts the specified mount.
		/// </summary>
		/// <param name="mount">The mount.</param>
		public void Mount ( string mount ) {
			Mount ( this.DefaultDevice, mount );
		}
		/// <summary>
		/// Gets or sets the default device.
		/// </summary>
		/// <value>The default device.</value>
		public string DefaultDevice { get; set; }

		/// <summary>
		/// Gets the path of the SDK Tools
		/// </summary>
		public string SdkPath { get; set; }

		/// <summary>
		/// Gets or sets the top process.
		/// </summary>
		/// <value>The top process.</value>
		private Process TopProcess { get; set; }


		private DeviceMonitor DeviceMonitor { get; set; }

		/// <summary>
		/// Connects the specified device.
		/// </summary>
		/// <param name="device">The device.</param>
		public void Connect ( string device ) {
			try {

				if ( DeviceMonitor != null ) {
					DeviceMonitor.Stop ( );
					DeviceMonitor = null;
				}

				DeviceMonitor = new DeviceMonitor ( device );
				DeviceMonitor.Connected += ( s, e ) => {
					if ( this.Connected != null ) {
						this.State = e.State;
						this.Connected ( this, e );
					}
				};
				DeviceMonitor.Disconnected += ( s, e ) => {
					Disconnect ( e.Device );
          if ( this.Disconnected != null ) {
						this.State = e.State;
						this.Disconnected ( this, e );
					}
				};
				DeviceMonitor.DeviceStateChanged += ( s, e ) => {
					if ( this.DeviceStateChanged != null ) {
						this.State = e.State;
						this.DeviceStateChanged ( this, e );
					}
				};

				DeviceMonitor.Start ( );
      } catch ( Exception ex ) {
				this.LogError ( ex.Message, ex );
				State = DeviceState.Unknown;
			}
		}

		/// <summary>
		/// Connects this instance.
		/// </summary>
		public void Connect ( ) {
			Connect ( this.DefaultDevice );
		}

		/// <summary>
		/// Disconnects the specified device.
		/// </summary>
		/// <param name="device">The device.</param>
		public void Disconnect ( string device ) {
			try {
				if ( DeviceMonitor != null && !DeviceMonitor.HasExited ) {
					FolderManagement.TempPathCleanup ( );
					DeviceMonitor.Stop ( );
					State = DeviceState.Offline;
				}
			} catch ( Exception ) {
				State = DeviceState.Unknown;
			} finally {
				if ( this.Disconnected != null ) {
					this.Disconnected ( this, new DeviceEventArgs ( device ) );
				}
			}
		}

		/// <summary>
		/// Disconnects this instance.
		/// </summary>
		public void Disconnect ( ) {
			Disconnect ( this.DefaultDevice );
		}

		/// <summary>
		/// Tops the process kill.
		/// </summary>
		public void TopProcessKill ( ) {
			if ( TopProcess != null && !TopProcess.HasExited ) {
				try {
					TopProcess.Kill ( );
				} catch {
				}
			}
		}

		/// <summary>
		/// Tops the process run.
		/// </summary>
		public void TopProcessRun ( ) {
			try {
				if ( TopProcess == null || TopProcess.HasExited ) {
					TopProcess = new Process ( );
					ProcessStartInfo psi = new ProcessStartInfo ( FolderManagement.GetSdkTool ( ADB_COMMAND ), string.Format ( CultureInfo.InvariantCulture, "{0} top -d 1 -s cpu", AdbCommandArguments ( DefaultDevice, AdbCommand.Shell ) ) );
					psi.CreateNoWindow = true;
					psi.ErrorDialog = false;
					psi.WindowStyle = ProcessWindowStyle.Hidden;
					psi.UseShellExecute = false;
					psi.RedirectStandardOutput = true;
					psi.RedirectStandardError = true;

					TopProcess.StartInfo = psi;
					TopProcess.OutputDataReceived += delegate ( object sender, DataReceivedEventArgs e ) {
						if ( TopProcessUpdateStarted != null ) {
							TopProcessUpdateStarted ( this, EventArgs.Empty );
						}
						Regex regex = new Regex ( Properties.Resources.TopProcessRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace |
							RegexOptions.Multiline );
						if ( string.IsNullOrEmpty ( e.Data ) ) {
							return;
						}

						Match m = regex.Match ( e.Data );
						while ( m.Success ) {
							int pid = 0;
							int.TryParse ( m.Groups[1].Value, out pid );
							int cpu = 0;
							int.TryParse ( m.Groups[2].Value, out cpu );
							int thread = 0;
							int.TryParse ( m.Groups[4].Value, out thread );
							long vss = 0;
							long.TryParse ( m.Groups[5].Value, out vss );
							long rss = 0;
							long.TryParse ( m.Groups[6].Value, out rss );
							string uid = m.Groups[7].Value.Trim ( );
							string name = m.Groups[8].Value.Trim ( );

							ProcessInfo pi = new ProcessInfo ( pid, name, thread, vss, rss, uid, cpu );
							if ( TopProcessUpdated != null ) {
								TopProcessUpdated ( this, new ProcessInfoEventArgs ( pi ) );
							}
							m = m.NextMatch ( );
						}

						if ( TopProcessUpdateComplete != null ) {
							TopProcessUpdateComplete ( this, EventArgs.Empty );
						}
					};

					TopProcess.ErrorDataReceived += delegate ( object sender, DataReceivedEventArgs e ) {

					};

					TopProcess.Exited += delegate ( object sender, EventArgs e ) {

					};
					TopProcess.Start ( );
					TopProcess.BeginOutputReadLine ( );
					TopProcess.BeginErrorReadLine ( );
				}
			} catch ( Exception ex ) {
				this.LogError ( ex.Message, ex );
			}
		}

		public void AdbProcessCleanUp ( ) {
			Process[] procs = Process.GetProcessesByName ( "adb" );
			foreach ( var item in procs ) {
				try {
					item.Kill ( );
				} catch { }
			}
		}

		/// <summary>
		/// Determines whether [is app directory] [the specified path].
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>
		/// 	<c>true</c> if [is app directory] [the specified path]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsAppDirectory ( string path ) {
			return string.Compare ( path, SYSTEM_APP_PATH, false ) == 0 ||
				string.Compare ( path, APP_PRIVATE_PATH, false ) == 0 ||
				string.Compare ( path, APP_PUBLIC_PATH, false ) == 0 ||
				string.Compare ( path, APP_SD_PUBLIC_PATH, false ) == 0 ||
				string.Compare ( path, APP_SD_PRIVATE_PATH, false ) == 0;
		}

		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="local">local arg</param>
		/// <param name="remote">remote arg</param>
		public void Forward ( string local, string remote, bool rebind ) {
			Forward ( this.DefaultDevice, local, remote, rebind );
		}

		/// <summary>
		/// Forwards the specified device.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="local">The local.</param>
		/// <param name="remote">The remote.</param>
		public void Forward ( string device, string local, string remote, bool rebind ) {
			var roptions = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;
			local.Require ( ( i ) => {
				var match = i.IsMatch ( Resources.Strings.ForwardTypeRegexPattern, roptions );
				return !string.IsNullOrWhiteSpace ( i ) && match;
			}, "local argument must not be null/empty and must have the type:destination", "local" );
			remote.Require ( ( i ) => {
				var match = i.IsMatch(Resources.Strings.ForwardTypeRegexPattern,roptions);
				return !string.IsNullOrWhiteSpace ( i ) && match;
			}, "remote argument must not be null/empty and must have the type:destination", "remote" );

			CommandRun ( device, AdbCommand.Forward, "{2}{0} {1}".With ( local, remote, !rebind ? "--no-rebind " : string.Empty ));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="local">local arg</param>
		/// <param name="remote">remote arg</param>
		public void ForwardRemove ( string local ) {
			ForwardRemove ( this.DefaultDevice, local );
		}

		/// <summary>
		/// Forwards the specified device.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="local">The local.</param>
		/// <param name="remote">The remote.</param>
		public void ForwardRemove ( string device, string local ) {
			var roptions = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;
			local.Require ( ( i ) => {
				var match = i.IsMatch ( Resources.Strings.ForwardTypeRegexPattern, roptions );
				return !string.IsNullOrWhiteSpace ( i ) && match;
			}, "local argument must not be null/empty and must have the type:destination", "local" );
			CommandRun ( device, AdbCommand.Forward, "--remove {0} ".With ( local ) );
		}

		/// <summary>
		/// 
		/// </summary>
		public void ForwardRemoveAll ( ) {
			ForwardRemoveAll ( this.DefaultDevice );
		}

		/// <summary>
		/// Forwards the specified device.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="local">The local.</param>
		/// <param name="remote">The remote.</param>
		public void ForwardRemoveAll ( string device ) {
			CommandRun ( device, AdbCommand.Forward, "--remove-all" );
		}

		/// <summary>
		/// 
		/// </summary>
		public void ForwardList ( ) {
			ForwardList ( this.DefaultDevice );
		}

		/// <summary>
		/// Forwards the specified device.
		/// </summary>
		/// <param name="device">The device.</param>
		public void ForwardList ( string device ) {
			var result = CommandRun ( device.Require ( ), AdbCommand.Forward, "--list");
			this.LogDebug ( result.Output.ToString ( ) );
		}

		/// <summary>
		/// Reverses the specified local.
		/// </summary>
		/// <param name="local">local arg</param>
		/// <param name="remote">remote arg</param>
		/// <param name="rebind">if set to <c>true</c> [rebind].</param>
		public void Reverse ( string local, string remote, bool rebind ) {
			Reverse ( this.DefaultDevice, local, remote, rebind );
		}

		/// <summary>
		/// Forwards the specified device.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="local">The local.</param>
		/// <param name="remote">The remote.</param>
		/// <param name="rebind">if set to <c>true</c> [rebind].</param>
		public void Reverse ( string device, string local, string remote, bool rebind ) {
			var roptions = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;
			local.Require ( ( i ) => {
				var match = i.IsMatch ( Resources.Strings.ForwardTypeRegexPattern, roptions );
				return !string.IsNullOrWhiteSpace ( i ) && match;
			}, "local argument must not be null/empty and must have the type:destination", "local" );
			remote.Require ( ( i ) => {
				var match = i.IsMatch ( Resources.Strings.ForwardTypeRegexPattern, roptions );
				return !string.IsNullOrWhiteSpace ( i ) && match;
			}, "remote argument must not be null/empty and must have the type:destination", "remote" );

			CommandRun ( device, AdbCommand.Reverse, "{2}{0} {1}".With ( local, remote, !rebind ? "--no-rebind " : string.Empty ) );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="local">local arg</param>
		/// <param name="remote">remote arg</param>
		public void ReverseRemove ( string local ) {
			ReverseRemove ( this.DefaultDevice, local );
		}

		/// <summary>
		/// Forwards the specified device.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="local">The local.</param>
		/// <param name="remote">The remote.</param>
		public void ReverseRemove ( string device, string local ) {
			var roptions = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;
			local.Require ( ( i ) => {
				var match = i.IsMatch ( Resources.Strings.ForwardTypeRegexPattern, roptions );
				return !string.IsNullOrWhiteSpace ( i ) && match;
			}, "local argument must not be null/empty and must have the type:destination", "local" );
			CommandRun ( device, AdbCommand.Reverse, "--remove {0} ".With ( local ) );
		}

		/// <summary>
		/// 
		/// </summary>
		public void ReverseRemoveAll ( ) {
			ReverseRemoveAll ( this.DefaultDevice );
		}

		/// <summary>
		/// Forwards the specified device.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="local">The local.</param>
		/// <param name="remote">The remote.</param>
		public void ReverseRemoveAll ( string device ) {
			CommandRun ( device, AdbCommand.Reverse, "--remove-all" );
		}

		/// <summary>
		/// 
		/// </summary>
		public void ReverseList ( ) {
			ReverseList ( this.DefaultDevice );
		}

		/// <summary>
		/// Forwards the specified device.
		/// </summary>
		/// <param name="device">The device.</param>
		public void ReverseList ( string device ) {
			var result = CommandRun ( device.Require(), AdbCommand.Reverse, "--list" );
			this.LogDebug ( result.Output.ToString ( ) );
		}

		/// <summary>
		/// Gets the property.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns></returns>
		public string GetProperty ( string propertyName ) {
			return GetProperty ( DefaultDevice, propertyName );
		}

		/// <summary>
		/// Gets the property.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns></returns>
		public string GetProperty ( string device, string propertyName ) {
			PropertyCommandResult pcr = new PropertyCommandResult ( ShellRun ( device, string.Format ( CultureInfo.InvariantCulture, "getprop {0}", propertyName ) ).Output.ToString ( ) );
			return pcr.Value;
		}

		public string GetProperty ( params string[] propertyNames ) {
			return GetProperty ( DefaultDevice, propertyNames );
		}

		public string GetProperty ( string device, params string[] propertyNames ) {
			foreach ( var item in propertyNames ) {
				PropertyCommandResult pcr = new PropertyCommandResult ( ShellRun ( device, string.Format ( CultureInfo.InvariantCulture, "getprop {0}", item ) ).Output.ToString ( ) );
				if ( !String.IsNullOrEmpty ( pcr.Value ) ) {
					return pcr.Value;
				}
			}

			return null;
		}

		/// <summary>
		/// Gets the properties.
		/// </summary>
		/// <returns></returns>
		public PropertyList GetProperties ( ) {
			return GetProperties ( DefaultDevice );
		}

		/// <summary>
		/// Gets the properties.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <returns></returns>
		public PropertyList GetProperties ( string device ) {
			GenericCommandResult result = ShellRun ( device, "getprop" ) as GenericCommandResult;

			PropertyListCommandResult plcr = new PropertyListCommandResult ( result.Output.ToString ( ) );
			return plcr.PropertyList;
		}

		/// <summary>
		/// Gets the adb version.
		/// </summary>
		/// <returns></returns>
		public string GetAdbVersion ( ) {
			try {
				string data = RunAdbCommand ( string.Empty, AdbCommand.Version, string.Empty, true, true );
				Regex regex = new Regex ( @"(\d{1,}\.\d{1,}\.\d{1,})", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline );
				Match m = regex.Match ( data.Trim ( ) );
				if ( m.Success ) {
					return m.Groups[1].Value;
				} else {
					return string.Empty;
				}
			} catch ( AdbException aex ) {
				this.LogError ( aex.Message, aex );
				return string.Empty;
			} catch ( Exception ex ) {
				this.LogError ( ex.Message, ex );
				throw;
			}
		}

		/// <summary>
		/// Gets the aapt version.
		/// </summary>
		/// <returns></returns>
		public string GetAaptVersion ( ) {
			try {
				string data = RunAaptCommand ( AaptCommand.Version, string.Empty );
				Regex regex = new Regex ( @"(\d{1,}\.\d{1,}(\.\d{1,}\.\d{1,})?)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline );
				Match m = regex.Match ( data );
				if ( m.Success ) {
					return m.Groups[1].Value;
				} else {
					return string.Empty;
				}
			} catch ( AdbException aex ) {
				this.LogError ( aex.Message, aex );
				return string.Empty;
			} catch ( Exception ex ) {
				this.LogError ( ex.Message, ex );
				throw;
			}
		}

		public Size GetScreenResolution( string device ) {
			var result = ShellRun ( device, "dumpsys window | grep \"cur=\"" ).Output.ToString();
			var m = result.Match ( Resources.Strings.DumpSysWindowScreenResolutionRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline );
			if(m.Success) {
				var w = int.Parse ( m.Groups[1].Value );
				var h = int.Parse ( m.Groups[2].Value );
				return new Size ( w, h );
			}

			return new Size ( 0, 0 );
		}

		public Size GetScreenResolution ( ) {
			return GetScreenResolution ( this.DefaultDevice );
		}

		public ScreenCaptureCommandResult ScreenCapture(string device, Size resolution, int bitrate, TimeSpan timeLimit, bool rotate, string file) {
			return CommandRun ( device, AdbCommand.ShellScreenCapture, 
				"--bit-rate {0} --time-limit {1} --size {2} --verbose {3}{4}".With ( bitrate, timeLimit.TotalSeconds, "{0}x{1}".With ( resolution.Width, resolution.Height ), rotate ? "--rotate " : "", file ) 
			) as ScreenCaptureCommandResult;
		}

		/// <summary>
		/// Gets the screen shot.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <returns></returns>
		/*public Image GetScreenShot ( string device ) {
			System.IO.FileInfo file = PullFile ( device, "/dev/graphics/fb0" );
			return Rgb565.ToImage ( file.FullName );

		}

		/// <summary>
		/// Gets the screen shot.
		/// </summary>
		/// <returns></returns>
		public Image GetScreenShot ( ) {
			return GetScreenShot ( this.DefaultDevice );
		}*/

		/// <summary>
		/// Uninstalls the apk.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="package">The package.</param>
		/// <returns></returns>
		public bool UninstallApk ( string device, string package ) {
			try {
				GenericCommandResult result = CommandRun ( device, AdbCommand.Uninstall, package ) as GenericCommandResult;
				this.LogDebug ( result.Output.ToString ( ).Trim ( ) );
				return string.Compare ( result.Output.ToString ( ).Trim ( ), "Success", false ) == 0 || result.Output.Length == 0;
			} catch ( Exception ex ) {
				this.LogError ( ex.Message, ex );
				return false;
			}
		}

		/// <summary>
		/// Uninstalls the apk.
		/// </summary>
		/// <param name="package">The package.</param>
		/// <returns></returns>
		public bool UninstallApk ( string package ) {
			return UninstallApk ( this.DefaultDevice, package );
		}

		/// <summary>
		/// Installs the apk. If it fails, it attempts to "reinstall".
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="apkFile">The apk file.</param>
		/// <returns></returns>
		public bool InstallApk ( string device, string apkFile ) {
			try {
				GenericCommandResult result = CommandRun ( device, AdbCommand.Install, string.Format ( "\"{0}\"", apkFile ) ) as GenericCommandResult;
				Regex r = new Regex ( @"\s?Success\s?(.*?)$", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace );
				this.LogDebug ( result.Output.ToString ( ).Trim ( ) );
				this.LogInfo ( result.SpecialOutput.ToString ( ).Trim ( ) );
				bool resvalue = r.IsMatch ( result.Output.ToString ( ).Trim ( ) ) || result.Output.Length == 0;
				if ( !resvalue ) {
					resvalue = ReinstallApk ( device, apkFile );
				}

				return resvalue;
			} catch ( Exception ex ) {
				this.LogError ( ex.Message, ex );
				return false;
			}
		}



		/// <summary>
		/// Installs the apk.
		/// </summary>
		/// <param name="apkFile">The apk file.</param>
		/// <returns></returns>
		public bool InstallApk ( string apkFile ) {
			return InstallApk ( this.DefaultDevice, apkFile );
		}

		/// <summary>
		/// Reinstalls the apk.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="apkFile">The apk file.</param>
		/// <returns></returns>
		public bool ReinstallApk ( string device, string apkFile ) {
			try {
				GenericCommandResult result = CommandRun ( device, AdbCommand.Install, string.Format ( "-r \"{0}\"", apkFile ) ) as GenericCommandResult;
				Regex r = new Regex ( @"\s?Failure\s?(.*?)$", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace );
				this.LogDebug ( result.Output.ToString ( ).Trim ( ) );
				this.LogInfo ( result.SpecialOutput.ToString ( ).Trim ( ) );
				var m = r.Match ( result.Output.ToString ( ).Trim ( ) );
				if ( m.Success ) {
					// found a failure message
					this.LogError ( m.Groups[1].Value );
					return false;
				}
				return !m.Success || result.Output.Length == 0;
			} catch ( Exception ex ) {
				this.LogError ( ex.Message, ex );
				return false;
			}
		}

		/// <summary>
		/// Reinstalls the apk.
		/// </summary>
		/// <param name="apkFile">The apk file.</param>
		/// <returns></returns>
		public bool ReinstallApk ( string apkFile ) {
			return ReinstallApk ( this.DefaultDevice, apkFile );
		}

		/// <summary>
		/// Flashes the image.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="image">The image.</param>
		public void FlashImage ( string device, string image ) {
			ShellRun ( device, string.Format ( "flash_image recovery \"{0}\"", image ) );
		}

		/// <summary>
		/// Flashes the image.
		/// </summary>
		/// <param name="image">The image.</param>
		public void FlashImage ( string image ) {
			FlashImage ( this.DefaultDevice, image );
		}

		public void FastbootEraseRecovery ( string device ) {
			ShellRun ( device, "fastboot erase recovery" );
		}

		/// <summary>
		/// Uses Fastboots to erase recovery image.
		/// </summary>
		public void FastbootEraseRecovery ( ) {
			FastbootEraseRecovery ( this.DefaultDevice );
		}

		public void FastbootFlashImage ( string device, string image ) {
			ShellRun ( device, string.Format ( "fastboot flash recovery \"{0}\"", image ) );
		}

		public void FastbootFlashImage ( string image ) {
			FastbootFlashImage ( this.DefaultDevice, image );
		}

		/// <summary>
		/// Reboots the default device.
		/// </summary>
		public void Reboot ( ) {
			Reboot ( this.DefaultDevice );
		}

		/// <summary>
		/// Reboots the specified device.
		/// </summary>
		/// <param name="device">The device.</param>
		public void Reboot ( string device ) {
			RunAdbCommand ( device, AdbCommand.Reboot, String.Empty );
		}

		public void Reboot ( string device, string mode ) {
			RunAdbCommand ( device, AdbCommand.Reboot, mode );
		}

		/// <summary>
		/// Reboots in to bootloader.
		/// </summary>
		/// <param name="device">The device.</param>
		public void RebootBootloader ( string device ) {
			Reboot ( device, "bootloader" );
		}

		/// <summary>
		/// Reboots in to bootloader.
		/// </summary>
		public void RebootBootloader ( ) {
			RebootRecovery ( this.DefaultDevice );
		}


		/// <summary>
		/// Reboots in to recovery.
		/// </summary>
		/// <param name="device">The device.</param>
		public void RebootRecovery ( string device ) {
			Reboot ( device, "recovery" );
		}

		/// <summary>
		/// Reboots in to recovery.
		/// </summary>
		public void RebootRecovery ( ) {
			RebootRecovery ( this.DefaultDevice );
		}

		/// <summary>
		/// Reboots the device in download mode.
		/// </summary>
		/// <param name="device">The device.</param>
		public void RebootDownloadMode ( String device ) {
			Reboot ( device, "download" );
			RunAdbCommand ( device, AdbCommand.Reboot, "download" );
		}

		/// <summary>
		/// Reboots the device in download mode.
		/// </summary>
		public void RebootDownloadMode ( ) {
			RebootDownloadMode ( this.DefaultDevice );
		}

		/// <summary>
		/// Applies the update.
		/// </summary>
		/// <param name="device">The device.</param>
		public void ApplyUpdate ( string device ) {
			/*
				mkdir -p /cache/recovery/
				echo 'boot-recovery' >/cache/recovery/command
				echo '--nandroid' >> /cache/recovery/command
				echo '--update_package=SDCARD:update.zip' >> /cache/recovery/command
			*/
			ShellRun ( device, "mkdir -p /cache/recovery/" );
			ShellRun ( device, "echo 'boot-recovery ' > /cache/recovery/command" );
			//ShellRun ( device, "echo '--nandroid ' >> /cache/recovery/command" );
			ShellRun ( device, "echo '--update_package=SDCARD:update.zip' >> /cache/recovery/command" );
			RebootRecovery ( device );
		}

		/// <summary>
		/// Applies the update.
		/// </summary>
		public void ApplyUpdate ( ) {
			ApplyUpdate ( this.DefaultDevice );
		}

		public void Rename ( string path, string newName ) {
			Rename ( DefaultDevice, path, newName );
		}

		public void Rename ( string device, string path, string newName ) {
			if ( string.IsNullOrEmpty ( newName ) ) {
				return;
			}
			ShellRun ( device, string.Format ( CultureInfo.InvariantCulture, "rn {0} {1}", path, newName ) );
		}

		/// <summary>
		/// Renames the file.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="file">The file.</param>
		/// <param name="newName">The new name.</param>
		public void RenameFile ( string device, string file, string newName ) {
			Rename ( device, file, newName );
		}

		/// <summary>
		/// Renames the file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="newName">The new name.</param>
		public void RenameFile ( string file, string newName ) {
			RenameFile ( this.DefaultDevice, file, newName );
		}

		/// <summary>
		/// Deletes the file.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="file">The file.</param>
		public void DeleteFile ( string device, string file ) {
			ShellRun ( device, string.Format ( "busybox rm -f \"{0}\"", file ) );
		}

		/// <summary>
		/// Deletes the file.
		/// </summary>
		/// <param name="file">The file.</param>
		public void DeleteFile ( string file ) {
			DeleteFile ( this.DefaultDevice, file );
		}

		/// <summary>
		/// Deletes the Directory. This will recursively remove all files and sub-directories as well 
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="path">The path to delete.</param>
		public void DeleteDirectory ( string device, string path ) {
			ShellRun ( device, string.Format ( "busybox rm -rf {0}", path ) );
		}

		/// <summary>
		/// Deletes the Directory. This will recursively remove all files and sub-directories as well 
		/// </summary>
		/// <param name="file">The file.</param>
		public void DeleteDirectory ( string path ) {
			DeleteDirectory ( this.DefaultDevice, path );
		}

		/// <summary>
		/// Pushes the file.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="local">The local.</param>
		/// <param name="remote">The remote.</param>
		/// <returns></returns>
		public TransferCommandResult PushFile ( string device, string local, string remote ) {
			return CommandRun ( device, AdbCommand.Push, string.Format ( "\"{0}\" \"{1}\"", local, remote ) ) as TransferCommandResult;
		}

		/// <summary>
		/// Pushes the file.
		/// </summary>
		/// <param name="local">The local.</param>
		/// <param name="remote">The remote.</param>
		/// <returns></returns>
		public TransferCommandResult PushFile ( string local, string remote ) {
			return PushFile ( this.DefaultDevice, local, remote );
		}

		/// <summary>
		/// Pulls the files.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="paths">The paths.</param>
		/// <returns></returns>
		public List<System.IO.FileInfo> PullFiles ( string device, List<string> paths ) {
			List<System.IO.FileInfo> files = new List<System.IO.FileInfo> ( );
			string tempPath = FolderManagement.TempFolder;
			if ( !System.IO.Directory.Exists ( tempPath ) ) {
				System.IO.Directory.CreateDirectory ( tempPath );
			}
			foreach ( string var in paths ) {
				// get full path to use as the file name
				var cacheKey = Cache.GetCacheKey ( var );
				string newFile = System.IO.Path.Combine ( tempPath, System.IO.Path.GetFileName ( cacheKey ) );
				TransferCommandResult result = CommandRun ( device, AdbCommand.Pull, string.Format ( "\"{0}\" \"{1}\"", var, newFile ) ) as TransferCommandResult;
				files.Add ( new System.IO.FileInfo ( newFile ) );
			}
			return files;
		}

		/// <summary>
		/// Pulls the files.
		/// </summary>
		/// <param name="paths">The paths.</param>
		/// <returns></returns>
		public List<System.IO.FileInfo> PullFiles ( List<string> paths ) {
			return PullFiles ( this.DefaultDevice, paths );
		}

		/// <summary>
		/// Pulls the file.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public System.IO.FileInfo PullFile ( string device, string path ) {
			List<System.IO.FileInfo> fiList = PullFiles ( device, new List<string> ( new string[] { path } ) );

			if ( fiList.Count > 0 ) {
				return fiList[0];
			} else {
				return null;
			}
		}

		/// <summary>
		/// Pulls the file.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public System.IO.FileInfo PullFile ( string path ) {
			return PullFile ( this.DefaultDevice, path );
		}

		/// <summary>
		/// Pulls the directory.
		/// </summary>
		/// <param name="remotePath">The remote path.</param>
		/// <param name="localPath">The local path.</param>
		/// <returns></returns>
		public System.IO.DirectoryInfo PullDirectory ( string remotePath, string localPath ) {
			return PullDirectory ( DefaultDevice, remotePath, localPath );
		}

		/// <summary>
		/// Pulls the directory.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="remotePath">The remote path.</param>
		/// <param name="localPath">The local path.</param>
		/// <returns></returns>
		public System.IO.DirectoryInfo PullDirectory ( string device, string remotePath, string localPath ) {
			if ( !remotePath.EndsWith ( new string ( new char[] { Path.DirectorySeparatorChar } ) ) ) {
				remotePath = remotePath + Path.DirectorySeparatorChar;
			}
			TransferCommandResult result = CommandRun ( device, AdbCommand.Pull, string.Format ( "\"{0}\" \"{1}\"", remotePath, localPath ) ) as TransferCommandResult;

			return new System.IO.DirectoryInfo ( localPath );
		}

		/// <summary>
		/// Pulls the directory.
		/// </summary>
		/// <param name="remotePath">The remote path.</param>
		/// <returns></returns>
		public System.IO.DirectoryInfo PullDirectory ( string remotePath ) {
			return PullDirectory ( DefaultDevice, remotePath, System.IO.Path.Combine (FolderManagement.TempFolder, Path.GetDirectoryName ( remotePath ) ) );
		}

		/// <summary>
		/// Gets the installed packages.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <returns></returns>
		public PackageManagerListPackagesCommandResult GetInstalledPackages ( string device ) {
			PackageManagerListPackagesCommandResult result = CommandRun ( device, AdbCommand.ShellPackageManager, "list packages -f" ) as PackageManagerListPackagesCommandResult;
			return result;
		}

		/// <summary>
		/// Gets the installed packages.
		/// </summary>
		/// <returns></returns>
		public PackageManagerListPackagesCommandResult GetInstalledPackages ( ) {
			return GetInstalledPackages ( this.DefaultDevice );
		}

		/// <summary>
		/// Gets the installed packages apk information.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <returns></returns>
		public List<AaptBrandingCommandResult> GetInstalledPackagesApkInformation ( string device ) {
			PackageManagerListPackagesCommandResult result = GetInstalledPackages ( device );
			List<AaptBrandingCommandResult> apkInfo = new List<AaptBrandingCommandResult> ( );
			foreach ( var item in result.Packages.Keys ) {
				AaptBrandingCommandResult ainfo = GetApkInformation ( result.Packages[item] );
				ainfo.DevicePath = result.Packages[item];
				if ( string.IsNullOrEmpty ( ainfo.Package ) ) {
					ainfo.Package = item;
				}
				apkInfo.Add ( ainfo );
			}
			apkInfo.Sort ( new DroidExplorer.Core.Components.AaptBrandingCommandResultComparer ( ) );
			return apkInfo;
		}


		/// <summary>
		/// Gets the installed packages apk information.
		/// </summary>
		/// <returns></returns>
		public List<AaptBrandingCommandResult> GetInstalledPackagesApkInformation ( ) {
			return GetInstalledPackagesApkInformation ( this.DefaultDevice );
		}

		/// <summary>
		/// Determines whether the specified package is installed on the device.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="package">The package.</param>
		/// <returns>
		/// 	<c>true</c> if the specified package is installed on the device; otherwise, <c>false</c>.
		/// </returns>
		public bool IsPackageInstalled ( string device, string package ) {
			GenericCommandResult gr = new GenericCommandResult ( RunAdbCommand ( device, AdbCommand.Shell, string.Format ( CultureInfo.InvariantCulture, "pm path {0}", package ) ) );

			if ( gr.Output.Length > 0 ) {
				return gr.Output.ToString ( ).StartsWith ( "package:" );
			} else {
				return false;
			}

		}

		/// <summary>
		/// Determines whether the specified package is installed on the default device.
		/// </summary>
		/// <param name="package">The package.</param>
		/// <returns>
		/// 	<c>true</c> if the specified package is installed on the default device; otherwise, <c>false</c>.
		/// </returns>
		public bool IsPackageInstalled ( string package ) {
			return IsPackageInstalled ( this.DefaultDevice, package );
		}

		public AaptBrandingCommandResult GetApkInformation ( string apkFile ) {
			System.IO.FileInfo lApk = PullFile ( apkFile );
			if ( lApk == null || !lApk.Exists ) {
				throw new System.IO.FileNotFoundException ( "File was not found on device.", System.IO.Path.GetFileName ( apkFile ) );
			}

			return GetLocalApkInformation ( lApk.FullName );
		}

		public AaptBrandingCommandResult GetApkInformationFromLocalCache ( String apkFile, String cacheDirectory ) {
			string keyName = apkFile;
			if ( keyName.StartsWith ( "/" ) ) {
				keyName = keyName.Substring ( 1 );
			}
			keyName = keyName.Replace ( "/", "." );

			// find if there is a local cache
			System.IO.FileInfo lcache = new System.IO.FileInfo ( System.IO.Path.Combine ( cacheDirectory, String.Format ( "{0}.cache", keyName ) ) );
			if ( lcache.Exists ) {
				AaptBrandingCommandResult result = null;
				XmlSerializer ser = new XmlSerializer ( typeof ( AaptBrandingCommandResult ) );
				using ( System.IO.FileStream fs = new System.IO.FileStream ( lcache.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read ) ) {
					result = ser.Deserialize ( fs ) as AaptBrandingCommandResult;
				}

				return result;
			} else {
				System.IO.FileInfo lApk = PullFile ( apkFile );
				if ( lApk == null || !lApk.Exists ) {
					throw new System.IO.FileNotFoundException ( "File was not found on device.", System.IO.Path.GetFileName ( apkFile ) );
				}

				AaptBrandingCommandResult result = GetApkInformation ( lApk.FullName );
				XmlSerializer ser = new XmlSerializer ( typeof ( AaptBrandingCommandResult ) );
				using ( System.IO.FileStream fs = new System.IO.FileStream ( lcache.FullName, System.IO.FileMode.Create, System.IO.FileAccess.Write ) ) {
					ser.Serialize ( fs, result );
				}
				return result;
			}
		}

		public AaptBrandingCommandResult GetLocalApkInformation ( string apkFile ) {
			if ( string.IsNullOrEmpty ( apkFile ) || !System.IO.File.Exists ( apkFile ) ) {
				throw new System.IO.FileNotFoundException ( "File was not found on device.", System.IO.Path.GetFileName ( apkFile ) );
			}

			AaptBrandingCommandResult result = AaptCommandRun ( AaptCommand.Dump_Badging, string.Format ( "\"{0}\"", apkFile ) ) as AaptBrandingCommandResult;
			result.LocalApk = apkFile;
			return result;
		}

		public List<string> GetApkPermissions ( string apkFile ) {
			System.IO.FileInfo lApk = PullFile ( apkFile );
			if ( lApk == null || !lApk.Exists ) {
				throw new System.IO.FileNotFoundException ( "File was not found on device.", apkFile );
			}

			return GetLocalApkPermissions ( lApk.FullName );
		}

		public List<string> GetLocalApkPermissions ( string apkFile ) {
			System.IO.FileInfo lApk = new System.IO.FileInfo ( apkFile );
			if ( lApk == null || !lApk.Exists ) {
				throw new System.IO.FileNotFoundException ( "File was not found on device.", apkFile );
			}

			AaptPermissionsCommandResult result = AaptCommandRun ( AaptCommand.Dump_Permissions, string.Format ( "\"{0}\"", apkFile ) ) as AaptPermissionsCommandResult;

			if ( result != null ) {
				return result.Permissions;
			} else {
				return null;
			}
		}

		/// <summary>
		/// Gets the apk icon image.
		/// </summary>
		/// <param name="apkFile">The apk file.</param>
		/// <returns></returns>
		public Image GetApkIconImage ( string apkFile ) {
			System.IO.FileInfo lApk = PullFile ( apkFile );
			if ( lApk == null || !lApk.Exists ) {
				throw new System.IO.FileNotFoundException ( "File was not found on device.", System.IO.Path.GetFileName ( apkFile ) );
			}

			AaptBrandingCommandResult result = GetLocalApkInformation ( lApk.FullName );
			result.LocalApk = lApk.FullName;

			if ( !string.IsNullOrEmpty ( result.Icon ) ) {
				string outPath = System.IO.Path.Combine ( lApk.Directory.FullName, System.IO.Path.GetFileNameWithoutExtension ( lApk.Name ) );
				try {
					ZipHelper.Unzip ( lApk.FullName, outPath, result.Icon, true, true );
				} catch ( Exception ex ) {
					this.LogError ( ex.Message, ex );
					return null;
				}
				System.IO.FileInfo imageFile = new System.IO.FileInfo ( System.IO.Path.Combine ( outPath, System.IO.Path.GetFileName ( result.Icon ) ) );
				if ( imageFile.Exists ) {
					return Image.FromFile ( imageFile.FullName );
				} else {
					return null;
				}
			} else {
				return null;
			}
		}

		public Image GetLocalApkIconImage ( string apkFile ) {
			System.IO.FileInfo lApk = new System.IO.FileInfo ( apkFile );
			if ( lApk == null || !lApk.Exists ) {
				throw new System.IO.FileNotFoundException ( "File was not found on device.", System.IO.Path.GetFileName ( apkFile ) );
			}

			AaptBrandingCommandResult result = GetLocalApkInformation ( lApk.FullName );
			result.LocalApk = lApk.FullName;

			if ( !string.IsNullOrEmpty ( result.Icon ) ) {
				string outPath = System.IO.Path.Combine ( FolderManagement.TempFolder, System.IO.Path.GetFileNameWithoutExtension ( lApk.Name ) );
				try {
					ZipHelper.Unzip ( lApk.FullName, outPath, result.Icon, true, true );
				} catch ( Exception ex ) {
					this.LogError ( ex.Message, ex );
					return null;
				}
				System.IO.FileInfo imageFile = new System.IO.FileInfo ( System.IO.Path.Combine ( outPath, System.IO.Path.GetFileName ( result.Icon ) ) );
				if ( imageFile.Exists ) {
					return Image.FromFile ( imageFile.FullName );
				} else {
					return null;
				}
			} else {
				return null;
			}
		}



		/// <summary>
		/// Lists the directories.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public List<DroidExplorer.Core.IO.FileSystemInfo> ListDirectories ( string path ) {
			return ListDirectories ( this.DefaultDevice, path );
		}

		/// <summary>
		/// Lists the directories.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public List<DroidExplorer.Core.IO.FileSystemInfo> ListDirectories ( string device, string path ) {
			List<DroidExplorer.Core.IO.FileSystemInfo> fsi = GetDirectoryContents ( device, path );
			List<DroidExplorer.Core.IO.FileSystemInfo> result = new List<DroidExplorer.Core.IO.FileSystemInfo> ( );
			foreach ( DroidExplorer.Core.IO.FileSystemInfo var in fsi ) {
				if ( var.IsDirectory ) {
					result.Add ( var );
				}
			}
			return result;
		}

		/// <summary>
		/// Gets the directory contents.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public List<DroidExplorer.Core.IO.FileSystemInfo> GetDirectoryContents ( string path ) {
			return GetDirectoryContents ( this.DefaultDevice, path );
		}

		/// <summary>
		/// Gets the directory contents.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public List<DroidExplorer.Core.IO.FileSystemInfo> GetDirectoryContents ( string device, string path ) {
			DirectoryListCommandResult result = ShellLSRun ( device, string.Format ( Properties.Resources.ListDirectoryCommand, path ) ) as DirectoryListCommandResult;
			this.LogDebug ( result.Output.ToString ( ) );
			result.FileSystemItems.Sort ( new FileSystemInfoComparer ( ) );
			return result.FileSystemItems;
		}

		/// <summary>
		/// Gets or sets the state.
		/// </summary>
		/// <value>The state.</value>
		public DeviceState State {
			get { return this._deviceState; }
			private set { this._deviceState = value; }
		}

		/// <summary>
		/// Launches the shell window.
		/// </summary>
		/// <param name="device">The device.</param>
		public void LaunchShellWindow ( string device ) {
			LaunchShellWindow ( device, string.Empty );
		}

		/// <summary>
		/// Launches the shell window.
		/// </summary>
		public void LaunchShellWindow ( ) {
			LaunchShellWindow ( this.DefaultDevice );
		}

		/// <summary>
		/// Launches the shell window.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="initialCommand">The initial command.</param>
		public void LaunchShellWindow ( string device, string initialCommand ) {
			Process shell = new Process ( );
			StringBuilder commandArg = new StringBuilder ( AdbCommandArguments ( device, AdbCommand.Shell ) );
			if ( !string.IsNullOrEmpty ( initialCommand ) ) {
				commandArg.AppendFormat ( " {0}", initialCommand );
			}
			ProcessStartInfo psi = new ProcessStartInfo ( FolderManagement.GetSdkTool ( ADB_COMMAND ), commandArg.ToString ( ) );
			psi.CreateNoWindow = false;
			psi.ErrorDialog = true;
			psi.WindowStyle = ProcessWindowStyle.Normal;
			shell.StartInfo = psi;
			shell.Start ( );
		}

		/// <summary>
		/// Launches the redirected shell window.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="initialCommand">The initial command.</param>
		/// <param name="shellHandler">The shell handler.</param>
		public void LaunchRedirectedShellWindow ( string device, string initialCommand, IShellProcess shellHandler ) {
			new Thread ( new ThreadStart ( delegate ( ) {

				var args = AdbCommandArguments ( device, CommandRunner.AdbCommand.Shell );
				var tool = FolderManagement.GetSdkTool ( CommandRunner.ADB_COMMAND );
				shellHandler.StartProcess ( tool, "{0} {1}".With ( args, initialCommand ).Trim ( ) );
			} ) ).Start ( );
		}

		/// <summary>
		/// Handles the OutputDataReceived event of the shell control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Diagnostics.DataReceivedEventArgs"/> instance containing the event data.</param>
		void shell_OutputDataReceived ( object sender, DataReceivedEventArgs e ) {
			throw new NotImplementedException ( );
		}



		/// <summary>
		/// Launches the hierarchy viewer.
		/// </summary>
		public void LaunchHierarchyViewer ( ) {
			LaunchProcessWindow ( FolderManagement.GetSdkTool ( HIERARCHYVIEWER_COMMAND ), string.Empty, false );
		}

		/// <summary>
		/// Launches the dalvik debug monitor.
		/// </summary>
		public void LaunchDalvikDebugMonitor ( ) {
			LaunchProcessWindow ( FolderManagement.GetSdkTool ( DDMS_COMMAND ), string.Empty, false );
		}

		public void LaunchSdkManager ( ) {
			LaunchProcessWindow ( FolderManagement.GetSdkTool ( SDKMANAGER_COMMAND ), string.Empty, false );
		}

		public void LaunchAvdManager ( ) {
			LaunchProcessWindow ( FolderManagement.GetSdkTool ( AVDMANAGER_COMMAND ), string.Empty, false );
		}

		/// <summary>
		/// Launches the process window.
		/// </summary>
		/// <param name="process">The process.</param>
		/// <param name="args">The args.</param>
		/// <param name="visible">if set to <c>true</c> [visible].</param>
		public void LaunchProcessWindow ( string process, string args, bool visible ) {
			Process shell = new Process ( );
			ProcessStartInfo psi = new ProcessStartInfo ( process, args );
			psi.CreateNoWindow = !visible;
			psi.ErrorDialog = visible;
			psi.WindowStyle = visible ? ProcessWindowStyle.Normal : ProcessWindowStyle.Hidden;
			shell.StartInfo = psi;
			shell.Start ( );
		}

		/// <summary>
		/// Shell run a command.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns></returns>
		public CommandResult ShellRun ( string command ) {
			return ShellRun ( this.DefaultDevice, command );
		}

		/// <summary>
		/// Shell run a command.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="command">The command.</param>
		/// <returns></returns>
		public CommandResult ShellRun ( string device, string command ) {
			return CommandRun ( device, AdbCommand.Shell, command );
		}
		/// <summary>
		/// Shell runs the LS command.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns></returns>
		private CommandResult ShellLSRun ( string command ) {
			return ShellLSRun ( this.DefaultDevice, command );
		}
		/// <summary>
		/// Shell runs the LS command.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="command">The command.</param>
		/// <returns></returns>
		private CommandResult ShellLSRun ( string device, string command ) {
			return CommandRun ( device, AdbCommand.ShellLS, command );
		}

		/// <summary>
		/// Gets the devices.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<DeviceListItem> GetDevices ( ) {
			var list = ( CommandRun ( string.Empty, AdbCommand.Devices, string.Empty ) as DeviceListCommandResult ).Devices;
      return list;
		}

		public bool TcpConnect ( string connection ) {
			// check if it is already connected.
			if ( !this.GetDevices().Any ( x => x.SerialNumber == connection ) ) {
				this.LogDebug ( "Connecting to {0} via TCP/IP", connection );
				var result = ( CommandRun ( string.Empty, AdbCommand.Connect, connection ) as TcpConnectCommandResult ).Successful;
				if ( result ) {
					this.LogDebug ( "Successfully connected to {0}", connection );
				} else {
					this.LogWarn ( "Unable to connect to {0}", connection );
				}
				return result;
			} else {
				return true;
			}
		}

		/// <summary>
		/// Gets the serial number.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <returns></returns>
		public string GetSerialNumber ( string device ) {
			return CommandRun ( device, AdbCommand.Get_SerialNo, string.Empty ).Output.ToString ( ).Trim ( );
		}

		/// <summary>
		/// Gets the serial number.
		/// </summary>
		/// <returns></returns>
		public string GetSerialNumber ( ) {
			return GetSerialNumber ( this.DefaultDevice );
		}

		public BatteryInfo GetBatteryInfo ( ) {
			return GetBatteryInfo ( DefaultDevice, 5 * 60 * 1000 );
		}

		public BatteryInfo GetBatteryInfo ( String device ) {
			return GetBatteryInfo ( device, 5 * 60 * 1000 );
		}
		public BatteryInfo GetBatteryInfo ( long freshness ) {
			return GetBatteryInfo ( DefaultDevice, freshness );
		}

		public BatteryInfo GetBatteryInfo ( String device, long freshness ) {
			if ( lastBatteryInfo != null
								&& this.lastBatteryCheckTime > ( DateTime.Now.AddMilliseconds ( -freshness ) ) ) {
				return lastBatteryInfo;
			}
			var receiver = new BatteryReceiver ( );
			var result = this.CommandRun ( device, AdbCommand.Shell, "dumbsys battery" );



			//"dumpsys battery", receiver, BATTERY_TIMEOUT );
			lastBatteryInfo = receiver.BatteryInfo;
			lastBatteryCheckTime = DateTime.Now;
			return lastBatteryInfo;
		}

		public void DeviceBackup ( String device, String output, bool apk, bool system, bool shared ) {
			var file = new System.IO.FileInfo ( output );
			if ( !file.Directory.Exists ) {
				file.Directory.Create ( );
			}
			CommandRun ( device, AdbCommand.Backup, String.Format ( " -f {0} -all -{1}apk -{2}system -{3}shared", file.FullName.QuoteIfHasSpace ( ),
				apk ? String.Empty : "no",
				system ? String.Empty : "no",
				shared ? String.Empty : "no" ) );
			this.LogDebug ( "Backup Completed" );
		}

		public void DeviceBackup ( String output, bool apk, bool system, bool shared ) {
			DeviceBackup ( this.DefaultDevice, output, apk, system, shared );
		}

		public void DeviceBackup ( String output ) {
			DeviceBackup ( this.DefaultDevice, output );
		}

		public void DeviceBackup ( String device, String output ) {
			DeviceBackup ( device, output, true, true, false );
		}

		public void DeviceRestore ( String backupFile ) {
			DeviceRestore ( this.DefaultDevice, backupFile );
		}

		public void DeviceRestore ( String device, String backupFile ) {
			var bf = new System.IO.FileInfo ( backupFile );
			if ( bf.Exists ) {
				this.LogDebug ( "starting backup restore from '{0}'", bf.FullName );
				CommandRun ( device, AdbCommand.Restore, bf.FullName.QuoteIfHasSpace ( ) );
				this.LogDebug ( "Restore Completed" );
			} else {
				this.LogWarn ( "Unable to find '{0}'", bf.FullName );
			}
		}

		/// <summary>
		/// Gets the process ids.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <returns></returns>
		public List<int> GetProcessIds ( string device ) {
			return ( CommandRun ( device, AdbCommand.Jdwp, string.Empty ) as IntegerListCommandResult ).Values;
		}

		/// <summary>
		/// Gets the process ids.
		/// </summary>
		/// <returns></returns>
		public List<int> GetProcessIds ( ) {
			return GetProcessIds ( this.DefaultDevice );
		}

		/// <summary>
		/// Gets the process info list.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <returns></returns>
		public List<DroidExplorer.Core.IO.ProcessInfo> GetProcessInfoList ( string device ) {
			List<DroidExplorer.Core.IO.ProcessInfo> procs = new List<DroidExplorer.Core.IO.ProcessInfo> ( );
			foreach ( int pid in this.GetProcessIds ( device ) ) {
				string data = ShellRun ( string.Format ( "ps {0}", pid ) ).Output.ToString ( );
				procs.AddRange ( new ProcessInfoListCommandResult ( data ).Processes );
			}
			return procs;
		}

		/// <summary>
		/// Gets the process info list.
		/// </summary>
		/// <returns></returns>
		public List<DroidExplorer.Core.IO.ProcessInfo> GetProcessInfoList ( ) {
			return GetProcessInfoList ( this.DefaultDevice );
		}

		/// <summary>
		/// Gets the state of the device.
		/// </summary>
		/// <returns></returns>
		public DeviceState GetDeviceState ( string device ) {
			DeviceState state = StringToDeviceState ( CommandRun ( device, AdbCommand.Get_State, string.Empty ).Output.ToString ( ).Trim ( ) );
			if ( state == DeviceState.Unknown ) {
				return InternalGetDeviceState ( device );
			} else {
				return state;
			}
		}

		public DeviceState GetDeviceStatus ( string device ) {
			DeviceState state = StringToDeviceState ( CommandRun ( device, AdbCommand.Status_Window, string.Empty ).Output.ToString ( ).Trim ( ) );
			if ( state == DeviceState.Unknown ) {
				return InternalGetDeviceState ( device );
			} else {
				return state;
			}
		}

		internal DeviceState InternalGetDeviceState ( string device ) {
			DeviceListCommandResult dlcr = CommandRun ( device, AdbCommand.Devices, string.Empty ) as DeviceListCommandResult;
			var lookup = string.IsNullOrEmpty ( device ) ?
				string.IsNullOrEmpty ( DefaultDevice ) ?
				dlcr.Devices.Count == 0 ?
					string.Empty : dlcr.Devices[0].SerialNumber :
				dlcr.Devices.Count == 0 ?
					string.Empty : dlcr.Devices[0].SerialNumber :
				device;
			var d = dlcr.Devices.SingleOrDefault ( m => string.Compare ( lookup, m.SerialNumber, true ) == 0 );
			if ( d == null ) {
				return DeviceState.Unknown;
			} else {
				return StringToDeviceState ( d.State );
			}
		}

		/// <summary>
		/// Gets the state of the device.
		/// </summary>
		/// <returns></returns>
		public DeviceState GetDeviceState ( ) {
			return GetDeviceState ( this.DefaultDevice );
		}

		/// <summary>
		/// Aapts the command run.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="args">The args.</param>
		/// <returns></returns>
		private CommandResult AaptCommandRun ( AaptCommand command, string args ) {
			switch ( command ) {
				case AaptCommand.Dump_Badging:
					return new AaptBrandingCommandResult ( RunAaptCommand ( command, args ) );
				case AaptCommand.Dump_Permissions:
					return new AaptPermissionsCommandResult ( RunAaptCommand ( command, args ) );
				case AaptCommand.Dump_Configuration:
				case AaptCommand.Dump_Resources:
				case AaptCommand.Dump_XmlStrings:
				case AaptCommand.Dump_XmlTree:
				case AaptCommand.List:
				case AaptCommand.Package:
				case AaptCommand.Remove:
				case AaptCommand.Add:
				case AaptCommand.Version:
					return new GenericCommandResult ( string.Format ( "{0} command not supported", AaptCommandToString ( command ) ) );
				default:
					return new GenericCommandResult ( string.Format ( "unknown command ({0}) not supported", AaptCommandToString ( command ) ) );
			}
		}


		/// <summary>
		/// runs the command
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="command">The command.</param>
		/// <param name="args">The args.</param>
		/// <returns></returns>
		private CommandResult CommandRun ( string device, AdbCommand command, string args ) {
			var regexOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline;


			// we may need to re-tcp connect if adb root fails. so lets do that.
			if ( device.IsMatch ( DroidExplorer.Resources.Strings.IPDeviceRegex, regexOptions ) || device.IsMatch ( DroidExplorer.Resources.Strings.HostDeviceRegex, regexOptions ) ) {
				TcpConnect ( device );
			}


			switch ( command ) {
				case AdbCommand.Connect:
					return new TcpConnectCommandResult ( RunAdbCommand ( device, command, args, true, false ) );
				case AdbCommand.Devices:
					return new DeviceListCommandResult ( RunAdbCommand ( device, command, "-l", true, false ) );
				case AdbCommand.Help:
					this.LogWarn ( "Help command not supported" );
					return new GenericCommandResult ( "* help command not supported *" );
				case AdbCommand.Root:
					return new GenericCommandResult ( RunAdbCommand ( device, command, args, true, false ) );
				case AdbCommand.Status_Window:
					try {
						return new GenericCommandResult ( RunAdbCommand ( device, AdbCommand.Get_State, args, true, false ) );
					} catch ( AdbException aex) {
						this.LogWarn ( aex.Message, aex );
						return new GenericCommandResult ( "Unknown" );
					}
				case AdbCommand.Wait_For_Device:
					this.LogWarn ( "Wait-For-Device command not supported" );
					return new GenericCommandResult ( "* wait for device command not supported *" );
				case AdbCommand.ShellLS:
					string path = args.Remove ( 0, Properties.Resources.ListDirectoryCommand.Trim ( ).Length - 4 ).Trim ( );
					return new DirectoryListCommandResult ( RunAdbCommand ( device, AdbCommand.Shell, args ), path );
				case AdbCommand.ShellScreenCapture:
					return new ScreenCaptureCommandResult ( RunAdbCommand ( device, AdbCommand.Shell, "screenrecord " + args ) );
				case AdbCommand.ShellPackageManager:
					return new PackageManagerListPackagesCommandResult ( RunAdbCommand ( device, AdbCommand.Shell, string.Format ( "pm {0}", args ) ) );
				case AdbCommand.Jdwp:
					return new IntegerListCommandResult ( RunAdbCommand ( device, command, args ) );
				case AdbCommand.Remount:
					return new GenericCommandResult ( RunAdbCommand ( device, command, args, false, true ) );
				case AdbCommand.Push:
				case AdbCommand.Pull:
					return new TransferCommandResult ( RunAdbCommand ( device, command, args ) );
				case AdbCommand.Shell:
				case AdbCommand.Get_SerialNo:
				case AdbCommand.Version:
				case AdbCommand.Get_State:
				case AdbCommand.Uninstall:
				case AdbCommand.Install:
				case AdbCommand.Backup:
				case AdbCommand.Restore:
					return new GenericCommandResult ( RunAdbCommand ( device, command, args, true, true ) );
				case AdbCommand.Start_Server:
				case AdbCommand.Kill_Server:
					return new GenericCommandResult ( RunAdbCommand ( device, command, args, false, true ) );
				case AdbCommand.Forward:
				case AdbCommand.Reverse:
					return new GenericCommandResult ( RunAdbCommand ( device, command, args, false, true ) );
				case AdbCommand.Bugreport:
				case AdbCommand.Logcat:
				
				case AdbCommand.Emu:
				case AdbCommand.Sync:
				case AdbCommand.PPP:
					this.LogWarn ( string.Format ( CultureInfo.InvariantCulture, "* {0} command not supported *", AdbCommandToString ( command ) ) );
					return new GenericCommandResult ( string.Format ( CultureInfo.InvariantCulture, "* {0} command not supported *", AdbCommandToString ( command ) ) );
				default:
					this.LogWarn ( string.Format ( CultureInfo.InvariantCulture, "* unknown command ({0}) not supported *", AdbCommandToString ( command ) ) );
					return new GenericCommandResult ( string.Format ( "* unknown command ({0}) not supported *", AdbCommandToString ( command ) ) );
			}
		}

		/// <summary>
		/// Runs the aapt command.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="args">The args.</param>
		/// <returns></returns>
		private string RunAaptCommand ( AaptCommand command, string args ) {
			//string localPath = System.IO.Path.GetDirectoryName ( typeof ( CommandRunner ).Assembly.Location );

			StringBuilder result = new StringBuilder ( );
			Process proc = new Process ( );
			StringBuilder commandArg = new StringBuilder ( AaptCommandToString ( command ) );
			if ( !string.IsNullOrEmpty ( args ) ) {
				commandArg.AppendFormat ( " {0}", args );
			}
			ProcessStartInfo psi = new ProcessStartInfo ( FolderManagement.GetSdkTool ( AAPT_COMMAND ), commandArg.ToString ( ) );
			this.LogDebug ( "{0} {1}", System.IO.Path.GetFileName ( psi.FileName ), psi.Arguments );

			psi.CreateNoWindow = true;
			psi.ErrorDialog = true;
			psi.UseShellExecute = false;
			psi.RedirectStandardOutput = true;
			psi.RedirectStandardError = true;
			psi.WindowStyle = ProcessWindowStyle.Hidden;
			proc.StartInfo = psi;
			proc.OutputDataReceived += delegate ( object sender, DataReceivedEventArgs e ) {
				if ( !string.IsNullOrEmpty ( e.Data ) ) {
					result.AppendLine ( e.Data.Trim ( ) );
				}
			};

			proc.ErrorDataReceived += delegate ( object sender, DataReceivedEventArgs e ) {
				if ( !string.IsNullOrEmpty ( e.Data ) ) {
					result.AppendLine ( e.Data.Trim ( ) );
				}
			};

			proc.Exited += delegate ( object sender, EventArgs e ) {

			};
			proc.Start ( );
			proc.BeginOutputReadLine ( );
			proc.BeginErrorReadLine ( );
			proc.WaitForExit ( );
			return result.ToString ( );
		}

		private string RunAdbCommand ( string device, AdbCommand command, string args ) {
			return RunAdbCommand ( device, command, args, true, true );
		}

		/// <summary>
		/// Runs the adb command.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="command">The command.</param>
		/// <param name="args">The args.</param>
		/// <returns></returns>
		private string RunAdbCommand ( string device, AdbCommand command, string args, bool wait, bool logCommand ) {
			try {
				StringBuilder result = new StringBuilder ( );
				Process proc = new Process ( );
				StringBuilder commandArg = new StringBuilder ( AdbCommandArguments ( device, command ) );
				if ( !string.IsNullOrEmpty ( args ) ) {
					commandArg.AppendFormat ( " {0}", args );
				}
				ProcessStartInfo psi = new ProcessStartInfo ( FolderManagement.GetSdkTool ( ADB_COMMAND ), commandArg.ToString ( ) );
				if ( logCommand ) {
					this.LogDebug ( "{0} {1}", System.IO.Path.GetFileName ( psi.FileName ), psi.Arguments );
				}

				psi.CreateNoWindow = true;
				psi.ErrorDialog = false;
				psi.UseShellExecute = false;
				psi.RedirectStandardOutput = true;
				psi.RedirectStandardError = true;
				psi.WindowStyle = ProcessWindowStyle.Hidden;
				proc.StartInfo = psi;
				proc.OutputDataReceived += delegate ( object sender, DataReceivedEventArgs e ) {
					if ( !string.IsNullOrEmpty ( e.Data ) ) {
						result.AppendLine ( e.Data.Trim ( ) );
					}
				};

				proc.ErrorDataReceived += delegate ( object sender, DataReceivedEventArgs e ) {
					if ( !string.IsNullOrEmpty ( e.Data ) ) {
						result.AppendLine ( e.Data.Trim ( ) );
					}
				};

				proc.Exited += delegate ( object sender, EventArgs e ) {

				};
				proc.Start ( );
				proc.BeginOutputReadLine ( );
				proc.BeginErrorReadLine ( );

				if ( wait ) {
					proc.WaitForExit ( );
				} else {
					Thread.Sleep ( 250 );
				}

				return result.ToString ( );
			} catch ( Win32Exception wex ) {
				this.LogError ( wex.Message, wex );
			} catch ( Exception ex ) {
				this.LogError ( ex.Message, ex );
			}
			return string.Empty;
		}

		/// <summary>
		/// gets Adbs command arguments.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="command">The command.</param>
		/// <returns></returns>
		public string AdbCommandArguments ( string device, AdbCommand command ) {
			if ( string.IsNullOrEmpty ( device ) ) {
				return AdbCommandToString ( command );
			} else {
				return string.Format ( "-s {0} {1}", device, AdbCommandToString ( command ) );
			}
		}

		/// <summary>
		/// Convert the Adb Command to the string value
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns></returns>
		private string AdbCommandToString ( AdbCommand command ) {
			return command.ToString ( ).Replace ( "_", "-" ).ToLower ( );
		}

		/// <summary>
		/// Convert the aapt Command to the string value
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns></returns>
		private string AaptCommandToString ( AaptCommand command ) {
			return command.ToString ( ).Replace ( "_", " " ).ToLower ( );
		}

		/// <summary>
		/// Gets the device state from the string value
		/// </summary>
		/// <param name="val">The val.</param>
		/// <returns></returns>
		private DeviceState StringToDeviceState ( string val ) {
			try {
				object o = Enum.Parse ( typeof ( DeviceState ), val, true );
				if ( o == null ) {
					return DeviceState.Unknown;
				} else {
					return (DeviceState)o;
				}
			} catch ( Exception ex ) {
				return DeviceState.Unknown;
			}

		}


		public static ISettings Settings { get; set; }


		public static CommandRunner Instance {
			get {
				if ( _commandRunner == null ) {
					_commandRunner = new CommandRunner ( );
				}
				return _commandRunner;
			}
		}
	}
}
