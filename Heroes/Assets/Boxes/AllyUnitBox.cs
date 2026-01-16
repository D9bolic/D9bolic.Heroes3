namespace Heroes.Assets.Boxes;

public class AllyUnitBox(IMapItem item) : WrapperBase(item)
{
    protected override string GetName(IMapItem item)
    {
        return $"{item.Name}:Ally";
    }
}