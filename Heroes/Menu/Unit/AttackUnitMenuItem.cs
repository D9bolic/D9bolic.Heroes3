using Heroes.Map;
using Heroes.Players;
using Heroes.Units.Army;
using Heroes.Utils;

namespace Heroes.Menu.Unit;

public class AttackUnitMenuItem : IMenuItem
{
    private readonly IMap _map;
    private readonly IUnit _unit;
    private readonly IMenuFactory _menuFactory;
    private readonly IMenuBreaker _unitMenuBreaker;
    private IMenu _menu;

    public AttackUnitMenuItem(IMap map, IUnit unit, IMenuFactory menuFactory, IMenuBreaker unitMenuBreaker)
    {
        _map = map;
        _unit = unit;
        _menuFactory = menuFactory;
        _unitMenuBreaker = unitMenuBreaker;
    }

    public void Dispose()
    {
    }

    public bool CanRender()
    {
        return _map
            .GetEnemies(_unit.Cell, _unit.StateLine.AttackRange, _unit.Player)
            .Any();
    }

    public string Render()
    {
        return $"Attack enemy unit";
    }

    public void Select()
    {
        _menu = _menuFactory.CreateMenu(_unitMenuBreaker, () => _map
            .GetEnemies(_unit.Cell, _unit.StateLine.AttackRange, _unit.Player)
            .Select(x => new AttackSelectionUnitMenuItem(x, _unit, _unitMenuBreaker))
            .ToArray());
        _menu.Render();
    }
}