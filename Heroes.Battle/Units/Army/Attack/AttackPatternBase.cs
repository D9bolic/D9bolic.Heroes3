using Heroes.Events;
using Heroes.Map;
using Heroes.Battle.Utils;

namespace Heroes.Battle.Units.Army.Attack;

public abstract class AttackPatternBase
{
    private readonly IGameEventBus _eventBus;

    protected AttackPatternBase(IGameEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    protected void CalculateDamage(IUnit attacker, IUnit defender, int damageMin, int damageMax)
    {
        var different = attacker.StateLine.Attack - defender.StateLine.Defence;
        var damage = System.Security.Cryptography.RandomNumberGenerator.GetInt32(damageMin, damageMax);
        if (different > 0)
        {
            damage = (int)Math.Round(damage + (different * damage * 0.1));
        }

        _eventBus.Publish(new DamageDealt(attacker.Name, defender.Name, damage));
        defender.Wounds += damage;

        if (defender.IsDead())
        {
            _eventBus.Publish(new UnitDied(defender.Name));
        }
        else
        {
            _eventBus.Publish(new UnitHitPointsRemaining(defender.Name, defender.HitPoints()));
        }
    }

    protected void CounterAttack(IUnit attacker, IUnit defender, IMap map)
    {
        if (defender.IsDead() || defender.CounterAttacks < 1)
        {
            return;
        }

        _eventBus.Publish(new CounterAttackStarted(defender.Name, attacker.Name));
        defender.AttackPattern.Attack(map, attacker);
        defender.CounterAttacks--;
    }
}
