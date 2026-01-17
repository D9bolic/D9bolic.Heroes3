using Heroes.Map;
using Heroes.Menu.Interfaces;
using Heroes.Players;
using Heroes.Utils;

namespace Heroes.Menu;

public class ConsoleMenu : IMenu
{
    private readonly TurnInformation _turnInformation;
    private readonly IEnumerable<IMenuItem> _itemsProvider;
    private readonly IMenuBreaker _menuBreaker;

    public ConsoleMenu(TurnInformation turnInformation, IEnumerable<IMenuItem> itemsProvider, IMenuBreaker menuBreaker)
    {
        _turnInformation = turnInformation;
        _itemsProvider = itemsProvider;
        _menuBreaker = menuBreaker;
    }

    public void Render()
    {
        Console.Clear();
        Console.WriteLine("\x1b[3J");

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
            _turnInformation.Map.Draw(extra.Union(_turnInformation.Elements, MapItemsComparer.Instance));
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
        } while (!_menuBreaker.ShouldMenuBreak && _itemsProvider.Any(x => x.CanRender()));
    }
}