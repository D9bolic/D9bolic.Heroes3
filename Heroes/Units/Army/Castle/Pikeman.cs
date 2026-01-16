using System.Drawing;
using Heroes.Units.Army.Attack;

namespace Heroes.Units.Army.Castle;

public class Pikeman : UnitBase
{
    public Pikeman(Point coordinates) : base("Pikeman", coordinates)
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
        
        AttackPattern = new MeleeAttackPattern(this);
    }
}