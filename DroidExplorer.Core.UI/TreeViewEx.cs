using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DroidExplorer.Core.UI {
	/// <summary>
	/// A double buffered treeview
	/// </summary>
	public class TreeViewEx : TreeView {
		/// <summary>
		/// Initializes a new instance of the <see cref="TreeViewEx"/> class.
		/// </summary>
		public TreeViewEx ( )
			: base ( ) {
			this.SetStyle ( ControlStyles.OptimizedDoubleBuffer, true );
		}
	}
}
