using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DroidExplorer.UI {
	public interface ISplashDialog {

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ISplashDialog"/> is running.
		/// </summary>
		/// <value><c>true</c> if running; otherwise, <c>false</c>.</value>
		bool Running { get; set; }
		/// <summary>
		/// Shows this instance.
		/// </summary>
		void Show ( );
		void Show ( IWin32Window owner );
		DialogResult ShowDialog ( );
		DialogResult ShowDialog ( IWin32Window owner );

		bool IsDisposed { get; }
		/// <summary>
		/// Closes this instance.
		/// </summary>
		void Close ( );
		void Hide ( );

		void SetLoadSteps ( int value );
		void SetStepText ( string text );
		void IncrementLoadStep ( int value );
	}
}
