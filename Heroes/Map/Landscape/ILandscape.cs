using Heroes.Assets;

namespace Heroes.Map.Landscape;

public interface ILandscape : IMapItem
{
    bool CanMoveInto { get; }
}