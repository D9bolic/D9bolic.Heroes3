using System.Drawing;
using Heroes.Map.Assets;

namespace Heroes.Map;

public interface IMap
{
    IEnumerable<IMapItem> Cells { get; }

    void Draw(IEnumerable<IMapItem> mapItems);

    public IEnumerable<IMapItem> GetCellsInDistance(Point point, int distance);
}