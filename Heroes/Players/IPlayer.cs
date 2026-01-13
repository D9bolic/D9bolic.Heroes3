using Heroes.Units;
using Heroes.Units.Army;
using Heroes.Units.Heroes;

namespace Heroes.Players;

public interface IPlayer
{
    public string Name { get; }

    public IHero Hero { get; }
    
    public List<IUnit> Army { get; }

    void Activate();
    
    bool CheckLoose();
}