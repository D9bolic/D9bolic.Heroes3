using Heroes.Players;

namespace Heroes.Menu.Interfaces;

public interface IMenuFactory
{
    public IMenu CreateMenu(IMenuBreaker menuBreaker, TurnInformation turnInformation, params IMenuItem[] items);
}

public class ConsoleMenuFactory : IMenuFactory
{
    public IMenu CreateMenu(IMenuBreaker menuBreaker, TurnInformation turnInformation, params IMenuItem[] items)
    {
        return new ConsoleMenu(turnInformation, items, menuBreaker);
    }
}