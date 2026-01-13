using System.Drawing;
using Heroes.Units;
using Heroes.Units.Army;

namespace Heroes.Map;

public interface ICell
{
    public Point Coordinates { get; }
}