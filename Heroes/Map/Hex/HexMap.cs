using Heroes.Assets;
using Heroes.Map.Landscape;
using Point = System.Drawing.Point;

namespace Heroes.Map.Hex;

public class HexMap : IMap
{
    private readonly int _columns;
    private readonly int _rows;
    private readonly List<ILandscape> _cells = new List<ILandscape>();
    private readonly IAssetsStore _assetsStore;
    private readonly IDrawableItem _newLineItem = new NewLineItem();
    private readonly IDrawableItem _shiftItem = new ShiftItem();
    private readonly IEnumerable<(string Direction, Point Coordinates)> 
        _evenNeighborOffsets = new
        (string direction, Point coordinates)[]
        {
            new("right", new Point(+1, 0)),
            new("left", new Point(-1, 0)),
            new("down-right", new Point(0, +1)),
            new("up-left", new Point(0, -1)),
            new("up-right", new Point(+1, +1)),
            new("down-right", new Point(+1, -1)),
        };
    
    private readonly IEnumerable<(string Direction, Point Coordinates)> 
        _oddNeighborOffsets = new
            (string direction, Point coordinates)[]
            {
                new("right", new Point(+1, 0)),
                new("left", new Point(-1, 0)),
                new("down-right", new Point(0, +1)),
                new("up-left", new Point(0, -1)),
                new("up-right", new Point(-1, -1)),
                new("down-right", new Point(-1, +1)),
            };

    public HexMap(int columns, int rows, IAssetsStore assetsStore)
    {
        _columns = columns;
        _rows = rows;
        _assetsStore = assetsStore;

        for (var row = 0; row < _rows; row++)
        {
            for (var column = 0; column < _columns; column++)
            {
                _cells.Add(new EmptyCell(new Point(column, row)));
            }
        }
    }

    public IEnumerable<ILandscape> Cells => _cells;

    public void InitializeLandscape(IEnumerable<ILandscape> landscapes)
    {
        var query = from cell in _cells
            join landscale in landscapes on cell.Coordinates equals landscale.Coordinates
            select new
            {
                Landscape = landscale,
                Index = _cells.IndexOf(cell),
            };

        foreach (var cell in query.ToArray())
        {
            _cells[cell.Index] = cell.Landscape;
        }
    }

    public void Draw(IEnumerable<IMapItem> mapItems)
    {
        for (var row = 0; row < _rows; row++)
        {
            if (row % 2 != 0)
            {
                _assetsStore.GetAsset(_shiftItem).Draw();
            }

            for (var column = 0; column < _columns; column++)
            {
                var coordinate = new Point(column, row);
                var mapItem = mapItems.FirstOrDefault(x => x.Coordinates.Equals(coordinate));
                var asset = mapItem is null
                    ? _assetsStore.GetAsset(_cells.First(x => x.Coordinates.Equals(coordinate)))
                    : _assetsStore.GetAsset(mapItem);

                asset.Draw();
            }

            _assetsStore.GetAsset(_newLineItem).Draw();
            if (row >= _rows - 1)
            {
                _assetsStore.GetAsset(_newLineItem).Draw();
                _assetsStore.GetAsset(_newLineItem).Draw();
            }
        }
    }

    public IEnumerable<IMapItem> GetClosePoints(Point point)
    {        
        var offsets = point.Y % 2 == 1
            ? _evenNeighborOffsets
            : _oddNeighborOffsets;
        var neighbors = new HashSet<Point>();
        foreach (var offset in offsets)
        {
            neighbors.Add(new Point
            {
                X = point.X + offset.Coordinates.X,
                Y = point.Y + offset.Coordinates.Y
            });
        }

        return Cells.Where(x => neighbors.Contains(x.Coordinates)).ToArray();
    }

    public string GetDirection(Point from, Point to)
    {
        var offsets = from.Y % 2 == 1
            ? _evenNeighborOffsets
            : _oddNeighborOffsets;
        var direction = offsets
            .FirstOrDefault(o => o.Coordinates.X + from.X == to.X &&
                                 o.Coordinates.Y + from.Y == to.Y)
            .Direction;
        return string.IsNullOrEmpty(direction) ? $"{to.X}, {to.Y}" : direction;
    }
}