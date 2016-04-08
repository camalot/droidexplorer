using System;
using System.Collections.Generic;
using System.Text;

namespace DroidExplorer.Core.IO {
	/// <summary>
	/// 
	/// </summary>
	/// <seealso cref="System.IFormatProvider" />
	/// <seealso cref="System.ICustomFormatter" />
	public class FileSizeFormatProvider : IFormatProvider, ICustomFormatter {
		/// <summary>
		/// Returns an object that provides formatting services for the specified type.
		/// </summary>
		/// <param name="formatType">An object that specifies the type of format object to return.</param>
		/// <returns>
		/// An instance of the object specified by <paramref name="formatType" />, if the <see cref="T:System.IFormatProvider" /> implementation can supply that type of object; otherwise, null.
		/// </returns>
		public object GetFormat ( Type formatType ) {
			if ( formatType == typeof ( ICustomFormatter ) )
				return this; return null;
		}
		private const string fileSizeFormat = "fs";
		private const Decimal OneKiloByte = 1024M;
		private const Decimal OneMegaByte = OneKiloByte * 1024M;
		private const Decimal OneGigaByte = OneMegaByte * 1024M;
		/// <summary>
		/// Converts the value of a specified object to an equivalent string representation using specified format and culture-specific formatting information.
		/// </summary>
		/// <param name="format">A format string containing formatting specifications.</param>
		/// <param name="arg">An object to format.</param>
		/// <param name="formatProvider">An object that supplies format information about the current instance.</param>
		/// <returns>
		/// The string representation of the value of <paramref name="arg" />, formatted as specified by <paramref name="format" /> and <paramref name="formatProvider" />.
		/// </returns>
		public string Format ( string format, object arg, IFormatProvider formatProvider ) {
			if ( format == null || !format.StartsWith ( fileSizeFormat ) ) {
				return defaultFormat ( format, arg, formatProvider );
			}
			if ( arg is string ) {
				return defaultFormat ( format, arg, formatProvider );
			}
			Decimal size;
			try {
				size = Convert.ToDecimal ( arg );
			} catch ( InvalidCastException ) {
				return defaultFormat ( format, arg, formatProvider );
			}
			string suffix;
			if ( size > OneGigaByte ) {
				size /= OneGigaByte; suffix = "GB";
			} else if ( size > OneMegaByte ) {
				size /= OneMegaByte; suffix = "MB";
			} else if ( size > OneKiloByte ) {
				size /= OneKiloByte; suffix = "KB";
			} else { suffix = "B"; }
			string precision = format.Substring ( 2 );
			if ( String.IsNullOrEmpty ( precision ) )
				precision = "2";
			return String.Format ( "{0:N" + precision + "}{1}", size, suffix );
		}

		/// <summary>
		/// Defaults the format.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="arg">The argument.</param>
		/// <param name="formatProvider">The format provider.</param>
		/// <returns></returns>
		private static string defaultFormat ( string format, object arg, IFormatProvider formatProvider ) {
			IFormattable formattableArg = arg as IFormattable;
			if ( formattableArg != null ) {
				return formattableArg.ToString ( format, formatProvider );
			} return arg.ToString ( );
		}
	}
}
