using Heroes.Map;
using Heroes.Players;
using Heroes.Units.Army;
using Heroes.Utils;

namespace Heroes.Units.Menu.UnitMenu.Actions;

public class MovementUnitAction(IMap map, IPlayer player) : ISelectableUnitAction
{
    public IEnumerable<IUnitMenuItem> GenerateMenuItems(IUnit unit, IUnitMenuBreaker unitMenuBreaker)
    {
        return map
            .GetLandscapeCells(unit.Cell, 1)
            .Where(unit.CanMove)
            .Select(x => new MovementUnitMenuItem(x, unit, unitMenuBreaker))
            .ToArray();
    }

    public class MovementUnitMenuItem : IUnitMenuItem
    {
        private readonly IUnitMenuBreaker _breaker;
        private ConsoleColor _previousTextColor;
        private ConsoleColor _previousBackgroundColor;

        public ICell Cell { get; }

        public IUnit Unit { get; }

        public MovementUnitMenuItem(ICell Cell, IUnit unit, IUnitMenuBreaker breaker)
        {
            _breaker = breaker;
            this.Cell = Cell;
            Unit = unit;

            _previousTextColor = Cell.TextColor;
            _previousBackgroundColor = Cell.BackgroundColor;
            Cell.BackgroundColor = ConsoleColor.Green;
            Cell.TextColor = ConsoleColor.Black;
        }

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

        public void OnSelected()
        {
            Unit.Cell = Cell;
            _previousTextColor = ConsoleColor.Green;
            _previousBackgroundColor = ConsoleColor.Black;
            _breaker.Unit.MovementLeft--;
            _breaker.ShouldStop(unit => unit.MovementLeft <= 0);
        }

        public void Dispose()
        {
            Cell.BackgroundColor = _previousBackgroundColor;
            Cell.TextColor = _previousTextColor;
        }
    }
}