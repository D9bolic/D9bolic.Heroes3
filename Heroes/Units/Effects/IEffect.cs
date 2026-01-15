using Heroes.Units.Army;

namespace Heroes.Units.Effects;

public interface IEffect
{
    UnitStateLine Mutate(UnitStateLine unitState);
    
    int TurnsLeft { get; set; }
}