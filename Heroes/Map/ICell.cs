using System.Drawing;
using Heroes.Units;
using Heroes.Units.Army;

namespace Heroes.Map;

public interface ICell
{
    public Point Coordinates { get; }
    
    IMapItem? PlacedItem { get; set; }
    
    public IMap Map { get; }
    
    public ConsoleColor TextColor { get; set; }
    
    public ConsoleColor BackgroundColor { get; set; }
}