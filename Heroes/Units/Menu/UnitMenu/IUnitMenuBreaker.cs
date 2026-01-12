using Heroes.Units.Army;

namespace Heroes.Units.Menu.UnitMenu;

public interface IUnitMenuBreaker
{
    bool IsStopped { get; }
    
    IUnit Unit { get; }
    
    void ShouldStop(Func<IUnit, bool> condition);
}