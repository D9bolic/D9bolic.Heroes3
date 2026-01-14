namespace Heroes.Map;

public class ConsoleNewLineAsset() : ConsoleAssetBase, IAsset
{
    public void Draw()
    {
        var origRow = Console.CursorTop;

        SetCursorPosition(0, origRow + 3);
    }
}