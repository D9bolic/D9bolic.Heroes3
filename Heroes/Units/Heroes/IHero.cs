using Heroes.Units.Effects;
using Heroes.Units.Heroes.Spells;

namespace Heroes.Units.Heroes;

public interface IHero
{
    string Name { get; }
    
    HeroStateLine StateLine { get; }
    
    IEnumerable<IEffect> Abilities { get; }
    
    ICollection<ISpell> SpellBook { get; }
    
    int Mana { get; set; }
}