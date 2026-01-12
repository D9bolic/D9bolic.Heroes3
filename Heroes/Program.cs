// See https://aka.ms/new-console-template for more information

using System.Drawing;
using System.Security.Cryptography;
using Heroes.Map;
using Heroes.Map.Rectangle;
using Heroes.Players;
using Heroes.Units;
using Heroes.Units.Army;
using Heroes.Units.Army.Castle;
using Heroes.Units.Army.Rampart;
using Heroes.Units.Heroes.Castle;
using Heroes.Units.Menu.UnitMenu;
using Heroes.Utils;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var map = new RectangleMap(5,3);
map.GenerateRandomObstacles(2);

var player1 = SetupPlayer1(map);
var player2 = SetupPlayer2(map);
var tracker = new InitiativeTracker(player1.Army.Union(player2.Army));
foreach (var unit in tracker)
{
    if (unit.Player.CheckLoose())
    {
       Console.WriteLine($"Player {unit.Player.Name} loosed!");
       break; 
    }

    unit.Player.Activate();
    unit.Activate();
    var menu = new ConsoleUnitMenu(map, unit.Player, unit);
    menu.Render();
    unit.Deactivate();
    unit.Player.Deactivate();
}

IPlayer SetupPlayer1(IMap map)
{
    var player = new Player
    {
        Name = "Player 1",
        Hero = new Christian(),
    };

    var placements = new List<UnitPlacement>
    {
        new UnitPlacement(new Pikeman(player), new Point(0, 0)),
        new UnitPlacement(new Griffin(player), new Point(0, 2)),
    };
    foreach (var placement in placements)
    {
        map.PlaceUnit(placement.Unit, placement.Location);
    }
    player.Deactivate();
    return player;
}


IPlayer SetupPlayer2(IMap map)
{
    var player = new Player
    {
        Name = "Player 2",
        Hero = new Clancy(),
    };
    
    var placements = new List<UnitPlacement>
    {
        new UnitPlacement(new Elf(player), new Point(4, 0)),
        new UnitPlacement(new Centaur(player), new Point(4, 2)),
    };
    foreach (var placement in placements)
    {
        map.PlaceUnit(placement.Unit, placement.Location);
    }
    
    player.Deactivate();
    return player;
}