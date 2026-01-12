using Heroes.Map;
using Heroes.Players;

namespace Heroes.Units.Army.Castle;

public class Griffin : UnitBase
{
    public Griffin(IPlayer player) : base(player, "g", "Griffin")
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
    public override bool CanMove(ICell cell)
    {
        return cell.PlacedItem is ILandscape;
    }
}