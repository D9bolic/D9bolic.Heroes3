using System.Drawing;

namespace Heroes.Map;

public interface ICell
{
    public Point Coordinates { get; }
}