using Heroes.Map;

namespace Heroes.Units.Army.Attack;

public class MeleeAttackPattern : AttackPatternBase, IAttackPattern
{
    private readonly IUnit _unit;

    public MeleeAttackPattern(IUnit unit)
    {
        _unit = unit;
    }

    public virtual void Attack(IMap map, IUnit enemy)
    {
        var stateLine = _unit.StateLine;
        AttackInternal(map, enemy, stateLine.DamageMin, stateLine.DamageMax);
    }

    protected void AttackInternal(IMap map, IUnit enemy, int damageMin, int damageMax)
    {
        CalculateDamage(_unit, enemy, damageMin, damageMax);
        CounterAttack(_unit, enemy, map);
    }

    public IEnumerable<IUnit> GetTargets(IMap map, IEnumerable<IUnit> enemies)
    {
        var cells = map
            .GetClosePoints(_unit.Coordinates)
            .Select(x => x.Coordinates)
            .ToArray();
        return enemies
            .Where(u => cells.Contains(u.Coordinates))
            .ToArray();
    }
}