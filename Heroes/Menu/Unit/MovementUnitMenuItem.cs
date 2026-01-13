using Heroes.Map;
using Heroes.Players;
using Heroes.Units.Army;
using Heroes.Utils;

namespace Heroes.Menu.Unit;

public class MovementUnitMenuItem : IMenuItem
{
    private readonly IMap _map;
    private readonly IUnit _unit;
    private readonly IMenuFactory _menuFactory;
    private IMenu _menu;

    public MovementUnitMenuItem(IMap map, IUnit unit, IMenuFactory menuFactory)
    {
        _map = map;
        _unit = unit;
        _menuFactory = menuFactory;
    }

    public void Dispose()
    {
    }

    public bool CanRender()
    {
        return _map
            .GetLandscapeCells(_unit.Cell, 1)
            .Where(_unit.CanMove)
            .Any() && _unit.MovementLeft > 0;
    }

    public string Render()
    {
        return $"Move unit {_unit.Name}";
    }

    public void Select()
    {
        var menuBreaker = new MenuBreaker
        {
            ShouldMenuBreak = false,
        };

        _menu = _menuFactory.CreateMenu(menuBreaker, () => _map
            .GetLandscapeCells(_unit.Cell, 1)
            .Where(_unit.CanMove)
            .Select(x => new MovementCellSelectionMenuItem(x, _unit, menuBreaker))
            .OfType<IMenuItem>()
            .Union(new []{new ExitMenuItem(menuBreaker)})
            .ToArray());
        _menu.Render();
    }
}