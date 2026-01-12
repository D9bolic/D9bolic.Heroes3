using Heroes.Units.Army;

namespace Heroes.Units.Menu.SpellMenu;

public class SpellMenuBreaker : ISpellMenuBreaker
{
    public bool IsStopped { get; private set; }
    
    public IUnit Unit { get; init; }
    
    public void ShouldStop(Func<IUnit, bool> condition)
    {
        IsStopped = condition(Unit);
    }
}