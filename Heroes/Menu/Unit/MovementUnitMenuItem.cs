using Heroes.Map;
using Heroes.Players;
using Heroes.Units.Army;
using Heroes.Utils;

namespace Heroes.Menu.Unit;

public class MovementUnitMenuItem : IMenuItem
{
    private readonly TurnInformation _turn;
    private readonly IMenuFactory _menuFactory;
    private IMenu _menu;

    public MovementUnitMenuItem(TurnInformation turn, IMenuFactory menuFactory)
    {
        _turn = turn;
        _menuFactory = menuFactory;
    }

    public void Dispose()
    {
    }

    public bool CanRender()
    {
        return GetSuitableCells().Any() && _turn.ActiveUnit.MovementLeft > 0;
    }

    public string Render()
    {
        return $"Move unit {_turn.ActiveUnit.Name}";
    }

    public void Select()
    {
        var cells = GetSuitableCells();
        var menuBreaker = new MenuBreaker
        {
            ShouldMenuBreak = true,
        };

        _menu = _menuFactory.CreateMenu(menuBreaker, cells
            .Select<IMapItem, IMenuItem>(x => new MovementCellSelectionMenuItem(x, _turn.ActiveUnit, menuBreaker))
            .Union(new[] {new ExitMenuItem(menuBreaker)})
            .ToArray());
        _menu.Render(new TurnInformation()
        {
            Allies = _turn.Allies,
            Enemies = _turn.Enemies,
            ActiveUnit = _turn.ActiveUnit,
            Map = _turn.Map,
            Obstacles = _turn.Obstacles,
            ExtraObjects = cells.Select(x => new SelectionBox(x)).ToArray(),
        });
    }

    private IEnumerable<IMapItem> GetSuitableCells()
    {
        var units = _turn
            .Allies
            .Union(_turn.Enemies)
            .Select(o => o.Coordinates)
            .ToArray();

        return _turn.Map
            .GetCellsInDistance(_turn.ActiveUnit.Coordinates, 1)
            .Where(i => !units.Contains(i.Coordinates))
            .ToArray();
    }
}