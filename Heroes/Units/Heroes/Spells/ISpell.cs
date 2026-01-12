using Heroes.Units.Army;

namespace Heroes.Units.Heroes.Spells;

public interface ISpell
{
    public int Cost { get; }
}

public interface IDefenceSpell : ISpell
{
    public void Apply(IUnit unit);
}

public interface IAttackSpell : ISpell
{
    public void Apply(IUnit unit);
}