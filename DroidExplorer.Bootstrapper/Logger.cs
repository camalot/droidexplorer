using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;
using System.Globalization;
using log4net.Core;

namespace DroidExplorer.Bootstrapper {
	/// <summary>
	/// 
	/// </summary>
	public static class Logger {

		/// <summary>
		/// Initializes the <see cref="Logger"/> class.
		/// </summary>
		static Logger ( ) {
			Level = Level.All;
			Log = LogManager.GetLogger ( MethodBase.GetCurrentMethod ( ).DeclaringType );
		}

		/// <summary>
		/// Gets or sets the log.
		/// </summary>
		/// <value>The log.</value>
		public static ILog Log { get; set; }


		public static Level Level {
			get { return LogManager.GetRepository ( ).Threshold; }
			set { LogManager.GetRepository ( ).Threshold = value; }
		}

		#region Fatal
		/// <summary>
		/// Logs the fatal message.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="message">The message.</param>
		public static void LogFatal ( Type type, string message ) {
			Log = LogManager.GetLogger ( type );
			Log.Fatal ( message );
		}

		/// <summary>
		/// Logs the fatal message.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="message">The message.</param>
		/// <param name="ex">The ex.</param>
		public static void LogFatal ( Type type, string message, Exception ex ) {
			Log = LogManager.GetLogger ( type );
			Log.Fatal ( message, ex );
		}

		/// <summary>
		/// Logs the fatal message.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public static void LogFatal ( Type type, string format, params object[] args ) {
			Log = LogManager.GetLogger ( type );
			Log.FatalFormat ( CultureInfo.CurrentCulture, format, args );
		}

