using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DroidExplorer.Core.UI;
using DroidExplorer.Core;
using System.Drawing;
using System.Text.RegularExpressions;

namespace DroidExplorer.Plugins {
	/// <summary>
	/// 
	/// </summary>
  public class ConsoleWriter : StringWriter {
    private delegate void AppendDelegate ( string text );
    private delegate void SetTextColorDelegate ( ConsoleColor color );

		/// <summary>
		/// Initializes a new instance of the <see cref="ConsoleWriter" /> class.
		/// </summary>
		/// <param name="textBox">The text box.</param>
    public ConsoleWriter ( ref RichTextBox textBox ) {
      TextBox = textBox;
    }

    private RichTextBox TextBox { get; set; }

    #region WriteLine
		/// <summary>
		/// Writes a string followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="value">The string to write. If <paramref name="value" /> is null, only the line termination characters are written.</param>
    public override void WriteLine ( string value ) {
      Write ( string.Format ( "{0}{1}", value, Environment.NewLine ) );
    }

		/// <summary>
		/// Writes the text representation of a Boolean followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="value">The Boolean to write.</param>
    public override void WriteLine ( bool value ) {
      WriteLine ( string.Format ( "{0}", value ) );
    }

		/// <summary>
		/// Writes a character followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="value">The character to write to the text stream.</param>
    public override void WriteLine ( char value ) {
      WriteLine ( string.Format ( "{0}", value ) );
    }

		/// <summary>
		/// Writes an array of characters followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="buffer">The character array from which data is read.</param>
    public override void WriteLine ( char[ ] buffer ) {
      WriteLine ( string.Format ( "{0}", buffer ) );
    }

		/// <summary>
		/// Writes a subarray of characters followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="buffer">The character array from which data is read.</param>
		/// <param name="index">The index into <paramref name="buffer" /> at which to begin reading.</param>
		/// <param name="count">The maximum number of characters to write.</param>
    public override void WriteLine ( char[ ] buffer, int index, int count ) {
      WriteLine ( new string ( buffer, index, count ) );
    }

		/// <summary>
		/// Writes the text representation of a decimal value followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="value">The decimal value to write.</param>
    public override void WriteLine ( decimal value ) {
      WriteLine ( string.Format ( "{0}", value ) );
    }

		/// <summary>
		/// Writes the text representation of a 8-byte floating-point value followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="value">The 8-byte floating-point value to write.</param>
    public override void WriteLine ( double value ) {
      WriteLine ( string.Format ( "{0}", value ) );
    }

		/// <summary>
		/// Writes the text representation of a 4-byte floating-point value followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="value">The 4-byte floating-point value to write.</param>
    public override void WriteLine ( float value ) {
      WriteLine ( string.Format ( "{0}", value ) );
    }

		/// <summary>
		/// Writes the text representation of a 4-byte signed integer followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="value">The 4-byte signed integer to write.</param>
    public override void WriteLine ( int value ) {
      WriteLine ( string.Format ( "{0}", value ) );
    }

		/// <summary>
		/// Writes the text representation of an 8-byte signed integer followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="value">The 8-byte signed integer to write.</param>
    public override void WriteLine ( long value ) {
      WriteLine ( string.Format ( "{0}", value ) );
    }

		/// <summary>
		/// Writes the text representation of an object by calling ToString on this object, followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="value">The object to write. If <paramref name="value" /> is null, only the line termination characters are written.</param>
    public override void WriteLine ( object value ) {
      WriteLine ( string.Format ( "{0}", value ) );
    }

		/// <summary>
		/// Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.
		/// </summary>
		/// <param name="format">The formatted string.</param>
		/// <param name="arg0">The object to write into the formatted string.</param>
    public override void WriteLine ( string format, object arg0 ) {
      WriteLine ( string.Format ( format, arg0 ) );
    }

		/// <summary>
		/// Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.
		/// </summary>
		/// <param name="format">The formatting string.</param>
		/// <param name="arg0">The object to write into the format string.</param>
		/// <param name="arg1">The object to write into the format string.</param>
    public override void WriteLine ( string format, object arg0, object arg1 ) {
      WriteLine ( string.Format ( format, arg0, arg1 ) );
    }

		/// <summary>
		/// Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.
		/// </summary>
		/// <param name="format">The formatting string.</param>
		/// <param name="arg0">The object to write into the format string.</param>
		/// <param name="arg1">The object to write into the format string.</param>
		/// <param name="arg2">The object to write into the format string.</param>
    public override void WriteLine ( string format, object arg0, object arg1, object arg2 ) {
      WriteLine ( string.Format ( format, arg0, arg1, arg2 ) );
    }

		/// <summary>
		/// Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.
		/// </summary>
		/// <param name="format">The formatting string.</param>
		/// <param name="arg">The object array to write into format string.</param>
    public override void WriteLine ( string format, params object[ ] arg ) {
      WriteLine ( string.Format ( format, arg ) );
    }

