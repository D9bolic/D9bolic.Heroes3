using System.Drawing;

namespace Heroes.Map;


public interface IDrawableItem
{
    public string Name { get; }
}

public interface IMapItem : IDrawableItem
{
    public Point Coordinates { get; set; }
}