using Heroes.Assets.ConsoleAssets.Assets;

namespace Heroes.Assets.ConsoleAssets.Patterns.Hexagonal;

public class HexConsoleAsset(ConsoleAsset asset)
    : ConsoleAssetBase, IAsset
{
    public void Draw()
    {
        var originBackground = ConsoleColor.Black;
        var originColor = ConsoleColor.Gray;
        System.Console.BackgroundColor = originBackground;
        System.Console.ForegroundColor =originColor;
        var origRow = System.Console.CursorTop;
        var origCol = System.Console.CursorLeft;


        SetCursorPosition(origCol + 2, origRow);
        System.Console.Write("─");
        SetCursorPosition(origCol, origRow + 1);
        System.Console.Write("/");
        System.Console.BackgroundColor = asset.BackgroundColor;
        System.Console.ForegroundColor = asset.TextColor;
        System.Console.Write("   ");
        System.Console.BackgroundColor = originBackground;
        System.Console.ForegroundColor = originColor;
        System.Console.Write("\\");
        SetCursorPosition(origCol, origRow + 2);
        System.Console.BackgroundColor = asset.BackgroundColor;
        System.Console.ForegroundColor = asset.TextColor;
        System.Console.Write($"  ");
        System.Console.BackgroundColor = originBackground;
        System.Console.ForegroundColor = originColor;
        asset.Draw();
        System.Console.BackgroundColor = asset.BackgroundColor;
        System.Console.ForegroundColor = asset.TextColor;
        System.Console.Write($"  ");
        System.Console.BackgroundColor = originBackground;
        System.Console.ForegroundColor = originColor;

        SetCursorPosition(origCol, origRow + 3);
        System.Console.Write("\\");
        System.Console.BackgroundColor = asset.BackgroundColor;
        System.Console.ForegroundColor = asset.TextColor;
        System.Console.Write("   ");
        System.Console.BackgroundColor = originBackground;
        System.Console.ForegroundColor = originColor;
        System.Console.Write("/");
        SetCursorPosition(origCol + 2, origRow + 4);
        System.Console.Write("─");
        
        SetCursorPosition(origCol + 8, origRow);
    }
}