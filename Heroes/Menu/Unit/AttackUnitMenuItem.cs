using Heroes.Assets;
using Heroes.Map;
using Heroes.Menu.Interfaces;
using Heroes.Players;
using Heroes.Units.Army;
using Heroes.Utils;

namespace Heroes.Menu.Unit;

public class AttackUnitMenuItem : IMenuItem
{
    private readonly TurnInformation _turn;
    private readonly IMenuFactory _menuFactory;
    private readonly IMenuBreaker _menuBreaker;
    private IMenu _menu;

    public AttackUnitMenuItem(TurnInformation _turn, IMenuFactory menuFactory, IMenuBreaker menuBreaker)
    {
        this._turn = _turn;
        _menuFactory = menuFactory;
        _menuBreaker = menuBreaker;
    }

    public IEnumerable<IMapItem> ExtraObjects => Array.Empty<IMapItem>();

    public bool CanRender()
    {
        return GetEnemiesInAttackRange().Any();
    }

    public string Render()
    {
        return $"Attack enemy unit";
    }

    public void Select()
    {
        var enemies = GetEnemiesInAttackRange();
        
        var exitMenuBreaker = new MenuBreaker();
        var menuBreaker = new AttackMenuBreaker(exitMenuBreaker, _menuBreaker);

        _menu = _menuFactory
            .CreateMenu(menuBreaker, _turn, 
                enemies
                .Select<IUnit, IMenuItem>(x => new AttackSelectionUnitMenuItem(x, _turn.ActiveUnit, _menuBreaker))
                .Union([new ExitMenuItem(exitMenuBreaker)])
                .ToArray());
        
        _menu.Render(new TurnInformation()
        {
            Allies = _turn.Allies,
            Enemies = _turn.Enemies,
            ActiveUnit = _turn.ActiveUnit,
            Map = _turn.Map,
            Obstacles = _turn.Obstacles,
        });
    }
    
    private IEnumerable<IUnit> GetEnemiesInAttackRange()
    {
       return _turn.ActiveUnit.AttackPattern.GetTargets(_turn.Map, _turn.Enemies);
    }
    
    private class AttackMenuBreaker : IMenuBreaker
    {
        private readonly IMenuBreaker _exitMenuBreaker;
        private readonly IMenuBreaker _upMenuBreaker;

        public AttackMenuBreaker(IMenuBreaker exitMenuBreaker, IMenuBreaker upMenuBreaker)
        {
            _exitMenuBreaker = exitMenuBreaker;
            _upMenuBreaker = upMenuBreaker;
        }

        public bool ShouldMenuBreak { get => _upMenuBreaker.ShouldMenuBreak || _exitMenuBreaker.ShouldMenuBreak; set => _exitMenuBreaker.ShouldMenuBreak = value; }
        public bool AnyActionInvoked { get=> _upMenuBreaker.AnyActionInvoked || _exitMenuBreaker.AnyActionInvoked; set => _exitMenuBreaker.ShouldMenuBreak = value; }
    }
}