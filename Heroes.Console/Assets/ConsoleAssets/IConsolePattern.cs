using Heroes.Console.Assets.ConsoleAssets.Assets;

namespace Heroes.Console.Assets.ConsoleAssets;

public interface IConsolePattern
{
    IAsset Wrap(ConsoleAsset asset);

    IEnumerable<(string Key, IAsset Asset)> SpecificAssets { get; }
}
