using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroidExplorer.Core {
	public static partial class DroidExplorerCoreExtensions {
		public static String QuoteIfHasSpace(this String s) {
			var quote = s.Contains(" ");
			return String.Format("{0}{1}{0}", quote ? "\"" : String.Empty, s);
		}
	}
}
