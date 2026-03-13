using System.Drawing;
using Heroes.Adventure;
using Heroes.Adventure.Entities;
using Heroes.Adventure.Entities.Cities;
using Heroes.Adventure.Entities.Heroes;
using Heroes.Adventure.Entities.Resources;
using Heroes.Adventure.Menu;
using Heroes.Adventure.Players;
using Heroes.Console.Assets.ConsoleAssets;
using Heroes.Console.Assets.ConsoleAssets.Patterns.Rectangle;
using Heroes.Console.Menu;
using Heroes.Console.Rendering;
using Heroes.Events;
using Heroes.Map.Rectangle;
using Heroes.Menu;
using Heroes.Menu.Interfaces;
using Heroes.Battle.Menu.Unit;
using Heroes.Battle.Players;
using Heroes.Battle.Units.Army.Castle;
using Heroes.Battle.Units.Army.Rampart;
using Heroes.Units.Heroes.Castle.Knight;
using Heroes.Units.Heroes.Rampart.Ranger;
using Heroes.Utils;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var eventBus = new GameEventBus();
var eventHandler = new ConsoleGameEventHandler(eventBus);
var userInteraction = new ConsoleUserInteraction();

var assetStore = new ConsoleAssetsStore(new RectanglePattern());
var menuFactory = new ConsoleMenuFactory(assetStore);

// === Adventure Phase ===
RunAdventurePhase(eventBus, menuFactory);

// === Battle Phase ===
RunBattlePhase(eventBus, menuFactory, userInteraction);

void RunAdventurePhase(IGameEventBus bus, IMenuFactory factory)
{
    var adventureMap = new RectangleMap(12, 6);

    var player1Hero = new AdventureHero(new Christian(), new Point(0, 0), 5);
    var player2Hero = new AdventureHero(new Clancy(), new Point(11, 5), 5);

    var adventurePlayer1 = new AdventurePlayer
    {
        Name = "Player 1",
        Heroes = [player1Hero],
    };
    adventurePlayer1.Resources[ResourceType.Gold] = 5000;
    adventurePlayer1.Resources[ResourceType.Wood] = 10;
    adventurePlayer1.Resources[ResourceType.Ore] = 10;

    var adventurePlayer2 = new AdventurePlayer
    {
        Name = "Player 2",
        Heroes = [player2Hero],
    };
    adventurePlayer2.Resources[ResourceType.Gold] = 5000;
    adventurePlayer2.Resources[ResourceType.Wood] = 10;
    adventurePlayer2.Resources[ResourceType.Ore] = 10;

    var entities = new List<IAdventureEntity>
    {
        new City("Castle", new Point(1, 0)) { Owner = adventurePlayer1 },
        new City("Rampart", new Point(10, 5)) { Owner = adventurePlayer2 },
        new ResourceSite("Gold Mine", new Point(5, 2), ResourceType.Gold, 1000),
        new ResourceSite("Sawmill", new Point(7, 3), ResourceType.Wood, 2),
        new NeutralUnit("Pikemen", new Point(6, 3), 10),
        new PointOfInterest("Fountain of Youth", new Point(3, 4)),
    };

    var obstacles = adventureMap.GenerateRandomObstacles(4,
        entities.Union(adventurePlayer1.Heroes).Union(adventurePlayer2.Heroes).ToArray());
    adventureMap.InitializeLandscape(obstacles);

    var context = new AdventureContext
    {
        Map = adventureMap,
        Entities = entities,
    };

    var players = new[] { adventurePlayer1, adventurePlayer2 };
    int day = 1;

    bus.Publish(new Heroes.Adventure.Events.NewDayStarted(day));

    for (int turn = 0; turn < 4; turn++)
    {
        foreach (var player in players)
        {
            context.ActivePlayer = player;
            bus.Publish(new Heroes.Adventure.Events.AdventureTurnStarted(player.Name, day));

            foreach (var hero in player.Heroes)
            {
                hero.ResetMovement();
            }

            var turnBreaker = new MenuBreaker { ShouldMenuBreak = false };
            var heroMenuItems = player.Heroes
                .Select(h => (IMenuItem)new SelectHeroMenuItem(h, context, factory, turnBreaker))
                .ToList();
            heroMenuItems.Add(new EndTurnMenuItem(turnBreaker));

            var menu = factory.CreateMenu(turnBreaker, context, heroMenuItems.ToArray());
            menu.Render();
        }

        day++;
        bus.Publish(new Heroes.Adventure.Events.NewDayStarted(day));
    }
}

void RunBattlePhase(IGameEventBus bus, IMenuFactory factory, IUserInteraction interaction)
{
    var battleMap = new RectangleMap(10, 4);

    var player1 = SetupBattlePlayer1(bus);
    var player2 = SetupBattlePlayer2(bus);
    var obstacles = battleMap.GenerateRandomObstacles(2, player1.Army.Concat(player2.Army).ToArray());
    battleMap.InitializeLandscape(obstacles);
    var tracker = new InitiativeTracker(player1, player2, battleMap);
    foreach (var turn in tracker)
    {
        if (turn.Player.CheckLoose())
        {
            bus.Publish(new GameOver(turn.Player.Name));
            break;
        }

        turn.Player.Activate();
        turn.ActiveUnit.Activate();
        var menuBreaker = new MenuBreaker
        {
            ShouldMenuBreak = false,
        };
        var menu = factory.CreateMenu(menuBreaker, turn,
            new MovementUnitMenuItem(turn, factory, menuBreaker),
            new AttackUnitMenuItem(turn, factory, menuBreaker, interaction),
            new DefenceMenuItem(turn, menuBreaker));

        menu.Render();
    }
}

IPlayer SetupBattlePlayer1(IGameEventBus bus)
{
    var player = new Player(bus)
    {
        Name = "Player 1",
        Hero = new Christian(),
    };

    player.Army.Add(new Pikeman(new Point(0, 0), bus));
    player.Army.Add(new Griffin(new Point(0, 2), bus));

    return player;
}

IPlayer SetupBattlePlayer2(IGameEventBus bus)
{
    var player = new Player(bus)
    {
        Name = "Player 2",
        Hero = new Clancy(),
    };

    player.Army.Add(new Elf(new Point(9, 0), bus));
    player.Army.Add(new Centaur(new Point(9, 2), bus));

    return player;
}
