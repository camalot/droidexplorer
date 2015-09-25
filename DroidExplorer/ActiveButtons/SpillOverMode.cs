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
	/// 	Determines the behaviour of the <see cref = "T:ActiveButton"></see> 
	/// 	items when they spill over the edge of the title bar.
	/// </summary>
	internal enum SpillOverMode {
		/// <summary>
		/// 	Hide <see cref = "T:ActiveButton"></see> instances.
		/// </summary>
		Hide,
		/// <summary>
		/// 	Increase the minimum size of the parent form to compensate.
		/// </summary>
		IncreaseSize
	}
}