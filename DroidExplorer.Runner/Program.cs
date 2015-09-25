using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using DroidExplorer.Core;
using DroidExplorer.Core.Plugins;
using System.Globalization;
using log4net.Config;
using System.IO;
using DroidExplorer.Configuration;
using DroidExplorer.UI;
using DroidExplorer.Core.UI;

namespace DroidExplorer.Runner {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] arguments) {
			try {
				using(Stream stream = typeof(Program).Assembly.GetManifestResourceStream("DroidExplorer.Runner.DroidExplorer.Runner.log4net")) {
					XmlConfigurator.Configure(stream);
				}
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);

				CommandRunner.Settings = Settings.Instance;
				Settings.Instance.SystemSettings = RegistrySettings.Instance;

				string sdkPath = Settings.Instance.SystemSettings.SdkPath;
				if(String.IsNullOrEmpty(sdkPath)) {
					throw new ApplicationException("SDK was not successfully set during install.");
				}
				CommandRunner.Instance.SdkPath = sdkPath;
				CommandRunner.Instance.StartServer();

				if(!FolderManagement.VerifyAndroidSdkTools ()) {
					throw new ApplicationException("Unable to locate the android SDK tools, please reinstall the application.");
				}


				Logger.LogDebug(typeof( RunnerPluginHost ), "Launching Runner with args: {0}", string.Join(" ", arguments));
				Arguments args = new Arguments(arguments);

				// check if we need to know about a specific device
				var defDevice = String.Empty;

				if(!args.Contains("nodevice")) {
					Logger.LogDebug(typeof( RunnerPluginHost ), "Need Device");
					var devices = CommandRunner.Instance.GetDevices().Select(d => d.SerialNumber).ToList();
					if(devices.Count != 1) {
						GenericDeviceSelectionForm selectDevice = new GenericDeviceSelectionForm();
						if(selectDevice.ShowDialog() == DialogResult.OK) {
							defDevice = selectDevice.SelectedDevice;
							CommandRunner.Instance.DefaultDevice = defDevice;
							Logger.LogDebug ( "Setting device to '{0}'", defDevice );
						} else {
							return;
						}
					} else {
						defDevice = devices[0];
						Logger.LogDebug ( "Setting device to '{0}'", defDevice );
					}
				}
				var phost = new RunnerPluginHost(defDevice);

				string typeAssembly = string.Empty;
				if(!args.Contains("type", "t")) {
					SelectPluginDialog spd = new SelectPluginDialog();
					if(spd.ShowDialog() == DialogResult.OK) {
						typeAssembly = spd.SelectedPlugin.Replace(" ", string.Empty);
						arguments = spd.CommandlineArguments;
					} else {
						return;
					}
				} else {
					typeAssembly = args["type", "t"];
				}

				Logger.LogDebug(typeof( RunnerPluginHost ), "Launching Plugin {0}", typeAssembly);
				var plugin = DroidExplorer.Configuration.Settings.Instance.PluginSettings.GetPlugins(phost)
					.SingleOrDefault(m => String.Compare(typeAssembly, m.Id, true) == 0);
				if(plugin != null) {
					try {
						Logger.LogDebug ( typeof ( RunnerPluginHost ), "Initializing plugin: {0}", plugin.Name );
						plugin.Initialize ( phost );
						Logger.LogDebug(typeof( RunnerPluginHost ), "running plugin: {0} with args: {1}", plugin.Name, String.Join(" ", arguments));
						plugin.Execute(phost, arguments);
					} catch(Exception ex) {
						plugin.LogError(ex.Message, ex);
					}
				} else {
					Logger.LogDebug(typeof( RunnerPluginHost ), "plugin '{0}' not found", typeAssembly);
					throw new ApplicationException("Error executing plugin, check the name to ensure it exists");
				}
			} catch(Exception ex) {
				Logger.LogError(typeof( RunnerPluginHost ), ex.Message, ex);
			}
		}

	}
}
