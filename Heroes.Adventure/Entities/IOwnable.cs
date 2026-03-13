using Heroes.Adventure.Players;

namespace Heroes.Adventure.Entities;

public interface IOwnable
{
    AdventurePlayer? Owner { get; set; }
}
