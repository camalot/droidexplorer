using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Service {
	public class UnsupportedPlatformException : Exception {
		public UnsupportedPlatformException ( )
			: this ( "This service was compiled to run on x86 platform, can not run on x64 platform." ) {

		}

		public UnsupportedPlatformException ( string message )
			: base ( message ) {

		}
	}
}
