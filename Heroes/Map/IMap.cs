namespace Heroes.Map;

public interface IMap
{
    IEnumerable<ICell> Cells { get; }

    void Draw(IEnumerable<IMapItem> mapItems);
}