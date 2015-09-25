using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Core {
	public static partial class DroidExplorerCoreExtensions {
		public static void IntReverseForRawImage(this byte[] source, Action<byte[]> action) {
			var step = 4;
			for ( int i = 0; i < source.Count ( ); i += step ) {
				var b = new byte[step];
				for ( int x = b.Length - 1; x >= 0; --x ) {
					b[( step - 1 ) - x] = source[i + x];
				}

				b[2] = source[i + 0];
				b[1] = source[i + 1];
				b[0] = source[i + 2];
				b[3] = source[i + 3];

				action ( b );
			}
		}
	}
}
