using Heroes.Units.Army;
using Heroes.Units.Effects;

namespace Heroes.Battle.Units.Effects;

public class Defence : IEffect
{
    public UnitStateLine Mutate(UnitStateLine unitState)
    {
        return new UnitStateLine
        {
            Initiative = unitState.Initiative,
            Attack = unitState.Attack,
            Defence = unitState.Defence + 2,
            Speed = unitState.Speed,
            DamageMax = unitState.DamageMax,
            DamageMin = unitState.DamageMin,
            HitPoints = unitState.HitPoints,
        };
    }

    public int TurnsLeft { get; set; }

    public override string ToString()
    {
        return "Defence";
    }
}
