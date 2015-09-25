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
namespace DroidExplorer.ActiveButtons {
	/// <summary>
	/// 	Defines an ActiveMenu item.
	/// </summary>
	public interface IActiveItem {
		/// <summary>
		/// 	Gets the height of the control.
		/// </summary>
		/// <value></value>
		/// <returns>The height of the control in pixels.</returns>
		int Top { get; }

		/// <summary>
		/// 	Gets the distance, in pixels, between the left edge of the control and the left edge of its container's client area.
		/// </summary>
		/// <value></value>
		/// <returns>An <see cref = "T:System.Int32"></see> representing the distance, in pixels, between the left edge of the control and the left edge of its container's client area.</returns>
		int Left { get; }

		/// <summary>
		/// 	Gets the width of the control.
		/// </summary>
		/// <value></value>
		/// <returns>The width of the control in pixels.</returns>
		int Width { get; }

		/// <summary>
		/// 	Gets the height of the control.
		/// </summary>
		/// <value></value>
		/// <returns>The height of the control in pixels.</returns>
		int Height { get; }
	}
}