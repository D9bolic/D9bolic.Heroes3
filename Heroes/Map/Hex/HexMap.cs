using Heroes.Assets;
using Point = System.Drawing.Point;

namespace Heroes.Map.Hex;

public class HexMap : IMap
{
    private readonly int _columns;
    private readonly int _rows;
    private readonly List<HexCell> _cells = new List<HexCell>();
    private readonly IAssetsStore _assetsStore;
    private readonly IDrawableItem _newLineItem = new NewLineItem();
    private readonly IDrawableItem _shiftItem = new ShiftItem();

    public HexMap(int columns, int rows, IAssetsStore assetsStore)
    {
        _columns = columns;
        _rows = rows;
        _assetsStore = assetsStore;

        for (var row = 0; row < _rows; row++)
        {
            for (var column = 0; column < _columns; column++)
            {
                _cells.Add(new HexCell(new Point(column, row)));
            }
        }
    }

    public IEnumerable<IMapItem> Cells => _cells;

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
        var neighbors = GetNeighbors(point);
        return Cells.Where(x => neighbors.Contains(x.Coordinates)).ToArray();
    }

    public static List<Point> GetNeighbors(Point hex) {
        List<Point> neighbors = new List<Point>();
        int q = hex.X;
        int r = hex.Y;

        // Define neighbor offsets based on row parity (odd-r example)
        int[][] neighborOffsets;
        if (r % 2 == 1) { 
            // Even row
            neighborOffsets = new int[][] {
                new int[] {+1, 0},
                new int[] {-1, 0}, 
                new int[] {0, +1}, 
                new int[] {0, -1}, 
                new int[] {+1, +1}, 
                new int[] {+1, -1}
            };
        } else { // Odd row
            neighborOffsets = new int[][] {
                new int[] {+1, 0},
                new int[] {-1, 0}, 
                new int[] {0, +1}, 
                new int[] {0, -1}, 
                new int[] {-1, +1}, 
                new int[] {-1, -1}
            };
        }

        foreach (var offset in neighborOffsets) {
            neighbors.Add(new Point { X = q + offset[0], Y = r + offset[1] });
        }
        
        return neighbors;
    }
}