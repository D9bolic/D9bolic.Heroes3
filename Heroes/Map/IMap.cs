using System.Drawing;
using Heroes.Assets;
using Heroes.Map.Landscape;

namespace Heroes.Map;

public interface IMap
{
    IEnumerable<ILandscape> Cells { get; }
    
    void InitializeLandscape(IEnumerable<ILandscape> landscapes);

    void Draw(IEnumerable<IMapItem> mapItems);

    public IEnumerable<IMapItem> GetClosePoints(Point point);
    
    public string GetDirection(Point from, Point to);
}