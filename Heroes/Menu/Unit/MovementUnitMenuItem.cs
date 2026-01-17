using Heroes.Assets;
using Heroes.Map;
using Heroes.Menu.Interfaces;
using Heroes.Players;
using Heroes.Units.Army;
using Heroes.Utils;

namespace Heroes.Menu.Unit;

public class MovementUnitMenuItem : IMenuItem
{
    private readonly TurnInformation _turn;
    private readonly IMenuFactory _menuFactory;
    private readonly IMenuBreaker _menuBreaker;
    private IMenu _menu;

    public MovementUnitMenuItem(TurnInformation turn, IMenuFactory menuFactory, IMenuBreaker menuBreaker)
    {
        _turn = turn;
        _menuFactory = menuFactory;
        _menuBreaker = menuBreaker;
    }

    public bool CanRender()
    {
        return GetSuitableCells().Any() && _turn.ActiveUnit.MovementLeft > 0;
    }

    public IEnumerable<IMapItem> ExtraObjects => Array.Empty<IMapItem>();

    public string Render()
    {
        return $"Move unit {_turn.ActiveUnit.Name}";
    }

    public void Select()
    {
        var movementMenuBreaker = new MenuBreaker
        {
            ShouldMenuBreak = false,
        };
        var exitMenuBreaker = new MenuBreaker
        {
            ShouldMenuBreak = false,
        };

        while (!exitMenuBreaker.ShouldMenuBreak && CanRender())
        {
            var cells = GetSuitableCells();
            _menu = _menuFactory.CreateMenu(movementMenuBreaker,
                _turn,
                cells
                    .Select<IMapItem, IMenuItem>(x =>
                        new MovementCellSelectionMenuItem(x, _turn, movementMenuBreaker))
                    .Union([new ExitMenuItem(exitMenuBreaker)])
                    .ToArray());
            _menu.Render();
        }

        _menuBreaker.AnyActionInvoked = movementMenuBreaker.ShouldMenuBreak;
    }

    private IEnumerable<IMapItem> GetSuitableCells()
    {
        var obstacles = _turn
            .Allies
            .Union(_turn.Enemies)
            .OfType<IMapItem>()
            .Union(_turn.Map.Cells.Where(x => !x.CanMoveInto && !_turn.ActiveUnit.CanFly))
            .Select(o => o.Coordinates)
            .ToList();

        return _turn.Map
            .GetClosePoints(_turn.ActiveUnit.Coordinates)
            .Where(i => !obstacles.Contains(i.Coordinates))
            .OrderBy(x => x.Coordinates.X)
            .ThenBy(x => x.Coordinates.Y)
            .ToArray();
    }
}