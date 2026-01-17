using Heroes.Map;

namespace Heroes.Units.Army.Attack;

public interface IAttackPattern
{
    void Attack(IMap map, IUnit enemy);

    IEnumerable<IUnit> GetTargets(IMap map, IEnumerable<IUnit> enemies);
}