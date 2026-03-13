namespace Heroes.Events;

public interface IGameEventBus
{
    void Publish<TEvent>(TEvent gameEvent) where TEvent : GameEvent;

    void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : GameEvent;
}
