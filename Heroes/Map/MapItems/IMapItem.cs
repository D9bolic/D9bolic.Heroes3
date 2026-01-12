namespace Heroes.Map;

public interface IMapItem
{
    public ICell? Cell { get; set; }
    
    public string Literal { get; }
    
    public string Name { get; }
}

public interface ILandscape : IMapItem
{
    ConsoleColor TextColor { get; }
    
    ConsoleColor BackgroundColor { get; }

    public bool IsPointToMove { get; }
}