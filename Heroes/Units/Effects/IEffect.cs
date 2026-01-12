using Heroes.Units.Army;

namespace Heroes.Units.Effects;

public interface IEffect
{
    UnitStateLine Mutate(UnitStateLine unitState);
}

public interface ILongEffect : IEffect
{
    int TurnsLeft { get; set; }
}