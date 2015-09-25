using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Core;
using log4net.Config;
using DroidExplorer.Core.IO;
using System.Reflection;
using System.IO;
using log4net;
using DroidExplorer.Configuration;

namespace DroidExplorer.Service {
	class Program {
		static void Main ( string[ ] args ) {
			using ( Stream stream = typeof ( Program ).Assembly.GetManifestResourceStream ( "DroidExplorer.Service.DroidExplorer.Service.log4net" ) ) {
				XmlConfigurator.Configure ( stream );
			}

			DroidExplorer.Configuration.Settings.Instance.Loaded += delegate ( object sender, EventArgs e ) {
				SetSdkPath ( );
			};

			SetSdkPath ( );

			Arguments arguments = new Arguments ( args );
			Logger.Log = LogManager.GetLogger ( MethodBase.GetCurrentMethod ( ).DeclaringType );
			Logger.Level = Logger.Levels.Info | Logger.Levels.Error | Logger.Levels.Warn;

			if ( arguments.Contains( "console" ,"c" ) ) {
				DevicesMonitor dm = new DevicesMonitor ( );
				dm.Start ( );
				Console.WriteLine ( "Press {ENTER} to exit" );
				Console.ReadLine ( );
				dm.Stop ( );
			} else {
				try {
					// start the service.
					System.ServiceProcess.ServiceBase.Run ( new DeviceService ( ) );
				} catch ( Exception ex ) {
					Logger.LogError ( typeof ( Program ), ex.Message, ex );
					throw;
				}
			}
		}

		private static void SetSdkPath ( ) {
			CommandRunner.Settings = DroidExplorer.Configuration.Settings.Instance;
			Settings.Instance.SystemSettings = RegistrySettings.Instance;
			CommandRunner.Instance.SdkPath = DroidExplorer.Configuration.Settings.Instance.SystemSettings.SdkPath;
			Logger.LogInfo ( typeof ( Program ), "Sdk Path set to : {0}", CommandRunner.Instance.SdkPath );
			CommandRunner.Instance.StartServer ( );
		}

	}
}
