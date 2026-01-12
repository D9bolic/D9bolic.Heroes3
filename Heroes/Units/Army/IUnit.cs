using System.Drawing;
using Heroes.Map;
using Heroes.Players;
using Heroes.Units.Effects;

namespace Heroes.Units.Army;

public interface IUnit : IMapItem, IDisposable
{
    public int Wounds { get; set; }
    
    public IPlayer Player { get; }

    public UnitStateLine StateLine { get; }
    
    LongEffectsList LongEffects { get; }

    public int MovementLeft { get; set; }
    
    public int HitPoints { get; }
    
    void Activate();

    void Deactivate();

    void MarkAsAlly();
    
    void MarkAsEnemy();
    
    void CounterAttack(IUnit target);
    
    void Defence(IUnit attacker);

    bool CanMove(ICell cell);
}

public record UnitPlacement(IUnit Unit, Point Location);