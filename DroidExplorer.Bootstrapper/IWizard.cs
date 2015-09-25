using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DroidExplorer.Bootstrapper {
	public interface IWizard {
		event EventHandler CancelRequest;
		event EventHandler NextClick;
		event EventHandler BackClick;

		void Next ( );
		void Back ( );
		void Error ( Exception ex );
		void Cancel ( );

		Button NextButton { get; }
		Button BackButton { get; }
		Button CancelButton { get; }

		string GetInstallPath ( );
		string GetSdkPath ( );
		bool UseExistingSdk { get; set; }
		bool Installed { get; }
		int StepIndex { get; }
		bool PromptExit { get; set; }
		bool PromptCancel { get; set; }

		void Hide ( );
		void Show ( );

		bool InvokeRequired { get; }
		object Invoke ( Delegate method, params object[] args );
		object Invoke ( Delegate method );
	}
}
