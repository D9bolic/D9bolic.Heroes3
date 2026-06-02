using System.Text.Json.Serialization;
using Heroes.Units.Army;

namespace Heroes.Battle.Units.Army.Catalog;

public sealed record UnitDefinition
{
    public required string Name { get; init; }

    public required int Level { get; init; }

    public string? Faction { get; init; }

    public bool CanFly { get; init; }

    public int CounterAttacks { get; init; } = 1;

    public required UnitAttackPattern AttackPattern { get; init; }

    public required UnitStateLine StateLine { get; init; }

    public string? ImagePath { get; init; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UnitAttackPattern
{
    Melee,
    Distance,
}
