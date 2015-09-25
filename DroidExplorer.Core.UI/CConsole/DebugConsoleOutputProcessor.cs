using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camalot.Common.Extensions;

namespace DroidExplorer.Core.UI.CConsole {
	public class DebugConsoleOutputProcessor : IConsoleOutputProcessor {

		public DebugConsoleOutputProcessor(ConsoleControl console) {
			this.Console = console.Require();
		}

		public ConsoleControl Console { get; private set; }

		public void Process(string output) {
			var split = output.Split(new string[] { "\r\n" }, StringSplitOptions.None);
			foreach(var line in split) {
				var l = "{0}{1}".With(line, Environment.NewLine);
				var parsed = ParsedOutput(l);
				Console.InternalRichTextBox.SelectionColor = Console.ForeColor;
				Console.InternalRichTextBox.ReadOnly = false;
				Console.InternalRichTextBox.SelectedText += parsed.REReplace("[\r]{1,}\n?", Environment.NewLine);
				Console.InputStart = Console.InternalRichTextBox.SelectionStart;
				Console.InternalRichTextBox.ReadOnly = !Console.IsInputEnabled;

			}
		}

		private string ParsedOutput(string line) {
			if(string.IsNullOrWhiteSpace(line)) {
				return string.Empty;
			}

			var pattern = @"^(V|D|I|W|E|F|S)\/";
			if(line.IsMatch(pattern)) {
				var level = line.Match(pattern).Groups[1].Value;
				switch(level) {
					case "V":
						this.Console.ForeColor = Color.LightYellow;
						break;
					case "D":
						this.Console.ForeColor = Color.MediumSeaGreen;
						break;
					case "I":
						this.Console.ForeColor = Color.LightSlateGray;
						break;
					case "W":
						this.Console.ForeColor = Color.LightGoldenrodYellow;
						break;
					case "E":
						this.Console.ForeColor = Color.IndianRed;
						break;
					case "F":
						this.Console.ForeColor = Color.OrangeRed;
						break;
					case "S":
						this.Console.ForeColor = Color.LightSteelBlue;
						break;
					default:
						break;
				}
				return line.REReplace(pattern, "");
			} else {
				return line;
			}
		}
	}
}
