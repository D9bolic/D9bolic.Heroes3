using Heroes.Console.Assets;
using Heroes.Console.Rendering;
using Heroes.Menu.Interfaces;

namespace Heroes.Console.Menu;

public class ConsoleMenuFactory : IMenuFactory
{
    private readonly IAssetsStore _assetsStore;

    public ConsoleMenuFactory(IAssetsStore assetsStore)
    {
        _assetsStore = assetsStore;
    }

    public IMenu CreateMenu(IMenuBreaker menuBreaker, IMenuContext menuContext, params IMenuItem[] items)
    {
        var mapVisitor = new ConsoleMapVisitor(_assetsStore);
        return new ConsoleMenu(menuContext, items, menuBreaker, mapVisitor);
    }
}
