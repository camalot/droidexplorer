using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Core;
using DroidExplorer.UI;

namespace DroidExplorer.Core.UI.Components {
  public class DirectoryTreeNode : TreeNode {
    private delegate void AddChildTreeNodesDelegate ( TreeNodeCollection tnc, List<DroidExplorer.Core.IO.FileSystemInfo> fsiList );
    private delegate void AddDummyNodeDelegate ( TreeNodeCollection tnc );
    private delegate void TreeNodeItemDelegate ( DirectoryTreeNode dtn );

    public DirectoryTreeNode ( DroidExplorer.Core.IO.FileSystemInfo fsi )
      : base ( fsi.Name ) {
      this.DirectoryInfo = fsi;
      if ( this.DirectoryInfo.IsLink && this.DirectoryInfo.IsDirectory ) {
        this.SelectedImageIndex = this.ImageIndex = 2;
      } else {
        this.SelectedImageIndex = this.ImageIndex = 0;
      }
      this.Nodes.Add ( new DummyTreeNode ( ) );
    }

    public DroidExplorer.Core.IO.FileSystemInfo DirectoryInfo { get; private set; }

    public string LinuxPath {
      get {
        string path = this.FullPath.Replace ( "//", "/" );
        if ( !path.EndsWith ( "/" ) ) {
          path += "/";
        }
        return path;
      }
    }

		private bool IsRoot {
			get {
				return this.Parent == null && DirectoryInfo.FullPath == "/";
			}
		}

    public List<DroidExplorer.Core.IO.FileSystemInfo> OnAfterSelect ( CommandRunner runner ) {
      return runner.GetDirectoryContents ( this.LinuxPath );
    }

    public void OnBeforeExpand ( CommandRunner runner, bool populateChildren ) {
      TreeView tv = this.TreeView;
			if(!IsRoot) {
				if(tv != null && tv.InvokeRequired) {
					tv.Invoke((Action)(() => {
						this.Nodes.Clear();
						SetExpandedImageIndex(this);
					}));
				} else {
					this.Nodes.Clear();
					SetExpandedImageIndex(this);
				}
			}


      string path = this.FullPath.Replace ( "//", "/" );
      if ( !path.EndsWith ( "/" ) ) {
        path += "/";
      }

      if ( populateChildren && this.Nodes.Count == 0 ) {
        List<DroidExplorer.Core.IO.FileSystemInfo> fileSystemInfoList = runner.ListDirectories ( path );

        if ( tv != null && tv.InvokeRequired ) {
					tv.Invoke((Action)(() => {
						this.AddChildTreeNodes(this.Nodes, fileSystemInfoList);
					}));
        } else { // no need to invoke
          AddChildTreeNodes ( this.Nodes, fileSystemInfoList );
        }
      }
    }

    public void OnBeforeCollapse ( CommandRunner runner ) {
      TreeView tv = this.TreeView;
      if ( tv != null && tv.InvokeRequired ) {
				tv.Invoke((Action)(() => {
					SetCollapsedImageIndex(this);
				}));
      } else {
        SetCollapsedImageIndex ( this );
      }
    }

    public void OnAfterCollapse ( CommandRunner runner ) {
			if(!IsRoot) { // only wipe out for non-root node.
				TreeView tv = this.TreeView;
				if(tv != null && tv.InvokeRequired) {
					tv.Invoke(new GenericDelegate(this.Nodes.Clear));
					tv.Invoke(new AddDummyNodeDelegate(this.AddDummyNode), new object[] { this.Nodes });
				} else { // no need to invoke
					this.Nodes.Clear();
					AddDummyNode(this.Nodes);
				}
			}
    }

    private void SetCollapsedImageIndex ( DirectoryTreeNode dtn ) {
      if ( dtn.DirectoryInfo.IsLink && dtn.DirectoryInfo.IsDirectory ) {
        dtn.SelectedImageIndex = dtn.ImageIndex = 2;
      } else {
        dtn.SelectedImageIndex = dtn.ImageIndex = 0;
      }
    }

    private void SetExpandedImageIndex ( DirectoryTreeNode dtn ) {
      if ( dtn.DirectoryInfo.IsLink && dtn.DirectoryInfo.IsDirectory ) {
        dtn.SelectedImageIndex = dtn.ImageIndex = 3;
      } else {
        dtn.SelectedImageIndex = dtn.ImageIndex = 1;
      }
    }

    private void AddChildTreeNodes ( TreeNodeCollection tnc, List<DroidExplorer.Core.IO.FileSystemInfo> fsiList ) {
      foreach ( DroidExplorer.Core.IO.FileSystemInfo fsi in fsiList ) {
        tnc.Add ( new DirectoryTreeNode ( fsi ) );
      }
    }

    private void AddDummyNode ( TreeNodeCollection tnc ) {
      tnc.Add ( new DummyTreeNode ( ) );
    }
  }
}
