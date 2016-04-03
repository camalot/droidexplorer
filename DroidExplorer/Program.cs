using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using DroidExplorer.UI;
using DroidExplorer.Core;
using System.IO;
using System.Drawing;
using DroidExplorer.Core.Plugins;
using DroidExplorer.Core.UI;
using DroidExplorer.Tools;
using DroidExplorer.Configuration;
using log4net.Config;
using DroidExplorer.Controls;
using DroidExplorer.Configuration.Net;
using System.Threading;
using DroidExplorer.Core.Exceptions;
using DroidExplorer.Components;

namespace DroidExplorer {
	/// <summary>
	/// 
	/// </summary>
	static class Program {
		private static PackageManagerForm _packageManager = null;
		private static List<IPlugin> _loadedPlugins = null;
		private static int StartUpStepCount = 18;
		/// <summary>
		/// Initializes the <see cref="Program"/> class.
		/// </summary>
		static Program() {

		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] arguments) {
			try {
				using(var stream = typeof(Program).Assembly.GetManifestResourceStream(DroidExplorer.Resources.Strings.Log4NetDroidExplorer)) {
					XmlConfigurator.Configure(stream);
				}
				var errorMessages = new List<string>();

				var args = new Arguments(arguments);
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
				Application.ThreadException +=
					(sender, e) => DroidExplorer.Core.Logger.LogError(typeof(Program), e.Exception.Message, e.Exception);

				// show the splash dialog
				Program.SplashManager.ShowSplashDialog(StartUpStepCount);

				// load settings
				Logger.LogDebug ( typeof ( Program ), "Loading Droid Explorer Settings" );
				Program.SplashManager.SplashDialog.SetStepText("Loading Settings...");
				CommandRunner.Settings = Settings.Instance;
				Settings.Instance.SystemSettings = RegistrySettings.Instance;
				Program.SplashManager.SplashDialog.IncrementLoadStep(1);


				// cleanup adb? should this be done?
				Application.ApplicationExit += delegate(object sender, EventArgs e) {
					//CommandRunner.Instance.AdbProcessCleanUp ( );
				};

				// locate sdk tools
				Logger.LogDebug ( typeof ( Program ), "Locating SDK Path" );
				Program.SplashManager.SplashDialog.SetStepText(DroidExplorer.Resources.Strings.SplashLocatingSdk);
				var sdkPath = Settings.Instance.SystemSettings.SdkPath;
				Logger.LogDebug ( typeof ( Program ), "SDK Path Set to {0}", sdkPath );

				if ( string.IsNullOrEmpty(sdkPath)) {
					throw new ApplicationException("Unable to locate the android SDK tools, please reinstall the application.");
				}
				Program.SplashManager.SplashDialog.IncrementLoadStep(1);

				// verify the sdk tools
				Logger.LogDebug ( typeof ( Program ), "Verifying SDK Tools" );
				Program.SplashManager.SplashDialog.SetStepText("Verifying SDK Tools");

				// verify the sdk tools. 
				var valid = FolderManagement.VerifyAndroidSdkTools ();
				if(!valid){
					// if not valid - lets ask the user where it is.
					Program.SplashManager.SplashDialog.HideExt();
					Logger.LogWarn( typeof ( Program ), "Unable to verify the SDK tools" );

					var browser = new FolderBrowserDialog();
					browser.Description = "Unable to locate SDK tools path. Please select the location where you have the Android SDK installed.";
					browser.ShowNewFolderButton = false;
					var dResult = DialogResult.OK;
					do {
						dResult = browser.ShowDialog();
						if(dResult == DialogResult.OK) {
							Settings.Instance.SystemSettings.SdkPath = browser.SelectedPath;
							valid = FolderManagement.VerifyAndroidSdkTools ();
						}
						// keep asking until the user selects a valid location, or they cancel.
					} while(!valid && dResult == DialogResult.OK);

					if(!valid) {
						// they canceled. we are out of here.
						throw new ApplicationException(DroidExplorer.Resources.Strings.SdkPathNotFoundMessage);
					} 

					// bring it back.
					Program.SplashManager.SplashDialog.AsyncShow();
				}

				
				Program.SplashManager.SplashDialog.IncrementLoadStep(1);

				// set the sdk path
				CommandRunner.Instance.SdkPath = sdkPath;
				Program.SplashManager.SplashDialog.SetStepText(DroidExplorer.Resources.Strings.SplashStartAdb);
				try {
					// start the adb server
					Logger.LogDebug ( typeof ( Program ), "Starting ADB Server" );

					CommandRunner.Instance.StartServer();
					Program.SplashManager.SplashDialog.IncrementLoadStep(1);
				} catch(AdbRootException arex) {
					Logger.LogWarn ( typeof ( Program ), arex.Message );
				}
				// get the attached devices
				Program.SplashManager.SplashDialog.SetStepText(DroidExplorer.Resources.Strings.SplashGetDevices);
				// BUG: 15695
				// fixed this. It was because adb root was disconnecting the device and restarting it.
				// adb root is now called when you connect to a device. Although, it can cause the device to 
				// not be found... another bug...
				Logger.LogDebug ( typeof ( Program ), "Getting connected devices" );
				var devices = CommandRunner.Instance.GetDevices().ToList();
				Program.SplashManager.SplashDialog.IncrementLoadStep(1);

				new Thread(() => {
					// process the devices and send them off to the cloud
					Logger.LogDebug ( typeof ( Program ), "Registering anonymous statistics for device types" );
					CloudStatistics.Instance.RegisterModels(devices);
				}).Start();
				// we will increment even though the process is on a new thread.
				Program.SplashManager.SplashDialog.IncrementLoadStep(1);

				// pass in the initial path?
				// TODO: Not yet implemented.
				//var initialPath = "/";
				//if ( args.Contains ( "p", "path" ) && !string.IsNullOrWhiteSpace ( args["p", "path"] ) ) {
				//	initialPath = args["p", "path"];
				//}


				// are we attaching to a specific device?
				if ( args.Contains("d", "device")) {
					CommandRunner.Instance.DefaultDevice = args["d", "device"];
					Logger.LogDebug( typeof ( Program ), DroidExplorer.Resources.Strings.SplashConnectingDevice, CommandRunner.Instance.DefaultDevice);
					Application.Run(new MainForm(  ) );
				} else {
					// get the attached devices
					string selectedDevice = null;
					if(devices.Count() != 1) {
						// we have 0 or > 1 so we need to display the "selection dialog"
						var dsf = new GenericDeviceSelectionForm();
						Program.SplashManager.SplashDialog.HideExt();
						if(dsf.ShowDialog(null) == DialogResult.OK) {
							CommandRunner.Instance.DefaultDevice = dsf.SelectedDevice;
							Logger.LogDebug ( typeof ( Program ), DroidExplorer.Resources.Strings.SplashConnectingDevice, CommandRunner.Instance.DefaultDevice );
							selectedDevice = dsf.SelectedDevice;
						} else {
							Program.SplashManager.CloseSplashDialog();
							return;
						}
						Program.SplashManager.SplashDialog.AsyncShow();
					} else {
						selectedDevice = devices[0].SerialNumber;
						CommandRunner.Instance.DefaultDevice = selectedDevice;
					}

					// try to run adb as root:
					try {
						Program.SplashManager.SplashDialog.SetStepText(DroidExplorer.Resources.Strings.SplashRunAdbAsRoot);
						Logger.LogDebug ( typeof ( Program ), DroidExplorer.Resources.Strings.SplashRunAdbAsRoot );
						CommandRunner.Instance.StartAdbAsRoot(selectedDevice);
						// sleep a half second to let it start up.
						Thread.Sleep ( 500 );
					} catch(AdbRootException are) {

						Logger.LogDebug ( typeof ( Program ), "Unable to launch ADB as root" );

						// if we are unable to run as adb root
						if ( Settings.Instance.WarnWhenNoAdbRoot) {
							SplashManager.SplashDialog.Invoke((Action)(() => {
								SplashManager.SplashDialog.Visible = false;
							}));
							//NoAdbRootCommands	Continue|Show more information|Exit	

							var result = TaskDialog.ShowCommandBox(
								DroidExplorer.Resources.Strings.NoAdbRootTitle,
								are.Message,
								DroidExplorer.Resources.Strings.NoAdbRootMessage,
								string.Empty,
								string.Empty,
								DroidExplorer.Resources.Strings.NoAdbRootCheckboxText,
								DroidExplorer.Resources.Strings.NoAdbRootCommands,
							false, SysIcons.Warning, SysIcons.Warning);
							switch(TaskDialog.CommandButtonResult) {
								case 0: // Continue
									// continue
									if(TaskDialog.VerificationChecked) {
										// do not warn again.
										Settings.Instance.WarnWhenNoAdbRoot = false;
									}
									break;
								case 1: // continue and get more info : http://android.stackexchange.com/a/96066/1951
									// android enthusiasts
									CommandRunner.Instance.LaunchProcessWindow(DroidExplorer.Resources.Strings.AdbRootQAUrl, string.Empty, true);
									if(TaskDialog.VerificationChecked) {
										// do not warn again.
										Settings.Instance.WarnWhenNoAdbRoot = false;
									}
									break;
								case 2: // link to adbd insecure by chainfire on google play 
									CommandRunner.Instance.LaunchProcessWindow(DroidExplorer.Resources.Strings.AdbdInsecureAppUrl, string.Empty, true);
									Application.Exit();
									return;
								default: // 3
									Application.Exit();
									return;
							}
							SplashManager.SplashDialog.Invoke((Action)(() => {
								SplashManager.SplashDialog.Visible = true;
							}));
						}
					}
					// increment for adb root
					Program.SplashManager.SplashDialog.IncrementLoadStep(1);

					// minor hackory for getting the device for cloud stats
					//var d = new Managed.Adb.Device(selectedDevice, Managed.Adb.DeviceState.Online, "unknown", "unknown", "unknown");
					var d = Managed.Adb.AdbHelper.Instance.GetDevices ( Managed.Adb.AndroidDebugBridge.SocketAddress ).Single ( m => m.SerialNumber == selectedDevice );

					// get the device properties for registration
					//foreach ( var i in CommandRunner.Instance.GetProperties(selectedDevice)) {
					//	d.Properties.Add(i.Key, i.Value);
					//}
					if(!Settings.Instance.SystemSettings.RecordDeviceInformationToCloud) {
						Program.SplashManager.SplashDialog.SetStepText(
							String.Format(DroidExplorer.Resources.Strings.SplashRegisterDevice,
								KnownDeviceManager.Instance.GetDeviceFriendlyName(selectedDevice)
							)
						);
						// register the device
						CloudStatistics.Instance.RegisterDevice(d, devices.Single(x => x.SerialNumber == d.SerialNumber));
						Program.SplashManager.SplashDialog.IncrementLoadStep(1);
					} else {
						// we have to increment but nothing else here
						Program.SplashManager.SplashDialog.IncrementLoadStep(1);
					}

					Logger.LogDebug ( typeof ( Program ), "Launching Main UI" );
					// connect to device
					Program.SplashManager.SplashDialog.SetStepText(string.Format(DroidExplorer.Resources.Strings.SplashConnectingDevice, KnownDeviceManager.Instance.GetDeviceFriendlyName(CommandRunner.Instance.DefaultDevice)));

					Application.Run(new MainForm( ) );
				}
			} catch(Exception ex) {
				var result = TaskDialog.ShowCommandBox(DroidExplorer.Resources.Strings.ErrorUncaughtTitle, ex.Message,
					DroidExplorer.Resources.Strings.ErrorUncaughtMessage, ex.ToString(),
					string.Empty, string.Empty, DroidExplorer.Resources.Strings.ErrorUncaughtCommands,
					false, SysIcons.Error, SysIcons.Error);
				switch(TaskDialog.CommandButtonResult) {
					case 1:
						// continue
						break;
					case 2:
						// android enthusiasts
						CommandRunner.Instance.LaunchProcessWindow(DroidExplorer.Resources.Strings.SupportUrl, string.Empty, true);
						break;
					case 3:
						// report bug
						CommandRunner.Instance.LaunchProcessWindow(DroidExplorer.Resources.Strings.IssueTrackerCreateUrl, string.Empty, true);
						break;
					default: // 0
						Application.Restart();
						break;
				}
			}
		}

