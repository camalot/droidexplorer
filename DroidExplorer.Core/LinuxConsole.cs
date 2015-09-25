using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DroidExplorer.Core {
  public class LinuxConsole {
    public const string COLOR_START = @"\e[";
    public const string COLOR_STOP = @"\e[m";
    public const string COLOR_CODE_END = "m";

    public enum ConsoleColor {
      DEFAULT = 0,
      Black = 30,
      Red = 31,
      Green = 32,
      Brown = 33,
      Blue = 34,
      Purple = 35,
      Cyan = 36
    }

    public enum ConsoleColorAttribute {
      Normal = 0,
      Light = 1
    }

		public static Color ToColor(ConsoleColor color, ConsoleColorAttribute attribute) {
			switch(color) {
				case ConsoleColor.Black:
					if(attribute != ConsoleColorAttribute.Light) {
						return Color.DarkGray;
					} else {
						return Color.Black;
					}
				case ConsoleColor.Red:
					if(attribute != ConsoleColorAttribute.Light) {
						return Color.Red;
					} else {
						return Color.IndianRed;
					}
				case ConsoleColor.Green:
					if(attribute != ConsoleColorAttribute.Light) {
						return Color.LightGreen;
					} else {
						return Color.FromArgb(0, 0, 192, 0);
					}
				case ConsoleColor.Brown:
					if(attribute != ConsoleColorAttribute.Light) {
						return Color.Gray;
					} else {
						return Color.LightGray;
					}
				case ConsoleColor.Blue:
					if(attribute != ConsoleColorAttribute.Light) {
						return Color.DarkBlue;
					} else {
						return Color.Blue;
					}
				case ConsoleColor.Purple:
					if(attribute != ConsoleColorAttribute.Light) {
						return Color.DarkMagenta;
					} else {
						return Color.Magenta;
					}
				case ConsoleColor.Cyan:
					if(attribute != ConsoleColorAttribute.Light) {
						return Color.DarkCyan;
					} else {
						return Color.Cyan;
					}
			}

			return Color.FromArgb(0, 0, 192, 0);
		}

    public static System.ConsoleColor ToWindowsConsoleColor ( ConsoleColor color, ConsoleColorAttribute attrib ) {
      switch ( color ) {
        case ConsoleColor.Black:
          return System.ConsoleColor.Black;
        case ConsoleColor.Red:
          if ( attrib != ConsoleColorAttribute.Light ) {
            return System.ConsoleColor.DarkRed;
          } else {
            return System.ConsoleColor.Red;
          }
        case ConsoleColor.Green:
          if ( attrib != ConsoleColorAttribute.Light ) {
            return System.ConsoleColor.DarkGreen;
          } else {
            return System.ConsoleColor.Green;
          }
        case ConsoleColor.Brown:
          if ( attrib != ConsoleColorAttribute.Light ) {
            return System.ConsoleColor.DarkGray;
          } else {
            return System.ConsoleColor.Gray;
          }
        case ConsoleColor.Blue:
          if ( attrib != ConsoleColorAttribute.Light ) {
            return System.ConsoleColor.DarkBlue;
          } else {
            return System.ConsoleColor.Blue;
          }
        case ConsoleColor.Purple:
          if ( attrib != ConsoleColorAttribute.Light ) {
            return System.ConsoleColor.DarkMagenta;
          } else {
            return System.ConsoleColor.Magenta;
          }
        case ConsoleColor.Cyan:
          if ( attrib != ConsoleColorAttribute.Light ) {
            return System.ConsoleColor.DarkCyan;
          } else {
            return System.ConsoleColor.Cyan;
          }
      }

      return System.ConsoleColor.DarkGreen;
    }

  }
}
