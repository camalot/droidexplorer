using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Camalot.Common.Extensions;

namespace DroidExplorer.Core.UI.CConsole {
	public class LinuxConsoleOutputProcessor : IConsoleOutputProcessor {

		public LinuxConsoleOutputProcessor(ConsoleControl console) {
			this.Console = console.Require();
		}

		//private enum ConsoleColors {
		//	UNKNOWN = 0,

		//	FBlack = 30,
		//	FRed = 31,
		//	FGreen = 32,
		//	FYellow = 33,
		//	FBlue = 34,
		//	FMegenta = 35,
		//	FCyan = 36,
		//	FWhite = 37,

		//	BBlack = 40,
		//	BRed = 41,
		//	BGreen = 42,
		//	BYellow = 43,
		//	BBlue = 44,
		//	BMegenta = 45,
		//	BCyan = 46,
		//	BWhite = 47
		//}

		public void Process(string output) {
			//  Write the output.
			// replaces any \r?+\n with environment newline
			var split = output.Split((char)0x1B);
			foreach(var group in split) {
				var sb = new StringBuilder(group);
				var parsed = ParsedOutput(sb.ToString());
				Console.InternalRichTextBox.SelectionColor = Console.ForeColor;
				Console.InternalRichTextBox.SelectionBackColor = Console.SelectionBackColor;
				Console.InternalRichTextBox.SelectedText += parsed.REReplace("(\r){1,}\n+", Environment.NewLine);
				Console.InputStart = Console.InternalRichTextBox.SelectionStart;
			}
		}

		private string ParsedOutput(string data) {
			if(string.IsNullOrWhiteSpace(data)) {
				return string.Empty;
			}

			var r = @"\e\[(\d{1,2});(\d{1,2})(?:;(\d{0,2}))?m";
			var end = @"\e\[0m";
			//\e[2J\e[H
			// \e7[r\e[999;999H\e[6n
			var ignored = @"(\e\[H|\e7|\e\[r|\e\[\d{1,};?\d{0,}H|\e\[6n|\e\d{1,}\|)";
			var clear = @"\e\[2J";

			var sb = new StringBuilder(data);
			sb.Insert(0, (char)0x1B);
			var withEscape = sb.ToString();
			if(withEscape.IsMatch(ignored)) {
				data = withEscape.REReplace(ignored, "");
			} else if(withEscape.IsMatch(clear)) {
				Console.ForeColor = Console.DefaultForeColor;
				Console.SelectionBackColor = Console.BackColor;
				Console.ClearOutput();
				return string.Empty;
			} else if(withEscape.IsMatch(end)) {
				// reset forecolor to default
				Console.ForeColor = Console.DefaultForeColor;
				Console.SelectionBackColor = Console.BackColor;
				data = withEscape.REReplace(end, "");
			} else if(withEscape.IsMatch(r)) {
				var m = withEscape.Match(r, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline);

				var code = m.Groups[2].Value;
				LinuxConsole.ConsoleColor fcolor = (LinuxConsole.ConsoleColor)int.Parse(m.Groups[2].Value);
				LinuxConsole.ConsoleColorAttribute attrib = (LinuxConsole.ConsoleColorAttribute)int.Parse(m.Groups[1].Value);

				var clr = LinuxConsole.ToColor(fcolor, attrib);
				Console.ForeColor = clr;

				var bcode = m.Groups[3].Value.Or("0");
				if((LinuxConsole.ConsoleColor)int.Parse(bcode) != LinuxConsole.ConsoleColor.DEFAULT) {
					LinuxConsole.ConsoleColor bcolor = (LinuxConsole.ConsoleColor)int.Parse(m.Groups[2].Value);
					var bclr = LinuxConsole.ToColor(bcolor, attrib);
					Console.SelectionBackColor = bclr;
				}

				data = withEscape.REReplace(r, "");
			}

			return data;
		}

		public ConsoleControl Console { get; private set; }

	}
}
