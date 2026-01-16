using System.Drawing;
using Heroes.Assets;

namespace Heroes.Map;

public interface IMap
{
    IEnumerable<IMapItem> Cells { get; }

    void Draw(IEnumerable<IMapItem> mapItems);

    public IEnumerable<IMapItem> GetClosePoints(Point point);
}