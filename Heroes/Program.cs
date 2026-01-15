// See https://aka.ms/new-console-template for more information

using System.Drawing;
using System.Reflection;
using Heroes.Map;
using Heroes.Map.Assets.ConsoleAssets;
using Heroes.Map.Assets.ConsoleAssets.Patterns.Hexagonal;
using Heroes.Map.Assets.ConsoleAssets.Patterns.Rectangle;
using Heroes.Map.Hex;
using Heroes.Map.Rectangle;
using Heroes.Menu;
using Heroes.Menu.Interfaces;
using Heroes.Menu.Unit;
using Heroes.Players;
using Heroes.Units.Army;
using Heroes.Units.Army.Castle;
using Heroes.Units.Army.Rampart;
using Heroes.Units.Heroes.Castle.Knight;
using Heroes.Units.Heroes.Rampart.Ranger;
using Heroes.Utils;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var assetStore = new ConsoleAssetsStore(new RectanglePattern());
var map = new RectangleMap(5,3, assetStore);

var menuFactory = new ConsoleMenuFactory();

var player1 = SetupPlayer1();
var player2 = SetupPlayer2();
var obstacles = map.GenerateRandomObstacles(2, player1.Army.Union(player2.Army).ToArray());

var tracker = new InitiativeTracker(player1, player2, map, obstacles);
foreach (var turn in tracker)
{
    if (turn.Player.CheckLoose())
    {
       Console.WriteLine($"Player {turn.Player.Name} loosed!");
       break; 
    }
    
    turn.Player.Activate();
    turn.ActiveUnit.Activate();
    var menuBreaker = new MenuBreaker
    {
        ShouldMenuBreak = false,
    };
    var menu = menuFactory.CreateMenu(menuBreaker, turn,
        new MovementUnitMenuItem(turn, menuFactory, menuBreaker),
        new AttackUnitMenuItem(turn, menuFactory, menuBreaker),
        new DefenceMenuItem(turn, menuBreaker));

    menu.Render(turn);
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