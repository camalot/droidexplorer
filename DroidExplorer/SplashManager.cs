using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.UI;
using System.Threading;
using System.Windows.Forms;

namespace DroidExplorer {
	public class SplashManager : ISplashHost {

		private SplashManager ( ) {

		}

		#region ISplashHost Members

		/// <summary>
		/// Gets the splash dialog.
		/// </summary>
		/// <value>The splash dialog.</value>
		public SplashDialog SplashDialog {
			get;
			private set;
		}

		ISplashDialog ISplashHost.SplashDialog {
			get { return this.SplashDialog; }
		}

		public void ShowSplashDialog ( int maxSteps ) {
			if ( SplashDialog == null || SplashDialog.IsDisposed || !SplashDialog.Running ) {
				SplashDialog = new SplashDialog ( );
				// load splash screen
				ThreadPool.QueueUserWorkItem ( new WaitCallback ( delegate ( object o ) {
					SplashDialog.SetLoadSteps ( maxSteps );
					SplashDialog.Show ( );
					while ( SplashDialog.Running )
						Application.DoEvents ( );
					SplashDialog.Close ( );
				} ) );
			}
		}

		public void ShowSplashDialog ( ) {
			ShowSplashDialog ( 0 );
		}

		public void CloseSplashDialog ( ) {
			if ( SplashDialog != null && !SplashDialog.IsDisposed && SplashDialog.Running ) {
				SplashDialog.Running = false;
			}
		}

		#endregion

		#region static members
		/// <summary>
		/// 
		/// </summary>
		private static SplashManager _instance;
		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static SplashManager Instance {
			get {
				if ( _instance == null ) {
					_instance = new SplashManager ( );
				}
				return _instance;
			}
		}
		#endregion

	}
}
