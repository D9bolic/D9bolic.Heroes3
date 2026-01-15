using Heroes.Map;

namespace Heroes.Utils;

public class MapItemsComparer : IEqualityComparer<IMapItem>
{
    private static Lazy<MapItemsComparer> _instance = new Lazy<MapItemsComparer>(() => new MapItemsComparer());
    
    private MapItemsComparer(){}
    
    public bool Equals(IMapItem? x, IMapItem? y)
    {
        return x.Coordinates.Equals(y.Coordinates);
    }

    public int GetHashCode(IMapItem obj)
    {
        return obj.Coordinates.GetHashCode();
    }

    public static MapItemsComparer Instance => _instance.Value;
}