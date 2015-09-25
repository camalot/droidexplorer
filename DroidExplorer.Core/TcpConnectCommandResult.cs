using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Core {
	internal class TcpConnectCommandResult : CommandResult {
		public TcpConnectCommandResult(string data): base() {
			Successful = false;
			ProcessData(data);
		}
		public bool Successful { get; private set; }
		protected override void ProcessData(string data) {
			Successful = data.StartsWith("connected to ");
			base.ProcessData(data);
		}
	}
}
