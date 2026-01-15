using Heroes.Map;

namespace Heroes.Units.Army.Attack;

public class DistanceAttackPattern : AttackPatternBase, IAttackPattern
{
    private readonly IUnit _unit;
    private readonly IAttackPattern _closePattern;

    public DistanceAttackPattern(IUnit unit) : this(unit, new CloseDistanceAttackPattern(unit))
    {
    }
    
    public DistanceAttackPattern(IUnit unit, IAttackPattern closePattern)
    {
        _unit = unit;
        _closePattern = closePattern;
    }

    public void Attack(IMap map, IUnit enemy)
    {
        var cells = map
            .GetCellsInDistance(_unit.Coordinates, 1)
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