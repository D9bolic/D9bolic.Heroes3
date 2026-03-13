using Heroes.Battle.Units.Army;

namespace Heroes.Battle.Utils;

public static class UnitExtensions
{
    public static bool IsDead(this IUnit unit)
    {
        return unit.StateLine.HitPoints - unit.Wounds <= 0;
    }

    public static int HitPoints(this IUnit unit)
    {
        return unit.StateLine.HitPoints - unit.Wounds;
    }
}
