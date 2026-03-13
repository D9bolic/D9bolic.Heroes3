using Heroes.Assets;

namespace Heroes.Console.Assets;

public interface IAssetsStore
{
    IAsset GetAsset(IDrawableItem item);
}
