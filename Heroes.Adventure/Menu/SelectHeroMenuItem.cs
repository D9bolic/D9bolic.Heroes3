using Heroes.Adventure.Entities.Heroes;
using Heroes.Assets;
using Heroes.Menu.Interfaces;

namespace Heroes.Adventure.Menu;

public class SelectHeroMenuItem : IMenuItem
{
    private readonly AdventureHero _hero;
    private readonly AdventureContext _context;
    private readonly IMenuFactory _menuFactory;
    private readonly IMenuBreaker _menuBreaker;

    public SelectHeroMenuItem(AdventureHero hero, AdventureContext context, IMenuFactory menuFactory, IMenuBreaker menuBreaker)
    {
        _hero = hero;
        _context = context;
        _menuFactory = menuFactory;
        _menuBreaker = menuBreaker;
    }

    public IEnumerable<IMapItem> ExtraObjects => Array.Empty<IMapItem>();

    public bool CanRender() => _hero.MovementPointsLeft > 0;

    public string Render() => $"Select hero {_hero.Name} (MP: {_hero.MovementPointsLeft}/{_hero.MaxMovementPoints})";

    public void Select()
    {
        _context.ActiveHero = _hero;
        var heroMenuBreaker = new Heroes.Menu.MenuBreaker { ShouldMenuBreak = false };

        while (!heroMenuBreaker.ShouldMenuBreak && _hero.MovementPointsLeft > 0)
        {
            var moveItem = new MoveHeroMenuItem(_hero, _context, _menuFactory, heroMenuBreaker);
            var exitItem = new Heroes.Menu.ExitMenuItem(heroMenuBreaker);
            var menu = _menuFactory.CreateMenu(heroMenuBreaker, _context, moveItem, exitItem);
            menu.Render();
        }
    }
}
