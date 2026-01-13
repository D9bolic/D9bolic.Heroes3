using Heroes.Map;
using Heroes.Units.Army;

namespace Heroes.Menu.Unit;

public class AttackSelectionUnitMenuItem : IMenuItem
{
    private readonly IMenuBreaker _breaker;
    private readonly ConsoleColor _previousTextColor;
    private readonly ConsoleColor _previousBackgroundColor;
        
    public ICell Cell { get; }
    
    public IUnit Unit { get; }

    public AttackSelectionUnitMenuItem(ICell Cell, IUnit unit, IMenuBreaker breaker)
    {
        _breaker = breaker;
        this.Cell = Cell;
        Unit = unit;
        _previousTextColor = Cell.TextColor;
        _previousBackgroundColor = Cell.BackgroundColor;
        Cell.BackgroundColor = ConsoleColor.Red;
        Cell.TextColor = ConsoleColor.Black;
    }

    public bool CanRender() => true;

    public string Render()
    {
        return $"Attack unit {Cell.PlacedItem!.Name} on cell {Cell.Coordinates.X}, {Cell.Coordinates.Y}";
    }

    public void Select()
    {
        (Cell.PlacedItem as IUnit)!.Defence(Unit);
        (Cell.PlacedItem as IUnit)!.CounterAttack(Unit);
        Console.ReadKey();
        _breaker.ShouldMenuBreak = true;
    }

    public void Dispose()
    {
        Cell.BackgroundColor = _previousBackgroundColor;
        Cell.TextColor = _previousTextColor;
    }
}