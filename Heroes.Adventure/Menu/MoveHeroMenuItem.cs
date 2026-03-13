using Heroes.Adventure.Entities;
using Heroes.Adventure.Entities.Heroes;
using Heroes.Assets;
using Heroes.Menu;
using Heroes.Menu.Interfaces;

namespace Heroes.Adventure.Menu;

public class MoveHeroMenuItem : IMenuItem
{
    private readonly AdventureHero _hero;
    private readonly AdventureContext _context;
    private readonly IMenuFactory _menuFactory;
    private readonly IMenuBreaker _menuBreaker;

    public MoveHeroMenuItem(AdventureHero hero, AdventureContext context, IMenuFactory menuFactory, IMenuBreaker menuBreaker)
    {
        _hero = hero;
        _context = context;
        _menuFactory = menuFactory;
        _menuBreaker = menuBreaker;
    }

    public IEnumerable<IMapItem> ExtraObjects => Array.Empty<IMapItem>();

    public bool CanRender() => GetReachableCells().Any() && _hero.MovementPointsLeft > 0;

    public string Render() => $"Move {_hero.Name}";

    public void Select()
    {
        var movementMenuBreaker = new MenuBreaker { ShouldMenuBreak = false };
        var exitMenuBreaker = new MenuBreaker { ShouldMenuBreak = false };

        while (!exitMenuBreaker.ShouldMenuBreak && CanRender())
        {
            var cells = GetReachableCells();
            var menuItems = cells
                .Select<IMapItem, IMenuItem>(cell =>
                    new MoveHeroCellSelectionMenuItem(cell, _hero, _context, movementMenuBreaker))
                .Union([new ExitMenuItem(exitMenuBreaker)])
                .ToArray();

            var menu = _menuFactory.CreateMenu(movementMenuBreaker, _context, menuItems);
            menu.Render();
        }

        _menuBreaker.AnyActionInvoked = movementMenuBreaker.ShouldMenuBreak;
    }

    private IEnumerable<IMapItem> GetReachableCells()
    {
        var obstacles = _context.Entities
            .OfType<IMapItem>()
            .Union(_context.ActivePlayer.Heroes.Where(h => h != _hero))
            .Union(_context.Map.Cells.Where(x => !x.CanMoveInto))
            .Select(o => o.Coordinates)
            .ToList();

        return _context.Map
            .GetClosePoints(_hero.Coordinates)
            .Where(i => !obstacles.Contains(i.Coordinates))
            .OrderBy(x => x.Coordinates.X)
            .ThenBy(x => x.Coordinates.Y)
            .ToArray();
    }
}
