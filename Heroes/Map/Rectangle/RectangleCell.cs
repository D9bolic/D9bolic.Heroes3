using Alba.CsConsoleFormat;
using System.Drawing;
using Heroes.Units;
using Heroes.Units.Army;
using Heroes.Utils;
using Point = System.Drawing.Point;

namespace Heroes.Map.Rectangle;

public class RectangleCell : ICell
{
    private IMapItem? _item;
    private ILandscape? _landscape;

    public RectangleCell(Point coordinates, IMap map)
    {
        Map = map;
        Coordinates = coordinates;
        _item = new EmptyMapItem();
    }

    public Point Coordinates { get; }

    public IMap Map { get; }

    public ConsoleColor TextColor { get; set; }

    public ConsoleColor BackgroundColor { get; set; }

    public IMapItem? PlacedItem
    {
        get => _item ?? new EmptyMapItem();
        set
        {
            if (_item is ILandscape landscape)
            {
                _landscape = landscape;
            }

            if (value is null && _landscape is not null)
            {
                _item = _landscape;
                BackgroundColor = _landscape.BackgroundColor;
                TextColor = _landscape.TextColor;
                
                return;
            }

            _item = value;
            BackgroundColor = ConsoleColor.Black;
            TextColor = ConsoleColor.White;
        }
    }
}