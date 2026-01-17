namespace Heroes.Assets;

public interface IAssetsStore
{
    IAsset GetAsset(IDrawableItem item);
}