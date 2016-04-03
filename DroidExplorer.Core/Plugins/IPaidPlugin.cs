using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Core.Plugins {
	/// <summary>
	/// 
	/// </summary>
	/// <seealso cref="DroidExplorer.Core.Plugins.IPlugin" />
	public interface IPaidPlugin : IPlugin {

		/// <summary>
		/// Gets the signature.
		/// </summary>
		/// <returns></returns>
		string GetSignature ( );

	}
}
