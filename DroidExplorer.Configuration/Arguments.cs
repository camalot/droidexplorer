using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace DroidExplorer.Core {
	/// <summary>
	/// An command line argument parser
	/// </summary>
	public class Arguments {
		private Dictionary<string, string> _parameters;

		/// <summary>
		/// Initializes a new instance of the <see cref="Arguments"/> class.
		/// </summary>
		/// <param name="args">The args.</param>
		public Arguments ( string[] args ) {
			Parse ( args );
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Arguments"/> class.
		/// </summary>
		public Arguments ( )
			: this ( new string[] { } ) {

		}

		/// <summary>
		/// Parse the specified arguments.
		/// </summary>
		/// <param name="args">The args.</param>
		/// <remarks>
		///  Valid parameters forms:
		///		{-,/,--}param{ ,=,:}((",')value(",'))
	  /// </remarks>
		/// <example>
		///		-param1 value1 --param2 /param3:"Test-:-work" 
		///   /param4=happy -param5 '--=nice=--'
		/// </example>
		public void Parse ( string[] args ) {
			Parameters = new Dictionary<string, string> ( );
			Regex spliter = new Regex ( @"^-{1,2}|^/|=|:", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace );
			Regex remover = new Regex ( @"^['""]?(.*?)['""]?$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace );

			string parameter = null;
			string[] parts;
			foreach ( string txt in args ) {
				// Look for new parameters (-,/ or --) and a
				// possible enclosed value (=,:)
				parts = spliter.Split ( txt, 3 );

				switch ( parts.Length ) {
					// Found a value (for the last parameter 
					// found (space separator))
					case 1:
						if ( parameter != null ) {
							if ( !Parameters.ContainsKey ( parameter ) ) {
								parts[ 0 ] = remover.Replace ( parts[ 0 ], "$1" );
								Parameters.Add ( parameter, parts[ 0 ] );
							}
							parameter = null;
						}
						// else Error: no parameter waiting for a value (skipped)
						break;

					// Found just a parameter
					case 2:
						// The last parameter is still waiting. 
						// With no value, set it to true.
						if ( parameter != null ) {
							if ( !Parameters.ContainsKey ( parameter ) ) {
								Parameters.Add ( parameter, string.Empty );
							}
						}
						parameter = parts[ 1 ];
						break;

					// Parameter with enclosed value
					case 3:
						// The last parameter is still waiting. 
						// With no value, set it to true.
						if ( parameter != null ) {
							if ( !Parameters.ContainsKey ( parameter ) ) {
								Parameters.Add ( parameter, string.Empty );
							}
						}

						parameter = parts[ 1 ];

						// Remove possible enclosing characters (",')
						if ( !Parameters.ContainsKey ( parameter ) ) {
							parts[ 2 ] = remover.Replace ( parts[ 2 ], "$1" );
							Parameters.Add ( parameter, parts[ 2 ] );
						}

						parameter = null;
						break;
				}
			}
			// In case a parameter is still waiting
			if ( parameter != null ) {
				if ( !Parameters.ContainsKey ( parameter ) ) {
					Parameters.Add ( parameter, string.Empty );
				}
			}
		}

		/// <summary>
		/// Gets or sets the parameters.
		/// </summary>
		/// <value>The parameters.</value>
		private Dictionary<string, string> Parameters {
			get { return this._parameters; }
			set { this._parameters = value; }
		}


		/// <summary>
		/// Gets the <see cref="System.String"/> with the specified param.
		/// </summary>
		/// <value></value>
		/// <exception cref="KeyNotFoundException" />
		public string this[ string param ] {
			get {
				return GetFirstParamValue ( param );
			}
		}

		/// <summary>
		/// Gets the param or the second param, if the first doesn't exist.
		/// </summary>
		/// <value></value>
		/// <exception cref="KeyNotFoundException"></exception>
		public string this[ params string[] param ] {
			get {
				return GetFirstParamValue ( param );
			}
		}

		/// <summary>
		/// Gets the specified param value.
		/// </summary>
		/// <param name="param">The param.</param>
		/// <returns></returns>
		/// <exception cref="KeyNotFoundException" />
		public string Get ( string param ) {
			return this[ param ];
		}

		/// <summary>
		/// Gets the specified param value.
		/// </summary>
		/// <param name="param">The param list.</param>
		/// <returns></returns>
		/// <exception cref="KeyNotFoundException" />
		public string Get ( params string[] param ) {
			return this[ param ];
		}

		/// <summary>
		/// Determines whether the specified param contains param.
		/// </summary>
		/// <param name="param">The param.</param>
		/// <returns>
		/// 	<c>true</c> if the specified param contains param; otherwise, <c>false</c>.
		/// </returns>
		public bool Contains ( string param ) {
			return InternalContainsAny ( param );
		}

		/// <summary>
		/// Determines whether contains any param in the specified param list.
		/// </summary>
		/// <param name="param">The param.</param>
		/// <returns>
		/// 	<c>true</c> if contains any param in the specified param list; otherwise, <c>false</c>.
		/// </returns>
		public bool Contains ( params string[] param ) {
			return InternalContainsAny ( param );
		}

		/// <summary>
		/// Determines whether the specified value contains value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		/// 	<c>true</c> if the specified value contains value; otherwise, <c>false</c>.
		/// </returns>
		public bool ContainsValue ( string value ) {
			return Parameters.ContainsValue ( value );
		}

		/// <summary>
		/// Gets the keys.
		/// </summary>
		/// <value>The keys.</value>
		public string[] Keys {
			get {
				if ( Parameters.Keys.Count > 0 ) {
					string[] keys = new string[ Parameters.Keys.Count ];
					Parameters.Keys.CopyTo ( keys, 0 );
					return keys;
				} else {
					return null;
				}
			}
		}

		/// <summary>
		/// Gets the values.
		/// </summary>
		/// <value>The values.</value>
		public string[] Values {
			get {
				if ( Parameters.Values.Count > 0 ) {
					string[] vals = new string[ Parameters.Values.Count ];
					Parameters.Values.CopyTo ( vals, 0 );
					return vals;
				} else {
					return null;
				}
			}
		}

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
		public int Count { get { return this.Parameters.Count; } }

		/// <summary>
		/// Removes all parameters from this collection
		/// </summary>
		public void Clear ( ) {
			Parameters.Clear ( );
		}

		/// <summary>
		/// Determines whether contains the specified param.
		/// </summary>
		/// <param name="param">The param.</param>
		/// <returns></returns>
		private bool InternalContains ( string param ) {
			return Parameters.ContainsKey ( param );
		}

		/// <summary>
		/// Determines whether contains any param in the specified param list.
		/// </summary>
		/// <param name="param">The param.</param>
		/// <returns></returns>
		private bool InternalContainsAny ( params string[] param ) {
			bool found = false;
			foreach ( string s in param ) {
				if ( InternalContains ( s ) ) {
					found = true;
					break;
				}
			}
			return found;
		}

		/// <summary>
		/// Gets the first param value.
		/// </summary>
		/// <param name="param">The param.</param>
		/// <returns></returns>
		/// <exception cref="KeyNotFoundException" />
		private string GetFirstParamValue ( params string[] param ) {
			foreach ( string s in param ) {
				if ( Contains ( s ) ) {
					return this.Parameters[ s ];
				}
			}

			throw new KeyNotFoundException ( "None of the params were found." );
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </returns>
		public override string ToString ( ) {
			StringBuilder sb = new StringBuilder ( );
			foreach ( string key in Parameters.Keys )
				sb.AppendFormat ( "/{0}={1} ", key, Parameters[ key ] );
			return sb.ToString ( ).Trim ( );
		}
	}
}
