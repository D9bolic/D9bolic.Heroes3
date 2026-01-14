using System.Drawing;

namespace Heroes.Units.Army.Rampart;

public class Elf : UnitBase
{
    public Elf(Point coordinates) : base("Elf", coordinates)
    {
        StateLine = new UnitStateLine
        {
            Initiative = 6,
            Speed = 2,
            AttackRange = 5,
            Attack = 9,
            Defence = 5,
            HitPoints = 15,
            DamageMin = 3,
            DamageMax = 5,
        };
    }
}