using Heroes.Assets.ConsoleAssets.Assets;

namespace Heroes.Assets.ConsoleAssets;

public class ConsoleAssetsStore : IAssetsStore
{
    private readonly IConsolePattern _consolePattern;
    private readonly Dictionary<string, IAsset> _inMemoryStore = new();

    public ConsoleAssetsStore(IConsolePattern consolePattern)
    {
        _consolePattern = consolePattern;
        SetupUnit("Griffin", "g");
        SetupUnit("Pikeman", "p");
        SetupUnit("Centaur", "c");
        SetupUnit("Elf", "e");

        SetupLandscape("Rock", "R");
        SetupLandscape("Empty", " ");
        foreach (var box in consolePattern.SpecificAssets)
        {
            _inMemoryStore.Add(box.Key, box.Asset);
        }
    }

    private void SetupUnit(string name, string literal)
    {
        _inMemoryStore.Add($"{name}", _consolePattern.Wrap(new ConsoleAsset(ConsoleColor.Black, ConsoleColor.White, literal)));
        _inMemoryStore.Add($"{name}:Ally", _consolePattern.Wrap(new ConsoleAsset(ConsoleColor.Green, ConsoleColor.Black, literal)));
        _inMemoryStore.Add($"{name}:Enemy", _consolePattern.Wrap(new ConsoleAsset(ConsoleColor.Red, ConsoleColor.Black, literal)));
        _inMemoryStore.Add($"{name}:Attack", _consolePattern.Wrap(new ConsoleAsset(ConsoleColor.Black, ConsoleColor.Red, literal)));
        _inMemoryStore.Add($"{name}:Selection", _consolePattern.Wrap(new ConsoleAsset(ConsoleColor.Black, ConsoleColor.Green, literal)));
    }

    private void SetupLandscape(string name, string literal)
    {
        _inMemoryStore.Add($"{name}", _consolePattern.Wrap(new ConsoleAsset(ConsoleColor.Yellow, ConsoleColor.Black, literal)));
        _inMemoryStore.Add($"{name}:Selection", _consolePattern.Wrap(new ConsoleAsset(ConsoleColor.Black, ConsoleColor.Green, literal)));
    }

    public IAsset GetAsset(IDrawableItem item)
    {
        return _inMemoryStore[item.Name];
    }
}