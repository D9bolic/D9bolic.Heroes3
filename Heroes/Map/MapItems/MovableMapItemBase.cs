namespace Heroes.Map;

public abstract class MovableMapItemBase : IMapItem
{
    protected ICell? _cell;
    protected bool IsCellInited = false;

    protected MovableMapItemBase(string literal, string name)
    {
        Literal = literal;
        Name = name;
    }

    public virtual ICell? Cell
    {
        get => _cell;
        set
        {
            if (_cell is not null)
            {
                _cell.PlacedItem = null;
            }

            _cell = value;
            
            if (value is not null)
            {
                value.PlacedItem = this;
            }
        }
    }
    
    public string Literal { get; }
    
    public string Name { get; }
}