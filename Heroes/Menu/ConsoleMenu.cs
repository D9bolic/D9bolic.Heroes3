using Heroes.Map;

namespace Heroes.Menu;

public class ConsoleMenu : IMenu
{
    private readonly Func<IEnumerable<IMenuItem>> _itemsProvider;
    private readonly IMenuBreaker _menuBreaker;
    private readonly IMap _map;

    public ConsoleMenu(IMap _map, Func<IEnumerable<IMenuItem>> itemsProvider, IMenuBreaker menuBreaker)
    {
        this._map = _map;
        _itemsProvider = itemsProvider;
        _menuBreaker = menuBreaker;
    }
    
    public void Render()
    {
        Console.Clear();
        Console.WriteLine("\x1b[3J");

        while (!_menuBreaker.ShouldMenuBreak)
        {
            var items = _itemsProvider().Where(x => x.CanRender()).ToArray();
            var indexedItems = items
                .Select((item, index) => new
                {
                    UnitMenuItem = item,
                    Number = index + 1,
                }).ToArray();

            _map?.Draw();
            foreach (var item in indexedItems)
            {
                Console.WriteLine($"{item.Number}) {item.UnitMenuItem.Render()}");
            }

            Console.WriteLine("Please select action!");
            int number = -1;
            while (!int.TryParse(Console.ReadLine(), out number) || indexedItems.All(m => m.Number != number))
            {
                foreach (var item in indexedItems)
                {
                    Console.WriteLine($"{item.Number}) {item.UnitMenuItem.Render()}");
                }

                Console.WriteLine("Please select action!");
            }

            indexedItems.First(x => x.Number == number).UnitMenuItem.Select();
            foreach (var item in indexedItems)
            {
                item.UnitMenuItem.Dispose();
            }
        }
    }
}