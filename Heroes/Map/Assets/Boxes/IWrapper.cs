using Heroes.Map;

namespace Heroes.Menu.Unit;

public interface IWrapper : IMapItem
{
    IMapItem Item { get; }

    IMapItem Unwrap();
}