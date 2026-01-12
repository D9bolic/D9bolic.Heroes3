using Heroes.Units.Army;

namespace Heroes.Units.Menu.SpellMenu.Actions;

public interface ISelectableSpellAction
{    
    IEnumerable<ISpellMenuItem> GenerateMenuItems(IUnit unit, ISpellMenuBreaker unitMenuBreaker);
}