using Heroes.Players;

namespace Heroes.Units.Army.Castle;

public class Pikeman : UnitBase
{
    public Pikeman(IPlayer player) : base(player, "p", "Pikeman")
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