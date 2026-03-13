using Heroes.Console.Rendering;
using Heroes.Map;
using Heroes.Menu.Interfaces;
using Heroes.Utils;

namespace Heroes.Console.Menu;

public class ConsoleMenu : IMenu
{
    private readonly IMenuContext _menuContext;
    private readonly IEnumerable<IMenuItem> _itemsProvider;
    private readonly IMenuBreaker _menuBreaker;
    private readonly IMapVisitor _mapVisitor;

    public ConsoleMenu(IMenuContext menuContext, IEnumerable<IMenuItem> itemsProvider, IMenuBreaker menuBreaker, IMapVisitor mapVisitor)
    {
        _menuContext = menuContext;
        _itemsProvider = itemsProvider;
        _menuBreaker = menuBreaker;
        _mapVisitor = mapVisitor;
    }

    public void Render()
    {
        System.Console.Clear();
        System.Console.WriteLine("\x1b[3J");

        do
        {
            var items = _itemsProvider.Where(x => x.CanRender()).ToArray();
            var indexedItems = items
                .Select((item, index) => new
                {
                    UnitMenuItem = item,
                    Number = index + 1,
                }).ToArray();

            var extra = items.SelectMany(x => x.ExtraObjects);
            _menuContext.Map.Render(_mapVisitor, extra.Union(_menuContext.Elements, MapItemsComparer.Instance));
            foreach (var item in indexedItems)
            {
                System.Console.WriteLine($"{item.Number}) {item.UnitMenuItem.Render()}");
            }

            System.Console.WriteLine("Please select action!");
            int number = -1;
            while (!int.TryParse(System.Console.ReadLine(), out number) || indexedItems.All(m => m.Number != number))
            {
                foreach (var item in indexedItems)
                {
                    System.Console.WriteLine($"{item.Number}) {item.UnitMenuItem.Render()}");
                }

                System.Console.WriteLine("Please select action!");
            }

            indexedItems.First(x => x.Number == number).UnitMenuItem.Select();
        } while (!_menuBreaker.ShouldMenuBreak && _itemsProvider.Any(x => x.CanRender()));
    }
}
