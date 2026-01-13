using System.Drawing;

namespace Heroes.Map;

public interface IMapItem
{
    public Point Coordinates { get; }
    
    public string Name { get; }
}