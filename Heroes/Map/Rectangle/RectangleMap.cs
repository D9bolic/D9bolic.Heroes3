using Heroes.Assets;
using Point = System.Drawing.Point;

namespace Heroes.Map.Rectangle;

public class RectangleMap : IMap
{
    private readonly int _columns;
    private readonly int _rows;
    private readonly List<RectangleCell> _cells = new List<RectangleCell>();
    private readonly IAssetsStore _assetsStore;
    private readonly IDrawableItem _newLineItem = new NewLineItem();

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

            _assetsStore.GetAsset(_newLineItem).Draw();
        }
    }

    public IEnumerable<IMapItem> GetClosePoints(Point point)
    {
        int q = point.X;
        int r = point.Y;
        List<Point> neiborgs = new List<Point>();
        var neighborOffsets = new int[][]
        {
            new int[] {+1, 0}, new int[] {0, -1},
            new int[] {-1, 0}, new int[] {0, +1},
        };

        foreach (var offset in neighborOffsets)
        {
            neiborgs.Add(new Point {X = q + offset[0], Y = r + offset[1]});
        }

        return Cells.Where(x => neiborgs.Contains(x.Coordinates)).ToArray();
    }
}