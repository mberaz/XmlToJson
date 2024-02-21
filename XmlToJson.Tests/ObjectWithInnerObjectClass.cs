namespace XmlToJson.Tests;

public class ObjectWithInnerObjectClass
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string age { get; set; }
    public InnerObject inner { get; set; }
}

public class InnerObject
{
    public bool isActive { get; set; }
    public int rank { get; set; }
}