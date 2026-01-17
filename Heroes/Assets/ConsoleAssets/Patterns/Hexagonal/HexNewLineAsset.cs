using Heroes.Assets.ConsoleAssets.Assets;

namespace Heroes.Assets.ConsoleAssets.Patterns.Hexagonal;

public class HexNewLineAsset() : ConsoleAssetBase, IAsset
{
    public void Draw()
    {
        var origRow = System.Console.CursorTop;

        SetCursorPosition(0, origRow + 2);
    }
}