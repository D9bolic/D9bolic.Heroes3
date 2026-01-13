using System.Drawing;
using Heroes.Map;

namespace Heroes.Menu.Unit;

public class AttackedUnitBox : IMapItem
{
    private readonly IMapItem _item;

    public AttackedUnitBox(IMapItem item)
    {
        _item = item;
    }

    public Point Coordinates => _item.Coordinates;

    public string Name => $"{_item.Name}:Attack";
}

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

public class EnemyUnitBox : IMapItem
{
    private readonly IMapItem _item;

    public EnemyUnitBox(IMapItem item)
    {
        _item = item;
    }

    public Point Coordinates => _item.Coordinates;

    public string Name => $"{_item.Name}:Enemy";
}

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