/*=============================================================================
*
*	(C) Copyright 2011, Michael Carlisle (mike.carlisle@thecodeking.co.uk)
*
*   http://www.TheCodeKing.co.uk
*  
*	All rights reserved.
*	The code and information is provided "as-is" without waranty of any kind,
*	either expresed or implied.
*
*-----------------------------------------------------------------------------
*	History:
*		01/09/2007	Michael Carlisle				Version 1.0
*=============================================================================
*/
using System;
using System.Drawing;
using System.Windows.Forms;
using DroidExplorer.ActiveButtons.Themes;

namespace DroidExplorer.ActiveButtons {
	/// <summary>
	/// 	An instance of a button which can be added to the
	/// 	ActiveButtons menu.
	/// </summary>
	/// <example>
	/// 	The ActiveButton class can be used to create new
	/// 	button instances, which can be added to the current IActiveMenu. The
	/// 	Height and Width properties of the button class are auto-configured 
	/// 	based on the current platform and theme.
	/// 	<code>
	/// 		// get an instance of the menu for the current form
	/// 		IActiveMenu menu = ActiveMenu.GetInstance(this);
	/// 
	/// 		// create a new instance of ActiveButton
	/// 		ActiveButton button = new ActiveButton();
	/// 
	/// 		// set the button properties
	/// 		button.Text = "One";
	/// 		button.BackColor = Color.Red;
	/// 
	/// 		// attach button event handlers
	/// 		button.Click += new EventHandler(button_Click);
	/// 
	/// 		// add the button to the title bar menu
	/// 		menu.Items.Add(button);
	/// 	</code>
	/// </example>
	public class ActiveButton : Button, IActiveItem, ThemedItem {
		private Size buttonSize;
		private int buttonX;
		private int buttonY;
		private string text;
		private Size textSize;
		private ITheme theme;

		#region ThemedItem implementation

		/// <summary>
		/// 	Gets or sets the theme.
		/// </summary>
		/// <value>The theme.</value>
		ITheme ThemedItem.Theme {
			get { return theme; }
			set {
				theme = value;
				CalcButtonSize();
			}
		}

		/// <summary>
		/// 	Gets or sets the distance, in pixels, between the top edge of the control and the top edge of its container's client area.
		/// </summary>
		/// <value></value>
		/// <returns>An <see cref = "T:System.Int32"></see> representing the distance, in pixels, between the bottom edge of the control and the top edge of its container's client area.</returns>
		/// <PermissionSet><IPermission class = "System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Unrestricted = "true" /><IPermission class = "System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Unrestricted = "true" /><IPermission class = "System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Flags = "UnmanagedCode, ControlEvidence" /><IPermission class = "System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Unrestricted = "true" /></PermissionSet>
		int ThemedItem.Top {
			get { return base.Top; }
			set { base.Top = value; }
		}

		/// <summary>
		/// 	Gets or sets the distance, in pixels, between the left edge of the control and the left edge of its container's client area.
		/// </summary>
		/// <value></value>
		/// <returns>An <see cref = "T:System.Int32"></see> representing the distance, in pixels, between the left edge of the control and the left edge of its container's client area.</returns>
		/// <PermissionSet><IPermission class = "System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Unrestricted = "true" /><IPermission class = "System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Unrestricted = "true" /><IPermission class = "System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Flags = "UnmanagedCode, ControlEvidence" /><IPermission class = "System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Unrestricted = "true" /></PermissionSet>
		int ThemedItem.Left {
			get { return base.Left; }
			set { base.Left = value; }
		}

		#endregion

		/// <summary>
		/// 	Initializes a new instance of the <see cref = "ActiveButton" /> class.
		/// </summary>
		public ActiveButton() {
			Initialize();
		}

		/// <summary>
		/// 	Gets or sets the text property of the button control.
		/// </summary>
		/// <value>The text.</value>
		public new string Text {
			get { return text; }
			set {
				text = value;
				CalcButtonSize();
			}
		}

		/// <summary>
		/// 	Gets or sets the background color of the control.
		/// </summary>
		/// <value></value>
		/// <returns>A <see cref = "T:System.Drawing.Color"></see> value representing the background color.</returns>
		public new Color BackColor {
			get { return base.BackColor; }
			set { base.BackColor = value; }
		}

		#region IActiveItem Members

