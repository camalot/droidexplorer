using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DroidExplorer.Core.IO;
using Managed.Adb;

namespace DroidExplorer.Core {
	public static partial class DroidExplorerCoreExtensions {

		public static Permission ToPermission(this FilePermission perm) {
			return new Permission ( perm.ToString ( ) );
		}

	}
}
