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
	internal class XPStyle : Styled {
		public XPStyle(Form form)
			: base(form) {
		}

		public override Color BackColor {
			get {
				if(base.backColor == Color.Empty) {
					base.backColor = Color.FromKnownColor(KnownColor.ActiveBorder);
				}
				return base.backColor;
			}
		}

		public override Size FrameBorder {
			get {
				if(base.frameBorder == Size.Empty) {
					base.frameBorder = new Size(base.FrameBorder.Width + 2, base.FrameBorder.Height);
				}
				return base.frameBorder;
			}
		}
	}
}