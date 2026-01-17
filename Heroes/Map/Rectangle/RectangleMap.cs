using Heroes.Assets;
using Heroes.Map.Hex;
using Heroes.Map.Landscape;
using Point = System.Drawing.Point;

namespace Heroes.Map.Rectangle;

public class RectangleMap : IMap
{
    private readonly int _columns;
    private readonly int _rows;
    private readonly List<ILandscape> _cells = new List<ILandscape>();
    private readonly IAssetsStore _assetsStore;
    private readonly IDrawableItem _newLineItem = new NewLineItem();

    private readonly IEnumerable<(string Direction, Point Coordinates)> _neighborOffsets = new
        (string direction, Point coordinates)[]
        {
            new("right", new Point(+1, 0)),
            new("up", new Point(0, -1)),
            new("left", new Point(-1, 0)),
            new("down", new Point(0, +1)),
        };

    public RectangleMap(int columns, int rows, IAssetsStore assetsStore)
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

            _assetsStore.GetAsset(_newLineItem).Draw();
        }
    }

    public IEnumerable<IMapItem> GetClosePoints(Point point)
    {
        var neiborgs = new HashSet<Point>();
        foreach (var offset in _neighborOffsets)
        {
            neiborgs.Add(new Point
            {
                X = point.X + offset.Coordinates.X,
                Y = point.Y + offset.Coordinates.Y
            });
        }

        return Cells.Where(x => neiborgs.Contains(x.Coordinates)).ToArray();
    }

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

    public string GetDirection(Point from, Point to)
    {
        var direction = _neighborOffsets
            .FirstOrDefault(o => o.Coordinates.X + from.X == to.X &&
                                 o.Coordinates.Y + from.Y == to.Y)
            .Direction;
        return string.IsNullOrEmpty(direction) ? $"{to.X}, {to.Y}" : direction;
    }
}