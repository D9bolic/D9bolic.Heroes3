using Heroes.Assets.ConsoleAssets.Assets;

namespace Heroes.Assets.ConsoleAssets.Patterns.Rectangle;

public class RectangleConsoleAsset(ConsoleAsset asset)
    : ConsoleAssetBase, IAsset
{
    public void Draw()
    {
        var originBackground = ConsoleColor.Black;
        var originColor = ConsoleColor.Gray;
        System.Console.BackgroundColor = originBackground;
        System.Console.ForegroundColor = originColor;
        var origRow = System.Console.CursorTop;
        var origCol = System.Console.CursorLeft;


        System.Console.Write("┌─┐");
        SetCursorPosition(origCol, origRow + 1);
        System.Console.Write($"│");
        asset.Draw();
        System.Console.Write($"│");

        SetCursorPosition(origCol, origRow + 2);
        System.Console.Write("└─┘");
        
        SetCursorPosition(origCol + 3, origRow);
    }
}