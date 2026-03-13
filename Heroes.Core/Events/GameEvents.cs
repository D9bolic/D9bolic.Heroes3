using Heroes.Units.Army;

namespace Heroes.Events;

public abstract record GameEvent;

public record PlayerTurnStarted(string PlayerName) : GameEvent;

public record UnitActivated(
    string UnitName,
    IReadOnlyList<string> ActiveEffects,
    int Speed,
    int HitPoints,
    int DamageMin,
    int DamageMax,
    int Attack,
    int Defence) : GameEvent;

public record DamageDealt(
    string AttackerName,
    string DefenderName,
    int Damage) : GameEvent;

public record UnitDied(string UnitName) : GameEvent;

public record UnitHitPointsRemaining(
    string UnitName,
    int RemainingHitPoints) : GameEvent;

public record CounterAttackStarted(
    string AttackerName,
    string DefenderName) : GameEvent;

public record GameOver(string LosingPlayerName) : GameEvent;

public record CombatCompleted : GameEvent;
