namespace Heroes.Map;

public interface IAssetsStore
{
    IAsset GetAsset(IDrawableItem item);
}