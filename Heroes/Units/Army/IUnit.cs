using Heroes.Map;
using Heroes.Units.Effects;

namespace Heroes.Units.Army;

public interface IUnit : IMapItem
{
    public bool CanFly { get; }
    
    public int Wounds { get; set; }

    public UnitStateLine StateLine { get; }
    
    LongEffectsList LongEffects { get; }

    public int MovementLeft { get; set; }
    
    public int HitPoints { get; }
    
    public string Name { get; }
    
    void Activate();
    
    void CounterAttack(IUnit target);
    
    void Defence(IUnit attacker);
}