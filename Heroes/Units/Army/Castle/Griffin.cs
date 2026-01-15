using System.Drawing;
using Heroes.Units.Army.Attack;

namespace Heroes.Units.Army.Castle;

public class Griffin : UnitBase
{
    public Griffin(Point coordinates) : base("Griffin", coordinates)
    {
        StateLine = new UnitStateLine
        {
            Initiative = 6,
            Speed = 3,
            Attack = 8,
            Defence = 8,
            HitPoints = 25,
            DamageMin = 3,
            DamageMax = 6,
        };

        AttackPattern = new MeleeAttackPattern(this);
    }

    public override bool CanFly => true;
}