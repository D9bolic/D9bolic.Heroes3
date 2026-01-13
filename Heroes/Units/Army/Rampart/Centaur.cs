using System.Drawing;
using Heroes.Map;
using Heroes.Players;

namespace Heroes.Units.Army.Rampart;


public class Centaur : UnitBase
{
    [Asset("Unit:Rampart:Centaur", "c")]
    public Centaur(Point coordinates) : base("Centaur", coordinates)
    {
        StateLine = new UnitStateLine
        {
            Initiative = 1,
            Speed = 2,
            AttackRange = 1,
            Attack = 5,
            Defence = 3,
            HitPoints = 8,
            DamageMin = 2,
            DamageMax = 3,
        };
    }
}