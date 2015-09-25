using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Core.Exceptions {
	public class InvalidRgb565FileFormatException : Exception {
		public InvalidRgb565FileFormatException ()
			: base ( "The format of the image is not valid" ) {

		}

		public InvalidRgb565FileFormatException ( string message )
			: base ( message ) {

		}
	}
}
