using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Core.Net;

namespace DroidExplorer.Configuration.UI {
	/// <summary>
	/// 
	/// </summary>
  public class ProxyUIEditor : PropertyGridEditor {

		/// <summary>
		/// Initializes a new instance of the <see cref="ProxyUIEditor"/> class.
		/// </summary>
    public ProxyUIEditor ( ) {
      this.SelectedObject = Settings.Instance.Proxy;


    }

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.PropertyGrid.PropertyValueChanged"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PropertyValueChangedEventArgs"/> that contains the event data.</param>
    protected override void OnPropertyValueChanged ( System.Windows.Forms.PropertyValueChangedEventArgs e ) {
      base.OnPropertyValueChanged ( e );
      GridItem gi = e.ChangedItem;
      ProxyInfo pi = this.SelectedObject as ProxyInfo;
      /*switch ( gi.Label ) {
        case "Enabled":
          pi.Enabled = ( bool )gi.Value;
          break;
        case "ExecuteOnUnload":
          pi.ExecuteOnUnload = ( bool )gi.Value;
          break;
        case "ExecuteOnLoad":
          pi.ExecuteOnLoad = ( bool )gi.Value;
          break;
      }*/
    }

  }
}
