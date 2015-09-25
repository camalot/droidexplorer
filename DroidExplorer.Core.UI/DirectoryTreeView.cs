using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Core.UI.Components;

namespace DroidExplorer.Core.UI {
	public class DirectoryTreeView : TreeViewEx {
		public DirectoryTreeNode FindNodeFromPath ( string path ) {
			return RecursiveFind ( this.Nodes, path );
		}

		private DirectoryTreeNode RecursiveFind ( TreeNodeCollection root, string path ) {
			DirectoryTreeNode result = null;
			foreach ( TreeNode node in root ) {
				if ( node is DirectoryTreeNode ) {
					DirectoryTreeNode dnode = node as DirectoryTreeNode;
					if ( string.Compare ( dnode.LinuxPath, path, false ) == 0 ) {
						return dnode;
					}
					if ( dnode.Nodes.Count > 0 ) {
						result = RecursiveFind ( dnode.Nodes, path );
						if ( result != null ) {
							return result;
						}
					}
				}
			}

			return null;
		}

		public void ExpandToPath ( string path ) {
			string spath = path;
			if ( spath.StartsWith ( new string ( new char[] { System.IO.Path.AltDirectorySeparatorChar } ) ) ) {
				spath = spath.Substring ( 1 );
			}

			string[] pathItems = spath.Split ( new char[] { System.IO.Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries );
			string p = string.Empty;
			foreach ( var item in pathItems ) {
				p += string.Format ( "{0}{1}", System.IO.Path.AltDirectorySeparatorChar, item );
				var dtn = FindNodeFromPath ( p + System.IO.Path.AltDirectorySeparatorChar );
				if ( dtn != null ) {
					if ( this.InvokeRequired ) {
						this.Invoke ( ( ) => {
							if ( !dtn.IsExpanded ) {
								dtn.Expand ( );
							}
						} );
					} else {
						if ( !dtn.IsExpanded ) {
							dtn.Expand ( );
						}

					}
				} else {
					this.LogWarn ( "Could not find path '{0}'", p );
				}
			}
		}
	}
}
