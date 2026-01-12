using System.Collections;
using Heroes.Units;
using Heroes.Units.Army;

namespace Heroes.Players;

public class InitiativeTracker : IInitiativeTracker
{
    private readonly IEnumerable<IUnit> _units;

    public InitiativeTracker(IEnumerable<IUnit> units)
    {
        _units = units.OrderBy(x => x.StateLine.Initiative).ToArray();
    }

    public IEnumerator<IUnit> GetEnumerator()
    {
        for (var i = 0; i < _units.Count(); i++)
        {
            var unit = _units.ElementAt(i);
            if (unit.HitPoints <= 0 || unit.Cell is null)
            {
                continue;
            }

            yield return unit;
            if (i == _units.Count() - 1)
            {
                i = 0;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}