using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Utils.Extensions
{
    /// <summary>A helper class for working with XML strings</summary>
    public static class XmlExtensions
    {
        /// <summary>Deserialize an object from an XML string.</summary>
        /// <typeparam name="T">The type of object to deserialize into.</typeparam>
        /// <param name="xmlMessage">A string containing only an xml representation of the object to deserialize.</param>
        /// <returns>An object representative of the xml passed in.</returns>
        public static T DeserializeFromXml<T>(this string xmlMessage)
        {
            using (var sr = new StringReader(xmlMessage))
            {
                var xs = new XmlSerializer(typeof(T));
                return (T)xs.Deserialize(sr);
            }
        }

        /// <summary>
        ///     Serializes and object to an XML string.
        /// </summary>
        /// <typeparam name="T">The type of object to serialize.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="xn">The namespaces to use when writing the XML.</param>
        /// <returns>An xml string representation of a serializable object.</returns>
        public static string SerializeToXml<T>(this T obj, XmlSerializerNamespaces xn = null)
        {
            var xs = new XmlSerializer(typeof(T));

            using (var sw = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sw))
                {
                    xs.Serialize(writer, obj, xn);
                    return sw.ToString();
                }
            }
        }

        public static bool IsXml(this string input)
        {
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(input);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
