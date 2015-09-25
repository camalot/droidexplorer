using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camalot.Common.Extensions;

namespace DroidExplorer.Core {
	public static partial class DroidExplorerCoreExtensions {
		public static string ToSafeFileName(this Core.IO.FileSystemInfo fsi) {
			var invalid = new string(System.IO.Path.GetInvalidFileNameChars());
			return fsi.Name.REReplace("[{0}]".With(invalid), "");
		}
	}
}
