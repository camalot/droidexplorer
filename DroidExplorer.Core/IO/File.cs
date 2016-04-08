using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Camalot.Common.Extensions;

namespace DroidExplorer.Core.IO {
	/// <summary>
	/// 
	/// </summary>
	public static class File {

		/// <summary>
		/// Existses the specified file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		public static bool Exists (string file) {
			var options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline;
      var result = CommandRunner.Instance.ShellRun ( "find {0}".With ( file ) );
			return !result.Output.ToString().IsMatch ( @"(?:not\sfound|no\ssuch\sfile\sor\sdirectory)", options );
		}
	}
}
