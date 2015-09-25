using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Text;
using DroidExplorer.Core.UI.CConsole.Api;
using Camalot.Common.Extensions;

namespace DroidExplorer.Core.UI.CConsole {

	/// <summary>
	/// The console event handler is used for console events.
	/// </summary>
	/// <param name="sender">The sender.</param>
	/// <param name="args">The <see cref="ConsoleEventArgs"/> instance containing the event data.</param>
	public delegate void ConsoleEventHandler(object sender, ConsoleEventArgs args);

	/// <summary>
	/// The Console Control allows you to embed a basic console in your application.
	/// </summary>
	[ToolboxBitmap(typeof(Resfinder), "DroidExplorer.Core.UI.CConsole.ConsoleControl.bmp")]
	public partial class ConsoleControl : UserControl {

		private Color _caretColor = Color.FromArgb(0, 0, 192, 0);
		private Size _caretSize = new Size(8, 14);
		private Color _defaultForeColor;
		private bool _isDisposed;
		private delegate IntPtr GetControlHandleDelegate(Control ctrl);
		private readonly ProcessInterface processInterace = new ProcessInterface();
		private bool isInputEnabled = true;
		private string lastInput;
		private List<KeyMapping> keyMappings = new List<KeyMapping>();
		public event ConsoleEventHandler OnConsoleOutput;
		public event ConsoleEventHandler OnConsoleInput;
		public event ConsoleEventHandler OnProcessExit;


		/// <summary>
		/// Initializes a new instance of the <see cref="ConsoleControl"/> class.
		/// </summary>
		public ConsoleControl() {
			//  Initialise the component.
			InitializeComponent();

			DefaultForeColor = Color.FromArgb(0, 0, 192, 0);
			CaretColor = Color.FromArgb(0, 0, 192, 0);
			ErrorColor = Color.FromArgb(0, 192, 0, 0);
			OutputProcessor = new DefaultConsoleOutputProcessor(this);
			CaretSize = new Size(8, 16);
			CommandHistory = new List<string>();
			HistoryIndex = -1;

			//  Show diagnostics disabled by default.
			ShowDiagnostics = false;

			//  Input enabled by default.
			IsInputEnabled = true;

			//  Disable special commands by default.
			SendKeyboardCommandsToProcess = false;

			//  Initialise the keymappings.
			InitialiseKeyMappings();

			//  Handle process events.
			processInterace.OnProcessOutput += processInterace_OnProcessOutput;
			processInterace.OnProcessError += processInterace_OnProcessError;
			processInterace.OnProcessInput += processInterace_OnProcessInput;
			processInterace.OnProcessExit += processInterace_OnProcessExit;

			//  Wait for key down messages on the rich text box.
			richTextBoxConsole.KeyDown += richTextBoxConsole_KeyDown;
			richTextBoxConsole.MouseUp += richTextBoxConsole_MouseUp;
			richTextBoxConsole.MouseDown += richTextBoxConsole_MouseDown;
			richTextBoxConsole.HandleCreated += richTextBoxConsole_HandleCreated;
			richTextBoxConsole.GotFocus += richTextBoxConsole_GotFocus;
			richTextBoxConsole.SelectionChanged += richTextBoxConsole_SelectionChanged;
		}


		/// <summary>
		/// Handles the OnProcessError event of the processInterace control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="args">The <see cref="ProcessEventArgs"/> instance containing the event data.</param>
		void processInterace_OnProcessError(object sender, ProcessEventArgs args) {
			//  Write the output, in red
			WriteOutput(args.Content, ErrorColor);

			//  Fire the output event.
			FireConsoleOutputEvent(args.Content);
		}

		/// <summary>
		/// Handles the OnProcessOutput event of the processInterace control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="args">The <see cref="ProcessEventArgs"/> instance containing the event data.</param>
		void processInterace_OnProcessOutput(object sender, ProcessEventArgs args) {
			//  Write the output, in white
			WriteOutput(args.Content);

			//  Fire the output event.
			FireConsoleOutputEvent(args.Content);
		}