		/// <summary>
		/// 	Gets the distance, in pixels, between the top edge of the control and the top edge of its container's client area.
		/// </summary>
		/// <value></value>
		/// <returns>An <see cref = "T:System.Int32"></see> representing the distance, in pixels, between the bottom edge of the control and the top edge of its container's client area.</returns>
		/// <PermissionSet><IPermission class = "System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Unrestricted = "true" /><IPermission class = "System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Unrestricted = "true" /><IPermission class = "System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Flags = "UnmanagedCode, ControlEvidence" /><IPermission class = "System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Unrestricted = "true" /></PermissionSet>
		public new int Top {
			get { return base.Top; }
		}

		/// <summary>
		/// 	Gets the distance, in pixels, between the left edge of the control and the left edge of its container's client area.
		/// </summary>
		/// <value></value>
		/// <returns>An <see cref = "T:System.Int32"></see> representing the distance, in pixels, between the left edge of the control and the left edge of its container's client area.</returns>
		/// <PermissionSet><IPermission class = "System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Unrestricted = "true" /><IPermission class = "System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Unrestricted = "true" /><IPermission class = "System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Flags = "UnmanagedCode, ControlEvidence" /><IPermission class = "System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Unrestricted = "true" /></PermissionSet>
		public new int Left {
			get { return base.Left; }
		}

		/// <summary>
		/// 	Gets the width of the control.
		/// </summary>
		/// <value></value>
		/// <returns>The width of the control in pixels.</returns>
		/// <PermissionSet><IPermission class = "System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Unrestricted = "true" /><IPermission class = "System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Unrestricted = "true" /><IPermission class = "System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Flags = "UnmanagedCode, ControlEvidence" /><IPermission class = "System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Unrestricted = "true" /></PermissionSet>
		public new int Width {
			get { return base.Width; }
		}

		/// <summary>
		/// 	Gets the height of the control.
		/// </summary>
		/// <value></value>
		/// <returns>The height of the control in pixels.</returns>
		/// <PermissionSet><IPermission class = "System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Unrestricted = "true" /><IPermission class = "System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Unrestricted = "true" /><IPermission class = "System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Flags = "UnmanagedCode, ControlEvidence" /><IPermission class = "System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version = "1" Unrestricted = "true" /></PermissionSet>
		public new int Height {
			get { return base.Height; }
		}

		#endregion

		/// <summary>
		/// 	Initializes this instance.
		/// </summary>
		protected void Initialize() {
			if(Win32.DwmIsCompositionEnabled || Application.RenderWithVisualStyles) {
				base.BackColor = Color.Transparent;
			} else {
				base.BackColor = Color.FromKnownColor(KnownColor.Control);
			}
			Font = new Font(base.Font.FontFamily, 7.5F, FontStyle.Regular);
			base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			base.Paint += ActiveButton_Paint;
			base.SystemColorsChanged += ActiveButton_SystemColorsChanged;
			base.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			base.FlatAppearance.BorderSize = 0;
			CalcButtonSize();
		}

		/// <summary>
		/// 	Handles the SystemColorsChanged event of the ActiveButton control.
		/// </summary>
		/// <param name = "sender">The source of the event.</param>
		/// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
		private void ActiveButton_SystemColorsChanged(object sender, EventArgs e) {
			CalcButtonSize();
		}

		/// <summary>
		/// 	Calculates the size of the button.
		/// </summary>
		private void CalcButtonSize() {
			if(theme != null) {
				buttonSize = theme.SystemButtonSize;
				if(BackColor == Color.Empty) {
					BackColor = theme.BackColor;
				}
			} else {
				buttonSize = SystemInformation.CaptionButtonSize;
			}

			base.Width = buttonSize.Width;
			base.Height = buttonSize.Height;

			using(Graphics e = Graphics.FromHwnd(Handle)) {
				textSize = e.MeasureString(Text, Font).ToSize();
			}
			if(base.Width < textSize.Width + 6) {
				base.Width = textSize.Width + 6;
			}

			buttonX = (base.Width - textSize.Width) / 2 - 1;
			buttonY = (base.Height - textSize.Height) / 2 - 1;
		}

		/// <summary>
		/// 	Handles the Paint event of the ActiveButton control.
		/// </summary>
		/// <param name = "sender">The source of the event.</param>
		/// <param name = "e">The <see cref = "System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
		private void ActiveButton_Paint(object sender, PaintEventArgs e) {
			using(SolidBrush brush = new SolidBrush(base.ForeColor)) {
				e.Graphics.DrawString(Text, base.Font, brush, buttonX, buttonY);
			}
		}
	}
}