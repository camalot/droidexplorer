using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camalot.Common.Extensions;

namespace DroidExplorer.Core.UI.CConsole {
	public class ConsoleWriter : StringWriter {
		/// <summary>
		/// Initializes a new instance of the <see cref="ConsoleWriter" /> class.
		/// </summary>
		/// <param name="textBox">The text box.</param>
		public ConsoleWriter(ref ConsoleControl console, Logger.Levels level) {
			Console = console;
			Level = level;
		}

		private ConsoleControl Console { get; set; }
		private Logger.Levels Level { get; set; }
		#region WriteLine
		/// <summary>
		/// Writes a string followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="value">The string to write. If <paramref name="value" /> is null, only the line termination characters are written.</param>
		public override void WriteLine(string value) {
			Write(string.Format("{0}{1}", value, Environment.NewLine));
		}

		/// <summary>
		/// Writes the text representation of a Boolean followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="value">The Boolean to write.</param>
		public override void WriteLine(bool value) {
			WriteLine(string.Format("{0}", value));
		}

		/// <summary>
		/// Writes a character followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="value">The character to write to the text stream.</param>
		public override void WriteLine(char value) {
			WriteLine(string.Format("{0}", value));
		}

		/// <summary>
		/// Writes an array of characters followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="buffer">The character array from which data is read.</param>
		public override void WriteLine(char[] buffer) {
			WriteLine(string.Format("{0}", buffer));
		}

		/// <summary>
		/// Writes a subarray of characters followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="buffer">The character array from which data is read.</param>
		/// <param name="index">The index into <paramref name="buffer" /> at which to begin reading.</param>
		/// <param name="count">The maximum number of characters to write.</param>
		public override void WriteLine(char[] buffer, int index, int count) {
			WriteLine(new string(buffer, index, count));
		}

		/// <summary>
		/// Writes the text representation of a decimal value followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="value">The decimal value to write.</param>
		public override void WriteLine(decimal value) {
			WriteLine(string.Format("{0}", value));
		}

		/// <summary>
		/// Writes the text representation of a 8-byte floating-point value followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="value">The 8-byte floating-point value to write.</param>
		public override void WriteLine(double value) {
			WriteLine(string.Format("{0}", value));
		}

		/// <summary>
		/// Writes the text representation of a 4-byte floating-point value followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="value">The 4-byte floating-point value to write.</param>
		public override void WriteLine(float value) {
			WriteLine(string.Format("{0}", value));
		}

		/// <summary>
		/// Writes the text representation of a 4-byte signed integer followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="value">The 4-byte signed integer to write.</param>
		public override void WriteLine(int value) {
			WriteLine(string.Format("{0}", value));
		}

		/// <summary>
		/// Writes the text representation of an 8-byte signed integer followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="value">The 8-byte signed integer to write.</param>
		public override void WriteLine(long value) {
			WriteLine(string.Format("{0}", value));
		}

		/// <summary>
		/// Writes the text representation of an object by calling ToString on this object, followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="value">The object to write. If <paramref name="value" /> is null, only the line termination characters are written.</param>
		public override void WriteLine(object value) {
			WriteLine(string.Format("{0}", value));
		}

		/// <summary>
		/// Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.
		/// </summary>
		/// <param name="format">The formatted string.</param>
		/// <param name="arg0">The object to write into the formatted string.</param>
		public override void WriteLine(string format, object arg0) {
			WriteLine(string.Format(format, arg0));
		}

		/// <summary>
		/// Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.
		/// </summary>
		/// <param name="format">The formatting string.</param>
		/// <param name="arg0">The object to write into the format string.</param>
		/// <param name="arg1">The object to write into the format string.</param>
		public override void WriteLine(string format, object arg0, object arg1) {
			WriteLine(string.Format(format, arg0, arg1));
		}

		/// <summary>
		/// Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.
		/// </summary>
		/// <param name="format">The formatting string.</param>
		/// <param name="arg0">The object to write into the format string.</param>
		/// <param name="arg1">The object to write into the format string.</param>
		/// <param name="arg2">The object to write into the format string.</param>
		public override void WriteLine(string format, object arg0, object arg1, object arg2) {
			WriteLine(string.Format(format, arg0, arg1, arg2));
		}

		/// <summary>
		/// Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.
		/// </summary>
		/// <param name="format">The formatting string.</param>
		/// <param name="arg">The object array to write into format string.</param>
		public override void WriteLine(string format, params object[] arg) {
			WriteLine(string.Format(format, arg));
		}

		/// <summary>
		/// Writes the text representation of a 4-byte unsigned integer followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="value">The 4-byte unsigned integer to write.</param>
		public override void WriteLine(uint value) {
			WriteLine(string.Format("{0}", value));
		}

		/// <summary>
		/// Writes the text representation of an 8-byte unsigned integer followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="value">The 8-byte unsigned integer to write.</param>
		public override void WriteLine(ulong value) {
			WriteLine(string.Format("{0}", value));
		}
		#endregion

		#region Write

		/// <summary>
		/// Writes a string to this instance of the StringWriter.
		/// </summary>
		/// <param name="value">The string to write.</param>
		public override void Write(string value) {

			if(value.IsMatch(@"^[VIDEWFS]\/")) {
				this.Console.WriteOutput(value);
			} else {
				// none = silent; all = verbose
				var lName = Level == Logger.Levels.None ? "S/" : "V/";
				if(Level != Logger.Levels.All && Level != Logger.Levels.None) {
					lName = "{0}/".With(Level.ToString().Substring(0, 1));
				}
				this.Console.WriteOutput(value);
			}
		}
		#endregion
	}
}
