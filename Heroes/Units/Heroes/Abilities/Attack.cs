using Heroes.Units.Army;
using Heroes.Units.Effects;

namespace Heroes.Units.Heroes.Abilities;

public class Attack : IEffect
{
    private readonly HeroStateLine _heroStateLine;

    public Attack(HeroStateLine heroStateLine)
    {
        _heroStateLine = heroStateLine;
    }
    public UnitStateLine Mutate(UnitStateLine unitState)
    {
        return new UnitStateLine
        {
            Initiative = unitState.Initiative,
            Attack = unitState.Attack + _heroStateLine.Attack,
            Defence = unitState.Defence,
            Speed = unitState.Speed,
            DamageMax = unitState.DamageMax,
            AttackRange = unitState.AttackRange,
            DamageMin = unitState.DamageMin,
            HitPoints = unitState.HitPoints,
        };
    }
}