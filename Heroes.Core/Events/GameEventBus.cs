namespace Heroes.Events;

public class GameEventBus : IGameEventBus
{
    private readonly Dictionary<Type, List<Delegate>> _handlers = new();

    public void Publish<TEvent>(TEvent gameEvent) where TEvent : GameEvent
    {
        if (_handlers.TryGetValue(typeof(TEvent), out var handlers))
        {
            foreach (var handler in handlers)
            {
                ((Action<TEvent>)handler)(gameEvent);
            }
        }
    }

    public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : GameEvent
    {
        var eventType = typeof(TEvent);
        if (!_handlers.TryGetValue(eventType, out var handlers))
        {
            handlers = new List<Delegate>();
            _handlers[eventType] = handlers;
        }

        handlers.Add(handler);
    }
}
