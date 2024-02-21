using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace XmlToJson.Tests
{
  

    [TestClass]
    public class JsonConvertorTests
    {
        [TestMethod]
        public void ObjectWithPlainArrayXml()
        {
            var xmlData = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                        <user>
                            <firstName>jonn</firstName>
                            <lastName>smith</lastName>
                            <age>20</age>
                            <hobbies>
                                <hobby>games</hobby>
                                <hobby>fishing</hobby>
                            </hobbies>
                        </user>
                        ";

            var schema = NJsonSchema.JsonSchema.FromType<ObjectWithPlainArrayClass>();
            var result = JsonConvertor.Convert(schema, xmlData);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ObjectWithInnerObjectXml()
        {
            var xmlData = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                        <user>
                            <firstName>jonn</firstName>
                            <lastName>smith</lastName>
                            <age>20</age>
                            <inner>
                                <isActive>true</isActive>
                                <rank>10</rank>
                            </inner>
                        </user>
                        ";

            var schema = NJsonSchema.JsonSchema.FromType<ObjectWithInnerObjectClass>();
            var result = JsonConvertor.Convert(schema, xmlData);

            Assert.IsNotNull(result);
        }
    }
}