namespace Heroes.Map;

public class RectanglePattern : IConsolePattern
{
    public IAsset Wrap(ConsoleAsset asset)
    {
        return new RectangleConsoleAsset(asset);
    }
}