		/// <summary>
		/// Handles the OnProcessInput event of the processInterace control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="args">The <see cref="ProcessEventArgs"/> instance containing the event data.</param>
		void processInterace_OnProcessInput(object sender, ProcessEventArgs args) {

		}

		/// <summary>
		/// Handles the OnProcessExit event of the processInterace control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="args">The <see cref="ProcessEventArgs"/> instance containing the event data.</param>
		void processInterace_OnProcessExit(object sender, ProcessEventArgs args) {
			try {
				if(!this.IsHandleCreated)
					return;

				//  Are we showing diagnostics?
				if(ShowDiagnostics && !this._isDisposed) {
					WriteOutput(Environment.NewLine + processInterace.ProcessFileName + " exited.", Color.FromArgb(255, 0, 255, 0));
				}

				if(this.InvokeRequired) {
					//  Read only again.
					Invoke((Action)(() => {
						richTextBoxConsole.ReadOnly = true;
					}));
				} else {
					richTextBoxConsole.ReadOnly = true;
				}

				// trigger process exit.
				if(this.OnProcessExit != null) {
					this.OnProcessExit(this, new ConsoleEventArgs());
				}
			} catch(Exception ex) {
				// if the window has been closed this blows up.
			}
		}

		/// <summary>
		/// Initialises the key mappings.
		/// </summary>
		private void InitialiseKeyMappings() {
			//  Map 'tab'.
			keyMappings.Add(new KeyMapping(false, false, false, Keys.Tab, "{TAB}", "\t"));

			//  Map 'Ctrl-C'.
			keyMappings.Add(new KeyMapping(true, false, false, Keys.C, "^(c)", "\x03\r\n"));

		}

		void richTextBoxConsole_SelectionChanged(object sender, EventArgs e) {
			ShowHideCaret();
		}

		void richTextBoxConsole_GotFocus(object sender, EventArgs e) {
			ShowHideCaret();
		}

		void richTextBoxConsole_HandleCreated(object sender, EventArgs e) {
			ShowHideCaret();
		}

		void richTextBoxConsole_MouseDown(object sender, MouseEventArgs e) {
			/*if(e.Button == System.Windows.Forms.MouseButtons.Left) {
				Console.WriteLine(this.richTextBoxConsole.GetCharIndexFromPosition(e.Location));
				Console.WriteLine(InputStart);
				if(this.richTextBoxConsole.GetCharIndexFromPosition(e.Location) < InputStart) {
					richTextBoxConsole.Select(richTextBoxConsole.Text.Length, 0);
				}
			}*/
			ShowHideCaret();
		}

		void richTextBoxConsole_MouseUp(object sender, MouseEventArgs e) {

			ShowHideCaret();
		}


		/// <summary>
		/// Handles the KeyDown event of the richTextBoxConsole control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
		void richTextBoxConsole_KeyDown(object sender, KeyEventArgs e) {
			//  Are we sending keyboard commands to the process?
			if(SendKeyboardCommandsToProcess && IsProcessRunning) {
				//  Get key mappings for this key event?
				var mappings = from k in keyMappings
											 where
											 (k.KeyCode == e.KeyCode &&
											 k.IsAltPressed == e.Alt &&
											 k.IsControlPressed == e.Control &&
											 k.IsShiftPressed == e.Shift)
											 select k;

				//  Go through each mapping, send the message.
				//foreach(var mapping in mappings) {
				//SendKeysEx.SendKeys(CurrentProcessHwnd, mapping.SendKeysMapping);
				//inputWriter.WriteLine(mapping.StreamMapping);
				//WriteInput("\x3", Color.White, false);
				//}

				//  If we handled a mapping, we're done here.
				if(mappings.Any()) {
					e.SuppressKeyPress = true;
					return;
				}
			}

			//  If we're at the input point and it's backspace, bail.
			if((richTextBoxConsole.SelectionStart <= InputStart) && e.KeyCode == Keys.Back) e.SuppressKeyPress = true;

			//  Are we in the read-only zone?
			if(richTextBoxConsole.SelectionStart < InputStart) {
				//  Allow arrows and Ctrl-C.
				if(!(e.KeyCode == Keys.Left ||
						e.KeyCode == Keys.Right ||
						e.KeyCode == Keys.Up ||
						e.KeyCode == Keys.Down ||
						(e.KeyCode == Keys.C && e.Control))) {
					e.SuppressKeyPress = true;
				}

			} else {
				if(e.KeyCode == Keys.Up) {
					if(HistoryIndex >= 0) {
						HistoryIndex--;
					}
					ScrollHistory();
					e.SuppressKeyPress = true;
				} else if(e.KeyCode == Keys.Down) {
					if(HistoryIndex < CommandHistory.Count) {
						HistoryIndex++;
					}
					ScrollHistory();
					e.SuppressKeyPress = true;
				}
			}

			//  Is it the return key?
			if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return) {

				//  Get the input.
				// move to the end...
				string input = richTextBoxConsole.Text.Substring(InputStart, (richTextBoxConsole.SelectionStart) - InputStart);

				if(!string.IsNullOrWhiteSpace(input)) {
					richTextBoxConsole.Select(richTextBoxConsole.Text.Length, 0);
					//  Write the input (without echoing).
					WriteInput(input, DefaultForeColor, false);

					CommandHistory.Add(input);
					HistoryIndex = 0;
				} else {
					// nothing there, ignore?

					e.SuppressKeyPress = true;
				}
			}

