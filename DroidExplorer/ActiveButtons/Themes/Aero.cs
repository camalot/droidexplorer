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
using System.Drawing;
using System.Windows.Forms;

namespace DroidExplorer.ActiveButtons.Themes {
	internal class Aero : ThemeBase {
		private Size maxFrameBorder = Size.Empty;
		private Size minFrameBorder = Size.Empty;

		public Aero(Form form)
			: base(form) {
		}

		public override Color BackColor {
			get { return Color.Transparent; }
		}

		public override Size ControlBoxSize {
			get {
				if(base.controlBoxSize == Size.Empty) {
					if(IsToolbar) {
						if(form.ControlBox) {
							base.controlBoxSize = new Size(SystemButtonSize.Width, SystemButtonSize.Height);
						} else {
							base.controlBoxSize = new Size(1, 0);
						}
					} else {
						if(!form.MaximizeBox && !form.MinimizeBox && form.ControlBox) {
							if(form.HelpButton) {
								base.controlBoxSize = new Size((2 * SystemButtonSize.Width) + 7, SystemButtonSize.Height);
							} else {
								base.controlBoxSize = new Size((1 * SystemButtonSize.Width) + 13, SystemButtonSize.Height);
							}
						} else {
							int index;
							index = (form.ControlBox) ? 3 : 0;
							base.controlBoxSize = new Size(index * SystemButtonSize.Width, SystemButtonSize.Height);
						}
					}
				}
				return base.controlBoxSize;
			}
		}

		public override Point ButtonOffset {
			get {
				if(base.buttonOffset == Point.Empty) {
					if(IsToolbar) {
						base.buttonOffset = new Point(0, 0);
					} else {
						base.buttonOffset = new Point(0, -2);
					}
				}
				return base.buttonOffset;
			}
		}

		public override Size FrameBorder {
			get {
				if(form.WindowState == FormWindowState.Maximized) {
					if(maxFrameBorder == Size.Empty) {
						switch(form.FormBorderStyle) {
							case FormBorderStyle.FixedToolWindow:
								maxFrameBorder = new Size(SystemInformation.FrameBorderSize.Width - 8, -1);
								break;
							case FormBorderStyle.SizableToolWindow:
								maxFrameBorder = new Size(SystemInformation.FrameBorderSize.Width - 3, 4);
								break;
							case FormBorderStyle.Sizable:
								maxFrameBorder = new Size(SystemInformation.FrameBorderSize.Width + 2, 7);
								break;
							default:
								maxFrameBorder = new Size(SystemInformation.FrameBorderSize.Width - 3, 2);
								break;
						}
					}
					return maxFrameBorder;
				} else {
					if(minFrameBorder == Size.Empty) {
						switch(form.FormBorderStyle) {
							case FormBorderStyle.FixedToolWindow:
								minFrameBorder = new Size(SystemInformation.FrameBorderSize.Width - 8, -1);
								break;
							case FormBorderStyle.SizableToolWindow:
								minFrameBorder = new Size(SystemInformation.FrameBorderSize.Width - 3, 4);
								break;
							case FormBorderStyle.Sizable:
								minFrameBorder = new Size(SystemInformation.FrameBorderSize.Width - 3, 1);
								break;
							case FormBorderStyle.Fixed3D:
								minFrameBorder = new Size(SystemInformation.Border3DSize.Width, -4);
								break;
							case FormBorderStyle.FixedSingle:
								minFrameBorder = new Size(SystemInformation.Border3DSize.Width - 2, -4);
								break;
							default:
								minFrameBorder = new Size(SystemInformation.Border3DSize.Width - 1, -4);
								break;
						}
					}
					return minFrameBorder;
				}
			}
		}

		public override Size SystemButtonSize {
			get {
				if(base.systemButtonSize == Size.Empty) {
					if(IsToolbar) {
						Size size = SystemInformation.SmallCaptionButtonSize;
						size.Height += 2;
						size.Width += 2;
						base.systemButtonSize = size;
					} else {
						Size size = SystemInformation.CaptionButtonSize;
						size.Height += 1;
						//size.Width -= 1;
						base.systemButtonSize = size;
					}
				}
				return base.systemButtonSize;
			}
		}
	}
}