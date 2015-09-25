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

using DroidExplorer.ActiveButtons.Themes;
namespace DroidExplorer.ActiveButtons {
	/// <summary>
	/// 	Internal interface used to set properties of the ActiveItem
	/// 	instances, and expose the current instance of ITheme. This allows
	/// 	buttons to be correctly positioned with the menu.
	/// </summary>
	internal interface ThemedItem {
		/// <summary>
		/// 	Gets or sets the theme.
		/// </summary>
		/// <value>The theme.</value>
		ITheme Theme { get; set; }

		/// <summary>
		/// 	Gets the height of the control.
		/// </summary>
		/// <value></value>
		/// <returns>The height of the control in pixels.</returns>
		int Top { get; set; }

		/// <summary>
		/// 	Gets the distance, in pixels, between the left edge of the control and the left edge of its container's client area.
		/// </summary>
		/// <value></value>
		/// <returns>An <see cref = "T:System.Int32"></see> representing the distance, in pixels, between the left edge of the control and the left edge of its container's client area.</returns>
		int Left { get; set; }
	}
}