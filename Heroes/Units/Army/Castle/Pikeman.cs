using System.Drawing;
using Heroes.Map;
using Heroes.Players;

namespace Heroes.Units.Army.Castle;

[Asset("Unit:Castle:Pikeman", "p")]
public class Pikeman : UnitBase
{
    public Pikeman(Point coordinates) : base("Pikeman", coordinates)
    {
        StateLine = new UnitStateLine
        {
            Initiative = 1,
            Speed = 1,
            AttackRange = 2,
            Attack = 4,
            Defence = 5,
            HitPoints = 10,
            DamageMin = 1,
            DamageMax = 3,
        };
    }
}