using Heroes.Map;

namespace Heroes.Menu;

public interface IMenuFactory
{
    public IMenu CreateMenu(IMenuBreaker menuBreaker, params IMenuItem[] items);
}

public class ConsoleMenuFactory : IMenuFactory
{
    private readonly IMap _map;

    public ConsoleMenuFactory(IMap map)
    {
        _map = map;
    }
    
    public IMenu CreateMenu(IMenuBreaker menuBreaker, params IMenuItem[] items)
    {
        return new ConsoleMenu(_map, items, menuBreaker);
    }
}