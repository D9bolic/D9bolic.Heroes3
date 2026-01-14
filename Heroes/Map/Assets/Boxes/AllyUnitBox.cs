using System.Drawing;
using Heroes.Map;

namespace Heroes.Menu.Unit;

public class AllyUnitBox(IMapItem item) : WrapperBase(item)
{
    protected override string GetName(IMapItem item)
    {
        return $"{item.Name}:Ally";
    }
}

public abstract class WrapperBase : IWrapper
{
    private readonly IMapItem _item;

    public WrapperBase(IMapItem item)
    {
        _item = item;
    }

    protected abstract string GetName(IMapItem item);

    public Point Coordinates => _item.Coordinates;

    public string Name => GetName(_item is IWrapper wrapper ? wrapper.Item : _item);

    public IMapItem Item => _item;
    
    public IMapItem Unwrap()
    {
        return _item;
    }
}