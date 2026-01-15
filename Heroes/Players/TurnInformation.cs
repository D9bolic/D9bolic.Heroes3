using Heroes.Map;
using Heroes.Menu.Unit;
using Heroes.Players;
using Heroes.Units.Army;
using Heroes.Utils;

namespace Heroes.Menu;

public class TurnInformation
{
    public IPlayer Player { get; set; }
    
    public IUnit ActiveUnit { get; set; }

    public IEnumerable<IUnit> Allies { get; set; } = new List<IUnit>();

    public IEnumerable<IUnit> Enemies { get; set; } = new List<IUnit>();

    public IEnumerable<IMapItem> Obstacles { get; set; } = new List<IMapItem>();

    public IMap Map { get; set; }

    public IEnumerable<IMapItem> Elements
    {
        get
        {
            return Allies
                    .Select<IUnit, IMapItem>(x => x == ActiveUnit 
                    ? new SelectionBox(x)
                    : new AllyUnitBox(x))
                .Union(Enemies.Select<IUnit, IMapItem>(x => new EnemyUnitBox(x)), MapItemsComparer.Instance)
                .Union(Obstacles, MapItemsComparer.Instance)
                .ToArray();
        }
    }
}