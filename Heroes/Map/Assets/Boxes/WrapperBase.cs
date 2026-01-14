using System.Drawing;
using Heroes.Map;

namespace Heroes.Menu.Unit;

public abstract class WrapperBase : IWrapper
{
    private readonly IMapItem _item;

    public WrapperBase(IMapItem item)
    {
        _item = item;
    }

    protected abstract string GetName(IMapItem item);

    public Point Coordinates
    {
        get => _item.Coordinates;
        set => _item.Coordinates = value;
    }

    public string Name => GetName(_item is IWrapper wrapper ? wrapper.Item : _item);

    public IMapItem Item => _item;
    
    public IMapItem Unwrap()
    {
        return _item;
    }
}