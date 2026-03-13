using System.Drawing;
using Heroes.Events;
using Heroes.Battle.Units.Army.Attack;
using Heroes.Units.Army;

namespace Heroes.Battle.Units.Army.Castle;

public class Griffin : UnitBase
{
    public Griffin(Point coordinates, IGameEventBus eventBus) : base("Griffin", coordinates, eventBus)
    {
        StateLine = new UnitStateLine
        {
            Initiative = 6,
            Speed = 6,
            Attack = 8,
            Defence = 8,
            HitPoints = 25,
            DamageMin = 3,
            DamageMax = 6,
        };

        AttackPattern = new MeleeAttackPattern(this, eventBus);
    }

    public override bool CanFly => true;
}
