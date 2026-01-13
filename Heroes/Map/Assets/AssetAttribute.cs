namespace Heroes.Map;

[AttributeUsage(AttributeTargets.Class)]
public class AssetAttribute : Attribute
{
    public string Name { get; }
    public string ShortName { get; }

    public AssetAttribute(string name, string shortName)
    {
        Name = name;
        ShortName = shortName;
    }
}