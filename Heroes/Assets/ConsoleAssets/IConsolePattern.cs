using Heroes.Assets.ConsoleAssets.Assets;

namespace Heroes.Assets.ConsoleAssets;

public interface IConsolePattern
{
    IAsset Wrap(ConsoleAsset asset);
    
    IEnumerable<(string Key, IAsset Asset)> SpecificAssets { get; }
}