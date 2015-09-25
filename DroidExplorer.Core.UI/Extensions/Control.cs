using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DroidExplorer.Core.UI {
	public static class DroidExplorerCoreUIExtensions {
		public static object Invoke (this Control control, Action action) {
			return control.Invoke ((Delegate)action );
		}

		public static void BeginInvoke ( this Control control, Action action ) {
			control.BeginInvoke ( (Delegate)action );
		}

	}
}
