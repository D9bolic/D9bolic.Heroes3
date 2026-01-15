namespace Heroes.Map.Assets.Boxes;

public class EnemyUnitBox(IMapItem item) : WrapperBase(item)
{
    protected override string GetName(IMapItem item)
    {
        return $"{item.Name}:Enemy";
    }
}