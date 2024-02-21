namespace XmlToJson.Tests;

public class ObjectWithInnerObjectListClass
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string age { get; set; }
    public List<InnerObject> innerList { get; set; }
}