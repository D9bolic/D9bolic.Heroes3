using System.Drawing;
using Heroes.Map;

namespace Heroes.Menu.Unit;

public class SelectionBox : IMapItem
{
    private readonly IMapItem _item;

    public SelectionBox(IMapItem item)
    {
        _item = item;
    }

    public Point Coordinates => _item.Coordinates;

    public string Name => $"{_item.Name}:Selection";
}