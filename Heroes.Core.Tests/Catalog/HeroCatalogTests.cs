using Heroes.Units.Heroes.Abilities;
using Heroes.Units.Heroes.Catalog;
using Defence = Heroes.Units.Heroes.Abilities.Defence;

namespace Heroes.Core.Tests.Catalog;

public class HeroCatalogTests
{
    private static readonly string AssetsDir =
        Path.Combine(AppContext.BaseDirectory, "assets", "heroes");

    private static readonly string SchemaPath =
        Path.Combine(AppContext.BaseDirectory, "schemas", "hero.schema.json");

    private static HeroCatalog LoadCatalog() =>
        HeroCatalog.LoadFromDirectory(AssetsDir, SchemaPath);

    [Fact]
    public void Catalog_loads_every_migrated_hero()
    {
        var catalog = LoadCatalog();

        Assert.Contains("christian", catalog.Ids);
        Assert.Contains("clancy", catalog.Ids);
    }

    [Fact]
    public void Sample_heroes_are_skipped()
    {
        var catalog = LoadCatalog();

        Assert.DoesNotContain("_sample", catalog.Ids);
    }

    [Theory]
    [InlineData("christian", "Christian", "castle", "knight", 2, 2, 1, 1)]
    [InlineData("clancy", "Clancy", "rampart", "ranger", 1, 3, 1, 1)]
    public void Migrated_heroes_preserve_existing_balance(
        string id, string name, string factionId, string classId,
        int attack, int defence, int wisdom, int power)
    {
        var def = LoadCatalog().GetDefinition(id);

        Assert.Equal(name, def.Name);
        Assert.Equal(factionId, def.FactionId);
        Assert.Equal(classId, def.ClassId);
        Assert.Equal(attack, def.StateLine.Attack);
        Assert.Equal(defence, def.StateLine.Defence);
        Assert.Equal(wisdom, def.StateLine.Wisdom);
        Assert.Equal(power, def.StateLine.Power);
    }

    [Fact]
    public void Create_returns_hero_with_definition_data_and_default_abilities()
    {
        var catalog = LoadCatalog();

        var hero = catalog.Create("christian");

        Assert.Equal("Christian", hero.Name);
        Assert.Equal(2, hero.StateLine.Attack);
        Assert.Equal(10, hero.Mana); // wisdom (1) * 10
        Assert.Empty(hero.SpellBook);
        Assert.Contains(hero.Abilities, a => a is Attack);
        Assert.Contains(hero.Abilities, a => a is Defence);
    }

    [Fact]
    public void Create_derives_mana_from_wisdom_when_not_specified()
    {
        var catalog = LoadCatalog();

        var clancy = catalog.Create("clancy");

        Assert.Equal(clancy.StateLine.Wisdom * 10, clancy.Mana);
    }

    [Fact]
    public void Create_throws_on_unknown_id()
    {
        var catalog = LoadCatalog();

        Assert.Throws<KeyNotFoundException>(() => catalog.Create("dragon"));
    }

    [Fact]
    public void Loader_rejects_heroes_violating_schema()
    {
        var temp = Directory.CreateTempSubdirectory("hero-catalog-tests-");
        try
        {
            // missing required factionId
            File.WriteAllText(
                Path.Combine(temp.FullName, "broken.json"),
                """
                {
                  "id": "broken",
                  "name": "Broken",
                  "stateLine": { "attack": 1, "defence": 1, "wisdom": 1, "power": 1 }
                }
                """);

            Assert.Throws<InvalidOperationException>(
                () => HeroCatalog.LoadFromDirectory(temp.FullName, SchemaPath));
        }
        finally
        {
            temp.Delete(recursive: true);
        }
    }
}
