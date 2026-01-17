using Heroes.Assets;
using Heroes.Assets.Boxes;
using Heroes.Map;
using Heroes.Menu.Interfaces;
using Heroes.Players;
using Heroes.Units.Army;

namespace Heroes.Menu.Unit;

public class MovementCellSelectionMenuItem : IMenuItem
{
    private readonly TurnInformation _turn;
    private readonly IMenuBreaker _breaker;

    public IMapItem Cell { get; }

    public MovementCellSelectionMenuItem(IMapItem Cell, TurnInformation turn, IMenuBreaker breaker)
    {
        _turn = turn;
        _breaker = breaker;
        this.Cell = Cell;
        ExtraObjects = new[] { new SelectionBox(Cell) };
    }

    public IEnumerable<IMapItem> ExtraObjects { get; }

    public bool CanRender() => true;

    public string Render()
    {
        return $"Move unit {_turn.Map.GetDirection(_turn.ActiveUnit.Coordinates, Cell.Coordinates)}";
    }

    public void Select()
    {
        _turn.ActiveUnit.Coordinates = Cell.Coordinates;
        _turn.ActiveUnit.MovementLeft--;
        _breaker.ShouldMenuBreak = true;
    }
}