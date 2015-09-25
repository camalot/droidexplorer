using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DroidExplorer.Core.UI.CConsole {
	public interface IConsoleOutputProcessor {
		ConsoleControl Console { get; }
		void Process(string output);
	}
}
