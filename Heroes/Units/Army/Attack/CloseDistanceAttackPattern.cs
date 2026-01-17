using Heroes.Map;

namespace Heroes.Units.Army.Attack;

public class CloseDistanceAttackPattern : MeleeAttackPattern
{
    private readonly IUnit _unit;

    public CloseDistanceAttackPattern(IUnit unit) : base(unit)
    {
        _unit = unit;
    }

    public override void Attack(IMap map, IUnit enemy)
    {
        var stateLine = _unit.StateLine;
        AttackInternal(map, enemy, stateLine.DamageMin / 2, stateLine.DamageMax / 2);
    }
}