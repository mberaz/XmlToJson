using Newtonsoft.Json.Linq;
using NJsonSchema;
using System.Xml.Linq;
using JsonSchema = NJsonSchema.JsonSchema;

namespace XmlToJson
{
    public class JsonConverter
    {
        public static JObject Convert(JsonSchema schema, string document) =>
                                    Convert(schema, XDocument.Parse(document));
        public static JObject Convert(JsonSchema schema, XDocument document)
        {
            return Parse(schema, document.Descendants().ToList());
        }

        private static JObject Parse(JsonSchema schema, List<XElement> elements)
        {
            var result = new JObject();

            foreach (var property in schema.Properties)
            {
                var element = elements.FirstOrDefault(e => e.Name == property.Key);
                var xmlValue = element?.Value;

                //if the XML value is NULL check the schema to see if there are a default value
                xmlValue ??= property.Value.Default?.ToString();

                //if the value is NULL and the property is required throw an Exception
                if (xmlValue == null && schema.RequiredProperties.Contains(property.Key))
                {
                    throw new Exception($"Required property [{property.Key}] is missing from XML");
                }

                if (xmlValue == null)
                {
                    break;
                }

                switch (property.Value.ActualTypeSchema.Type)
                {
                    case JsonObjectType.Boolean:
                    case JsonObjectType.String:
                    case JsonObjectType.Integer:
                    case JsonObjectType.Number:
                        result[property.Key] = ParsePlain(xmlValue, property.Value.Type);
                        break;

                    case JsonObjectType.Array:
                        if (property.Value.Item.Reference?.Type == JsonObjectType.Object)
                        {   //array of objects 
                            var array = element.Elements()
                                .Select(e => Parse(property.Value.Item.Reference, e.Elements().ToList()));
                            
                            result[property.Key] = new JArray(array);
                        }
                        else
                        {   //array of plain items
                            var array = element.Elements()
                                .Select(c => ParsePlain(c.Value, property.Value.Item.Type));
                           
                            result[property.Key] = new JArray(array);
                        }

                        break;

                    case JsonObjectType.Object:
                        result[property.Key] = Parse(property.Value.ActualTypeSchema, element.Descendants().ToList());

                        break;
                }
            }

            return result;
        }

        private static JToken ParsePlain(string xmlValue, JsonObjectType type)
        {
            return type switch
            {
                JsonObjectType.String => xmlValue,
                JsonObjectType.Boolean => bool.Parse(xmlValue),
                JsonObjectType.Integer => int.Parse(xmlValue),
                JsonObjectType.Number => double.Parse(xmlValue)
            };
        }
    }
}
