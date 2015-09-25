using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace DroidExplorer.Bootstrapper {
	public static class Extensions {
		private delegate void SetControlTextDelegate ( Control ctrl, string text );
		private delegate void SetProgressBarMaximumDelegate ( ProgressBar pb, int max );
		private delegate void SetProgressBarMinimumDelegate ( ProgressBar pb, int min );
		private delegate void SetProgressBarValueDelegate ( ProgressBar pb, int value );
		private delegate int GetProgressBarValueDelegate ( ProgressBar pb );
		private delegate void ProgressBarIncrementDelegate ( int increment );
		private delegate void GenericDelegate ( );
		private delegate void AddControlDelegate ( Control ctrl );
		private delegate void SetBooleanDelegate ( Control ctrl, bool val );
		private delegate void SetColorDelegate ( Control ctrl, Color color );
		private delegate void SetDockDelegate ( Control ctrl, DockStyle dock );
		private delegate IntPtr GetHandleDelegate ( Control window );

		/// <summary>
		/// Closes the ext.
		/// </summary>
		/// <param name="form">The form.</param>
		public static void CloseExt ( this Form form ) {
			try {
				if ( form.InvokeRequired ) {
					form.Invoke ( new GenericDelegate ( form.Close ) );
				} else {
					form.Close ( );
				}
			} catch ( ThreadAbortException ) { }
		}

		/// <summary>
		/// Hides the ext.
		/// </summary>
		/// <param name="wizard">The wizard.</param>
		public static void HideExt ( this IWizard wizard ) {
			try {
				if ( wizard.InvokeRequired ) {
					wizard.Invoke ( new GenericDelegate ( wizard.Hide ) );
				} else {
					wizard.Hide ( );
				}
			} catch ( ThreadAbortException ) {

			}
		}

		/// <summary>
		/// Shows the ext.
		/// </summary>
		/// <param name="wizard">The wizard.</param>
		public static void ShowExt ( this IWizard wizard ) {
			try {
				if ( wizard.InvokeRequired ) {
					wizard.Invoke ( new GenericDelegate ( wizard.Show ) );
				} else {
					wizard.Show ( );
				}
			} catch ( ThreadAbortException ) {

			}
		}


		public static IntPtr GetHandle ( this Control ctrl ) {
			try {
				if ( ctrl.InvokeRequired ) {
					return (IntPtr)ctrl.Invoke ( new GetHandleDelegate ( InternalGetHandle ), ctrl );
				} else {
					return InternalGetHandle ( ctrl );
				}
			} catch ( ThreadAbortException ) {
				return IntPtr.Zero;
			}
		}

		private static IntPtr InternalGetHandle ( Control ctrl ) {
			return ctrl.Handle;
		}

		public static void SetDock ( this Control ctrl, DockStyle dock ) {
			try {
				if ( ctrl.InvokeRequired ) {
					ctrl.Invoke ( new SetDockDelegate ( InternalSetDock ), ctrl, dock );
				} else {
					InternalSetDock ( ctrl, dock );
				}
			} catch ( ThreadAbortException ) {

			}
		}

		private static void InternalSetDock ( Control ctrl, DockStyle dock ) {
			ctrl.Dock = dock;
		}

		public static void SetBackColor ( this Control ctrl, Color color ) {
			try {
				if ( ctrl.InvokeRequired ) {
					ctrl.Invoke ( new SetColorDelegate ( InternalSetBackColor ), ctrl, color );
				} else {
					InternalSetBackColor ( ctrl, color );
				}
			} catch ( ThreadAbortException ) {

			}
		}

		private static void InternalSetBackColor ( Control ctrl, Color color ) {
			ctrl.BackColor = color;
		}

		public static void SetVisible ( this Control ctrl, bool visible ) {
			try {
				if ( ctrl.InvokeRequired ) {
					ctrl.Invoke ( new SetBooleanDelegate ( InternalSetControlVisible ), ctrl, visible );
				} else {
					InternalSetControlVisible ( ctrl, visible );
				}
			} catch ( ThreadAbortException ) {

			}
		}

		private static void InternalSetControlVisible ( Control ctrl, bool visible ) {
			ctrl.Visible = visible;
		}

		public static void SetEnabled ( this Control ctrl, bool enabled ) {
			try {
				if ( ctrl.InvokeRequired ) {
					ctrl.Invoke ( new SetBooleanDelegate ( InternalSetControlEnabled ), ctrl, enabled );
				} else {
					InternalSetControlEnabled ( ctrl, enabled );
				}
			} catch ( ThreadAbortException ) {

			}
		}

		private static void InternalSetControlEnabled ( Control ctrl, bool enabled ) {
			ctrl.Enabled = enabled;
		}


		public static void AddControl ( this Control ctrl, Control child ) {
			try {
				if ( ctrl.InvokeRequired ) {
					ctrl.Invoke ( new AddControlDelegate ( ctrl.Controls.Add ), child );
				} else {
					ctrl.Controls.Add ( child );
				}
			} catch ( ThreadAbortException ) {

			}

		}

		public static void ClearControls ( this Control ctrl ) {
			try {
				if ( ctrl.InvokeRequired ) {
					ctrl.Invoke ( new GenericDelegate ( ctrl.Controls.Clear ) );
				} else {
					ctrl.Controls.Clear ( );
				}
			} catch ( ThreadAbortException ) {

			}

		}

		/// <summary>
		/// Sets the control text.
		/// </summary>
		/// <param name="ctrl">The CTRL.</param>
		/// <param name="text">The text.</param>
		public static void SetText ( this Control ctrl, string text ) {
			try {
				if ( ctrl.InvokeRequired ) {
					ctrl.Invoke ( new SetControlTextDelegate ( InternalSetControlText ), ctrl, text );
				} else {
					InternalSetControlText ( ctrl, text );
				}
			} catch ( ThreadAbortException ) {

			}
		}

		private static void InternalSetControlText ( Control ctrl, string text ) {
			ctrl.Text = text;
		}

		/// <summary>
		/// Sets the progress bar maximum.
		/// </summary>
		/// <param name="pb">The pb.</param>
		/// <param name="max">The max.</param>
		public static void SetMaximum ( this  ProgressBar pb, int max ) {
			try {
				if ( pb.InvokeRequired ) {
					pb.Invoke ( new SetProgressBarMaximumDelegate ( InternalSetProgressBarMaximum ), pb, max );
				} else {
					InternalSetProgressBarMaximum ( pb, max );
				}
			} catch ( ThreadAbortException ) {

			}

		}

		private static void InternalSetProgressBarMaximum ( ProgressBar pb, int max ) {
			pb.Maximum = max;
		}



		/// <summary>
		/// Sets the progress bar minimum.
		/// </summary>
		/// <param name="pb">The pb.</param>
		/// <param name="min">The min.</param>
		public static void SetMinimum ( this ProgressBar pb, int min ) {
			try {
				if ( pb.InvokeRequired ) {
					pb.Invoke ( new SetProgressBarMinimumDelegate ( InternalSetProgressBarMinimum ), pb, min );
				} else {
					InternalSetProgressBarMinimum ( pb, min );
				}
			} catch ( ThreadAbortException ) {

			}

		}

		private static void InternalSetProgressBarMinimum ( ProgressBar pb, int min ) {
			pb.Minimum = min;
		}

		/// <summary>
		/// Sets the progress bar value.
		/// </summary>
		/// <param name="pb">The pb.</param>
		/// <param name="value">The value.</param>
		public static void SetValue ( this ProgressBar pb, int value ) {
			try {
				if ( pb.InvokeRequired ) {
					pb.Invoke ( new SetProgressBarValueDelegate ( InternalSetProgressBarValue ), pb, value );
				} else {
					InternalSetProgressBarValue ( pb, value );
				}
			} catch ( ThreadAbortException ) {

			}

		}

		private static void InternalSetProgressBarValue ( ProgressBar pb, int value ) {
			pb.Value = value;
		}

		public static int GetValue ( this ProgressBar pb ) {
			try {
				if ( pb.InvokeRequired ) {
					return (int)pb.Invoke ( new GetProgressBarValueDelegate ( InternalGetProgressBarValue ), pb );
				} else {
					return InternalGetProgressBarValue ( pb );
				}
			} catch ( ThreadAbortException ) {
				return 0;
			}

		}

		private static int InternalGetProgressBarValue ( ProgressBar pb ) {
			return pb.Value;
		}

		/// <summary>
		/// increments the progress bar
		/// </summary>
		/// <param name="pb">The pb.</param>
		/// <param name="increment">The increment.</param>
		public static void IncrementExt ( this ProgressBar pb, int increment ) {
			try {
				if ( pb.InvokeRequired ) {
					pb.Invoke ( new ProgressBarIncrementDelegate ( pb.Increment ), increment );
				} else {
					pb.Increment ( increment );
				}
			} catch ( ThreadAbortException ) {

			}

		}
	}
}
