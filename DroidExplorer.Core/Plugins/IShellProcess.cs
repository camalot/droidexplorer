using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace DroidExplorer.Core.Plugins {
  public interface IShellProcess {

    /*StreamReader OutputStream { get; }
    StreamReader ErrorStream { get; }
    StreamWriter InputStream { get; }

    Process BaseProcess { get; }

    void SetBaseProcess ( Process process );
    void SetOutputStream ( StreamReader reader );
    void SetErrorStream ( StreamReader reader );
    void SetInputStream ( StreamWriter writer );*/

		bool EchoInput { get; }
		bool IsProcessRunning { get; }
		void WriteInput(string input);
		void WriteOutput(string output);
		void StartProcess(string command, string args);
		void StopProcess();
  }

}
