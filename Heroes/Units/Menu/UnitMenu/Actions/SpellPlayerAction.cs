using Heroes.Map;
using Heroes.Players;
using Heroes.Units.Army;

namespace Heroes.Units.Menu.UnitMenu.Actions;

public class SpellPlayerAction(IMap map, IPlayer player) : ISelectableUnitAction
{
    public IEnumerable<IUnitMenuItem> GenerateMenuItems(IUnit unit, IUnitMenuBreaker unitMenuBreaker)
    {
        return player.Hero.SpellBook.Any() ? new []{new SpellUnitMenuItem(unitMenuBreaker)} : Enumerable.Empty<IUnitMenuItem>();
    }
    
    public class SpellUnitMenuItem : IUnitMenuItem
    {
        private readonly IUnitMenuBreaker _breaker;

        public SpellUnitMenuItem(IUnitMenuBreaker breaker)
        {
            _breaker = breaker;
        }
        public string Render()
        {
            return $"Use hero spells";
        }

        public void OnSelected()
        {
            Console.ReadKey();
            _breaker.ShouldStop(unit => true);
        }

        public void Dispose()
        {
        }
    }
}