namespace Heroes.Assets.ConsoleAssets.Assets;

public class ConsoleAsset(ConsoleColor TextColor, ConsoleColor BackgroundColor, string Literal)
    : ConsoleAssetBase, IAsset
{
    public ConsoleColor TextColor { get; } = TextColor;
    public ConsoleColor BackgroundColor { get; } =  BackgroundColor;
    public string Literal { get; } =  Literal;

    public void Draw()
    {
        var originBackground = System.Console.BackgroundColor;
        var originColor = System.Console.ForegroundColor;
        System.Console.BackgroundColor = BackgroundColor;
        System.Console.ForegroundColor = TextColor;
        System.Console.Write($"{Literal}");
        System.Console.BackgroundColor = originBackground;
        System.Console.ForegroundColor = originColor;
    }
}