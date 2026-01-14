namespace Heroes.Map;

public interface IConsolePattern
{
    IAsset Wrap(ConsoleAsset asset);
}

public class RectanglePattern : IConsolePattern
{
    public IAsset Wrap(ConsoleAsset asset)
    {
        return new RectangleConsoleAsset(asset);
    }
}

public class RectangleConsoleAsset(ConsoleAsset asset)
    : ConsoleAssetBase, IAsset
{
    public void Draw()
    {
        var originBackground = ConsoleColor.Black;
        var originColor = ConsoleColor.Gray;
        Console.BackgroundColor = originBackground;
        Console.ForegroundColor = originColor;
        var origRow = Console.CursorTop;
        var origCol = Console.CursorLeft;


        Console.Write("┌─┐");
        SetCursorPosition(origCol, origRow + 1);
        Console.Write($"│");
        asset.Draw();
        Console.Write($"│");

        SetCursorPosition(origCol, origRow + 2);
        Console.Write("└─┘");
        SetCursorPosition(origCol, origRow + 2);
        Console.Write("└─┘");
        
        SetCursorPosition(origCol + 3, origRow);
    }
}