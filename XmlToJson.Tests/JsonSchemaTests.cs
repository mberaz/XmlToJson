using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NJsonSchema;

namespace XmlToJson.Tests
{
    [TestClass]
    public class JsonSchemaTests
    {
        [TestMethod]
        public void CreateSchema()
        {
            var sampleJson = new JObject
            {
                ["firstName"] = "jonn",
                ["lastName"] = "smith",
                ["age"] = 20,
                ["hobbies"] = new JArray
                {
                    "games",
                    "fishing"
                }

            };
            var schema = NJsonSchema.JsonSchema.FromSampleJson(sampleJson.ToString());

            var attributes = schema.Properties;
            Assert.IsNotNull(attributes);
        }
    }
}
