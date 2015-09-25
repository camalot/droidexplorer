using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using DroidExplorer.Core;

namespace DroidExplorer.UI {
	public partial class SplashDialog : Form, ISplashDialog {

		public SplashDialog ( ) {
			this.Running = true;
			InitializeComponent ( );
			this.SetStyle ( ControlStyles.OptimizedDoubleBuffer, true );
			this.BackColor = Color.Fuchsia;
			this.TransparencyKey = Color.Fuchsia;
			logo.Image = DroidExplorer.Resources.Images.android192;
			title.Image = DroidExplorer.Resources.Images.droidexplorer_title_new;
			version.ForeColor = Color.FromArgb(255, 0, 192, 0);
			status.ForeColor = Color.FromArgb(255, 0, 192, 0);
			version.Text = string.Format ( CultureInfo.InvariantCulture, "Version {0} ({1})", this.GetType ( ).Assembly.GetName ( ).Version.ToString ( ), Logger.ApplicationArchitecture.ToString ( ) );
		}

		#region ISplashDialog Members

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ISplashDialog"/> is running.
		/// </summary>
		/// <value><c>true</c> if running; otherwise, <c>false</c>.</value>
		public bool Running { get; set; }


		public void SetLoadSteps ( int value ) {
			progress.SetMinimum ( 0 );
			progress.SetMaximum ( value );
			progress.SetValue ( 0 );
		}

		public void IncrementLoadStep ( int value ) {
			progress.IncrementExt ( value );
		}


		public void SetStepText ( string text ) {
			this.status.SetText ( text );
		}

		#endregion

		/// <summary>
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data.</param>
		protected override void OnPaint ( PaintEventArgs e ) {
			base.OnPaint ( e );
			e.Graphics.DrawImage ( DroidExplorer.Resources.Images.splash_background2, 0, 0 , this.Width, this.Height );
			//ControlPaint.DrawBorder3D ( e.Graphics, this.ClientRectangle, Border3DStyle.Raised );
		}

	}
}
