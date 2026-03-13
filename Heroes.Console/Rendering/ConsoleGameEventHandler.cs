using Heroes.Adventure.Events;
using Heroes.Events;

namespace Heroes.Console.Rendering;

public class ConsoleGameEventHandler
{
    public ConsoleGameEventHandler(IGameEventBus eventBus)
    {
        eventBus.Subscribe<PlayerTurnStarted>(OnPlayerTurnStarted);
        eventBus.Subscribe<UnitActivated>(OnUnitActivated);
        eventBus.Subscribe<DamageDealt>(OnDamageDealt);
        eventBus.Subscribe<UnitDied>(OnUnitDied);
        eventBus.Subscribe<UnitHitPointsRemaining>(OnUnitHitPointsRemaining);
        eventBus.Subscribe<CounterAttackStarted>(OnCounterAttackStarted);
        eventBus.Subscribe<GameOver>(OnGameOver);
        eventBus.Subscribe<AdventureTurnStarted>(OnAdventureTurnStarted);
        eventBus.Subscribe<HeroMoved>(OnHeroMoved);
        eventBus.Subscribe<HeroInteracted>(OnHeroInteracted);
        eventBus.Subscribe<ResourceCollected>(OnResourceCollected);
        eventBus.Subscribe<CityVisited>(OnCityVisited);
        eventBus.Subscribe<NewDayStarted>(OnNewDayStarted);
        eventBus.Subscribe<NewWeekStarted>(OnNewWeekStarted);
    }

    private void OnPlayerTurnStarted(PlayerTurnStarted e)
    {
        System.Console.WriteLine($"Player {e.PlayerName} turn");
    }

    private void OnUnitActivated(UnitActivated e)
    {
        System.Console.WriteLine($"Active creature: {e.UnitName}");
        System.Console.WriteLine($"Active effects");
        foreach (var effect in e.ActiveEffects)
        {
            System.Console.WriteLine(effect);
        }

        System.Console.WriteLine($"Speed: {e.Speed}");
        System.Console.WriteLine($"Health: {e.HitPoints}");
        System.Console.WriteLine($"Hit points: {e.HitPoints}");
        System.Console.WriteLine($"Damage: {e.DamageMin}-{e.DamageMax}");
        System.Console.WriteLine($"Attack: {e.Attack}");
        System.Console.WriteLine($"Defence: {e.Defence}");
    }

    private void OnDamageDealt(DamageDealt e)
    {
        System.Console.WriteLine($"{e.AttackerName} attacking {e.DefenderName} with damage {e.Damage}");
    }

    private void OnUnitDied(UnitDied e)
    {
        System.Console.WriteLine($"{e.UnitName} is dead");
    }

    private void OnUnitHitPointsRemaining(UnitHitPointsRemaining e)
    {
        System.Console.WriteLine($"{e.UnitName} {e.RemainingHitPoints} hit points left");
    }

    private void OnCounterAttackStarted(CounterAttackStarted e)
    {
        System.Console.WriteLine($"{e.AttackerName} counter attack {e.DefenderName}");
    }

    private void OnGameOver(GameOver e)
    {
        System.Console.WriteLine($"Player {e.LosingPlayerName} loosed!");
    }

    private void OnAdventureTurnStarted(AdventureTurnStarted e)
    {
        System.Console.WriteLine($"=== {e.PlayerName}'s turn (Day {e.Day}) ===");
    }

    private void OnHeroMoved(HeroMoved e)
    {
        System.Console.WriteLine($"{e.HeroName} moved (MP remaining: {e.RemainingMovementPoints})");
    }

    private void OnHeroInteracted(HeroInteracted e)
    {
        System.Console.WriteLine($"{e.HeroName} interacted with {e.EntityName}");
    }

    private void OnResourceCollected(ResourceCollected e)
    {
        System.Console.WriteLine($"Collected {e.Amount} {e.ResourceType}");
    }

    private void OnCityVisited(CityVisited e)
    {
        System.Console.WriteLine($"{e.HeroName} visited city {e.CityName}");
    }

    private void OnNewDayStarted(NewDayStarted e)
    {
        System.Console.WriteLine($"--- Day {e.Day} ---");
    }

    private void OnNewWeekStarted(NewWeekStarted e)
    {
        System.Console.WriteLine($"=== Week {e.Week} begins! ===");
    }
}
