using Heroes.Map;

namespace Heroes.Menu.Unit;

public interface IWrapper : IMapItem
{
    IMapItem Wrap();
    
    void Unwrap();
}