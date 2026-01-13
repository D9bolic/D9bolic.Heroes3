using System.Drawing;
using Heroes.Map;

namespace Heroes.Menu.Unit;

public class AllyUnitBox : IMapItem
{
    private readonly IMapItem _item;

    public AllyUnitBox(IMapItem item)
    {
        _item = item;
    }

    public Point Coordinates => _item.Coordinates;

    public string Name => $"{_item.Name}:Ally";
}