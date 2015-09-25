using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using DroidExplorer.UI;
using DroidExplorer.Core;

namespace DroidExplorer.Components {
	public class TransferDataObject : DataObject {

		public TransferDataObject ()
			: base () {

		}

		public TransferDataObject ( string format, object data )
			: base ( format, data ) {

		}

		public TransferDataObject ( object data )
			: base ( data ) {

		}

		public override object GetData ( string format ) {
			/*if ( string.Compare ( format, DataFormats.FileDrop, false ) == 0 ) {
				List<string> newFiles = new List<string> ( );
				string[] oldFiles = ( string[ ] )base.GetData ( format );
				List<System.IO.FileInfo> files = CommandRunner.Instance.PullFiles ( new List<string> ( oldFiles ) );
				foreach ( var item in files ) {
					newFiles.Add ( item.FullName );
				}
				return newFiles;
			} else {
				return base.GetData ( format );
			}*/

			return base.GetData ( format );
		}

		public override void SetFileDropList ( StringCollection filePaths ) {
			base.SetFileDropList ( filePaths );
		}

		public override void SetData ( string format, object data ) {
		  //Console.WriteLine ( "[{0}] SetData", this.GetType ().Name );
			/*if ( string.Compare ( format, DataFormats.FileDrop, false ) == 0 ) {
				List<string> newFiles = new List<string> ();
				string[] oldFiles = (string[])data;
				//List<System.IO.FileInfo> files = CommandRunner.Instance.PullFiles ( new List<string> ( oldFiles ) );
				TransferForm tf = new TransferForm ();
				if ( tf.PullDialog ( new List<string> ( oldFiles ), new System.IO.DirectoryInfo ( CommandRunner.Instance.TempDataPath ) ) == DialogResult.OK ) {
					foreach ( var item in oldFiles ) {
						newFiles.Add ( item.FullName );
					}
					base.SetData ( format, newFiles );
				}
			} else {
				base.SetData ( format, data );
			}*/
		}
		public override System.Collections.Specialized.StringCollection GetFileDropList () {
			//Console.WriteLine ( "[{0}] GetFileDropList", this.GetType ().Name );
			return base.GetFileDropList ();
			/*try {
				StringCollection sc = new StringCollection ();

				string[] oldFiles = (string[])base.GetData ( DataFormats.FileDrop );
				List<System.IO.FileInfo> files = CommandRunner.Instance.PullFiles ( new List<string> ( oldFiles ) );

				foreach ( var item in files ) {
					sc.Add ( item.FullName );
				}
				return sc;
			} catch ( Exception ex ) {
        Console.WriteLine ( "[{0}] {1}", this.GetType ( ).Name, ex.Message );
				return null;
			}*/

			/*List<DroidExplorer.Core.IO.FileInfo> files = new List<DroidExplorer.Core.IO.FileInfo> ();
			List<System.IO.FileInfo> lfiles = new List<System.IO.FileInfo> ();
			TransferForm transfer = new TransferForm ();
			System.IO.DirectoryInfo dest = new System.IO.DirectoryInfo ( CommandRunner.Instance.TempDataPath );
			foreach ( var item in oldFiles ) {
				files.Add ( DroidExplorer.Core.IO.FileInfo.Create ( System.IO.Path.GetFileName ( item ), 0, null, null, null, DateTime.Now, false, item ) );
				lfiles.Add ( new System.IO.FileInfo ( System.IO.Path.Combine ( dest.FullName, System.IO.Path.GetFileName ( item ) ) ) );
			}
			if ( transfer.PullDialog ( files, dest ) == DialogResult.OK ) {
				foreach ( var item in lfiles ) {
					sc.Add ( item.FullName );
				}
			}
			return sc;*/

			//return base.GetFileDropList ( );
		}

		private bool InDragLoop () {

			return ( 0 != (int)GetData ( "InShellDragLoop" ) );

		}
	}
}
