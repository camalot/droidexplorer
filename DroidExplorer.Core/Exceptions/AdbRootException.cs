using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroidExplorer.Core.Exceptions {
	public class AdbRootException : AdbException {
		public AdbRootException() : base("Unable to start ADB with root access") {

		}
	}
}
