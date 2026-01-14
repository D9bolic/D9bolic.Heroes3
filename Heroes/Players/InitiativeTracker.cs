using System.Collections;
using Heroes.Units.Army;

namespace Heroes.Players;

public record CurrentTurn(IUnit Unit, IPlayer Player);

public class InitiativeTracker : IInitiativeTracker
{
    private readonly IEnumerable<CurrentTurn> _units;

    public InitiativeTracker(params IPlayer[] players)
    {
        _units = players
            .SelectMany(p => p.Army.Select(u => new CurrentTurn(u, p)))
            .OrderBy(x => x.Unit.StateLine.Initiative).ToArray();
    }

    public IEnumerator<CurrentTurn> GetEnumerator()
    {
        for (var i = 0; i < _units.Count(); i++)
        {
            var unit = _units.ElementAt(i);
            if (unit.Unit.HitPoints <= 0)
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