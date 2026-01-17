using System.Security.Cryptography;
using Heroes.Map;
using Heroes.Utils;

namespace Heroes.Units.Army.Attack;

public abstract class AttackPatternBase
{
    protected void CalculateDamage(IUnit attacker, IUnit defender, int damageMin, int damageMax)
    {
        var different = attacker.StateLine.Attack - defender.StateLine.Defence;
        var damage = RandomNumberGenerator.GetInt32(damageMin, damageMax);
        if (different > 0)
        {
            damage = (int)Math.Round(damage + (different * damage * 0.1));
        }
        
        Console.WriteLine($"{attacker.Name} attacking {defender.Name} with damage {damage}");
        defender.Wounds += damage;

        if (defender.IsDead())
        {
            Console.WriteLine( $"{defender.Name} is dead");
        }
        else
        {
            Console.WriteLine($"{defender.Name} {defender.HitPoints()} hit points left");
        }
    }
    
    protected void CounterAttack(IUnit attacker, IUnit defender, IMap map)
    {
        if (defender.IsDead() || defender.CounterAttacks < 1)
        {
            return;
        }
        
        Console.WriteLine($"{defender.Name} counterattacking {attacker.Name}");
        defender.AttackPattern.Attack(map, attacker);
        defender.CounterAttacks--;
    }
}