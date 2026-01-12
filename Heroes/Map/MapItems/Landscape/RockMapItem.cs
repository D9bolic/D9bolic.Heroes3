namespace Heroes.Map;

public class RockMapItem : LandscapeBase, ILandscape
{
    public RockMapItem() : base("R", "Rock")
    {
    }

    public override ConsoleColor TextColor => ConsoleColor.DarkYellow;
    
    public override ConsoleColor BackgroundColor => ConsoleColor.Black;

    public override bool IsPointToMove => false;
}