			ShowHideCaret();
		}

		/// <summary>
		/// Writes the output to the console control.
		/// </summary>
		/// <param name="output">The output.</param>
		/// <param name="color">The color.</param>
		public void WriteOutput(string output, Color color) {
			try {
				if(!this.IsHandleCreated || this.IsDisposed || this._isDisposed)
					return;
				Invoke((Action)(() => {

					if(this.IsDisposed || !this.IsHandleCreated || this._isDisposed) {
						return;
					}

					if(string.IsNullOrEmpty(lastInput) == false &&
							(output == lastInput || output.Replace("\r\n", "") == lastInput)) {
						return;
					}

					//  Write the output.
					richTextBoxConsole.SelectionColor = color;
					richTextBoxConsole.SelectedText += output.REReplace("[\r]{1,}\n?", Environment.NewLine);
					InputStart = richTextBoxConsole.SelectionStart;

					Imports.SendMessage(richTextBoxConsole.Handle, Imports.WmVscroll, Imports.SbBottom, 0x0);

				}));
			} catch(ObjectDisposedException odex) {
				// I don't know why it keeps throwing this but it does.
			}
		}


		public void WriteOutput(string output) {
			try {
				if(this.IsDisposed || !this.IsHandleCreated || this._isDisposed) {
					return;
				}

				Invoke((Action)(() => {
					if(this.IsDisposed || !this.IsHandleCreated || this._isDisposed) {
						return;
					}
					if(string.IsNullOrEmpty(lastInput) == false &&
							(output == lastInput || output.Replace("\r\n", "") == lastInput))
						return;
					this.OutputProcessor.Process(output);
					Imports.SendMessage(richTextBoxConsole.Handle, Imports.WmVscroll, Imports.SbBottom, 0x0);
				}));
			} catch(ObjectDisposedException odex) {
				// I don't know why it keeps throwing this but it does.
			}


		}

		private void ScrollHistory() {
			richTextBoxConsole.Select(InputStart, richTextBoxConsole.Text.Length - InputStart);
			if(HistoryIndex < 0) {
				// past the end
				richTextBoxConsole.SelectedText = string.Empty;
				HistoryIndex = 0;
			} else if(HistoryIndex >= CommandHistory.Count) {
				// past the end
				richTextBoxConsole.SelectedText = string.Empty;
				HistoryIndex = CommandHistory.Count;
			} else {
				richTextBoxConsole.SelectedText = CommandHistory[HistoryIndex];
			}
			richTextBoxConsole.Select(richTextBoxConsole.Text.Length, 0);
		}



		/// <summary>
		/// Clears the output.
		/// </summary>
		public void ClearOutput() {
			richTextBoxConsole.Clear();
			InputStart = 0;
		}

