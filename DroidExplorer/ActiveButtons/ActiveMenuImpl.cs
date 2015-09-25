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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DroidExplorer.ActiveButtons.Themes;

namespace DroidExplorer.ActiveButtons {
	/// <summary>
	/// 	The menu for handling the render of buttons over the title bar.
	/// </summary>
	internal class ActiveMenuImpl : Form, IActiveMenu {
		/// <summary>
		/// 	A internal hashtable of instances against form objects to 
		/// 	ensure only one instance is even created per form.
		/// </summary>
		private static readonly Dictionary<Form, IActiveMenu> parents;

		private readonly IContainer components;

		/// <summary>
		/// 	The internal list of buttons to be rendered by the menu instance.
		/// </summary>
		private readonly ActiveItemsImpl items;

		private readonly Size originalMinSize;

		/// <summary>
		/// 	The instance's parent form to which it's attached.
		/// </summary>
		private readonly Form parentForm;

		private readonly SpillOverMode spillOverMode;
		private readonly ThemeFactory themeFactory;

		/// <summary>
		/// 	Stores the max width of the menu control, as this resizes when buttons
		/// 	are hidden and can throw out visibility calcs later if we don't store.
		/// </summary>
		private int containerMaxWidth;

		private bool isActivated;
		private ITheme theme;
		private ToolTip tooltip;

		static ActiveMenuImpl() {
			parents = new Dictionary<Form, IActiveMenu>();
		}

		/// <summary>
		/// 	Constructor sets up the menu and sets the required properties
		/// 	in order thay this may be displayed over the top of it's parent 
		/// 	form.
		/// </summary>
		private ActiveMenuImpl(Form form) {
			InitializeComponent();
			items = new ActiveItemsImpl();
			items.CollectionModified += ItemsCollectionModified;
			parentForm = form;
			Show(form);
			parentForm.Disposed += ParentFormDisposed;
			Visible = false;
			isActivated = form.WindowState != FormWindowState.Minimized;
			themeFactory = new ThemeFactory(form);
			theme = themeFactory.GetTheme();
			originalMinSize = form.MinimumSize;
			AttachHandlers();
			ToolTip.ShowAlways = true;
			TopMost = form.TopMost;
			TopMost = false;
			spillOverMode = SpillOverMode.IncreaseSize;
		}

		/// <summary>
		/// 	Sets up the window as a child window, so that it does not take focus
		/// 	from the parent when clicked. This is attached to the desktop, as it doesn't
		/// 	allow us to attach to the parent at this time. Subsequent
		/// 	calls in the constructor will change the window's parent after the handle 
		/// 	has been created. This sequence is important to ensure the child is attached
		/// 	and respects the z-ordering of the parent.
		/// </summary>
		protected override CreateParams CreateParams {
			get {
				CreateParams p = base.CreateParams;
				p.Style = (int)Win32.WS_CHILD;
				p.Style |= (int)Win32.WS_CLIPSIBLINGS;
				p.ExStyle &= (int)Win32.WS_EX_LAYERED;
				p.Parent = Win32.GetDesktopWindow();
				return p;
			}
		}

		#region IActiveMenu Members

		/// <summary>
		/// 	Gets the list of buttons for this menu instance.
		/// </summary>
		public IActiveItems Items {
			get { return items; }
		}

		/// <summary>
		/// 	Gets or sets the tool tip control used for rendering tool tips.
		/// </summary>
		/// <value>The tool tip settings.</value>
		public ToolTip ToolTip {
			get { return tooltip ?? (tooltip = new ToolTip()); }
			set { tooltip = value; }
		}

		#endregion

		/// <summary>
		/// 	Creates or returns the menu instance for a given form.
		/// </summary>
		public static IActiveMenu GetInstance(Form form) {
			if(!parents.ContainsKey(form)) {
				parents.Add(form, new ActiveMenuImpl(form));
			}
			return parents[form];
		}

		/// <summary>
		/// 	Raises the <see cref = "E:System.Windows.Forms.Form.Load"></see> event.
		/// </summary>
		/// <param name = "e">An <see cref = "T:System.EventArgs"></see> that contains the event data.</param>
		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			base.BringToFront();
		}

		/// <summary>
		/// 	Handles the CollectionModified event of the items control.
		/// </summary>
		/// <param name = "sender">The source of the event.</param>
		/// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
		private void ItemsCollectionModified(object sender, ListModificationEventArgs e) {
			Controls.Clear();
			foreach(ActiveButton button in Items) {
				Controls.Add(button);
			}
			CalcSize();
			OnPosition();
		}

		/// <summary>
		/// 	Remove the parent from the hashtable when disposed.
		/// </summary>
		private void ParentFormDisposed(object sender, EventArgs e) {
			var form = (Form)sender;
			if(form == null) {
				return;
			}
			if(parents.ContainsKey(form)) {
				parents.Remove(form);
			}
		}

