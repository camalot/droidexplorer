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

namespace DroidExplorer.ActiveButtons.Themes {
	internal class ThemeBase : ITheme {
		protected Color backColor = Color.Empty;
		protected Point buttonOffset = new Point(0, 0);
		protected Size controlBoxSize = Size.Empty;
		protected Form form;
		protected Size frameBorder = Size.Empty;
		protected bool? isDisplayed;
		protected bool? isToolbar;
		protected Size systemButtonSize = Size.Empty;

		public ThemeBase(Form form) {
			this.form = form;
		}

		protected bool IsToolbar {
			get {
				if(isToolbar == null) {
					isToolbar = form.FormBorderStyle == FormBorderStyle.FixedToolWindow ||
											form.FormBorderStyle == FormBorderStyle.SizableToolWindow;
				}
				return (bool)isToolbar;
			}
		}

		#region ITheme Members

		public virtual Color BackColor {
			get {
				if(backColor == Color.Empty) {
					backColor = Color.FromKnownColor(KnownColor.Control);
				}
				return backColor;
			}
		}


		public bool IsDisplayed {
			get {
				if(isDisplayed == null) {
					if((!form.ControlBox && string.IsNullOrEmpty(form.Text))
							|| form.FormBorderStyle == FormBorderStyle.None
							) {
						isDisplayed = false;
					} else {
						isDisplayed = true;
					}
				}
				return (bool)isDisplayed;
			}
		}

		public virtual Size ControlBoxSize {
			get {
				if(controlBoxSize == Size.Empty) {
					if(IsToolbar) {
						if(form.ControlBox) {
							controlBoxSize = new Size(SystemButtonSize.Width, SystemButtonSize.Height);
						} else {
							controlBoxSize = new Size(0, 0);
						}
					} else {
						int index;
						if(!form.MaximizeBox && !form.MinimizeBox && form.ControlBox) {
							index = (form.HelpButton) ? 2 : 1;
						} else {
							index = (form.ControlBox) ? 3 : 0;
						}
						controlBoxSize = new Size(index * SystemButtonSize.Width, SystemButtonSize.Height);
					}
				}
				return controlBoxSize;
			}
		}

		public virtual Point ButtonOffset {
			get { return buttonOffset; }
		}


		public virtual Size FrameBorder {
			get {
				if(frameBorder == Size.Empty) {
					switch(form.FormBorderStyle) {
						case FormBorderStyle.SizableToolWindow:
							frameBorder = new Size(SystemInformation.FrameBorderSize.Width + 2,
																		 SystemInformation.FrameBorderSize.Height + 2);
							break;
						case FormBorderStyle.Sizable:
							frameBorder = new Size(SystemInformation.FrameBorderSize.Width,
																		 SystemInformation.FrameBorderSize.Height + 2);
							break;
						case FormBorderStyle.FixedToolWindow:
							frameBorder = new Size(SystemInformation.Border3DSize.Width + 3,
																		 SystemInformation.Border3DSize.Height + 3);
							break;
						default:
							frameBorder = new Size(SystemInformation.Border3DSize.Width + 1,
																		 SystemInformation.Border3DSize.Height + 3);
							break;
					}
				}
				return frameBorder;
			}
		}

		public virtual Size SystemButtonSize {
			get {
				if(systemButtonSize == Size.Empty) {
					if(IsToolbar) {
						Size size = SystemInformation.ToolWindowCaptionButtonSize;
						size.Height -= 4;
						size.Width -= 1;
						systemButtonSize = size;
					} else {
						systemButtonSize = new Size(SystemInformation.CaptionButtonSize.Width,
																				SystemInformation.CaptionHeight - 2
																																					*
																																					Math.Max(
																																							SystemInformation.BorderSize.
																																									Height,
																																							SystemInformation.Border3DSize
																																									.Height)
																				- 1);
					}
				}
				return systemButtonSize;
			}
		}

		#endregion
	}
}