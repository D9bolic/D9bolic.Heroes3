namespace Heroes.Map;

public abstract class LandscapeBase : ILandscape
{
    protected ICell? _cell;
    protected bool IsCellInited = false;

    protected LandscapeBase(string literal, string name)
    {
        Literal = literal;
        Name = name;
    }


    public virtual ICell? Cell
    {
        get => _cell;
        set
        {
            if (value is null)
            {
                _cell = value;
            }

            if (value is not null)
            {
                _cell = value;
                _cell.PlacedItem = this;
                _cell.BackgroundColor = BackgroundColor;
                _cell.TextColor = TextColor;
            }
        }
    }
    
    public string Literal { get; }
    
    public string Name { get; }

    public abstract ConsoleColor TextColor { get; }
    
    public abstract ConsoleColor BackgroundColor { get; }

    public virtual bool IsPointToMove => true;
}