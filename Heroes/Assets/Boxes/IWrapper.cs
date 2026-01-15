namespace Heroes.Map.Assets.Boxes;

public interface IWrapper : IMapItem
{
    IMapItem Item { get; }

    IMapItem Unwrap();
}