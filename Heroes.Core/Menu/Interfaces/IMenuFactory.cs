namespace Heroes.Menu.Interfaces;

public interface IMenuFactory
{
    public IMenu CreateMenu(IMenuBreaker menuBreaker, IMenuContext menuContext, params IMenuItem[] items);
}
