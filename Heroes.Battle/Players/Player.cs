using Heroes.Events;
using Heroes.Battle.Units.Army;
using Heroes.Units.Army;
using Heroes.Units.Heroes;

namespace Heroes.Battle.Players;

public class Player : IPlayer
{
    private readonly IGameEventBus _eventBus;

    public Player(IGameEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public string Name { get; init; }

    public IHero Hero { get; init; }

    public List<IUnit> Army { get; } = new List<IUnit>();

    public void Activate()
    {
        _eventBus.Publish(new PlayerTurnStarted(Name));
    }

    public bool CheckLoose()
    {
        return !Army.Any();
    }
}
