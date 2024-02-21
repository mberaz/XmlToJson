using Newtonsoft.Json.Linq;

namespace XmlToJson.Tests
{
    [TestClass]
    public class JsonConverterTests
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
                        </user>";

            var schema = NJsonSchema.JsonSchema.FromType<ObjectWithPlainArrayClass>();
            var result = JsonConverter.Convert(schema, xmlData);

            Assert.IsNotNull(result);
            Assert.AreEqual(result["firstName"].Value<string>(), "jonn");
            Assert.AreEqual(result["lastName"].Value<string>(), "smith");
            Assert.AreEqual(result["age"].Value<int>(), 20);

            var array = result["hobbies"].Value<JArray>().Select(t=>t.Value<string>()).ToList();
            Assert.IsTrue(array.Contains("games")); 
            Assert.IsTrue(array.Contains("fishing")); 
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
                        </user>";

            var schema = NJsonSchema.JsonSchema.FromType<ObjectWithInnerObjectClass>();
            var result = JsonConverter.Convert(schema, xmlData);

            Assert.IsNotNull(result);

            Assert.AreEqual(result["firstName"].Value<string>(), "jonn");
            Assert.AreEqual(result["lastName"].Value<string>(), "smith");
            Assert.AreEqual(result["age"].Value<int>(), 20);

            Assert.AreEqual(result.SelectToken("inner.isActive").Value<bool>(), true);
            Assert.AreEqual(result.SelectToken("inner.rank").Value<int>(), 10);
        }

        [TestMethod]
        public void ObjectWithInnerObjectListXml()
        {
            var xmlData = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                        <user>
                            <firstName>jonn</firstName>
                            <lastName>smith</lastName>
                            <age>20</age>
                            <innerList>
                                <inner>
                                    <isActive>true</isActive>
                                    <rank>10</rank>
                                </inner>
                                <inner>
                                    <isActive>false</isActive>
                                    <rank>30</rank>
                                </inner>
                            </innerList>
                        </user>";

            var schema = NJsonSchema.JsonSchema.FromType<ObjectWithInnerObjectListClass>();
            var result = JsonConverter.Convert(schema, xmlData);

            Assert.IsNotNull(result);
            Assert.AreEqual(result["firstName"].Value<string>(), "jonn");
            Assert.AreEqual(result["lastName"].Value<string>(), "smith");
            Assert.AreEqual(result["age"].Value<int>(), 20);

            var array = result["innerList"].Value<JArray>().ToList();
            Assert.AreEqual(array[0].SelectToken("isActive").Value<bool>(), true);
            Assert.AreEqual(array[0].SelectToken("rank").Value<int>(), 10);

            Assert.AreEqual(array[1].SelectToken("isActive").Value<bool>(), false);
            Assert.AreEqual(array[1].SelectToken("rank").Value<int>(), 30);
        }
    }
}