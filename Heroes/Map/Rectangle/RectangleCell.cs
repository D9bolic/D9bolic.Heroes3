using Alba.CsConsoleFormat;
using System.Drawing;
using Heroes.Units;
using Heroes.Units.Army;
using Heroes.Utils;
using Point = System.Drawing.Point;

namespace Heroes.Map.Rectangle;

public class RectangleCell : ICell
{
    public RectangleCell(Point coordinates)
    {
        Coordinates = coordinates;
    }

    public Point Coordinates { get; }
}