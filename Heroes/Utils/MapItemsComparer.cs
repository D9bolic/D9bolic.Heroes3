using Heroes.Map;

namespace Heroes.Utils;

public class MapItemsComparer : IEqualityComparer<IMapItem>
{
    public bool Equals(IMapItem? x, IMapItem? y)
    {
        return x.Coordinates.Equals(y.Coordinates);
    }

    public int GetHashCode(IMapItem obj)
    {
        return obj.Coordinates.GetHashCode();
    }
}