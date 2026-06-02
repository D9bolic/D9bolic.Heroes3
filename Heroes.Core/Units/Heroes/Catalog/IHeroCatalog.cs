namespace Heroes.Units.Heroes.Catalog;

public interface IHeroCatalog
{
    IReadOnlyCollection<string> Ids { get; }

    HeroDefinition GetDefinition(string id);

    IHero Create(string id);
}
