using Heroes.Map;
using Heroes.Players;
using Heroes.Units.Army;
using Heroes.Units.Menu.SpellMenu.Actions;
using Heroes.Units.Menu.UnitMenu.Actions;

namespace Heroes.Units.Menu.SpellMenu;

public class ConsoleSpellMenu
{
    private readonly IMap _map;
    private readonly IPlayer _player;
    private readonly IEnumerable<ISelectableSpellAction> _actions;
    
    public ConsoleSpellMenu(IMap map, IPlayer player)
    {
        _map = map;
        _player = player;
        _actions =
        [
        ];
    }

    public void Render(IUnit unit)
    {
        var breaker = new SpellMenuBreaker
        {
            Unit = unit,
        };
        
        while(!breaker.IsStopped)
        {
            var indexedItems = _actions
                .SelectMany(x => x.GenerateMenuItems(unit, breaker))
                .Select((item, index) => new
                {
                    UnitMenuItem = item,
                    Number = index + 1,
                }).ToArray();

            _map.Draw();
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

            indexedItems.First(x => x.Number == number).UnitMenuItem.OnSelected();
            foreach (var item in indexedItems)
            {
                item.UnitMenuItem.Dispose();
            }
        }
    }
}