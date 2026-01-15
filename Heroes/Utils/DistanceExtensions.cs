using System.Security.Cryptography;
using Heroes.Map;
using Heroes.Map.Assets;
using Heroes.Map.Landscape;
using Heroes.Units.Army;

namespace Heroes.Utils;

public static class DistanceExtensions
{
    public static IEnumerable<ILandscape> GenerateRandomObstacles(this IMap map, int count, IEnumerable<IMapItem> items)
    {
        var obstacles =  new List<ILandscape>();
        for (int i = 0; i < count; i++)
        {
            var emptyCells = map
                .Cells
                .Where(x => !items.Union(obstacles).Any(e => e.Coordinates.Equals(x.Coordinates)))
                .ToArray();
            var index = RandomNumberGenerator.GetInt32(0, emptyCells.Length - 1);
            var rock = new Rock(emptyCells[index].Coordinates);
            obstacles.Add(rock);
        }

        return obstacles;
    }

    public static bool IsDead(this IUnit unit)
    {
        return unit.StateLine.HitPoints - unit.Wounds <= 0;
    }
    
    public static int HitPoints(this IUnit unit)
    {
        return unit.StateLine.HitPoints - unit.Wounds;
    }
}