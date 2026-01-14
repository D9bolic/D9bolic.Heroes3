using Heroes.Map;
using Heroes.Units.Army;

namespace Heroes.Menu.Unit;

public class AttackSelectionUnitMenuItem : IMenuItem
{
    private readonly IMenuBreaker _breaker;
        
    public IMapItem Enemy { get; }
    
    public IUnit Unit { get; }

    public AttackSelectionUnitMenuItem(IMapItem enemy, IUnit unit, IMenuBreaker breaker)
    {
        _breaker = breaker;
        this.Enemy = enemy;
        Unit = unit;
    }

    public bool CanRender() => true;

    public string Render()
    {
        return $"Attack unit {Enemy.Name} on cell {Enemy.Coordinates.X}, {Enemy.Coordinates.Y}";
    }

    public void Select()
    {
        var enemy = Enemy is IUnit unit ? unit : (IUnit)(((IWrapper) Enemy).Item);
        enemy!.Defence(Unit);
        enemy!.CounterAttack(Unit);
        Console.ReadKey();
        _breaker.ShouldMenuBreak = true;
    }
}