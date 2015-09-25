using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camalot.Common.Extensions;

namespace DroidExplorer.Core.UI.CConsole {
	public class DefaultConsoleOutputProcessor : IConsoleOutputProcessor {
		public DefaultConsoleOutputProcessor(ConsoleControl console) {
			this.Console = console.Require();
		}

		public ConsoleControl Console { get; private set; }

		public void Process(string output) {
			Console.InternalRichTextBox.SelectionColor = Console.ForeColor;
			Console.InternalRichTextBox.SelectedText += output.REReplace("[\r]{1,}\n?", Environment.NewLine);
			Console.InputStart = Console.InternalRichTextBox.SelectionStart;
		}
	}
}
