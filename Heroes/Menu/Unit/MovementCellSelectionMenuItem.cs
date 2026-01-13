using Heroes.Map;
using Heroes.Units.Army;

namespace Heroes.Menu.Unit;

public class MovementCellSelectionMenuItem : IMenuItem
{
    private readonly IMenuBreaker _breaker;
    private ConsoleColor _previousTextColor;
    private ConsoleColor _previousBackgroundColor;

    public ICell Cell { get; }

    public IUnit Unit { get; }

    public MovementCellSelectionMenuItem(ICell Cell, IUnit unit, IMenuBreaker breaker)
    {
        _breaker = breaker;
        this.Cell = Cell;
        Unit = unit;

        _previousTextColor = Cell.TextColor;
        _previousBackgroundColor = Cell.BackgroundColor;
        Cell.BackgroundColor = ConsoleColor.Green;
        Cell.TextColor = ConsoleColor.Black;
    }

    public bool CanRender() => true;

    public string Render()
    {
        if (Cell.Coordinates.X > Unit.Cell.Coordinates.X &&
            Cell.Coordinates.Y == Unit.Cell.Coordinates.Y)
        {
            return $"Move unit right";
        }

        if (Cell.Coordinates.X < Unit.Cell.Coordinates.X &&
            Cell.Coordinates.Y == Unit.Cell.Coordinates.Y)
        {
            return $"Move unit left";
        }

        if (Cell.Coordinates.X == Unit.Cell.Coordinates.X &&
            Cell.Coordinates.Y < Unit.Cell.Coordinates.Y)
        {
            return $"Move unit up";
        }

        if (Cell.Coordinates.X == Unit.Cell.Coordinates.X &&
            Cell.Coordinates.Y > Unit.Cell.Coordinates.Y)
        {
            return $"Move unit down";
        }

        return $"Move unit to cell {Cell.Coordinates.X}, {Cell.Coordinates.Y}";
    }

    public void Select()
    {
        Unit.Cell = Cell;
        _previousTextColor = ConsoleColor.Green;
        _previousBackgroundColor = ConsoleColor.Black;
        Unit.MovementLeft--;
        _breaker.ShouldMenuBreak = Unit.MovementLeft <= 0;
    }

    public void Dispose()
    {
        Cell.BackgroundColor = _previousBackgroundColor;
        Cell.TextColor = _previousTextColor;
    }
}