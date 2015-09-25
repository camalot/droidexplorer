using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DroidExplorer.Bootstrapper.Panels {
	/// <summary>
	/// 
	/// </summary>
	public class WizardPanel : Panel {

		/// <summary>
		/// Initializes a new instance of the <see cref="WizardPanel"/> class.
		/// </summary>
		internal WizardPanel ( ) {
			this.LogDebug ( "Creating Panel: {0}", this.GetType ( ).Name );
			this.Height = 257;
			this.Width = 573;
			this.Dock = DockStyle.Fill;
			this.BackColor = Color.Transparent;
			this.Font = new System.Drawing.Font ( "Tahoma", 8 );
			InitializeComponent ( );
		}

		public virtual void InitializeWizardPanel ( ) {
			this.LogDebug ( "Initializing Panel: {0}", this.GetType ( ).Name );
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WizardPanel"/> class.
		/// </summary>
		/// <param name="wizard">The wizard.</param>
		public WizardPanel ( IWizard wizard ) : this() {
			this.LogDebug ( "Creating Panel: {0}", this.GetType().FullName );
			Wizard = wizard;
		}

		/// <summary>
		/// Gets or sets the wizard.
		/// </summary>
		/// <value>The wizard.</value>
		public IWizard Wizard { get; set; }

		protected virtual void InitializeComponent ( ) { }

		public virtual void SetAdditionalText ( string text ) {

		}
	}
}
