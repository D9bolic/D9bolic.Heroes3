namespace Heroes.Map.Assets.Boxes;

public class SelectionBox(IMapItem item) : WrapperBase(item)
{
    protected override string GetName(IMapItem item)
    {
        return $"{item.Name}:Selection";
    }
}