		/// <summary>
		/// Logs the fatal message.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="provider">The provider.</param>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public static void LogFatal ( Type type, IFormatProvider provider, string format, params object[] args ) {
			Log = LogManager.GetLogger ( type );
			Log.FatalFormat ( provider, format, args );
		}

		/// <summary>
		/// Logs the fatal message.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="message">The message.</param>
		public static void LogFatal ( this object obj, string message ) {
			LogFatal ( obj.GetType ( ), message );
		}

		/// <summary>
		/// Logs the fatal message.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="message">The message.</param>
		/// <param name="ex">The ex.</param>
		public static void LogFatal ( this object obj, string message, Exception ex ) {
			LogFatal ( obj.GetType ( ), message, ex );
		}

		/// <summary>
		/// Logs the fatal message.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public static void LogFatal ( this object obj, string format, params object[] args ) {
			LogFatal ( obj, CultureInfo.CurrentCulture, format, args );
		}

		/// <summary>
		/// Logs the fatal message.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="provider">The provider.</param>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public static void LogFatal ( this object obj, IFormatProvider provider, string format, params object[] args ) {
			LogFatal ( obj.GetType ( ), provider, format, args );
		}
		#endregion

		#region Error
		/// <summary>
		/// Logs the error.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="message">The message.</param>
		public static void LogError ( Type type, string message ) {
			Log = LogManager.GetLogger ( type );
			Log.Error ( message );
		}

		/// <summary>
		/// Logs the error.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="message">The message.</param>
		/// <param name="ex">The ex.</param>
		public static void LogError ( Type type, string message, Exception ex ) {
			Log = LogManager.GetLogger ( type );
			Log.Error ( message, ex );
		}

		/// <summary>
		/// Logs the error.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public static void LogError ( Type type, string format, params object[] args ) {
			LogError ( type, CultureInfo.CurrentCulture, format, args );
		}

		/// <summary>
		/// Logs the error.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="provider">The provider.</param>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public static void LogError ( Type type, IFormatProvider provider, string format, params object[] args ) {
			Log = LogManager.GetLogger ( type );
			Log.ErrorFormat ( provider, format, args );
		}

		/// <summary>
		/// Logs the error message.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="message">The message.</param>
		public static void LogError ( this object obj, string message ) {
			LogError ( obj.GetType ( ), message );
		}

		/// <summary>
		/// Logs the error message.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="message">The message.</param>
		/// <param name="ex">The ex.</param>
		public static void LogError ( this object obj, string message, Exception ex ) {
			LogError ( obj.GetType ( ), message, ex );
		}

		/// <summary>
		/// Logs the error message.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public static void LogError ( this object obj, string format, params object[] args ) {
			LogError ( obj.GetType ( ), format, args );
		}

		/// <summary>
		/// Logs the error message.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="provider">The provider.</param>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public static void LogError ( this object obj, IFormatProvider provider, string format, params object[] args ) {
			LogError ( obj.GetType ( ), provider, format, args );
		}
		#endregion

		#region Warning

		/// <summary>
		/// Logs the warning.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="message">The message.</param>
		public static void LogWarning ( Type type, string message ) {
			Log = LogManager.GetLogger ( type );
			Log.Warn ( message );
		}

		/// <summary>
		/// Logs the warning.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="message">The message.</param>
		/// <param name="ex">The ex.</param>
		public static void LogWarning ( Type type, string message, Exception ex ) {
			Log = LogManager.GetLogger ( type );
			Log.Warn ( message, ex );
		}

		/// <summary>
		/// Logs the warning.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="fromat">The fromat.</param>
		/// <param name="args">The args.</param>
		public static void LogWarning ( Type type, string format, params object[] args ) {
			LogWarning ( type, CultureInfo.CurrentCulture, format, args );
		}

		/// <summary>
		/// Logs the warning.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="provider">The provider.</param>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public static void LogWarning ( Type type, IFormatProvider provider, string format, params object[] args ) {
			Log = LogManager.GetLogger ( type );
			Log.WarnFormat ( provider, format, args );
		}

		/// <summary>
		/// Logs the warning message.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="message">The message.</param>
		public static void LogWarning ( this object obj, string message ) {
			LogWarning ( obj.GetType ( ), message );
		}

		/// <summary>
		/// Logs the warning message.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="message">The message.</param>
		/// <param name="ex">The ex.</param>
		public static void LogWarning ( this object obj, string message, Exception ex ) {
			LogWarning ( obj.GetType ( ), message, ex );
		}

		/// <summary>
		/// Logs the warning message.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public static void LogWarning ( this object obj, string format, params object[] args ) {
			LogWarning ( obj.GetType ( ), format, args );
		}

		/// <summary>
		/// Logs the warning message.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="provider">The provider.</param>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public static void LogWarning ( this object obj, IFormatProvider provider, string format, params object[] args ) {
			LogWarning ( obj.GetType ( ), provider, format, args );
		}
		#endregion

		#region Info
		/// <summary>
		/// Logs the info.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="message">The message.</param>
		public static void LogInfo ( Type type, string message ) {
			Log = LogManager.GetLogger ( type );
			Log.Info ( message );
		}

		/// <summary>
		/// Logs the info.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="message">The message.</param>
		/// <param name="ex">The ex.</param>
		public static void LogInfo ( Type type, string message, Exception ex ) {
			Log = LogManager.GetLogger ( type );
			Log.Info ( message, ex );
		}

		/// <summary>
		/// Logs the info.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public static void LogInfo ( Type type, string format, params object[] args ) {
			LogInfo ( type, CultureInfo.CurrentCulture, format, args );
		}

		/// <summary>
		/// Logs the info.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="provider">The provider.</param>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public static void LogInfo ( Type type, IFormatProvider provider, string format, params object[] args ) {
			Log = LogManager.GetLogger ( type );
			Log.InfoFormat ( provider, format, args );
		}

		/// <summary>
		/// Logs the info message.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="message">The message.</param>
		public static void LogInfo ( this object obj, string message ) {
			LogInfo ( obj.GetType ( ), message );
		}

		/// <summary>
		/// Logs the info message.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="message">The message.</param>
		/// <param name="ex">The ex.</param>
		public static void LogInfo ( this object obj, string message, Exception ex ) {
			LogInfo ( obj.GetType ( ), message, ex );
		}

		/// <summary>
		/// Logs the info message.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public static void LogInfo ( this object obj, string format, params object[] args ) {
			LogInfo ( obj.GetType ( ), format, args );
		}

		/// <summary>
		/// Logs the info message.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="provider">The provider.</param>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public static void LogInfo ( this object obj, IFormatProvider provider, string format, params object[] args ) {
			LogInfo ( obj.GetType ( ), provider, format, args );
		}
		#endregion

		#region Debug
		/// <summary>
		/// Logs the debug.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="message">The message.</param>
		public static void LogDebug ( Type type, string message ) {
			Log = LogManager.GetLogger ( type );
			Log.Debug ( message );
		}

		/// <summary>
		/// Logs the debug.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="message">The message.</param>
		/// <param name="ex">The ex.</param>
		public static void LogDebug ( Type type, string message, Exception ex ) {
			Log = LogManager.GetLogger ( type );
			Log.Debug ( message, ex );
		}

		public static void LogDebug ( Type type, string format, params object[] args ) {
			LogDebug ( type, CultureInfo.CurrentCulture, format, args );
		}

		/// <summary>
		/// Logs the debug.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="provider">The provider.</param>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public static void LogDebug ( Type type, IFormatProvider provider, string format, params object[] args ) {
			Log = LogManager.GetLogger ( type );
			Log.DebugFormat ( provider, format, args );
		}

		/// <summary>
		/// Logs the debug message.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="message">The message.</param>
		public static void LogDebug ( this object obj, string message ) {
			LogDebug ( obj.GetType ( ), message );
		}

		/// <summary>
		/// Logs the debug message.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="message">The message.</param>
		/// <param name="ex">The ex.</param>
		public static void LogDebug ( this object obj, string message, Exception ex ) {
			LogDebug ( obj.GetType ( ), message, ex );
		}

		/// <summary>
		/// Logs the debug message.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public static void LogDebug ( this object obj, string format, params object[] args ) {
			LogDebug ( obj.GetType ( ), format, args );
		}

		/// <summary>
		/// Logs the debug message.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="provider">The provider.</param>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public static void LogDebug ( this object obj, IFormatProvider provider, string format, params object[] args ) {
			LogDebug ( obj.GetType ( ), provider, format, args );
		}
		#endregion
	}
}
