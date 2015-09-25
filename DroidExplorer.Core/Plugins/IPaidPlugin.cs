using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Core.Plugins {
	public interface IPaidPlugin : IPlugin {

		string GetSignature ( );

	}
}
