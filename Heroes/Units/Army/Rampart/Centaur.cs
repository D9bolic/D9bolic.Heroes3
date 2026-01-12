using Heroes.Players;

namespace Heroes.Units.Army.Rampart;

public class Centaur : UnitBase
{
    public Centaur(IPlayer player) : base(player, "c", "Centaur")
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