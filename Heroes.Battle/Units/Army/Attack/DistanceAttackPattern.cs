using Heroes.Events;
using Heroes.Map;

namespace Heroes.Battle.Units.Army.Attack;

public class DistanceAttackPattern : AttackPatternBase, IAttackPattern
{
    private readonly IUnit _unit;
    private readonly IAttackPattern _closePattern;

    public DistanceAttackPattern(IUnit unit, IGameEventBus eventBus)
        : this(unit, new CloseDistanceAttackPattern(unit, eventBus), eventBus)
    {
    }

    public DistanceAttackPattern(IUnit unit, IAttackPattern closePattern, IGameEventBus eventBus)
        : base(eventBus)
    {
        _unit = unit;
        _closePattern = closePattern;
    }

    public void Attack(IMap map, IUnit enemy)
    {
        var cells = map
            .GetClosePoints(_unit.Coordinates)
            .Select(x => x.Coordinates)
            .ToArray();
        if (cells.Contains(_unit.Coordinates))
        {
            _closePattern.Attack(map, _unit);
        }

        CalculateDamage(_unit, enemy, _unit.StateLine.DamageMin, _unit.StateLine.DamageMax);
        CounterAttack(_unit, enemy, map);
    }

    public IEnumerable<IUnit> GetTargets(IMap map, IEnumerable<IUnit> enemies)
    {
        return enemies;
    }
}
