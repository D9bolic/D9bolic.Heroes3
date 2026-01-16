using Heroes.Assets.ConsoleAssets.Assets;

namespace Heroes.Assets.ConsoleAssets.Patterns.Hexagonal;

public class HexPattern : IConsolePattern
{
    public IAsset Wrap(ConsoleAsset asset)
    {
        return new HexConsoleAsset(asset);
    }

    public IEnumerable<(string Key, IAsset Asset)> SpecificAssets =>
    [
        ("New Line", new HexNewLineAsset()),
        ("Shift", new HexShiftAsset()),
    ];
}