		/// <summary>
		/// 	Setup the handlers to reposition and resize the buttons when the parent
		/// 	is resized or styles are changed.
		/// </summary>
		protected void AttachHandlers() {
			parentForm.Deactivate += ParentFormDeactivate;
			parentForm.Activated += ParentFormActivated;
			parentForm.SizeChanged += ParentRefresh;
			parentForm.VisibleChanged += ParentRefresh;
			parentForm.Move += ParentRefresh;
			parentForm.SystemColorsChanged += TitleButtonSystemColorsChanged;
			// used to mask the menu control behind the buttons.
			if(Win32.DwmIsCompositionEnabled) {
				BackColor = Color.Fuchsia;
				TransparencyKey = Color.Fuchsia;
			} else {
				BackColor = Color.FromKnownColor(KnownColor.ActiveCaption);
				TransparencyKey = BackColor;
			}
		}

		/// <summary>
		/// 	Disables the tootips when the parent is activated.
		/// </summary>
		/// <param name = "sender">The source of the event.</param>
		/// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
		private void ParentFormDeactivate(object sender, EventArgs e) {
			ToolTip.ShowAlways = false;
		}

		/// <summary>
		/// 	Enabled the tooltip control when the parent form is active. This is necessary
		/// 	because the menu form is never active.
		/// </summary>
		/// <param name = "sender">The source of the event.</param>
		/// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
		private void ParentFormActivated(object sender, EventArgs e) {
			ToolTip.ShowAlways = true;
		}

		/// <summary>
		/// 	When the style is changed we need to re-calc button sizes as well as positions.
		/// </summary>
		private void TitleButtonSystemColorsChanged(object sender, EventArgs e) {
			theme = themeFactory.GetTheme();
			CalcSize();
			OnPosition();
		}

		/// <summary>
		/// 	Work out the buttons sizes based of sys diamensions. This doesn't work quite as expected
		/// 	as the buttons seem to have larger borders, which change per theme.
		/// </summary>
		private void CalcSize() {
			int left = 0;
			for(int i = (Items.Count - 1); i >= 0; i--) {
				var button = (ThemedItem)Items[i];
				button.Theme = theme;
				button.Left = left;
				left += Items[i].Width + theme.ButtonOffset.X;
				button.Top = theme.ButtonOffset.Y;
			}
			containerMaxWidth = left;

			if(spillOverMode == SpillOverMode.IncreaseSize) {
				int w = containerMaxWidth + theme.ControlBoxSize.Width + theme.FrameBorder.Width +
								theme.FrameBorder.Width;

				parentForm.MinimumSize = originalMinSize;

				if(parentForm.MinimumSize.Width <= w) {
					parentForm.MinimumSize = new Size(w, parentForm.MinimumSize.Height);
				}
			}
		}

		/// <summary>
		/// 	Handle changes to the parent, and make sure the menu is aligned to match.
		/// </summary>
		protected void ParentRefresh(object sender, EventArgs e) {
			if(parentForm.WindowState == FormWindowState.Minimized) {
				isActivated = false;
				Visible = false;
			} else {
				isActivated = true;
				OnPosition();
			}
		}

		/// <summary>
		/// 	Position the menu into the correct location, this varies per theme.
		/// </summary>
		private void OnPosition() {
			if(!IsDisposed) {
				if(theme == null || !theme.IsDisplayed) {
					Visible = false;
					return;
				}

				int top = theme.FrameBorder.Height;
				int left = theme.FrameBorder.Width + theme.ControlBoxSize.Width;

				Top = top + parentForm.Top;
				Left = parentForm.Left + parentForm.Width - containerMaxWidth - left;

				Visible = theme.IsDisplayed && isActivated;

				if(Visible) {
					if(Items.Count > 0) {
						Opacity = parentForm.Opacity;
						if(parentForm.Visible) {
							Opacity = parentForm.Opacity;
						} else {
							Visible = false;
						}
					}
					if(spillOverMode == SpillOverMode.Hide) {
						foreach(ActiveButton b in Items) {
							if(b.Left + Left - theme.FrameBorder.Width + 2 < parentForm.Left) {
								b.Visible = false;
							} else {
								b.Visible = true;
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// 	Clean up any resources being used.
		/// </summary>
		/// <param name = "disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// 	The standard properties of the menu. Changing properties or when they are set
		/// 	can effect the ability to attach to a parent, and leave the menu flaoting on the
		/// 	desktop.
		/// </summary>
		private void InitializeComponent() {
			SuspendLayout();
			AutoSize = true;
			AutoSizeMode = AutoSizeMode.GrowAndShrink;
			ClientSize = new Size(10, 10);
			ControlBox = false;
			FormBorderStyle = FormBorderStyle.None;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "ActiveMenu";
			ShowIcon = false;
			ShowInTaskbar = false;
			SizeGripStyle = SizeGripStyle.Hide;
			ResumeLayout(false);
		}
	}
}
