using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Core.Exceptions {
	public class ShellCommandUnresponsiveException : AdbException {
		public ShellCommandUnresponsiveException ( )
			: base ( "The shell command has become unresponsive" ) {

		}
	}
}
