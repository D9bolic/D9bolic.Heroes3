using Heroes.Events;

namespace Heroes.Adventure.Events;

public record AdventureTurnStarted(string PlayerName, int Day) : GameEvent;

public record HeroMoved(string HeroName, int RemainingMovementPoints) : GameEvent;

public record HeroInteracted(string HeroName, string EntityName) : GameEvent;

public record ResourceCollected(string ResourceType, int Amount) : GameEvent;

public record CityVisited(string HeroName, string CityName) : GameEvent;

public record NewDayStarted(int Day) : GameEvent;

public record NewWeekStarted(int Week) : GameEvent;
