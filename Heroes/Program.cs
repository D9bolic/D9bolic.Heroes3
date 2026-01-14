// See https://aka.ms/new-console-template for more information

using System.Drawing;
using System.Reflection;
using Heroes.Map;
using Heroes.Map.Rectangle;
using Heroes.Menu.Unit;
using Heroes.Players;
using Heroes.Units.Army.Castle;
using Heroes.Units.Army.Rampart;
using Heroes.Units.Heroes.Castle;
using Heroes.Utils;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var assetStore = new ConsoleAssetsStore(new RectanglePattern());
var map = new RectangleMap(5,3, assetStore);

//var menuFactory = new ConsoleMenuFactory(map);

var player1 = SetupPlayer1();
var player2 = SetupPlayer2();
var obstacles = map.GenerateRandomObstacles(2, player1.Army.Union(player2.Army).ToArray());

var tracker = new InitiativeTracker(player1, player2);
foreach (var turn in tracker)
{
    if (turn.Player.CheckLoose())
    {
       Console.WriteLine($"Player {turn.Player.Name} loosed!");
       break; 
    }

    turn.Player.Activate();
    turn.Unit.Activate();
    map.Draw(
        player1.Army.Select(x => new AllyUnitBox(x)).OfType<IMapItem>()
        .Union(player2.Army.Select(x => new EnemyUnitBox(x)).OfType<IMapItem>())
        .Union(obstacles).ToArray());
    /*var menuBreaker = new MenuBreaker
    {
        ShouldMenuBreak = false,
    };

    var menu = menuFactory.CreateMenu(menuBreaker,
        new MovementUnitMenuItem(map, turn, menuFactory),
        new AttackUnitMenuItem(map, turn, menuFactory, menuBreaker),
        new DefenceMenuItem(turn, menuBreaker));

    menu.Render();*/
}

IPlayer SetupPlayer1()
{
    var player = new Player
    {
        Name = "Player 1",
        Hero = new Christian(),
    };

    //ToDo Effects
    player.Army.Add(new Pikeman(new Point(0, 0)));
    player.Army.Add(new Griffin(new Point(0, 2)));
    
    return player;
}


IPlayer SetupPlayer2()
{
    var player = new Player
    {
        Name = "Player 2",
        Hero = new Clancy(),
    };
    
    player.Army.Add(new Elf(new Point(4, 0)));
    player.Army.Add(new Centaur(new Point(4, 2)));
    
    return player;
}