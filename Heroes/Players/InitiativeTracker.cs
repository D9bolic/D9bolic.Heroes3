using System.Collections;
using Heroes.Map;
using Heroes.Menu;
using Heroes.Units.Army;

namespace Heroes.Players;

public class InitiativeTracker : IInitiativeTracker
{
    private readonly IPlayer _player1;
    private readonly IPlayer _player2;
    private readonly IMap _map;
    private readonly IEnumerable<IMapItem> _obstacles;
    private readonly IEnumerable<CurrentTurn> _units;

    public InitiativeTracker(IPlayer player1, IPlayer player2, IMap map, IEnumerable<IMapItem> obstacles)
    {
        _player1 = player1;
        _player2 = player2;
        _map = map;
        _obstacles = obstacles;
        _units = _player1.Army.Select(u => new CurrentTurn(u, _player1))
            .Concat(_player2.Army.Select(u => new CurrentTurn(u, _player1)))
            .OrderBy(x => x.Unit.StateLine.Initiative).ToArray();
    }

    public IEnumerator<TurnInformation> GetEnumerator()
    {
        for (var i = 0; i < _units.Count(); i++)
        {
            var turn = _units.ElementAt(i);
            if (turn.Unit.HitPoints <= 0)
            {
                continue;
            }

            var enemyPlayer = _player1 == turn.Player ? _player2 : _player1;
            yield return new TurnInformation
            {
                Player = turn.Player,
                ActiveUnit = turn.Unit,
                Map = _map,
                Allies = turn.Player.Army,
                Enemies = enemyPlayer.Army,
                Obstacles = _obstacles,
            };
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

    private record CurrentTurn(IUnit Unit, IPlayer Player);
}