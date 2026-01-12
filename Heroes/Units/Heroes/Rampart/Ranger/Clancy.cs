using Heroes.Units.Effects;
using Heroes.Units.Heroes.Abilities;
using Heroes.Units.Heroes.Spells;
using Defence = Heroes.Units.Heroes.Abilities.Defence;

namespace Heroes.Units.Heroes.Castle;

public class Clancy : IHero
{
    public Clancy()
    {
        StateLine = new HeroStateLine
        {
            Attack = 1,
            Defence = 3,
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

    public string Name => "Clancy";
    public HeroStateLine StateLine { get;  }
    
    public IEnumerable<IEffect> Abilities { get;  }
    
    public ICollection<ISpell> SpellBook { get; } = new List<ISpell>();

    public int Mana { get; set; }
}