using Alba.CsConsoleFormat;
using Heroes.Units;
using Heroes.Units.Army;
using static System.ConsoleColor;
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
                _cells.Add(new RectangleCell(new Point(column, row), this));
            }
        }
    }

    public IEnumerable<ICell> Cells => _cells;

    public void Draw()
    {
        var grid = new Grid
        {
            Color = Gray,
        };


        var gridColumns = Enumerable.Range(0, _columns).Select(x => new Column
        {
            Width = GridLength.Auto
        });

        grid.Columns.Add(gridColumns);

        foreach (var cell in _cells)
        {
            grid.Children.Add(new Cell
            {
                Color = cell.TextColor,
                Background = cell.BackgroundColor,
                Children =
                {
                    new Span
                    {
                        Text = cell.PlacedItem is null ? " " : cell.PlacedItem.Literal,
                    }
                }
            });
        }
        ConsoleRenderer.RenderDocument(new Document(grid));
    }

    public void PlaceUnit(IUnit unit, Point coordinates)
    {
        var cell = _cells.First(x => x.Coordinates.X == coordinates.X && x.Coordinates.Y == coordinates.Y);
        unit.Cell = cell;
    }
}