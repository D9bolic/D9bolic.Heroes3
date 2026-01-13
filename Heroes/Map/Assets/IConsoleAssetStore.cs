namespace Heroes.Map;

public interface IConsoleAssetStore
{
    ConsoleAsset GetAsset(IMapItem item);
}