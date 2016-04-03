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

		/// <summary>
		/// Gets a value indicating whether [echo input].
		/// </summary>
		/// <value>
		///   <c>true</c> if [echo input]; otherwise, <c>false</c>.
		/// </value>
		bool EchoInput { get; }
		/// <summary>
		/// Gets a value indicating whether this instance is process running.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is process running; otherwise, <c>false</c>.
		/// </value>
		bool IsProcessRunning { get; }
		/// <summary>
		/// Writes the input.
		/// </summary>
		/// <param name="input">The input.</param>
		void WriteInput (string input);
		/// <summary>
		/// Writes the output.
		/// </summary>
		/// <param name="output">The output.</param>
		void WriteOutput (string output);
		/// <summary>
		/// Starts the process.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="args">The arguments.</param>
		void StartProcess (string command, string args);
		/// <summary>
		/// Stops the process.
		/// </summary>
		void StopProcess ();
  }

}
