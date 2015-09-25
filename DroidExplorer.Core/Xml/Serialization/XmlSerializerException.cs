using System;
using System.Collections.Generic;
using System.Text;

namespace DroidExplorer.Core.Xml.Serialization {
	/// <summary>
	/// 
	/// </summary>
	public class XmlSerializerException : Exception {
		/// <summary>
		/// Initializes a new instance of the <see cref="XmlSerializerException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		public XmlSerializerException ( string message )
			: base ( message ) {

		}
	}
}
