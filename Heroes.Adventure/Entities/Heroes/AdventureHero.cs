using System.Drawing;
using Heroes.Assets;
using Heroes.Units.Heroes;

namespace Heroes.Adventure.Entities.Heroes;

public class AdventureHero : IAdventureEntity
{
    private readonly IHero _hero;

    public AdventureHero(IHero hero, Point coordinates, int movementPoints)
    {
        _hero = hero;
        Coordinates = coordinates;
        MaxMovementPoints = movementPoints;
        MovementPointsLeft = movementPoints;
    }

    public IHero Hero => _hero;

    public string Name => _hero.Name;

    string IDrawableItem.Name => "AdventureHero";

    public Point Coordinates { get; set; }

    public int MaxMovementPoints { get; }

    public int MovementPointsLeft { get; set; }

    public bool IsInteractable => true;

    public void ResetMovement()
    {
        MovementPointsLeft = MaxMovementPoints;
    }
}
