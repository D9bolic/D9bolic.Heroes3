using Heroes.Assets;

namespace Heroes.Adventure.Entities;

public interface IAdventureEntity : IMapItem
{
    bool IsInteractable { get; }
}
