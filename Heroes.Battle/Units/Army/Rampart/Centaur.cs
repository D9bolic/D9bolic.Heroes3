using System.Drawing;
using Heroes.Events;
using Heroes.Battle.Units.Army.Attack;
using Heroes.Units.Army;

namespace Heroes.Battle.Units.Army.Rampart;

public class Centaur : UnitBase
{
    public Centaur(Point coordinates, IGameEventBus eventBus) : base("Centaur", coordinates, eventBus)
    {
        StateLine = new UnitStateLine
        {
            Initiative = 6,
            Speed = 6,
            Attack = 5,
            Defence = 3,
            HitPoints = 8,
            DamageMin = 2,
            DamageMax = 3,
        };

        AttackPattern = new MeleeAttackPattern(this, eventBus);
    }
}
