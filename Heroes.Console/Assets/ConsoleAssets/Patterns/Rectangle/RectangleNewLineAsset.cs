using Heroes.Console.Assets.ConsoleAssets.Assets;

namespace Heroes.Console.Assets.ConsoleAssets.Patterns.Rectangle;

public class RectangleNewLineAsset() : ConsoleAssetBase, IAsset
{
    public void Draw()
    {
        var origRow = System.Console.CursorTop;

        SetCursorPosition(0, origRow + 3);
    }
}
