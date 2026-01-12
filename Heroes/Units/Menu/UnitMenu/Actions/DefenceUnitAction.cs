using Heroes.Map;
using Heroes.Players;
using Heroes.Units.Army;
using Heroes.Units.Effects;

namespace Heroes.Units.Menu.UnitMenu.Actions;

public class DefenceUnitAction(IMap map, IPlayer player) : ISelectableUnitAction
{
    public IEnumerable<IUnitMenuItem> GenerateMenuItems(IUnit unit, IUnitMenuBreaker unitMenuBreaker)
    {
        return new[] {new DefenceMenuItem(unit, unitMenuBreaker)};
    }
    
    public class DefenceMenuItem : IUnitMenuItem
    {
        private readonly IUnitMenuBreaker _breaker;
    
        public IUnit Unit { get; }

        public DefenceMenuItem(IUnit unit, IUnitMenuBreaker breaker)
        {
            _breaker = breaker;
            Unit = unit;
        }
        public string Render()
        {
            return $"Defence unit";
        }

        public void OnSelected()
        {
            Unit.LongEffects.Add(new Defence
            {
                TurnsLeft = 1,
            });
            _breaker.ShouldStop(unit => true);
        }

        public void Dispose()
        {
        }
    }
}