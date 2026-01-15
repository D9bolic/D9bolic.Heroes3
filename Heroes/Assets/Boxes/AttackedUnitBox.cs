namespace Heroes.Map.Assets.Boxes;

public class AttackedUnitBox(IMapItem item) : WrapperBase(item)
{
    protected override string GetName(IMapItem item)
    {
        return $"{item.Name}:Attack";
    }
}