		/// <summary>
		/// Gets the splash manager.
		/// </summary>
		/// <value>The splash manager.</value>
		public static SplashManager SplashManager {
			get {
				return SplashManager.Instance;
			}
		}


		/// <summary>
		/// Gets the loaded plugins.
		/// </summary>
		/// <value>The loaded plugins.</value>
		public static List<IPlugin> LoadedPlugins {
			get { return _loadedPlugins ?? (_loadedPlugins = new List<IPlugin>()); }
		}

		/// <summary>
		/// Gets the package manager.
		/// </summary>
		/// <value>The package manager.</value>
		public static PackageManagerForm PackageManager {
			get {
				if(_packageManager == null || _packageManager.IsDisposed) {
					_packageManager = new PackageManagerForm();
				}
				return _packageManager;
			}
		}

		/// <summary>
		/// Gets the system icons.
		/// </summary>
		/// <value>The system icons.</value>
		public static Dictionary<string, int> SystemIcons {
			get {
				return SystemImageListHost.Instance.SystemIcons;
			}
		}
		/// <summary>
		/// Gets the local path.
		/// </summary>
		/// <value>The local path.</value>
		public static string LocalPath {
			get {
				return Path.GetDirectoryName(typeof(Program).Assembly.Location);
			}
		}
	}
}