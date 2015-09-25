using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Core.UI {
	public class OpenFileDialog : FileDialog {

		public OpenFileDialog ( ) : this ( "/" ) {

		}
		public OpenFileDialog ( string initialDirectory ) : base ( initialDirectory ) {
			this.OkText = "&Open";
			this.Title = "Open File";
		}
	}
}
