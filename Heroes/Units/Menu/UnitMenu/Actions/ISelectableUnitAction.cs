using Heroes.Units.Army;

namespace Heroes.Units.Menu.UnitMenu.Actions;

public interface ISelectableUnitAction
{    
    IEnumerable<IUnitMenuItem> GenerateMenuItems(IUnit unit, IUnitMenuBreaker unitMenuBreaker);
}