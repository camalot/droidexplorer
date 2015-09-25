using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Security.Policy;

namespace DroidExplorer.Core.Xml.Serialization {
	/// <summary>
	/// An Object Serializer
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class XmlSerializer<T> {

		#region CreateSerializer
		/// <summary>
		/// Creates the XML serializer.
		/// </summary>
		/// <returns></returns>
		public static XmlSerializer CreateXmlSerializer ( ) {
			return new XmlSerializer ( typeof ( T ) );
		}

		/// <summary>
		/// Creates the XML serializer.
		/// </summary>
		/// <param name="typeMapping">An XmlTypeMapping that maps one type to another.</param>
		/// <returns></returns>
		public static XmlSerializer CreateXmlSerializer ( XmlTypeMapping typeMapping ) {
			return new XmlSerializer ( typeMapping );
		}

		/// <summary>
		/// Creates the XML serializer.
		/// </summary>
		/// <param name="defaultNamespace">The default namespace of all XML elements in the XML document.</param>
		/// <returns></returns>
		public static XmlSerializer CreateXmlSerializer ( string defaultNamespace ) {
			return new XmlSerializer ( typeof ( T ), defaultNamespace );
		}

		/// <summary>
		/// Creates the XML serializer.
		/// </summary>
		/// <param name="extraTypes">A Type array of additional object types to serialize.</param>
		/// <returns></returns>
		public static XmlSerializer CreateXmlSerializer ( Type[] extraTypes ) {
			return new XmlSerializer ( typeof ( T ), extraTypes );
		}

		/// <summary>
		/// Creates the XML serializer.
		/// </summary>
		/// <param name="overrides">An XmlAttributeOverrides that extends or overrides the behavior of the class specified in the type parameter.</param>
		/// <returns></returns>
		public static XmlSerializer CreateXmlSerializer ( XmlAttributeOverrides overrides ) {
			return new XmlSerializer ( typeof ( T ), overrides );
		}

		/// <summary>
		/// Creates the XML serializer.
		/// </summary>
		/// <param name="root">An XmlRootAttribute that defines the XML root element properties.</param>
		/// <returns></returns>
		public static XmlSerializer CreateXmlSerializer ( XmlRootAttribute root ) {
			return new XmlSerializer ( typeof ( T ), root );
		}

		/// <summary>
		/// Creates the XML serializer.
		/// </summary>
		/// <param name="overrides">An XmlAttributeOverrides that extends or overrides the behavior of the class specified in the type parameter.</param>
		/// <param name="extraTypes">A Type array of additional object types to serialize.</param>
		/// <param name="root">An XmlRootAttribute that defines the XML root element properties.</param>
		/// <param name="defaultNamespace">The default namespace of all XML elements in the XML document.</param>
		/// <returns></returns>
		public static XmlSerializer CreateXmlSerializer ( XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace ) {
			return new XmlSerializer ( typeof ( T ), overrides, extraTypes, root, defaultNamespace );
		}

		/// <summary>
		/// Creates the XML serializer.
		/// </summary>
		/// <param name="overrides">An XmlAttributeOverrides that extends or overrides the behavior of the class specified in the type parameter.</param>
		/// <param name="extraTypes">A Type array of additional object types to serialize.</param>
		/// <param name="root">An XmlRootAttribute that defines the XML root element properties.</param>
		/// <param name="defaultNamespace">The default namespace of all XML elements in the XML document.</param>
		/// <param name="location">The location of the types.</param>
		/// <param name="evidence">An instance of the Evidence class that contains credentials required to access types.</param>
		/// <returns></returns>
		public static XmlSerializer CreateXmlSerializer ( XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace,
				string location, Evidence evidence ) {
			return new XmlSerializer ( typeof ( T ), overrides, extraTypes, root, defaultNamespace, location, evidence );
		}
		#endregion

		#region CanDeserialize
		/// <summary>
		/// Determines whether this instance can deserialize the specified reader.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can deserialize the specified reader; otherwise, <c>false</c>.
		/// </returns>
		public static bool CanDeserialize ( XmlReader reader ) {
			return CreateXmlSerializer ( ).CanDeserialize ( reader );
		}

		/// <summary>
		/// Determines whether this instance can deserialize the specified reader.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="extraTypes">The extra types.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can deserialize the specified reader; otherwise, <c>false</c>.
		/// </returns>
		public static bool CanDeserialize ( XmlReader reader, Type[] extraTypes ) {
			return CreateXmlSerializer ( extraTypes ).CanDeserialize ( reader );
		}

		/// <summary>
		/// Determines whether this instance can deserialize the specified stream.
		/// </summary>
		/// <param name="stream">The Xml stream.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can deserialize the specified stream; otherwise, <c>false</c>.
		/// </returns>
		public static bool CanDeserialize ( Stream stream ) {
			XmlReader reader = new XmlTextReader ( stream );
			bool canDeserialize = false;
			using ( reader ) {
				canDeserialize = CanDeserialize ( reader );
			}
			return canDeserialize;
		}

		/// <summary>
		/// Determines whether this instance can deserialize the specified stream.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="extraTypes">The extra types.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can deserialize the specified stream; otherwise, <c>false</c>.
		/// </returns>
		public static bool CanDeserialize ( Stream stream, Type[] extraTypes ) {
			XmlReader reader = new XmlTextReader ( stream );
			bool canDeserialize = false;
			using ( reader ) {
				canDeserialize = CanDeserialize ( reader, extraTypes );
			}
			return canDeserialize;
		}

		/// <summary>
		/// Determines whether this instance can deserialize the specified URI.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can deserialize the specified URI; otherwise, <c>false</c>.
		/// </returns>
		public static bool CanDeserialize ( Uri uri ) {
			XmlReader reader = new XmlTextReader ( uri.ToString ( ) );
			bool canDeserialize = false;
			using ( reader ) {
				canDeserialize = CanDeserialize ( reader );
			}
			return canDeserialize;
		}

		/// <summary>
		/// Determines whether this instance can deserialize the specified URI.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="extraTypes">The extra types.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can deserialize the specified URI; otherwise, <c>false</c>.
		/// </returns>
		public static bool CanDeserialize ( Uri uri, Type[] extraTypes ) {
			XmlReader reader = new XmlTextReader ( uri.ToString ( ) );
			bool canDeserialize = false;
			using ( reader ) {
				canDeserialize = CanDeserialize ( reader, extraTypes );
			}
			return canDeserialize;
		}

		/// <summary>
		/// Determines whether this instance can deserialize the specified file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can deserialize the specified file; otherwise, <c>false</c>.
		/// </returns>
		public static bool CanDeserialize ( FileInfo file ) {
			FileStream fs = file.OpenRead ( );
			bool canDeserialize = false;
			using ( fs ) {
				canDeserialize = CanDeserialize ( fs );
			}
			return canDeserialize;
		}

		/// <summary>
		/// Determines whether this instance can deserialize the specified file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="extraTypes">The extra types.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can deserialize the specified file; otherwise, <c>false</c>.
		/// </returns>
		public static bool CanDeserialize ( FileInfo file, Type[] extraTypes ) {
			FileStream fs = file.OpenRead ( );
			bool canDeserialize = false;
			using ( fs ) {
				canDeserialize = CanDeserialize ( fs, extraTypes );
			}
			return canDeserialize;
		}

		/// <summary>
		/// Determines whether this instance can deserialize the specified reader.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can deserialize the specified reader; otherwise, <c>false</c>.
		/// </returns>
		public static bool CanDeserialize ( TextReader reader ) {
			XmlTextReader treader = new XmlTextReader ( reader );
			bool canDeserialize = false;
			using ( treader ) {
				canDeserialize = CanDeserialize ( treader );
			}
			return canDeserialize;
		}

		/// <summary>
		/// Determines whether this instance can deserialize the specified reader.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="extraTypes">The extra types.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can deserialize the specified reader; otherwise, <c>false</c>.
		/// </returns>
		public static bool CanDeserialize ( TextReader reader, Type[] extraTypes ) {
			XmlTextReader treader = new XmlTextReader ( reader );
			bool canDeserialize = false;
			using ( treader ) {
				canDeserialize = CanDeserialize ( treader, extraTypes );
			}
			return canDeserialize;
		}

		/// <summary>
		/// Determines whether this instance can deserialize the specified doc.
		/// </summary>
		/// <param name="doc">The Xml Document.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can deserialize the specified xml document; otherwise, <c>false</c>.
		/// </returns>
		public static bool CanDeserialize ( XmlDocument doc ) {
			MemoryStream ms = new MemoryStream ( );
			doc.Save ( ms );
			ms.Position = 0;
			bool canDeserialize = false;
			using ( ms ) {
				canDeserialize = CanDeserialize ( ms );
			}
			return canDeserialize;
		}

		/// <summary>
		/// Determines whether this instance can deserialize the specified doc.
		/// </summary>
		/// <param name="doc">The doc.</param>
		/// <param name="extraTypes">The extra types.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can deserialize the specified doc; otherwise, <c>false</c>.
		/// </returns>
		public static bool CanDeserialize ( XmlDocument doc, Type[] extraTypes ) {
			MemoryStream ms = new MemoryStream ( );
			doc.Save ( ms );
			ms.Position = 0;
			bool canDeserialize = false;
			using ( ms ) {
				canDeserialize = CanDeserialize ( ms, extraTypes );
			}
			return canDeserialize;
		}

		/// <summary>
		/// Determines whether this instance can deserialize the specified element.
		/// </summary>
		/// <param name="element">The Xml Element.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can deserialize the specified Xml element; otherwise, <c>false</c>.
		/// </returns>
		public static bool CanDeserialize ( XmlElement element ) {
			if ( element == null )
				return false;
			XmlDocument doc = new XmlDocument ( );
			XmlElement ele = (XmlElement)doc.ImportNode ( element, true );
			doc.AppendChild ( ele );
			return CanDeserialize ( doc );
		}

		/// <summary>
		/// Determines whether this instance can deserialize the specified element.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="extraTypes">The extra types.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can deserialize the specified element; otherwise, <c>false</c>.
		/// </returns>
		public static bool CanDeserialize ( XmlElement element, Type[] extraTypes ) {
			if ( element == null )
				return false;
			XmlDocument doc = new XmlDocument ( );
			XmlElement ele = (XmlElement)doc.ImportNode ( element, true );
			doc.AppendChild ( ele );
			return CanDeserialize ( doc, extraTypes );
		}
		#endregion
		#region Deserialize
		/// <summary>
		/// Deserializes the specified stream.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <returns></returns>
		public static T Deserialize ( Stream stream ) {
			/*try {
				return (T)CreateXmlSerializer().Deserialize ( stream );
			} catch ( Exception ) {
				throw;
			}*/
			return Deserialize ( stream, string.Empty );
		}

		public static T Deserialize ( Stream stream, string defaultNamespace ) {
			try {
				return (T)CreateXmlSerializer ( defaultNamespace ).Deserialize ( stream );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Deserializes the specified stream.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="extraTypes">The extra types.</param>
		/// <returns></returns>
		public static T Deserialize ( Stream stream, Type[] extraTypes ) {
			try {
				return (T)CreateXmlSerializer ( extraTypes ).Deserialize ( stream );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Deserializers the specified reader.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <returns></returns>
		public static T Deserialize ( XmlReader reader ) {
			try {
				XmlSerializer ser = CreateXmlSerializer ( );
				if ( ser.CanDeserialize ( reader ) ) {
					return (T)ser.Deserialize ( reader );
				} else
					throw new XmlSerializerException ( string.Format ( "Unable to deserialize type {0}", typeof ( T ).FullName ) );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Deserializes the specified reader.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="extraTypes">The extra types.</param>
		/// <returns></returns>
		public static T Deserialize ( XmlReader reader, Type[] extraTypes ) {
			try {
				XmlSerializer ser = CreateXmlSerializer ( extraTypes );
				if ( ser.CanDeserialize ( reader ) ) {
					return (T)ser.Deserialize ( reader );
				} else
					throw new XmlSerializerException ( string.Format ( "Unable to deserialize type {0}", typeof ( T ).FullName ) );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Deserializes the specified reader.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <returns></returns>
		public static T Deserialize ( TextReader reader ) {
			try {
				return (T)CreateXmlSerializer ( ).Deserialize ( reader );
			} catch ( Exception ex ) {
				Logger.LogError ( typeof ( XmlSerializer), ex.Message, ex );
				throw;
			}
		}

		/// <summary>
		/// Deserializes the specified reader.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="extraTypes">The extra types.</param>
		/// <returns></returns>
		public static T Deserialize ( TextReader reader, Type[] extraTypes ) {
			try {
				return (T)CreateXmlSerializer ( extraTypes ).Deserialize ( reader );
			} catch ( Exception ex ) {
				Logger.LogError ( typeof ( XmlSerializer ), ex.Message, ex );

				throw;
			}
		}

		/// <summary>
		/// Deserializes the specified reader.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="events">The events.</param>
		/// <returns></returns>
		public static T Deserialize ( XmlReader reader, XmlDeserializationEvents events ) {
			try {
				XmlSerializer ser = CreateXmlSerializer ( );
				if ( ser.CanDeserialize ( reader ) ) {
					return (T)ser.Deserialize ( reader, events );
				} else
					throw new XmlSerializerException ( string.Format ( "Unable to deserialize type {0}", typeof ( T ).FullName ) );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Deserializes the specified reader.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="events">The events.</param>
		/// <param name="extraTypes">The extra types.</param>
		/// <returns></returns>
		public static T Deserialize ( XmlReader reader, XmlDeserializationEvents events, Type[] extraTypes ) {
			try {
				XmlSerializer ser = CreateXmlSerializer ( extraTypes );
				if ( ser.CanDeserialize ( reader ) ) {
					return (T)ser.Deserialize ( reader, events );
				} else
					throw new XmlSerializerException ( string.Format ( "Unable to deserialize type {0}", typeof ( T ).FullName ) );
			} catch ( Exception ) {
				throw;
			}
		}


		/// <summary>
		/// Deserializers the specified reader.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="encoding">The encoding.</param>
		/// <returns></returns>
		public static T Deserialize ( XmlReader reader, Encoding encoding ) {
			try {
				XmlSerializer ser = CreateXmlSerializer ( );
				if ( ser.CanDeserialize ( reader ) ) {
					return (T)ser.Deserialize ( reader, encoding != null ? encoding.WebName : string.Empty );
				} else
					throw new XmlSerializerException ( string.Format ( "Unable to deserialize type {0}", typeof ( T ).FullName ) );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Deserializes the specified reader.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="encoding">The encoding.</param>
		/// <param name="extraTypes">The extra types.</param>
		/// <returns></returns>
		public static T Deserialize ( XmlReader reader, Encoding encoding, Type[] extraTypes ) {
			try {
				XmlSerializer ser = CreateXmlSerializer ( extraTypes );
				if ( ser.CanDeserialize ( reader ) ) {
					return (T)ser.Deserialize ( reader, encoding != null ? encoding.WebName : string.Empty );
				} else
					throw new XmlSerializerException ( string.Format ( "Unable to deserialize type {0}", typeof ( T ).FullName ) );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Deserializes the specified file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		public static T Deserialize ( FileInfo file ) {
			return Deserialize ( file, string.Empty );
		}

		/// <summary>
		/// Deserializes the specified file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="defaultNamespace">The default namespace.</param>
		/// <returns></returns>
		public static T Deserialize ( FileInfo file, string defaultNamespace ) {
			if ( file.Exists ) {
				FileStream fs = new FileStream ( file.FullName, FileMode.Open, FileAccess.Read );
				T obj;
				using ( fs ) {
					obj = Deserialize ( fs, defaultNamespace );
				}
				return obj;
			} else
				throw new FileNotFoundException ( );
		}

		/// <summary>
		/// Deserializes the specified file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="extraTypes">The extra types.</param>
		/// <returns></returns>
		public static T Deserialize ( FileInfo file, Type[] extraTypes ) {
			if ( file.Exists ) {
				FileStream fs = new FileStream ( file.FullName, FileMode.Open, FileAccess.Read );
				T obj;
				using ( fs ) {
					obj = Deserialize ( fs, extraTypes );
				}
				return obj;
			} else
				throw new FileNotFoundException ( );
		}

		/// <summary>
		/// Deserializes the specified URL.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <returns>Deserialized object</returns>
		public static T Deserialize ( Uri url ) {
			try {
				XmlTextReader reader = new XmlTextReader ( url.ToString ( ) );
				T obj;
				using ( reader ) {
					obj = Deserialize ( reader );
				}
				return obj;
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Deserializes the specified URL.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <param name="extraTypes">The extra types.</param>
		/// <returns></returns>
		public static T Deserialize ( Uri url, Type[] extraTypes ) {
			try {
				XmlTextReader reader = new XmlTextReader ( url.ToString ( ) );
				T obj;
				using ( reader ) {
					obj = Deserialize ( reader, extraTypes );
				}
				return obj;
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Deserializes the specified XML document.
		/// </summary>
		/// <param name="xmlDocument">The XML document.</param>
		/// <returns></returns>
		public static T Deserialize ( XmlDocument xmlDocument ) {
			return Deserialize ( xmlDocument, string.Empty );
		}
		/// <summary>
		/// Deserializes the specified XML document.
		/// </summary>
		/// <param name="xmlDocument">The XML document.</param>
		/// <param name="defaultNamespace">The default namespace.</param>
		/// <returns></returns>
		public static T Deserialize ( XmlDocument xmlDocument, string defaultNamespace ) {
			try {
				MemoryStream ms = new MemoryStream ( );
				xmlDocument.Save ( ms );
				ms.Position = 0;
				T obj;
				using ( ms ) {
					obj = Deserialize ( ms, defaultNamespace );
				}
				return obj;
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Deserializes the specified XML document.
		/// </summary>
		/// <param name="xmlDocument">The XML document.</param>
		/// <param name="extraTypes">The extra types.</param>
		/// <returns></returns>
		public static T Deserialize ( XmlDocument xmlDocument, Type[] extraTypes ) {
			try {
				MemoryStream ms = new MemoryStream ( );
				xmlDocument.Save ( ms );
				ms.Position = 0;
				T obj;
				using ( ms ) {
					obj = Deserialize ( ms, extraTypes );
				}
				return obj;
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Deserializes the specified XML element.
		/// </summary>
		/// <param name="xmlElement">The XML element.</param>
		/// <returns></returns>
		public static T Deserialize ( XmlElement xmlElement ) {
			return Deserialize ( xmlElement, string.Empty );
		}

		/// <summary>
		/// Deserializes the specified XML element.
		/// </summary>
		/// <param name="xmlElement">The XML element.</param>
		/// <param name="defaultNamespace">The default namespace.</param>
		/// <returns></returns>
		public static T Deserialize ( XmlElement xmlElement, string defaultNamespace ) {
			try {
				XmlDocument doc = new XmlDocument ( );
				XmlElement ele = (XmlElement)doc.ImportNode ( xmlElement, true );
				doc.AppendChild ( ele );
				return Deserialize ( doc, defaultNamespace );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Deserializes the specified XML element.
		/// </summary>
		/// <param name="xmlElement">The XML element.</param>
		/// <param name="extraTypes">The extra types.</param>
		/// <returns></returns>
		public static T Deserialize ( XmlElement xmlElement, Type[] extraTypes ) {
			try {
				XmlDocument doc = new XmlDocument ( );
				XmlElement ele = (XmlElement)doc.ImportNode ( xmlElement, true );
				doc.AppendChild ( ele );
				return Deserialize ( doc, extraTypes );
			} catch ( Exception ) {
				throw;
			}
		}
		#endregion

		#region Serialize
		/// <summary>
		/// Serializes the specified stream.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="obj">The obj.</param>
		public static void Serialize ( Stream stream, T obj ) {
			Serialize ( stream, string.Empty, obj );
		}

		/// <summary>
		/// Serializes the specified stream.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="defaultNamespace">The default namespace.</param>
		/// <param name="obj">The obj.</param>
		public static void Serialize ( Stream stream, string defaultNamespace, T obj ) {
			try {
				CreateXmlSerializer ( defaultNamespace ).Serialize ( stream, obj );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Serializes the specified stream.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="obj">The obj.</param>
		/// <param name="extraType">Type of the extra.</param>
		public static void Serialize ( Stream stream, T obj, Type[] extraType ) {
			try {
				CreateXmlSerializer ( extraType ).Serialize ( stream, obj );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Serializes the specified text writer.
		/// </summary>
		/// <param name="textWriter">The text writer.</param>
		/// <param name="obj">The obj.</param>
		public static void Serialize ( TextWriter textWriter, T obj ) {
			try {
				CreateXmlSerializer ( ).Serialize ( textWriter, obj );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Serializes the specified text writer.
		/// </summary>
		/// <param name="textWriter">The text writer.</param>
		/// <param name="obj">The obj.</param>
		/// <param name="extraTypes">The extra types.</param>
		public static void Serialize ( TextWriter textWriter, T obj, Type[] extraTypes ) {
			try {
				CreateXmlSerializer ( extraTypes ).Serialize ( textWriter, obj );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Serializes the specified XML writer.
		/// </summary>
		/// <param name="xmlWriter">The XML writer.</param>
		/// <param name="obj">The obj.</param>
		public static void Serialize ( XmlWriter xmlWriter, T obj ) {
			try {
				CreateXmlSerializer ( ).Serialize ( xmlWriter, obj );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Serializes the specified XML writer.
		/// </summary>
		/// <param name="xmlWriter">The XML writer.</param>
		/// <param name="obj">The obj.</param>
		/// <param name="extraTypes">The extra types.</param>
		public static void Serialize ( XmlWriter xmlWriter, T obj, Type[] extraTypes ) {
			try {
				CreateXmlSerializer ( extraTypes ).Serialize ( xmlWriter, obj );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Serializes the specified stream.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="obj">The obj.</param>
		/// <param name="namespaces">The namespaces.</param>
		public static void Serialize ( Stream stream, T obj, XmlSerializerNamespaces namespaces ) {
			try {
				CreateXmlSerializer ( ).Serialize ( stream, obj, namespaces );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Serializes the specified stream.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="obj">The obj.</param>
		/// <param name="namespaces">The namespaces.</param>
		/// <param name="extraTypes">The extra types.</param>
		public static void Serialize ( Stream stream, T obj, XmlSerializerNamespaces namespaces, Type[] extraTypes ) {
			try {
				CreateXmlSerializer ( extraTypes ).Serialize ( stream, obj, namespaces );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Serializes the specified text writer.
		/// </summary>
		/// <param name="textWriter">The text writer.</param>
		/// <param name="obj">The obj.</param>
		/// <param name="namespaces">The namespaces.</param>
		public static void Serialize ( TextWriter textWriter, T obj, XmlSerializerNamespaces namespaces ) {
			try {
				CreateXmlSerializer ( ).Serialize ( textWriter, obj, namespaces );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Serializes the specified text writer.
		/// </summary>
		/// <param name="textWriter">The text writer.</param>
		/// <param name="obj">The obj.</param>
		/// <param name="namespaces">The namespaces.</param>
		/// <param name="extraTypes">The extra types.</param>
		public static void Serialize ( TextWriter textWriter, T obj, XmlSerializerNamespaces namespaces, Type[] extraTypes ) {
			try {
				CreateXmlSerializer ( extraTypes ).Serialize ( textWriter, obj, namespaces );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Serializes the specified XML writer.
		/// </summary>
		/// <param name="xmlWriter">The XML writer.</param>
		/// <param name="obj">The obj.</param>
		/// <param name="namespaces">The namespaces.</param>
		public static void Serialize ( XmlWriter xmlWriter, T obj, XmlSerializerNamespaces namespaces ) {
			try {
				CreateXmlSerializer ( ).Serialize ( xmlWriter, obj, namespaces );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Serializes the specified XML writer.
		/// </summary>
		/// <param name="xmlWriter">The XML writer.</param>
		/// <param name="obj">The obj.</param>
		/// <param name="namespaces">The namespaces.</param>
		/// <param name="extraTypes">The extra types.</param>
		public static void Serialize ( XmlWriter xmlWriter, T obj, XmlSerializerNamespaces namespaces, Type[] extraTypes ) {
			try {
				CreateXmlSerializer ( extraTypes ).Serialize ( xmlWriter, obj, namespaces );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Serializes the specified XML writer.
		/// </summary>
		/// <param name="xmlWriter">The XML writer.</param>
		/// <param name="obj">The obj.</param>
		/// <param name="namespaces">The namespaces.</param>
		/// <param name="encoding">The encoding.</param>
		public static void Serialize ( XmlWriter xmlWriter, T obj, XmlSerializerNamespaces namespaces, Encoding encoding ) {
			try {
				CreateXmlSerializer ( ).Serialize ( xmlWriter, obj, namespaces, encoding != null ? encoding.WebName : string.Empty );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Serializes the specified XML writer.
		/// </summary>
		/// <param name="xmlWriter">The XML writer.</param>
		/// <param name="obj">The obj.</param>
		/// <param name="namespaces">The namespaces.</param>
		/// <param name="encoding">The encoding.</param>
		/// <param name="extraTypes">The extra types.</param>
		public static void Serialize ( XmlWriter xmlWriter, T obj, XmlSerializerNamespaces namespaces, Encoding encoding, Type[] extraTypes ) {
			try {
				CreateXmlSerializer ( extraTypes ).Serialize ( xmlWriter, obj, namespaces, encoding != null ? encoding.WebName : string.Empty );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Serializes the specified XML writer.
		/// </summary>
		/// <param name="xmlWriter">The XML writer.</param>
		/// <param name="obj">The obj.</param>
		/// <param name="namespaces">The namespaces.</param>
		/// <param name="encoding">The encoding.</param>
		/// <param name="id">The id.</param>
		public static void Serialize ( XmlWriter xmlWriter, T obj, XmlSerializerNamespaces namespaces, Encoding encoding, string id ) {
			try {
				CreateXmlSerializer ( ).Serialize ( xmlWriter, obj, namespaces, encoding != null ? encoding.WebName : string.Empty, id );
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Serializes the specified XML writer.
		/// </summary>
		/// <param name="xmlWriter">The XML writer.</param>
		/// <param name="obj">The obj.</param>
		/// <param name="namespaces">The namespaces.</param>
		/// <param name="encoding">The encoding.</param>
		/// <param name="id">The id.</param>
		/// <param name="extraTypes">The extra types.</param>
		public static void Serialize ( XmlWriter xmlWriter, T obj, XmlSerializerNamespaces namespaces, Encoding encoding, string id, Type[] extraTypes ) {
			try {
				CreateXmlSerializer ( extraTypes ).Serialize ( xmlWriter, obj, namespaces, encoding != null ? encoding.WebName : string.Empty, id );
			} catch ( Exception ) {
				throw;
			}
		}
		#endregion
	}
}
