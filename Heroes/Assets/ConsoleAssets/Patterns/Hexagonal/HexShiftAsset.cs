using Heroes.Assets.ConsoleAssets.Assets;

namespace Heroes.Assets.ConsoleAssets.Patterns.Hexagonal;

public class HexShiftAsset() : ConsoleAssetBase, IAsset
{
    public void Draw()
    {
        var origRow = System.Console.CursorTop;
        var origCol = System.Console.CursorLeft;

        SetCursorPosition(origCol + 4, origRow);
    }
}

