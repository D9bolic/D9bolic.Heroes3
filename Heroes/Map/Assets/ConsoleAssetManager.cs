using Alba.CsConsoleFormat;

namespace Heroes.Map;

public class ConsoleAssetManager : IAssetManager
{
    private readonly IMap _map;
    private readonly IConsoleAssetStore _consoleAssetStore;

    public ConsoleAssetManager(IMap map, IConsoleAssetStore consoleAssetStore)
    {
        _map = map;
        _consoleAssetStore = consoleAssetStore;
    }
    
    public void DrawMap(IEnumerable<IMapItem> mapItems)
    {
        var grid = new Grid
        {
            Color = ConsoleColor.Gray,
        };
        var columns = _map.Cells.Max(x => x.Coordinates.X) + 1;

        var gridColumns = Enumerable.Range(0, columns).Select(x => new Column
        {
            Width = GridLength.Auto,
        });

        grid.Columns.Add(gridColumns);

        foreach (var cell in _map.Cells.OrderBy(x => x.Coordinates.X).OrderBy(x => x.Coordinates.Y))
        {
            var mapItem = mapItems.FirstOrDefault(x => x.Coordinates.Equals(cell.Coordinates));
            var asset = mapItem is null
                ? new ConsoleAsset(ConsoleColor.Gray, ConsoleColor.Black, $"({cell.Coordinates.X}, {cell.Coordinates.Y})")
                : _consoleAssetStore.GetAsset(mapItem);

            grid.Children.Add(new Cell
            {
                Color = asset.TextColor,
                TextAlign = TextAlign.Center,
                Background = asset.BackgroundColor,
                Children =
                {
                    new Span
                    {
                        Text = asset.Literal,
                    }
                }
            });
        }
        ConsoleRenderer.RenderDocument(new Document(grid));
    }
}