using System.Xml.Linq;

namespace XmlToJson.Tests
{
    [TestClass]
    public class JsonConvertorTests
    {
        [TestMethod]
        public void PlainXml()
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

            XDocument doc = XDocument.Parse(xmlData);
            var root = doc.Root;
            var name = root.Element("firstName").Value;
            Assert.IsNotNull(name);

            var hobbies = root.Element("hobbies").Descendants();
            foreach (var hobby in hobbies)
            {
                Assert.IsNotNull(hobby.Value);
            }
        }
    }
}