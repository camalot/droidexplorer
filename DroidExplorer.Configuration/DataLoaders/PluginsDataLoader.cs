using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Configuration.UI;
using System.Windows.Forms;

namespace DroidExplorer.Configuration.DataLoaders {
  public class PluginsDataLoader : IOptionNodeDataLoader{
    #region IOptionNodeDataLoader Members

		/// <summary>
		/// Loads the specified parent node.
		/// </summary>
		/// <param name="parentNode">The parent node.</param>
    public void Load ( System.Windows.Forms.TreeNode parentNode ) {
			foreach ( PluginInfo pi in Settings.Instance.PluginSettings.Plugins ) {
				OptionItemTreeNode oitn = new OptionItemTreeNode ( );
				oitn.Text = pi.Name;
				PropertyGridEditor pge = new PropertyGridEditor ( );
				/*pge.PropertyValueChanged += delegate ( object s, PropertyValueChangedEventArgs e ) {
					GridItem gi = e.ChangedItem;
					switch ( gi.Label ) {
						case "Enabled":
							pi.Enabled = (bool)gi.Value;
							break;
						case "ExecuteOnUnload":
							pi.ExecuteOnUnload = (bool)gi.Value;
							break;
						case "ExecuteOnLoad":
							pi.ExecuteOnLoad = (bool)gi.Value;
							break;
					}
				};*/
				pi.Plugin = Settings.Instance.PluginSettings.GetPlugin ( pi.ID.Replace ( " ", string.Empty ) );
				oitn.UIEditor = pge; 
				oitn.UIEditor.SetSourceObject ( pi );
				parentNode.Nodes.Add ( oitn );
			}
    }

    #endregion
  }
}
