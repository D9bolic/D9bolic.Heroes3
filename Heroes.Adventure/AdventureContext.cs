using Heroes.Adventure.Entities;
using Heroes.Adventure.Entities.Heroes;
using Heroes.Adventure.Players;
using Heroes.Assets;
using Heroes.Map;
using Heroes.Menu.Interfaces;

namespace Heroes.Adventure;

public class AdventureContext : IMenuContext
{
    public AdventurePlayer ActivePlayer { get; set; } = null!;

    public AdventureHero? ActiveHero { get; set; }

    public IMap Map { get; set; } = null!;

    public List<IAdventureEntity> Entities { get; init; } = [];

    public IEnumerable<IMapItem> Elements =>
        Entities.Cast<IMapItem>()
            .Concat(ActivePlayer.Heroes);
}
