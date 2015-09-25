using System;
using System.Collections.Generic;
using System.Text;

namespace DroidExplorer.UI {
	public interface ISplashHost {
		/// <summary>
		/// Gets the splash dialog.
		/// </summary>
		/// <value>The splash dialog.</value>
		ISplashDialog SplashDialog { get; }
		/// <summary>
		/// Shows the splash dialog.
		/// </summary>
		void ShowSplashDialog ( );
		void ShowSplashDialog ( int maxSteps );
		/// <summary>
		/// Closes the splash dialog.
		/// </summary>
		void CloseSplashDialog ( );
	}
}
