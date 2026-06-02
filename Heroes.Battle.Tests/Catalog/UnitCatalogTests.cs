using System.Drawing;
using Heroes.Battle.Units.Army.Catalog;
using Heroes.Events;

namespace Heroes.Battle.Tests.Catalog;

public class UnitCatalogTests
{
    private static readonly string AssetsDir =
        Path.Combine(AppContext.BaseDirectory, "assets", "units");

    private static readonly string SchemaPath =
        Path.Combine(AppContext.BaseDirectory, "schemas", "unit.schema.json");

    private static UnitCatalog LoadCatalog() =>
        UnitCatalog.LoadFromDirectory(AssetsDir, SchemaPath);

    [Fact]
    public void Catalog_loads_every_migrated_unit()
    {
        var catalog = LoadCatalog();

        Assert.Contains("Pikeman", catalog.Ids);
        Assert.Contains("Griffin", catalog.Ids);
        Assert.Contains("Centaur", catalog.Ids);
        Assert.Contains("Elf", catalog.Ids);
    }

    [Fact]
    public void Sample_units_are_skipped()
    {
        var catalog = LoadCatalog();

        // _sample.json must not pollute production ids even though it sits in the same folder.
        Assert.DoesNotContain("_sample", catalog.Ids);
    }

    [Theory]
    [InlineData("Pikeman", 1, "Castle", false, 4, 4, 10, 1, 3, 4, 5, UnitAttackPattern.Melee)]
    [InlineData("Griffin", 4, "Castle", true, 6, 6, 25, 3, 6, 8, 8, UnitAttackPattern.Melee)]
    [InlineData("Centaur", 1, "Rampart", false, 6, 6, 8, 2, 3, 5, 3, UnitAttackPattern.Melee)]
    [InlineData("Elf", 3, "Rampart", false, 6, 6, 15, 3, 5, 9, 5, UnitAttackPattern.Distance)]
    public void Migrated_units_preserve_existing_balance(
        string id, int level, string faction, bool canFly,
        int initiative, int speed, int hitPoints, int damageMin, int damageMax,
        int attack, int defence, UnitAttackPattern attackPattern)
    {
        var def = LoadCatalog().GetDefinition(id);

        Assert.Equal(level, def.Level);
        Assert.Equal(faction, def.Faction);
        Assert.Equal(canFly, def.CanFly);
        Assert.Equal(attackPattern, def.AttackPattern);
        Assert.Equal(initiative, def.StateLine.Initiative);
        Assert.Equal(speed, def.StateLine.Speed);
        Assert.Equal(hitPoints, def.StateLine.HitPoints);
        Assert.Equal(damageMin, def.StateLine.DamageMin);
        Assert.Equal(damageMax, def.StateLine.DamageMax);
        Assert.Equal(attack, def.StateLine.Attack);
        Assert.Equal(defence, def.StateLine.Defence);
    }

    [Fact]
    public void Levels_are_within_one_to_seven()
    {
        var catalog = LoadCatalog();

        foreach (var id in catalog.Ids)
        {
            var level = catalog.GetDefinition(id).Level;
            Assert.InRange(level, 1, 7);
        }
    }

    [Fact]
    public void Create_returns_unit_with_definition_data()
    {
        var catalog = LoadCatalog();
        var bus = new GameEventBus();

        var griffin = catalog.Create("Griffin", new Point(2, 3), bus);

        Assert.Equal("Griffin", griffin.Name);
        Assert.True(griffin.CanFly);
        Assert.Equal(new Point(2, 3), griffin.Coordinates);
        Assert.Equal(25, griffin.StateLine.HitPoints);
        Assert.Equal(8, griffin.StateLine.Attack);
    }

    [Fact]
    public void Create_throws_on_unknown_id()
    {
        var catalog = LoadCatalog();
        var bus = new GameEventBus();

        Assert.Throws<KeyNotFoundException>(
            () => catalog.Create("Dragon", new Point(0, 0), bus));
    }

    [Fact]
    public void Loader_rejects_units_violating_schema()
    {
        var temp = Directory.CreateTempSubdirectory("unit-catalog-tests-");
        try
        {
            // level 9 is out of range (schema caps at 7)
            File.WriteAllText(
                Path.Combine(temp.FullName, "broken.json"),
                """
                {
                  "name": "Broken",
                  "level": 9,
                  "attackPattern": "Melee",
                  "stateLine": {
                    "initiative": 1, "speed": 1, "hitPoints": 1,
                    "damageMin": 1, "damageMax": 1, "attack": 1, "defence": 1
                  }
                }
                """);

            Assert.Throws<InvalidOperationException>(
                () => UnitCatalog.LoadFromDirectory(temp.FullName, SchemaPath));
        }
        finally
        {
            temp.Delete(recursive: true);
        }
    }
}