		/// <summary>
		/// Writes the input to the console control.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <param name="color">The color.</param>
		/// <param name="echo">if set to <c>true</c> echo the input.</param>
		public void WriteInput(string input, Color color, bool echo) {
			Invoke((Action)(() => {
				if(!this.IsHandleCreated || this.IsDisposed || this._isDisposed || string.IsNullOrWhiteSpace(input)) {
					return;
				}

				//  Are we echoing?
				if(echo) {
					richTextBoxConsole.SelectionColor = color;
					richTextBoxConsole.SelectedText += input;
					InputStart = richTextBoxConsole.SelectionStart;
				}

				lastInput = input;

				//  Write the input.
				processInterace.WriteInput(input);

				//  Fire the event.
				FireConsoleInputEvent(input);
			}));
		}

		public void WriteInput(string input) {
			WriteInput(input, DefaultForeColor, false);
		}

		/// <summary>
		/// Runs a process.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="arguments">The arguments.</param>
		public void StartProcess(string fileName, string arguments) {
			//  Are we showing diagnostics?
			if(ShowDiagnostics) {
				WriteOutput("Preparing to run " + fileName, Color.FromArgb(255, 0, 255, 0));
				if(!string.IsNullOrEmpty(arguments))
					WriteOutput(" with arguments " + arguments + "." + Environment.NewLine, Color.FromArgb(255, 0, 255, 0));
				else
					WriteOutput("." + Environment.NewLine, Color.FromArgb(255, 0, 255, 0));
			}

			//  Start the process.
			processInterace.StartProcess(fileName, arguments);

			//  If we enable input, make the control not read only.
			if(IsInputEnabled) {
				if(InvokeRequired) {
					Invoke((Action)(() => {
						richTextBoxConsole.ReadOnly = false;
					}));
				} else {
					richTextBoxConsole.ReadOnly = false;
				}
			}
		}

		/// <summary>
		/// Stops the process.
		/// </summary>
		public void StopProcess() {
			//  Stop the interface.
			processInterace.StopProcess();
		}

		/// <summary>
		/// Fires the console output event.
		/// </summary>
		/// <param name="content">The content.</param>
		private void FireConsoleOutputEvent(string content) {
			//  Get the event.
			var theEvent = OnConsoleOutput;
			if(theEvent != null)
				theEvent(this, new ConsoleEventArgs(content));
		}

		/// <summary>
		/// Fires the console input event.
		/// </summary>
		/// <param name="content">The content.</param>
		private void FireConsoleInputEvent(string content) {
			//  Get the event.
			var theEvent = OnConsoleInput;
			if(theEvent != null)
				theEvent(this, new ConsoleEventArgs(content));
		}


