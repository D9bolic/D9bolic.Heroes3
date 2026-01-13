using Heroes.Units;
using Heroes.Units.Army;
using Heroes.Units.Heroes;

namespace Heroes.Players;

public class Player : IPlayer
{
    public string Name { get; init; }

    public IHero Hero { get; init; }
    
    public List<IUnit> Army { get; } = new List<IUnit>();
    
    public void Activate()
    {
        Console.WriteLine($"Player {Name} turn");
    }
    
    public bool CheckLoose()
    {
        return !Army.Any();
    }
}