using Heroes.Map.Assets.ConsoleAssets.Assets;

namespace Heroes.Map.Assets.ConsoleAssets.Patterns.Rectangle;

public class RectanglePattern : IConsolePattern
{
    public IAsset Wrap(ConsoleAsset asset)
    {
        return new RectangleConsoleAsset(asset);
    }

    public IEnumerable<(string Key, IAsset Asset)> SpecificAssets =>
    [
        ("New Line", new RectangleNewLineAsset())
    ];
}