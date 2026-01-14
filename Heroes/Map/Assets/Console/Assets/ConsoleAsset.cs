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