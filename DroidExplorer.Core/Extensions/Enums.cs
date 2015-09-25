using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Core {
	public static partial class DroidExplorerCoreExtensions {

		/// <summary>
		/// Converts the value to enum
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static T ToEnum<T> ( this int value ) {

			return (T)Enum.ToObject ( typeof ( T ), value );
		}

		/// <summary>
		/// Converts the value to enum
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static T ToEnum<T> ( this uint value ) {
			return (T)Enum.ToObject ( typeof ( T ), value );
		}

		/// <summary>
		/// Converts the value to enum
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static T ToEnum<T> ( this short value ) {
			return (T)Enum.ToObject ( typeof ( T ), value );
		}

		/// <summary>
		/// Converts the value to enum
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static T ToEnum<T> ( this ushort value ) {
			return (T)Enum.ToObject ( typeof ( T ), value );
		}

		/// <summary>
		/// Converts the value to enum
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static T ToEnum<T> ( this long value ) {
			return (T)Enum.ToObject ( typeof ( T ), value );
		}

		/// <summary>
		/// Converts the value to enum
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static T ToEnum<T> ( this ulong value ) {
			return (T)Enum.ToObject ( typeof ( T ), value );
		}

		/// <summary>
		/// Converts the value to enum
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static T ToEnum<T> ( this byte value ) {
			return (T)Enum.ToObject ( typeof ( T ), value );
		}

		/// <summary>
		/// Converts the value to enum
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static T ToEnum<T> ( this sbyte value ) {
			return (T)Enum.ToObject ( typeof ( T ), value );
		}
	}
}
