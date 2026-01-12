using Heroes.Map;
using Heroes.Players;
using Heroes.Units.Army;
using Heroes.Units.Menu.UnitMenu.Actions;

namespace Heroes.Units.Menu.UnitMenu;

public class ConsoleUnitMenu
{
    private readonly IMap _map;
    private readonly IPlayer _player;
    private readonly IUnit _unit;
    private readonly IEnumerable<ISelectableUnitAction> _actions;
    
    public ConsoleUnitMenu(IMap map, IPlayer player, IUnit unit)
    {
        _map = map;
        _player = player;
        _unit = unit;
        _actions =
        [
            new MovementUnitAction(map, player),
            new AttackUnitAction(map, player),
            new DefenceUnitAction(map, player)
        ];
    }

    public void Render()
    {
        var breaker = new UnitMenuBreaker
        {
            Unit = _unit,
        };
        
        Console.Clear();
        Console.WriteLine("\x1b[3J");
        
        while(!breaker.IsStopped)
        {
            var indexedItems = _actions
                .SelectMany(x => x.GenerateMenuItems(_unit, breaker))
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