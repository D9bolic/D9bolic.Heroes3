using Heroes.Units.Army;

namespace Heroes.Units.Menu.SpellMenu;

public interface ISpellMenuBreaker
{
    bool IsStopped { get; }
    
    IUnit Unit { get; }
    
    void ShouldStop(Func<IUnit, bool> condition);
}