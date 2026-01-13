using System.Reflection;
using Heroes.Units.Army;

namespace Heroes.Map;

public class ConsoleAssetStore : IConsoleAssetStore
{
    private readonly Dictionary<string, ConsoleAsset> _inMemoryStore = new();

    public ConsoleAssetStore(params Assembly[] unitAssemblies)
    {
        var units = unitAssemblies
            .SelectMany(x => x.DefinedTypes)
            .Where(x => typeof(IUnit).IsAssignableFrom(x) && x.IsDefined(typeof(AssetAttribute)))
            .Select(x =>
            {
                var attribute = x.GetCustomAttribute<AssetAttribute>();
                var item = new
                {
                    Name = attribute!.Name,
                    Literal = attribute!.ShortName,
                };

                return item;
            })
            .ToArray();

        foreach (var unit in units.DistinctBy(x => x.Name).ToArray())
        {
            SetupUnit(unit.Name, unit.Literal);
        }

        SetupLandscape("Landscape:Rock", "R");
        SetupLandscape("Landscape:Empty", "");
    }

    private void SetupUnit(string name, string literal)
    {
        _inMemoryStore.Add($"{name}", new ConsoleAsset(ConsoleColor.Black, ConsoleColor.White, literal));
        _inMemoryStore.Add($"{name}:Ally", new ConsoleAsset(ConsoleColor.Green, ConsoleColor.Black, literal));
        _inMemoryStore.Add($"{name}:Enemy", new ConsoleAsset(ConsoleColor.Red, ConsoleColor.Black, literal));
        _inMemoryStore.Add($"{name}:Attack", new ConsoleAsset(ConsoleColor.Black, ConsoleColor.Red, literal));
        _inMemoryStore.Add($"{name}:Selection", new ConsoleAsset(ConsoleColor.Black, ConsoleColor.Green, literal));
    }

    private void SetupLandscape(string name, string literal)
    {
        _inMemoryStore.Add($"{name}", new ConsoleAsset(ConsoleColor.Black, ConsoleColor.Yellow, literal));
        _inMemoryStore.Add($"{name}:Selection", new ConsoleAsset(ConsoleColor.Green, ConsoleColor.Black, literal));
    }

    public ConsoleAsset GetAsset(IMapItem item)
    {
        return _inMemoryStore[item.Name];
    }
}