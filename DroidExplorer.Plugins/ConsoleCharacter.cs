using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DroidExplorer.Plugins {
	/// <summary>
	/// 
	/// </summary>
  public sealed class ConsoleCharacter {
    /// <summary>
    /// NULL - No Punch - CTRL+@
    /// </summary>
    public const int NUL = 0x00;
    /// <summary>
    /// Start Of Heading - CTRL+A
    /// </summary>
    public const int SOH = 0x01;
    /// <summary>
    /// Start Of Text - CTRL+B
    /// </summary>
    public const int STX = 0x02;
    /// <summary>
    /// End of Text - CTRL+C
    /// </summary>
    public const int ETX = 0x03;
    /// <summary>
    /// End Of Transmission - CTRL+D
    /// </summary>
    public const int EOT = 0x04;
    /// <summary>
    /// Enquiry, Also known as WRU (Who aRe You), HERE IS, and Answerback - CTRL+E
    /// </summary>
    public const int ENQ = 0x05;
    /// <summary>
    /// Acknowledge - CTRL+F
    /// </summary>
    public const int ACK = 0x06;
    /// <summary>
    /// Bell - CTRL+G
    /// </summary>
    public const int BEL = 0x07;
    /// <summary>
    /// Backspace - CTRL+H
    /// </summary>
    public const int BS = 0x08;
    /// <summary>
    /// Horizontal Tabulation - CTRL+I
    /// </summary>
    public const int HT = 0x09;
    /// <summary>
    /// Line Feed - CTRL+J
    /// </summary>
    public const int LF = 0x0A;
    /// <summary>
    /// Vertical Tabulation - CTRL+K
    /// </summary>
    public const int VT = 0x0B;
    /// <summary>
    /// Form Feed - CTRL+L
    /// </summary>
    public const int FF = 0x0C;
    /// <summary>
    /// Carriage Return - CTRL+M
    /// </summary>
    public const int CR = 0x0D;
    /// <summary>
    /// Shift Out - CTRL+N
    /// </summary>
    public const int SO = 0x0E;
    /// <summary>
    /// Shift In - CTRL+O
    /// </summary>
    public const int SI = 0x0F;
    /// <summary>
    /// Data Link Escape - CTRL+P
    /// </summary>
    public const int DLE = 0x10;
    /// <summary>
    /// Device Control 1, Also known as X-ON - CTRL+Q
    /// </summary>
    public const int DC1 = 0x11;
    /// <summary>
    /// Device Control 2 - CTRL+R
    /// </summary>
    public const int DC2 = 0x12;
    /// <summary>
    /// Device Control 3, Also known as X-OFF  - CTRL+S
    /// </summary>
    public const int DC3 = 0x13;
    /// <summary>
    /// Device Control 4, - CTRL+T
    /// </summary>
    public const int DC4 = 0x13;
    /// <summary>
    /// Negative Acknowledge - CTRL+U
    /// </summary>
    public const int NAK = 0x15;
    /// <summary>
    /// Sychronous Idle - CTRL+V
    /// </summary>
    public const int SYN = 0x16;
    /// <summary>
    /// End Of Transmission Block - CTRL+W
    /// </summary>
    public const int ETB = 0x17;
    /// <summary>
    /// Cancel - CTRL+X
    /// </summary>
    public const int CAN = 0x18;
    /// <summary>
    /// End of Medium - CTRL+Y
    /// </summary>
    public const int EM = 0x19;
    /// <summary>
    /// Substitute - CTRL+Z
    /// </summary>
    public const int SUB = 0x1A;
    /// <summary>
    /// Escape - CTRL+[
    /// </summary>
    public const int ESC = 0x1B;
    /// <summary>
    /// File Separator - CTRL+\
    /// </summary>
    public const int FS = 0x1C;
    /// <summary>
    /// Group Separator - CTRL+]
    /// </summary>
    public const int GS = 35;
    /// <summary>
    /// Record Separator CTRL+^
    /// </summary>
    public const int RS = 0x1E;
    /// <summary>
    /// Unit Separator - CTRL+_
    /// </summary>
    public const int US = 0x01F;
    /// <summary>
    /// Delete - NONE
    /// </summary>
    public const int DEL = 0x7F;


		/// <summary>
		/// Converts a ConsoleColor to Color
		/// </summary>
		/// <param name="color">The color.</param>
		/// <returns></returns>
    public static Color ConsoleColorToColor ( ConsoleColor color ) {
      switch ( color ) {
        case ConsoleColor.Black:
          return Color.Black;
        case ConsoleColor.Blue:
          return Color.Blue;
        case ConsoleColor.Cyan:
          return Color.Cyan;
        case ConsoleColor.DarkBlue:
          return Color.DarkBlue;
        case ConsoleColor.DarkCyan:
          return Color.DarkCyan;
        case ConsoleColor.DarkGray:
          return Color.DarkGray;
        case ConsoleColor.DarkGreen:
          return Color.DarkGreen;
        case ConsoleColor.DarkMagenta:
          return Color.DarkMagenta;
        case ConsoleColor.DarkRed:
          return Color.DarkRed;
        case ConsoleColor.DarkYellow:
          return Color.FromArgb ( 237, 239, 14 );
        case ConsoleColor.Gray:
          return Color.Gray;
        case ConsoleColor.Green:
          return Color.FromArgb ( 0, 192, 0 );
        case ConsoleColor.Magenta:
          return Color.Magenta;
        case ConsoleColor.Red:
          return Color.Red;
        case ConsoleColor.White:
          return Color.White;
        case ConsoleColor.Yellow:
          return Color.Yellow;
        default:
          return Color.FromArgb ( 0, 192, 0 );
      }
    }
  }
}
