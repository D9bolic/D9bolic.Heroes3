namespace Heroes.Map;

public class EmptyMapItem : LandscapeBase, ILandscape
{
    public EmptyMapItem() : base(" ", "Empty cell")
    {
    }

    public override ConsoleColor TextColor => ConsoleColor.White;

    public override ConsoleColor BackgroundColor => ConsoleColor.Black;
}

