using Heroes.Adventure.Entities.Heroes;
using Heroes.Adventure.Events;
using Heroes.Assets;
using Heroes.Assets.Boxes;
using Heroes.Events;
using Heroes.Menu.Interfaces;

namespace Heroes.Adventure.Menu;

public class MoveHeroCellSelectionMenuItem : IMenuItem
{
    private readonly IMapItem _cell;
    private readonly AdventureHero _hero;
    private readonly AdventureContext _context;
    private readonly IMenuBreaker _breaker;

    public MoveHeroCellSelectionMenuItem(IMapItem cell, AdventureHero hero, AdventureContext context, IMenuBreaker breaker)
    {
        _cell = cell;
        _hero = hero;
        _context = context;
        _breaker = breaker;
        ExtraObjects = [new SelectionBox(cell)];
    }

    public IEnumerable<IMapItem> ExtraObjects { get; }

    public bool CanRender() => true;

    public string Render() => $"Move {_context.Map.GetDirection(_hero.Coordinates, _cell.Coordinates)}";

    public void Select()
    {
        _hero.Coordinates = _cell.Coordinates;
        _hero.MovementPointsLeft--;
        _breaker.ShouldMenuBreak = true;
    }
}
