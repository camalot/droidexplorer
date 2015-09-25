using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camalot.Common.Extensions;

namespace DroidExplorer.Core.Exceptions {
	public class UnsupportedAdbVersionException : Exception {
		public UnsupportedAdbVersionException(string version, string minVersion) : base("Adb version is {0}, minimum supported version is {1}".With(version,minVersion)){

		}
	}
}

