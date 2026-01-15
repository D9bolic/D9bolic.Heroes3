using Heroes.Map.Assets.ConsoleAssets.Assets;

namespace Heroes.Map.Assets.ConsoleAssets;

public interface IConsolePattern
{
    IAsset Wrap(ConsoleAsset asset);
    
    IEnumerable<(string Key, IAsset Asset)> SpecificAssets { get; }
}