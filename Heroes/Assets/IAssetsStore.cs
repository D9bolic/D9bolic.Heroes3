namespace Heroes.Map.Assets;

public interface IAssetsStore
{
    IAsset GetAsset(IDrawableItem item);
}