using System.Drawing;
using Heroes.Battle.Units.Army.Attack;
using Heroes.Events;

namespace Heroes.Battle.Units.Army.Catalog;

internal sealed class CatalogUnit : UnitBase
{
    private readonly bool _canFly;

    public CatalogUnit(UnitDefinition definition, Point coordinates, IGameEventBus eventBus)
        : base(definition.Name, coordinates, eventBus)
    {
        _canFly = definition.CanFly;
        CounterAttacks = definition.CounterAttacks;
        StateLine = definition.StateLine;
        AttackPattern = definition.AttackPattern switch
        {
            UnitAttackPattern.Melee => new MeleeAttackPattern(this, eventBus),
            UnitAttackPattern.Distance => new DistanceAttackPattern(this, eventBus),
            _ => throw new InvalidOperationException(
                $"Unknown attack pattern '{definition.AttackPattern}' for unit '{definition.Name}'."),
        };
    }

    public override bool CanFly => _canFly;
}
