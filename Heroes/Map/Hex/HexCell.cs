using Heroes.Map.Assets;
using Point = System.Drawing.Point;

namespace Heroes.Map.Hex;

public class HexCell : IMapItem
{
    public HexCell(Point coordinates)
    {
        Coordinates = coordinates;
    }

    public Point Coordinates { get; set; }

    public string Name => "Empty";
}