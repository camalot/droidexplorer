using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Core.UI;
using DroidExplorer.UI;
using DroidExplorer.Core.Plugins;
using DroidExplorer.Core;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Globalization;
using DroidExplorer.Core.UI.CConsole;
using Camalot.Common.Extensions;

namespace DroidExplorer.Plugins.UI {
	/// <summary>
	/// 
	/// </summary>
	public partial class ShellConsole : PluginForm, IShellProcess {
		private delegate int GetTextLengthDelegate();
		private delegate void SetSelectionStartDelegate(int start);
		private delegate void SetStatusLabelTextDelegate(ToolStripStatusLabel label, string text);
		private delegate IntPtr GetControlHandleDelegate(Control ctrl);
		private delegate int GetLineFromCharIndexDelegate(int index);
		private delegate int GetSelectionStartDelegate();
		private delegate int GetShellLinesLengthDelegate();
		private delegate string[] GetShellLinesDelegate();
		private delegate bool GenericBooleanDelegate();

		private Size _caretSize = new Size(8, 16);

		/// <summary>
		/// Initializes a new instance of the <see cref="ShellConsole"/> class.
		/// </summary>
		public ShellConsole(IPluginHost pluginHost) :base(pluginHost) {
			InitializeComponent ();
			this.shell.OnProcessExit += shell_OnProcessExit;
			this.shell.OutputProcessor = new LinuxConsoleOutputProcessor(this.shell);
			Run(String.Empty);
			this.deviceLabel.Text = PluginHost.CommandRunner.DefaultDevice;

		}


		void shell_OnProcessExit(object sender, Core.UI.CConsole.ConsoleEventArgs args) {
			Invoke((Action)(() => {
				this.Close();
			}));
		}


		#region Overrides

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Validating"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs"/> that contains the event data.</param>
		protected override void OnValidating(CancelEventArgs e) {
			base.OnValidating(e);
		}

		/// <summary>
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		protected override void OnResize(EventArgs e) {
			base.OnResize(e);
		}

		/// <summary>
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data.</param>
		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Enter"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		protected override void OnEnter(EventArgs e) {
			base.OnEnter(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.Activated"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		protected override void OnActivated(EventArgs e) {
			base.OnActivated(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.Closing"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs"/> that contains the event data.</param>
		protected override void OnClosing(CancelEventArgs e) {
			if(this.IsProcessRunning && !e.Cancel) {
				this.StopProcess();
			}
			base.OnClosing(e);
		}

		#endregion


		/// <summary>
		/// Sets the status label text.
		/// </summary>
		/// <param name="label">The label.</param>
		/// <param name="text">The text.</param>
		private void SetStatusLabelText(ToolStripStatusLabel label, string text) {
			label.Text = text;
		}

		/// <summary>
		/// Runs the specified command.
		/// </summary>
		/// <param name="command">The command.</param>
		public void Run(string command) {
			this.LogDebug("Run: {0}", command);
			//CommandRunner.Instance.LaunchRedirectedShellWindow ( CommandRunner.Instance.DefaultDevice, command, this );

			var args = this.PluginHost.CommandRunner.AdbCommandArguments( this.PluginHost.Device, CommandRunner.AdbCommand.Shell);
			var tool = FolderManagement.GetSdkTool (CommandRunner.ADB_COMMAND);
			this.shell.StartProcess(tool, args);
			if(!string.IsNullOrWhiteSpace(command)) {
				this.shell.WriteInput(command, Color.White, false);
			}

		}

		#region IShellProcess Members

		public bool EchoInput {
			get { return false; }
		}

		public bool IsProcessRunning {
			get { return this.shell.IsProcessRunning; }
		}

		public void WriteInput(string input) {
			this.shell.WriteInput(input);
		}

		public void WriteOutput(string output) {
			this.shell.WriteOutput(output);
		}

		public void StartProcess(string command, string args) {
			this.shell.StartProcess(command, args);
		}

		public void StopProcess() {
			this.shell.StopProcess();
		}
		#endregion



	}
}
