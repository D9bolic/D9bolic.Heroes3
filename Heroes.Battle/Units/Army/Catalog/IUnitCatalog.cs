using System.Drawing;
using Heroes.Events;

namespace Heroes.Battle.Units.Army.Catalog;

public interface IUnitCatalog
{
    IReadOnlyCollection<string> Ids { get; }

    UnitDefinition GetDefinition(string id);

    IUnit Create(string id, Point coordinates, IGameEventBus eventBus);
}
