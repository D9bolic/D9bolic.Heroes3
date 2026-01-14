using Heroes.Map;
using Heroes.Players;
using Heroes.Units.Army;
using Heroes.Utils;

namespace Heroes.Menu;

public class TurnInformation
{
    public IUnit ActiveUnit { get; set; }

    public IEnumerable<IMapItem> Allies { get; set; } = new List<IMapItem>();

    public IEnumerable<IMapItem> Enemies { get; set; } = new List<IMapItem>();

    public IEnumerable<IMapItem> Obstacles { get; set; } = new List<IMapItem>();

    public IEnumerable<IMapItem> ExtraObjects { get; set; } = new List<IMapItem>();

    public IMap Map { get; set; }

    public IEnumerable<IMapItem> Elements
    {
        get
        {
            var comparer = new MapItemsComparer();
            return ExtraObjects.Union(Allies, comparer).Union(Enemies, comparer).Union(Obstacles, comparer).ToArray();
        }
    }
}