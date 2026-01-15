using System.Drawing;

namespace Heroes.Units.Army.Castle;

public class Griffin : UnitBase
{
    public Griffin(Point coordinates) : base("Griffin", coordinates)
    {
        StateLine = new UnitStateLine
        {
            Initiative = 6,
            Speed = 3,
            AttackRange = 1,
            Attack = 8,
            Defence = 8,
            HitPoints = 25,
            DamageMin = 3,
            DamageMax = 6,
        };
    }

    public override bool CanFly => true;
}