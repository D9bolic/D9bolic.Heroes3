using Heroes.Map;
using Heroes.Units.Army;

namespace Heroes.Menu.Unit;

public class MovementCellSelectionMenuItem : IMenuItem
{
    private readonly IMenuBreaker _breaker;

    public IMapItem Cell { get; }

    public IUnit Unit { get; }

    public MovementCellSelectionMenuItem(IMapItem Cell, IUnit unit, IMenuBreaker breaker)
    {
        _breaker = breaker;
        this.Cell = Cell;
        Unit = unit;
        ExtraObjects = new[] { new SelectionBox(Cell) };
    }

    public IEnumerable<IMapItem> ExtraObjects { get; }

    public bool CanRender() => true;

    public string Render()
    {
        if (Cell.Coordinates.X > Unit.Coordinates.X &&
            Cell.Coordinates.Y == Unit.Coordinates.Y)
        {
            return $"Move unit right";
        }

        if (Cell.Coordinates.X < Unit.Coordinates.X &&
            Cell.Coordinates.Y == Unit.Coordinates.Y)
        {
            return $"Move unit left";
        }

        if (Cell.Coordinates.X == Unit.Coordinates.X &&
            Cell.Coordinates.Y < Unit.Coordinates.Y)
        {
            return $"Move unit up";
        }

        if (Cell.Coordinates.X == Unit.Coordinates.X &&
            Cell.Coordinates.Y > Unit.Coordinates.Y)
        {
            return $"Move unit down";
        }

        return $"Move unit to cell {Cell.Coordinates.X}, {Cell.Coordinates.Y}";
    }

    public void Select()
    {
        Unit.Coordinates = Cell.Coordinates;
        Unit.MovementLeft--;
        _breaker.ShouldMenuBreak = true;
    }
}