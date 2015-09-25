using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DroidExplorer.Configuration;
using DroidExplorer.Core.Plugins;

namespace DroidExplorer.Plugins.Configuration {
	public class ShellConfiguration : IPluginSettings {
		public Color BackgroundColor { get; set; }
		public Color ForeColor { get; set; }
		public Font Font { get; set; }
	}
}
