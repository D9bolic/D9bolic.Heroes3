using Heroes.Map;
using Heroes.Players;
using Heroes.Units.Army;
using Heroes.Utils;

namespace Heroes.Menu.Unit;

public class AttackUnitMenuItem : IMenuItem
{
    private readonly TurnInformation _turn;
    private readonly IMenuFactory _menuFactory;
    private readonly IMenuBreaker _unitMenuBreaker;
    private IMenu _menu;

    public AttackUnitMenuItem(TurnInformation _turn, IMenuFactory menuFactory, IMenuBreaker unitMenuBreaker)
    {
        this._turn = _turn;
        _menuFactory = menuFactory;
        _unitMenuBreaker = unitMenuBreaker;
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
        
        _menu = _menuFactory
            .CreateMenu(_unitMenuBreaker, _turn, 
                enemies
                .Select<IUnit, IMenuItem>(x => new AttackSelectionUnitMenuItem(x, _turn.ActiveUnit, _unitMenuBreaker))
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
        var cells = _turn.Map
            .GetCellsInDistance(_turn.ActiveUnit.Coordinates, _turn.ActiveUnit.StateLine.AttackRange)
            .Select(x => x.Coordinates)
            .ToArray();
        return _turn.Enemies
            .Where(u => cells.Contains(u.Coordinates))
            .ToArray();
    }
}