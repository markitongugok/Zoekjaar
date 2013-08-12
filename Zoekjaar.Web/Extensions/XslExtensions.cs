using System;
using System.IO;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Zoekjaar.Web.Extensions
{
	public static class XslExtensions
	{
		public static string Transform<T>(this T target, string transformFile) where T : class
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}

			XPathDocument xml = null;
			var serializer = new XmlSerializer(typeof(T));
			var namespaces = new XmlSerializerNamespaces();
			namespaces.Add(string.Empty, string.Empty);

			using (var ms = new MemoryStream())
			{
				serializer.Serialize(ms, target, namespaces);
				ms.Position = 0;
				xml = new XPathDocument(ms);
			}

			var transform = new XslCompiledTransform();
			transform.Load(transformFile);

			using (var ms = new MemoryStream())
			{
				transform.Transform(xml, null, ms);
				ms.Position = 0;
				var sr = new StreamReader(ms);
				return sr.ReadToEnd();
			}
		}
	}
}