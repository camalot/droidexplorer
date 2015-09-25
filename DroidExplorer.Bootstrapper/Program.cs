using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DroidExplorer.Bootstrapper.UI;
using log4net.Config;
using System.IO;
using System.Security.Policy;
using System.Diagnostics;

namespace DroidExplorer.Bootstrapper {
	public enum InstallMode {
		Install,
		Uninstall,
		Update,
		SdkOnly
	}

	/// <summary>
	/// 
	/// </summary>
	public enum ArchitectureTypes {
		/// <summary>
		/// x86 platform
		/// </summary>
		x86,
		/// <summary>
		/// x64 platform
		/// </summary>
		x64,
		/// <summary>
		/// ia64 platform
		/// </summary>
		ia64,
		/// <summary>
		/// "Any CPU" platform
		/// </summary>
		msil
	}

	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main ( string[] arguments ) {
			using ( Stream stream = typeof ( Program ).Assembly.GetManifestResourceStream ( "DroidExplorer.Bootstrapper.DroidExplorer.Bootstrapper.log4net" ) ) {
				XmlConfigurator.Configure ( stream );
			}

			Arguments args = new Arguments ( arguments );

			if ( args.Contains ( "l", "log" ) ) {
				Logger.Level = log4net.Core.Level.All;
			} else {
				Logger.Level = log4net.Core.Level.All;
			}

			Logger.LogInfo ( typeof ( Program ), "System Info: {0} {1}", Environment.OSVersion.VersionString, Program.ApplicationArchitecture );
			Logger.LogDebug ( typeof ( Program ), "Starting setup with arguments: {0}", string.Join ( ",", arguments ) );

			/*Logger.LogDebug ( typeof ( Program ), "Checking for Minimum .net version" );
			if ( Requirements.HasDotNet35SP1OrGreater ( ) ) {
				Logger.LogDebug ( typeof ( Program ), "At least .net version 3.5 SP 1 is installed." );
			} else {
				Logger.LogFatal ( typeof ( Program ), "The requirement of .net framework 3.5 SP 1 was not found." );
				if ( MessageBox.Show ( "Microsoft .NET Framework 3.5 SP 1 was not found on this machine.\nClick 'OK' to navigate to the Microsoft website to download, or click 'Cancel' to just exit.\n\nSetup will not continue.", "Missing .NET Framework 3.5 SP1", MessageBoxButtons.OKCancel, MessageBoxIcon.Error ) == DialogResult.OK ) {
					Process proc = new Process ( );
					ProcessStartInfo psi = new ProcessStartInfo ( "http://www.microsoft.com/downloads/details.aspx?FamilyID=AB99342F-5D1A-413D-8319-81DA479AB0D7&displaylang=en" );
					proc.StartInfo = psi;
					proc.Start ( );
				}
				return;
			}*/


			if ( !Requirements.IsCorrectPlatform ( ) ) {
				Logger.LogFatal ( typeof ( Program ), "Installing on the wrong platform." );
				MessageBox.Show ( string.Format ( "You are attempting to install the {0} version of Droid Explorer which is not supported on this platform. Please download the correct installer for your platform ({1})", Program.ApplicationArchitecture.ToString ( ), Requirements.Is64Bit ( ) ? ArchitectureTypes.x64.ToString ( ) : ArchitectureTypes.x86.ToString ( ) ),
					"Can not install", MessageBoxButtons.OK, MessageBoxIcon.Error );
				return;
			}


			if ( args.Contains ( "u", "uninstall", "x", "remove" ) ) {
				Mode = InstallMode.Uninstall;
				Logger.LogDebug ( typeof ( Program ), "Mode set to uninstall" );
			} else {
				if ( args.Contains ( "s", "sdk", "sdkonly" ) ) {
					/*Mode = InstallMode.SdkOnly;
					Logger.LogDebug ( typeof ( Program ), "Mode set to SDK Only" );*/
					Logger.LogDebug ( typeof ( Program ), "Mode '{0}' is no longer supported." );
				} else {
					Mode = InstallMode.Install;
					Logger.LogDebug ( typeof ( Program ), "Mode set to install" );
				}
			}

			/*Application.SetUnhandledExceptionMode ( UnhandledExceptionMode.Automatic );
			Application.ThreadException += delegate ( object sender, System.Threading.ThreadExceptionEventArgs e ) {
				Logger.LogError ( typeof ( Program ), e.Exception.Message, e.Exception );
				if ( wizard != null && !wizard.IsDisposed ) {
					wizard.Error ( e.Exception );
				}
			};*/

			Application.EnableVisualStyles ( );
			Application.SetCompatibleTextRenderingDefault ( false );
			Application.Run ( new WizardForm ( ) );

		}


		/// <summary>
		/// Gets or sets the mode.
		/// </summary>
		/// <value>The mode.</value>
		public static InstallMode Mode { get; internal set; }

		/// <summary>
		/// Gets the processor architecture the application was built for.
		/// </summary>
		/// <value>The processor architecture the application was built for.</value>
		public static ArchitectureTypes ApplicationArchitecture {
			get {
#if PLATFORMX86
				return ArchitectureTypes.x86;
#elif PLATFORMX64
					return ArchitectureTypes.x64;
#elif PLATFORMIA64
					return ArchitectureTypes.ia64;
#else
					return ArchitectureTypes.msil;
#endif
			}
		}
	}
}
