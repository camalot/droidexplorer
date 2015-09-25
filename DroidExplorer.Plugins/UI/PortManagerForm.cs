using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Camalot.Common.Extensions;
using DroidExplorer.Core.Plugins;
using DroidExplorer.Core.UI.Renderers.ToolStrip;

namespace DroidExplorer.Plugins.UI {
	public partial class PortManagerForm : Form {

		private readonly TreeNode RootForward;
		private readonly TreeNode RootReverse;

		private IPluginHost PluginHost { get; set; }

		public PortManagerForm ( IPluginHost host ) {
			PluginHost = host.Require ( );
			InitializeComponent ( );
			RootForward = new TreeNode ( "Forwards" );
			RootReverse = new TreeNode ( "Reverses" );

			forwardAdd.Image = Resources.Images.forward_add;
			forwardDelete.Image = Resources.Images.forward_delete;
			reverseAdd.Image = Resources.Images.reverse_add;
			reverseDelete.Image = Resources.Images.reverse_delete;

			this.treeView1.Nodes.Add ( RootForward );
			this.treeView1.Nodes.Add ( RootReverse );

			PluginHost.CommandRunner.ReverseList ( );
			PluginHost.CommandRunner.ForwardList ( );
		}
	}
}
