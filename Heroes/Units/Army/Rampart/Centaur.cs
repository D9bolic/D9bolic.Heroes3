using System.Drawing;
using Heroes.Units.Army.Attack;

namespace Heroes.Units.Army.Rampart;


public class Centaur : UnitBase
{
    public Centaur(Point coordinates) : base("Centaur", coordinates)
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
        
        AttackPattern = new MeleeAttackPattern(this);
    }
}