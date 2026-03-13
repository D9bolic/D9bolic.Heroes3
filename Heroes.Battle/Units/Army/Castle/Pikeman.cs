using System.Drawing;
using Heroes.Events;
using Heroes.Battle.Units.Army.Attack;
using Heroes.Units.Army;

namespace Heroes.Battle.Units.Army.Castle;

public class Pikeman : UnitBase
{
    public Pikeman(Point coordinates, IGameEventBus eventBus) : base("Pikeman", coordinates, eventBus)
    {
        StateLine = new UnitStateLine
        {
            Initiative = 4,
            Speed = 4,
            Attack = 4,
            Defence = 5,
            HitPoints = 10,
            DamageMin = 1,
            DamageMax = 3,
        };

        AttackPattern = new MeleeAttackPattern(this, eventBus);
    }
}
