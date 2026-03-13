using Heroes.Assets;
using Heroes.Map;
using Heroes.Battle.Units.Army.Attack;
using Heroes.Units.Army;
using Heroes.Units.Effects;

namespace Heroes.Battle.Units.Army;

public interface IUnit : IMapItem
{
    public bool CanFly { get; }

    public int Wounds { get; set; }

    public int CounterAttacks { get; set; }

    public IAttackPattern AttackPattern { get; }

    public UnitStateLine StateLine { get; }

    LongEffectsList LongEffects { get; }

    public int MovementLeft { get; set; }

    public string Name { get; }

    void Activate();

    void CounterAttack(IUnit target);

    void Defence(IUnit attacker);
}
