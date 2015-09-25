using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Camalot.Common.Extensions;
using DroidExplorer.Core.Plugins;
using DroidExplorer.Core.UI.Renderers.ToolStrip;
using DroidExplorer.UI;

namespace DroidExplorer.Core.UI {
	public partial class PluginForm : Form {
		public PluginForm ( IPluginHost pluginHost ) {
			this.StickyWindow = new StickyWindow ( this );
			this.StickyWindow.StickToOther = true;
			this.StickyWindow.StickOnResize = true;
			this.StickyWindow.StickOnMove = true;
			PluginHost = pluginHost.Require ( );

			ToolStripManager.Renderer = new VisualStudio2012Renderer ( );

		}

		protected StickyWindow StickyWindow { get; set; }
		protected IPluginHost PluginHost { get; set; }
	}


}
