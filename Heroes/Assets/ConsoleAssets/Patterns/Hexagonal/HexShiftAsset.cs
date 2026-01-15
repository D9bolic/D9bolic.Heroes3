using Heroes.Map.Assets.ConsoleAssets.Assets;

namespace Heroes.Map.Assets.ConsoleAssets.Patterns.Hexagonal;

public class HexShiftAsset() : ConsoleAssetBase, IAsset
{
    public void Draw()
    {
        var origRow = System.Console.CursorTop;
        var origCol = System.Console.CursorLeft;

        SetCursorPosition(origCol + 2, origRow);
    }
}

