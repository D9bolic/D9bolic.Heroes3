using System.Drawing;
using System.Security.Cryptography;
using Heroes.Map;
using Heroes.Players;
using Heroes.Units.Army;

namespace Heroes.Utils;

public static class DistanceExtensions
{
    public static void GenerateRandomObstacles(this IMap map, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var emptyCells = map.Cells.Where(x => x.PlacedItem is EmptyMapItem).ToArray();
            var index = RandomNumberGenerator.GetInt32(0, emptyCells.Length - 1);
            emptyCells[index].PlacedItem = new RockMapItem();
        }
    }
    
    public static IEnumerable<ICell> GetLandscapeCells(this IMap map, ICell point, int distance)
    {
        return map.GetCellsInDistance(point, distance)
            .Where(c => c.PlacedItem is ILandscape)
            .OrderBy(x => x.Coordinates.X)
            .ThenBy(x => x.Coordinates.Y)
            .ToArray();
    }
    
    public static IEnumerable<ICell> GetLandscapeCells(this IMap map)
    {
        return map.Cells
            .Where(c => c.PlacedItem is ILandscape)
            .OrderBy(x => x.Coordinates.X)
            .ThenBy(x => x.Coordinates.Y)
            .ToArray();
    }
    
    public static IEnumerable<ICell> GetAlly(this IMap map, IPlayer currentPlayer)
    {
        return map
            .Cells
            .Where(c => c.PlacedItem is IUnit unit && unit.Player == currentPlayer)
            .OrderBy(x => x.Coordinates.X)
            .ThenBy(x => x.Coordinates.Y)
            .ToArray();
    }
    
    public static IEnumerable<ICell> GetAlly(this IMap map, ICell point, int distance, IPlayer currentPlayer)
    {
        return map.GetCellsInDistance(point, distance)
            .Where(c => c.PlacedItem is IUnit unit && unit.Player == currentPlayer)
            .OrderBy(x => x.Coordinates.X)
            .ThenBy(x => x.Coordinates.Y)
            .ToArray();
    }
    
    public static IEnumerable<ICell> GetEnemies(this IMap map, IPlayer currentPlayer)
    {
        return map.Cells
            .Where(c => c.PlacedItem is IUnit unit && unit.Player != currentPlayer)
            .OrderBy(x => x.Coordinates.X)
            .ThenBy(x => x.Coordinates.Y)
            .ToArray();
    }
    
    public static IEnumerable<ICell> GetEnemies(this IMap map, ICell point, int distance, IPlayer currentPlayer)
    {
        return map.GetCellsInDistance(point, distance)
            .Where(c => c.PlacedItem is IUnit unit && unit.Player != currentPlayer)
            .OrderBy(x => x.Coordinates.X)
            .ThenBy(x => x.Coordinates.Y)
            .ToArray();
    }

    public static IEnumerable<ICell> GetCellsInDistance(this IMap map, ICell point, int distance)
    {
        var result = new List<ICell>();
        foreach (var cell in map.Cells)
        {
            if (cell.IsInDistance(point, distance))
            {
                result.Add(cell);
            }
        }

        return result;
    }
    
    public static bool IsInDistance(this ICell cell, ICell point, int distance)
    {
        if (cell.Coordinates.X > point.Coordinates.X && cell.Coordinates.Y > point.Coordinates.Y)
        {
            return false;
        }

        if (cell.Coordinates.X > point.Coordinates.X &&
            cell.Coordinates.X <= point.Coordinates.X + distance &&
            cell.Coordinates.Y == point.Coordinates.Y)
        {
            return true;
        }

        if (cell.Coordinates.X < point.Coordinates.X &&
            cell.Coordinates.X >= point.Coordinates.X - distance &&
            cell.Coordinates.Y == point.Coordinates.Y)
        {
            return true;
        }

        if (cell.Coordinates.Y > point.Coordinates.Y &&
            cell.Coordinates.Y <= point.Coordinates.Y + distance &&
            cell.Coordinates.X == point.Coordinates.X)
        {
            return true;
        }

        if (cell.Coordinates.Y < point.Coordinates.Y &&
            cell.Coordinates.Y >= point.Coordinates.Y - distance &&
            cell.Coordinates.X == point.Coordinates.X)
        {
            return true;
        }
        
        return false;
    }
}