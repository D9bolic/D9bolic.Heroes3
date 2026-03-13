using Heroes.Assets;
using Heroes.Assets.Boxes;
using Heroes.Menu.Interfaces;
using Heroes.Battle.Units.Army;
using Heroes.Units.Army;

namespace Heroes.Battle.Menu.Unit;

public class AttackSelectionUnitMenuItem : IMenuItem
{
    private readonly IMenuBreaker _breaker;
    private readonly IUserInteraction _userInteraction;

    public IUnit Enemy { get; }

    public IUnit Unit { get; }

    public AttackSelectionUnitMenuItem(IUnit enemy, IUnit unit, IMenuBreaker breaker, IUserInteraction userInteraction)
    {
        _breaker = breaker;
        _userInteraction = userInteraction;
        Enemy = enemy;
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
        _userInteraction.WaitForAcknowledgment();
        _breaker.ShouldMenuBreak = true;
    }
}
