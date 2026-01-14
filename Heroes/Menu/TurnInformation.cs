using Heroes.Map;
using Heroes.Players;
using Heroes.Units.Army;

namespace Heroes.Menu;

public class TurnInformation
{
    public IUnit ActiveUnit { get; set; }
    
    public IEnumerable<IMapItem> Allies { get; set; }
    
    public IEnumerable<IMapItem> Enemies { get; set; }
    
    public IEnumerable<IMapItem> Obstacles { get; set; }
    
    public IMap Map { get; set; }
    
    public IEnumerable<IMapItem> Elements => Allies.Concat(Enemies).Concat(Obstacles).ToArray();
}