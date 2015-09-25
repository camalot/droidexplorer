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
using System.Windows.Forms;

namespace DroidExplorer.ActiveButtons {
	/// <summary>
	/// 	Provides access to the <see cref = "T:ActiveButton"></see> instances
	/// 	attached to the menu instance.
	/// </summary>
	/// <example>
	/// 	This sample shows how to add &amp; remove buttons form the IActiveMenu
	/// 	using the Items list.
	/// 	<code>
	/// 		// get an instance of the menu for the current form
	/// 		IActiveMenu menu = ActiveMenu.GetInstance(this);
	/// 
	/// 		// add button to front the menu
	/// 		menu.Items.Add(button);
	/// 
	/// 		// insert button at position 2
	/// 		menu.Items.Insert(2, button);
	/// 
	/// 		// remove specific button
	/// 		menu.Remove(button);
	/// 
	/// 		// remove button at position 2
	/// 		menu.RemoveAt(2);
	/// 	</code>
	/// </example>
	public interface IActiveMenu {
		/// <summary>
		/// 	Gets the list of <see cref = "T:ActiveButton"></see> instances
		/// 	associated with the current menu instances.
		/// </summary>
		/// <value>The items.</value>
		IActiveItems Items { get; }

		/// <summary>
		/// 	Gets or sets the tool tip control used for rendering tool tips.
		/// </summary>
		/// <value>The tool tip settings.</value>
		ToolTip ToolTip { get; set; }
	}
}