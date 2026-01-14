using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace Heroes.Map;

public class ConsoleAsset(ConsoleColor TextColor, ConsoleColor BackgroundColor, string Literal)
    : ConsoleAssetBase, IAsset
{
    public void Draw()
    {
        var originBackground = Console.BackgroundColor;
        var originColor = Console.ForegroundColor;
        Console.BackgroundColor = BackgroundColor;
        Console.ForegroundColor = TextColor;
        Console.Write($"{Literal}");
        Console.BackgroundColor = originBackground;
        Console.ForegroundColor = originColor;
    }
}

public class ConsoleNewLineAsset() : ConsoleAssetBase, IAsset
{
    public void Draw()
    {
        var origRow = Console.CursorTop;

        SetCursorPosition(0, origRow + 3);
    }
}

public class ConsoleLineAsset(string Literal) : ConsoleAssetBase, IAsset
{
    public void Draw()
    {
        var origRow = Console.CursorTop;
        Console.Write(Literal);
    }
}

public abstract class ConsoleAssetBase
{
    protected void SetCursorPosition(int col, int row)
    {
        int originalBufferWidth = Console.BufferWidth;
        int originalBufferHeight = Console.BufferHeight;

        if (originalBufferWidth < col + 1)
        {
            SetWindowSize(col + 1, originalBufferHeight);
        }

        if (originalBufferHeight < row + 1)
        {
            SetWindowSize(originalBufferWidth, row + 1);
        }

        Console.SetCursorPosition(col, row);
    }

    protected void SetWindowSize(int width, int height)
    {
        try
        {
            //   Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
        }
        catch (ArgumentOutOfRangeException)
        {
        }
        catch (IOException)
        {
        }
    }
}