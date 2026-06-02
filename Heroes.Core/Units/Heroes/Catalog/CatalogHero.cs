using Heroes.Units.Effects;
using Heroes.Units.Heroes.Abilities;
using Heroes.Units.Heroes.Spells;
using Defence = Heroes.Units.Heroes.Abilities.Defence;

namespace Heroes.Units.Heroes.Catalog;

internal sealed class CatalogHero : IHero
{
    public CatalogHero(HeroDefinition definition)
    {
        Name = definition.Name;
        StateLine = definition.StateLine;
        Abilities = new List<IEffect>
        {
            new Attack(StateLine),
            new Defence(StateLine),
        };
        Mana = definition.Mana ?? StateLine.Wisdom * 10;
    }

    public string Name { get; }

    public HeroStateLine StateLine { get; }

    public IEnumerable<IEffect> Abilities { get; }

    public ICollection<ISpell> SpellBook { get; } = new List<ISpell>();

    public int Mana { get; set; }
}
