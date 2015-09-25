using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Diagnostics;
using log4net;
using System.Reflection;
using log4net.Repository;
using Camalot.Common.Extensions;

namespace DroidExplorer.Core {


	public enum ArchitectureTypes {
		x86,
		x64,
		ia64,
		msil
	}

	public static class Logger {

		[Flags]
		public enum Levels {
			None = 0,
			Debug = 1,
			Info = 2,
			Warn = 4,
			Error = 8,
			All = Debug | Info | Warn | Error,
		}

		static Logger() {
			Level = Levels.All;
			LogManager.GetRepository().Threshold = log4net.Core.Level.All;
			Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		}


		public static ILog Log { get; set; }
		private static Levels _level = Levels.All;

		public static Levels Level {
			get {
				return _level;
			}
			set {
				_level = value;
				/*ILoggerRepository repo = LogManager.GetRepository ( );
				if ( repo != null ) {
					if ( ( Level & Levels.None ) != 0 ) {
						repo.Threshold = log4net.Core.Level.Off;
					}
					if ( ( Level & Levels.Debug ) != 0 ) {
						repo.Threshold = log4net.Core.Level.Debug;
					}
					if ( ( Level & Levels.Info ) != 0 ) {
						repo.Threshold = log4net.Core.Level.Info;
					}
					if ( ( Level & Levels.Warn ) != 0 ) {
						repo.Threshold = log4net.Core.Level.Warn;
					}
					if ( ( Level & Levels.Error ) != 0 ) {
						repo.Threshold = log4net.Core.Level.Error;
					}
					if ( ( Level & Levels.All ) != 0 ) {
						repo.Threshold = log4net.Core.Level.All;
					}
				}*/
			}
		}
		public static ConsoleColor Color { get; private set; }

		#region Write
		public static void LogWrite(this object obj,  Levels level, string message, params object[] args) {
			LogWrite(obj.GetType(), level, message, args);
		}

		public static void LogWrite(Type type, Levels level, string message, params object[] args) {
			// none = silent; all = verbose
			var lName = level == Levels.None ? "S/" : "V/";
			if(level != Levels.All && level != Levels.None) {
				lName = "{0}/".With(level.ToString().Substring(0, 1));
			}

			Console.WriteLine("{1}[{0}] {2}".With(CultureInfo.InvariantCulture, type.Name, lName, message.With(CultureInfo.InvariantCulture, args)));
		}
		#endregion

		#region Debug
		public static void LogDebug(this object obj, string message) {
			LogDebug(obj, message, string.Empty);
		}

		public static void LogDebug(this object obj, string message, params object[] args) {
			LogDebug(obj.GetType(), message, args);
		}

		public static void LogDebug(Type type, string message, params object[] args) {
			if((Level & Levels.Debug) != 0) {
				LogWrite(type, Levels.Debug, message, args);
			}
			Log.DebugFormat(message, args);
		}

		public static void LogDebug(Type type, string message) {
			LogDebug(type, message, string.Empty);
		}
		#endregion

		#region Error
		public static void LogError(this object obj, string message) {
			LogError(obj.GetType(), message);
		}

		public static void LogError(Type type, string message) {
			if((Level & Levels.Error) != 0) {
				LogWrite(type, Levels.Error, message.Replace("{", "{{").Replace("}", "}}"), string.Empty);
			}
			Log.Error(message);
		}

		public static void LogError(Type type, string message, Exception ex) {
			LogError(type, string.Format(CultureInfo.InvariantCulture, "{0} - {1}", message, ex.ToString()));
		}

		public static void LogError(this object obj, string message, Exception ex) {
			LogError(obj, string.Format(CultureInfo.InvariantCulture, "{0} - {1}", message, ex.ToString()));
		}

		#endregion

		#region Warning
		public static void LogWarn(this object obj, string message) {
			LogWarn(obj, message, string.Empty);
		}

		public static void LogWarn(this object obj, string message, params object[] args) {
			LogWarn(obj.GetType(), message, args);
		}

		public static void LogWarn(Type type, string message, params object[] args) {
			if((Level & Levels.Warn) != 0) {
				LogWrite(type, Levels.Warn, message, args);
			}
			Log.WarnFormat(message, args);
		}

		public static void LogWarn(Type type, string message) {
			LogWarn(type, message, string.Empty);
		}

		#endregion

		#region Info
		public static void LogInfo(this object obj, string message) {
			LogInfo(obj, message, string.Empty);
		}

		public static void LogInfo(this object obj, string message, params object[] args) {
			LogInfo(obj.GetType(), message, args);
		}

		public static void LogInfo(Type type, string message, params object[] args) {
			if((Level & Levels.Info) != 0) {
				LogWrite(type, Levels.Info, message, args);
			}
			Log.InfoFormat(message, args);
		}

		public static void LogInfo(Type type, string message) {
			LogInfo(type, message, string.Empty);
		}

		#endregion


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
