using Heroes.Units.Army;
using Heroes.Units.Effects;

namespace Heroes.Units.Heroes.Abilities;

public class Defence : IEffect
{
    private readonly HeroStateLine _heroStateLine;

    public Defence(HeroStateLine heroStateLine)
    {
        _heroStateLine = heroStateLine;
    }
    public UnitStateLine Mutate(UnitStateLine unitState)
    {
        return new UnitStateLine
        {
            Initiative = unitState.Initiative,
            Attack = unitState.Attack,
            Defence = unitState.Defence + _heroStateLine.Defence,
            Speed = unitState.Speed,
            DamageMax = unitState.DamageMax,
            AttackRange = unitState.AttackRange,
            DamageMin = unitState.DamageMin,
            HitPoints = unitState.HitPoints,
        };
    }
}