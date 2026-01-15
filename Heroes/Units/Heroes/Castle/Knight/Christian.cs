using Heroes.Units.Effects;
using Heroes.Units.Heroes.Abilities;
using Heroes.Units.Heroes.Spells;
using Defence = Heroes.Units.Heroes.Abilities.Defence;

namespace Heroes.Units.Heroes.Castle.Knight;

public class Christian : IHero
{
    public Christian()
    {
        StateLine = new HeroStateLine
        {
            Attack = 2,
            Defence = 2,
            Wisdom = 1,
            Power = 1,
        };

        Abilities = new List<IEffect>
        {
            new Attack(StateLine),
            new Defence(StateLine),
        };

        Mana = StateLine.Wisdom * 10;
    }

    public string Name => "Christian";

    public HeroStateLine StateLine { get; }

    public IEnumerable<IEffect> Abilities { get; }
    
    public ICollection<ISpell> SpellBook { get; } = new List<ISpell>();

    public int Mana { get; set; }
}