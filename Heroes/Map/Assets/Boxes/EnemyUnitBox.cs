using System.Drawing;
using Heroes.Map;

namespace Heroes.Menu.Unit;

public class EnemyUnitBox(IMapItem item) : WrapperBase(item)
{
    protected override string GetName(IMapItem item)
    {
        return $"{item.Name}:Enemy";
    }
}