using Alba.CsConsoleFormat;
using Heroes.Units;
using Heroes.Units.Army;
using Point = System.Drawing.Point;

namespace Heroes.Map.Rectangle;

public class RectangleMap : IMap
{
    private readonly int _columns;
    private readonly int _rows;
    private readonly List<RectangleCell> _cells = new List<RectangleCell>();

    public RectangleMap(int columns, int rows)
    {
        _columns = columns;
        _rows = rows;

        for (var row = 0; row < _rows; row++)
        {
            for (var column = 0; column < _columns; column++)
            {
                _cells.Add(new RectangleCell(new Point(column, row)));
            }
        }
    }

    public IEnumerable<ICell> Cells => _cells;
    
    
    public IEnumerable<ICell> GetCellsInDistance(IMap map, ICell point, int distance)
    {
        var result = new List<ICell>();
        foreach (var cell in map.Cells)
        {
            if (IsInDistance(cell, point, distance))
            {
                result.Add(cell);
            }
        }

        return result;
    }
    
    public static bool IsInDistance(ICell cell, ICell point, int distance)
    {
        if (cell.Coordinates.X > point.Coordinates.X && cell.Coordinates.Y > point.Coordinates.Y)
        {
            return false;
        }

        if (cell.Coordinates.X > point.Coordinates.X &&
            cell.Coordinates.X <= point.Coordinates.X + distance &&
            cell.Coordinates.Y == point.Coordinates.Y)
        {
            return true;
        }

        if (cell.Coordinates.X < point.Coordinates.X &&
            cell.Coordinates.X >= point.Coordinates.X - distance &&
            cell.Coordinates.Y == point.Coordinates.Y)
        {
            return true;
        }

        if (cell.Coordinates.Y > point.Coordinates.Y &&
            cell.Coordinates.Y <= point.Coordinates.Y + distance &&
            cell.Coordinates.X == point.Coordinates.X)
        {
            return true;
        }

        if (cell.Coordinates.Y < point.Coordinates.Y &&
            cell.Coordinates.Y >= point.Coordinates.Y - distance &&
            cell.Coordinates.X == point.Coordinates.X)
        {
            return true;
        }
        
        return false;
    }
}