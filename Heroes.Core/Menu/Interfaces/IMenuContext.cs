using Heroes.Assets;
using Heroes.Map;

namespace Heroes.Menu.Interfaces;

public interface IMenuContext
{
    IMap Map { get; }

    IEnumerable<IMapItem> Elements { get; }
}