		/// <summary>
		/// Writes the text representation of a 4-byte unsigned integer followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="value">The 4-byte unsigned integer to write.</param>
    public override void WriteLine ( uint value ) {
      WriteLine ( string.Format ( "{0}", value ) );
    }

		/// <summary>
		/// Writes the text representation of an 8-byte unsigned integer followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="value">The 8-byte unsigned integer to write.</param>
    public override void WriteLine ( ulong value ) {
      WriteLine ( string.Format ( "{0}", value ) );
    }
    #endregion

    #region Write

		/// <summary>
		/// Writes a string to this instance of the StringWriter.
		/// </summary>
		/// <param name="value">The string to write.</param>
    public override void Write ( string value ) {
      try {
        if ( TextBox != null && !TextBox.IsDisposed ) {
          if ( TextBox.InvokeRequired ) {
            TextBox.Invoke ( new GenericDelegate ( AutoScroll ) );
            ProcessString ( value );
            TextBox.Invoke ( new GenericDelegate ( AutoScroll ) );
          } else {
            AutoScroll ( );
            ProcessString ( value );
            AutoScroll ( );
          }
        }
      } catch ( Exception ex ) {
        this.LogError ( ex.Message, ex );
      }
    }

		/// <summary>
		/// Processes the string.
		/// </summary>
		/// <param name="value">The value.</param>
    private void ProcessString ( string value ) {
      Regex logcat = new Regex ( @"^(?:(?:\#\s+)?(logcat|[WIDE])/)(.*)$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline );
      Match m1 = logcat.Match ( value );
      string text = string.Empty;
      ConsoleColor clr = Logger.Color;

      if ( m1.Success ) {
        string level = m1.Groups[ 1 ].Value;
        text = m1.Groups[ 2 ].Value;
        switch ( level ) {
          case "logcat":
            return;
          case "W": //warn
            clr = ConsoleColor.Yellow;
            break;
          case "I": //info
            clr = ConsoleColor.Cyan;
            break;
          case "D": //debug
            clr = ConsoleColor.Green;
            break;
          case "E": //error
            clr = ConsoleColor.Red;
            break;
          default:
            clr = Logger.Color;
            break;
        }
        if ( TextBox != null && !TextBox.IsDisposed ) {
          if ( TextBox.InvokeRequired ) {
            TextBox.Invoke ( new SetTextColorDelegate ( SetConsoleColor ), clr );
            TextBox.Invoke ( new AppendDelegate ( TextBox.AppendText ), text );
          } else {
            SetConsoleColor ( clr );
            TextBox.AppendText ( text );
          }
        }
      } else {
        Regex colored = new Regex ( @"(?:\e\[(\d{1,2};\d{1,2})m)?([^(\e)]*)(?:\e\[0m)?", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline );
        Match m = colored.Match ( value );
        while ( m.Success ) {
          string colorCode = m.Groups[ 1 ].Value;
          text = m.Groups[ 2 ].Value;
          LinuxConsole.ConsoleColor color = LinuxConsole.GetConsoleColor ( colorCode );
          LinuxConsole.ConsoleColorAttribute attrib = LinuxConsole.GetConsoleColorAttribute ( colorCode );
          if ( color != LinuxConsole.ConsoleColor.DEFAULT ) {
            clr = LinuxConsole.ToWindowsConsoleColor ( color, attrib );
          }
          if ( TextBox != null && !TextBox.IsDisposed ) {
            if ( TextBox.InvokeRequired ) {
              TextBox.Invoke ( new SetTextColorDelegate ( SetConsoleColor ), clr );
              TextBox.Invoke ( new AppendDelegate ( TextBox.AppendText ), text );
            } else {
              SetConsoleColor ( clr );
              TextBox.AppendText ( text );
            }
          }
          m = m.NextMatch ( );
        }
      }
    }


		/// <summary>
		/// Writes the text representation of a Boolean value to the text stream.
		/// </summary>
		/// <param name="value">The Boolean to write.</param>
    public override void Write ( bool value ) {
      Write ( string.Format ( "{0}", value ) );
    }

		/// <summary>
		/// Writes a character to this instance of the StringWriter.
		/// </summary>
		/// <param name="value">The character to write.</param>
    public override void Write ( char value ) {
      Write ( string.Format ( "{0}", value ) );
    }

		/// <summary>
		/// Writes a character array to the text stream.
		/// </summary>
		/// <param name="buffer">The character array to write to the text stream.</param>
    public override void Write ( char[ ] buffer ) {
      Write ( string.Format ( "{0}", buffer ) );
    }

		/// <summary>
		/// Writes the specified region of a character array to this instance of the StringWriter.
		/// </summary>
		/// <param name="buffer">The character array to read data from.</param>
		/// <param name="index">The index at which to begin reading from <paramref name="buffer" />.</param>
		/// <param name="count">The maximum number of characters to write.</param>
    public override void Write ( char[ ] buffer, int index, int count ) {
      Write ( string.Format ( "{0}", new string ( buffer, index, count ) ) );
    }

		/// <summary>
		/// Writes the text representation of a decimal value to the text stream.
		/// </summary>
		/// <param name="value">The decimal value to write.</param>
    public override void Write ( decimal value ) {
      Write ( string.Format ( "{0}", value ) );
    }

		/// <summary>
		/// Writes the text representation of an 8-byte floating-point value to the text stream.
		/// </summary>
		/// <param name="value">The 8-byte floating-point value to write.</param>
    public override void Write ( double value ) {
      Write ( string.Format ( "{0}", value ) );
    }

		/// <summary>
		/// Writes the text representation of a 4-byte floating-point value to the text stream.
		/// </summary>
		/// <param name="value">The 4-byte floating-point value to write.</param>
    public override void Write ( float value ) {
      Write ( string.Format ( "{0}", value ) );
    }

		/// <summary>
		/// Writes the text representation of a 4-byte signed integer to the text stream.
		/// </summary>
		/// <param name="value">The 4-byte signed integer to write.</param>
    public override void Write ( int value ) {
      Write ( string.Format ( "{0}", value ) );
    }

		/// <summary>
		/// Writes the text representation of an 8-byte signed integer to the text stream.
		/// </summary>
		/// <param name="value">The 8-byte signed integer to write.</param>
    public override void Write ( long value ) {
      Write ( string.Format ( "{0}", value ) );
    }

		/// <summary>
		/// Writes the text representation of an object to the text stream by calling ToString on that object.
		/// </summary>
		/// <param name="value">The object to write.</param>
    public override void Write ( object value ) {
      Write ( string.Format ( "{0}", value ) );
    }

		/// <summary>
		/// Writes out a formatted string, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.
		/// </summary>
		/// <param name="format">The formatting string.</param>
		/// <param name="arg0">An object to write into the formatted string.</param>
    public override void Write ( string format, object arg0 ) {
      Write ( string.Format ( format, arg0 ) );
    }

		/// <summary>
		/// Writes out a formatted string, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.
		/// </summary>
		/// <param name="format">The formatting string.</param>
		/// <param name="arg0">An object to write into the formatted string.</param>
		/// <param name="arg1">An object to write into the formatted string.</param>
    public override void Write ( string format, object arg0, object arg1 ) {
      Write ( string.Format ( format, arg0, arg1 ) );
    }

		/// <summary>
		/// Writes out a formatted string, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.
		/// </summary>
		/// <param name="format">The formatting string.</param>
		/// <param name="arg0">An object to write into the formatted string.</param>
		/// <param name="arg1">An object to write into the formatted string.</param>
		/// <param name="arg2">An object to write into the formatted string.</param>
    public override void Write ( string format, object arg0, object arg1, object arg2 ) {
      Write ( string.Format ( format, arg0, arg1, arg2 ) );
    }

		/// <summary>
		/// Writes out a formatted string, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.
		/// </summary>
		/// <param name="format">The formatting string.</param>
		/// <param name="arg">The object array to write into the formatted string.</param>
    public override void Write ( string format, params object[ ] arg ) {
      Write ( string.Format ( format, arg ) );
    }

		/// <summary>
		/// Writes the text representation of a 4-byte unsigned integer to the text stream.
		/// </summary>
		/// <param name="value">The 4-byte unsigned integer to write.</param>
    public override void Write ( uint value ) {
      Write ( string.Format ( "{0}", value ) );
    }

		/// <summary>
		/// Writes the text representation of an 8-byte unsigned integer to the text stream.
		/// </summary>
		/// <param name="value">The 8-byte unsigned integer to write.</param>
    public override void Write ( ulong value ) {
      Write ( string.Format ( "{0}", value ) );
    }
    #endregion

		/// <summary>
		/// Auto Scrolls the window
		/// </summary>
    public void AutoScroll ( ) {
      TextBox.SelectionStart = TextBox.Text.Length;
      TextBox.ScrollToCaret ( );
    }

		/// <summary>
		/// Sets the color of the console.
		/// </summary>
		/// <param name="color">The color.</param>
    public void SetConsoleColor ( ConsoleColor color ) {
      if ( TextBox != null && !TextBox.IsDisposed ) {
        if ( TextBox.InvokeRequired ) {
          TextBox.Invoke ( new SetTextColorDelegate ( SetTextColor ), color );
        } else {
          SetTextColor ( color );
        }
      }
    }

		/// <summary>
		/// Sets the color of the text.
		/// </summary>
		/// <param name="color">The color.</param>
    private void SetTextColor ( ConsoleColor color ) {
			this.TextBox.SelectionColor = color.ToColor ( );
      
    }
  }
}