		/// <summary>
		/// Gets or sets the input start.
		/// </summary>
		/// <value>
		/// The input start.
		/// </value>
		public int InputStart { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to show diagnostics.
		/// </summary>
		/// <value>
		///   <c>true</c> if show diagnostics; otherwise, <c>false</c>.
		/// </value>
		[Category("Console Control"), Description("Show diagnostic information, such as exceptions.")]
		public bool ShowDiagnostics { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is input enabled.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is input enabled; otherwise, <c>false</c>.
		/// </value>
		[Category("Console Control"), Description("If true, the user can key in input.")]
		public bool IsInputEnabled {
			get { return isInputEnabled; }
			set {
				isInputEnabled = value;
				richTextBoxConsole.ReadOnly = !value;
			}
		}

		/// <summary>
		/// Gets or sets the output processor.
		/// </summary>
		/// <value>
		/// The output processor.
		/// </value>
		public IConsoleOutputProcessor OutputProcessor { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [send keyboard commands to process].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [send keyboard commands to process]; otherwise, <c>false</c>.
		/// </value>
		[Category("Console Control"), Description("If true, special keyboard commands like Ctrl-C and tab are sent to the process.")]
		public bool SendKeyboardCommandsToProcess { get; set; }

		/// <summary>
		/// Gets a value indicating whether this instance is process running.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is process running; otherwise, <c>false</c>.
		/// </value>
		[Browsable(false)]
		public bool IsProcessRunning {
			get { return processInterace.IsProcessRunning; }
		}

		/// <summary>
		/// Gets the internal rich text box.
		/// </summary>
		[Browsable(false)]
		public RichTextBox InternalRichTextBox {
			get { return richTextBoxConsole; }
		}

		/// <summary>
		/// Gets the process interface.
		/// </summary>
		[Browsable(false)]
		public ProcessInterface ProcessInterface {
			get { return processInterace; }
		}

		/// <summary>
		/// Gets the key mappings.
		/// </summary>
		[Browsable(false)]
		public List<KeyMapping> KeyMappings {
			get { return keyMappings; }
		}

		/// <summary>
		/// Gets or sets the font of the text displayed by the control.
		/// </summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
		///   <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   </PermissionSet>
		public override Font Font {
			get {
				//  Return the base class font.
				return base.Font;
			}
			set {
				//  Set the base class font...
				base.Font = value;

				//  ...and the internal control font.
				richTextBoxConsole.Font = value;
			}
		}

		/// <summary>
		/// Gets or sets the background color for the control.
		/// </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		///   <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   </PermissionSet>
		public override Color BackColor {
			get {
				//  Return the base class background.
				return base.BackColor;
			}
			set {
				//  Set the base class background...
				base.BackColor = value;
				this.SelectionBackColor = value;
				//  ...and the internal control background.
				richTextBoxConsole.BackColor = value;
			}
		}

		public Color SelectionBackColor { get; set; }

		public override Color ForeColor {
			get {
				return base.ForeColor;
			}
			set {
				base.ForeColor = value;
			}
		}

		public Color ErrorColor { get; set; }
		/// <summary>
		/// Gets or sets the default color of the fore.
		/// </summary>
		/// <value>
		/// The default color of the fore.
		/// </value>
		public new Color DefaultForeColor {
			get {
				return _defaultForeColor;
			}
			set {
				_defaultForeColor = value;
				ForeColor = _defaultForeColor;
				CaretColor = _defaultForeColor;
				this.InvalidateCaret();
			}
		}


		/// <summary>
		/// Gets or sets the color of the caret.
		/// </summary>
		/// <value>
		/// The color of the caret.
		/// </value>
		public Color CaretColor {
			get {
				return _caretColor;
			}
			set {
				_caretColor = value;
				this.InvalidateCaret();
			}
		}

		private int HistoryIndex { get; set; }
		private List<string> CommandHistory { get; set; }


		private Size CaretSize {
			get {
				return _caretSize;
			}
			set {
				_caretSize = value;
				this.InvalidateCaret();
			}
		}

		private Bitmap Caret { get; set; }

		private void InvalidateCaret() {
			if(Caret != null) {
				Caret.Dispose();
				Caret = null;
			}

			Caret = new Bitmap(CaretSize.Width, CaretSize.Height);
			using(Graphics g = Graphics.FromImage(Caret)) {
				using(SolidBrush b = new SolidBrush(this.CaretColor)) {
					g.FillRectangle(b, new Rectangle(0, 0, Caret.Width, Caret.Height));
				}
			}
			ShowHideCaret();
		}

		private void InternalDispose(bool disposing) {
			if(disposing) {
				this._isDisposed = true;
			}
		}

		private void ShowHideCaret() {
			if(Caret == null) {
				InvalidateCaret();
			}
			IntPtr handle = IntPtr.Zero;
			if(this.InvokeRequired) {
				handle = (IntPtr)this.Invoke(new GetControlHandleDelegate(this.GetControlHandle), this.richTextBoxConsole);
			} else {
				handle = GetControlHandle(this.richTextBoxConsole);
			}

			Imports.HideCaret(handle);
			Imports.CreateCaret(handle, this.Caret.GetHbitmap(), CaretSize.Width, CaretSize.Height);
			Imports.ShowCaret(handle);
		}

		private IntPtr GetControlHandle(Control ctrl) {
			if(ctrl != null && !ctrl.IsDisposed) {
				return ctrl.Handle;
			} else {
				return IntPtr.Zero;
			}
		}

	}

	/// <summary>
	/// Used to allow us to find resources properly.
	/// </summary>
	public class Resfinder { }

}
