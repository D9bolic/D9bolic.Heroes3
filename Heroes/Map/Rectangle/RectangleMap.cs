using Point = System.Drawing.Point;

namespace Heroes.Map.Rectangle;

public class RectangleMap : IMap
{
    private readonly int _columns;
    private readonly int _rows;
    private readonly List<RectangleCell> _cells = new List<RectangleCell>();
    private readonly IAssetsStore _assetsStore;

    public RectangleMap(int columns, int rows, IAssetsStore assetsStore)
    {
        _columns = columns;
        _rows = rows;
        _assetsStore = assetsStore;

        for (var row = 0; row < _rows; row++)
        {
            for (var column = 0; column < _columns; column++)
            {
                _cells.Add(new RectangleCell(new Point(column, row)));
            }
        }
    }

    public IEnumerable<IMapItem> Cells => _cells;

    public void Draw(IEnumerable<IMapItem> mapItems)
    {
        for (var row = 0; row < _rows; row++)
        {
            for (var column = 0; column < _columns; column++)
            {
                var coordinate = new Point(column, row);
                var mapItem = mapItems.FirstOrDefault(x => x.Coordinates.Equals(coordinate));
                var asset = mapItem is null
                    ? _assetsStore.GetAsset(_cells.First(x => x.Coordinates.Equals(coordinate)))
                    : _assetsStore.GetAsset(mapItem);
                
                asset.Draw();
            }

            _assetsStore.GetAsset(new NewLineItem()).Draw();
        }
    }

    public IEnumerable<IMapItem> GetCellsInDistance(Point point, int distance)
    {
        var result = new List<IMapItem>();
        foreach (var cell in Cells)
        {
            if (IsInDistance(cell, point, distance))
            {
                result.Add(cell);
            }
        }

        return result;
    }
    
    public static bool IsInDistance(IMapItem cell, Point Coordinates, int distance)
    {
        if (cell.Coordinates.X > Coordinates.X && cell.Coordinates.Y > Coordinates.Y)
        {
            return false;
        }

        if (cell.Coordinates.X > Coordinates.X &&
            cell.Coordinates.X <= Coordinates.X + distance &&
            cell.Coordinates.Y == Coordinates.Y)
        {
            return true;
        }

        if (cell.Coordinates.X < Coordinates.X &&
            cell.Coordinates.X >= Coordinates.X - distance &&
            cell.Coordinates.Y == Coordinates.Y)
        {
            return true;
        }

        if (cell.Coordinates.Y > Coordinates.Y &&
            cell.Coordinates.Y <= Coordinates.Y + distance &&
            cell.Coordinates.X == Coordinates.X)
        {
            return true;
        }

        if (cell.Coordinates.Y < Coordinates.Y &&
            cell.Coordinates.Y >= Coordinates.Y - distance &&
            cell.Coordinates.X == Coordinates.X)
        {
            return true;
        }
        
        return false;
    }
}