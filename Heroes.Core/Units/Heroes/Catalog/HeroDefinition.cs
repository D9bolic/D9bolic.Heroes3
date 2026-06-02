namespace Heroes.Units.Heroes.Catalog;

public sealed record HeroDefinition
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required string FactionId { get; init; }

    public string? ClassId { get; init; }

    public required HeroStateLine StateLine { get; init; }

    public int? Mana { get; init; }

    public IReadOnlyList<string> StartingSpells { get; init; } = [];

    public IReadOnlyList<string> Abilities { get; init; } = [];

    public string? ImagePath { get; init; }
}
