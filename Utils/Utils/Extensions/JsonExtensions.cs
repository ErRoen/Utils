using System.Text;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Utils.Extensions
{
    public static class JsonExtensions
    {
        public static string SerializeToJson(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static byte[] SerializeToJsonByteArray(this object value)
        {
            var serializeAsString = value.SerializeToJson();
            return Encoding.UTF8.GetBytes(serializeAsString);
        }

        public static TEntity DeserializeFromJson<TEntity>(this string value)
        {
            return JsonConvert.DeserializeObject<TEntity>(value);
        }

        public static XmlDocument DeserializeXmlNodeFromJson(this string jsonContent, string deserializeRootElementName)
        {
            return JsonConvert.DeserializeXmlNode(jsonContent, deserializeRootElementName);
        }

        /// <summary>
        ///		Check return value whether xml of json
        /// </summary>
        /// <param name="input">Return string to check.</param>
        /// <returns>True if Json format, otherwise False</returns>
        public static bool IsJson(this string input)
        {
            var strInput = input.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}"))  //For object
                || (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    JToken.Parse(strInput);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
    }
}