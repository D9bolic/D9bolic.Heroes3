using Heroes.Map;
using Heroes.Map.Assets;
using Heroes.Map.Assets.Boxes;
using Heroes.Menu.Interfaces;
using Heroes.Units.Army;

namespace Heroes.Menu.Unit;

public class AttackSelectionUnitMenuItem : IMenuItem
{
    private readonly IMenuBreaker _breaker;
        
    public IUnit Enemy { get; }
    
    public IUnit Unit { get; }

    public AttackSelectionUnitMenuItem(IUnit enemy, IUnit unit, IMenuBreaker breaker)
    {
        _breaker = breaker;
        this.Enemy = enemy;
        Unit = unit;
        ExtraObjects = new[] { new AttackedUnitBox(Enemy) };
    }

    public IEnumerable<IMapItem> ExtraObjects { get; }

    public bool CanRender() => true;

    public string Render()
    {
        return $"Attack unit {Enemy.Name} on cell {Enemy.Coordinates.X}, {Enemy.Coordinates.Y}";
    }

    public void Select()
    {
        Enemy!.Defence(Unit);
        Enemy!.CounterAttack(Unit);
        Console.ReadKey();
        _breaker.ShouldMenuBreak = true;
    }
}