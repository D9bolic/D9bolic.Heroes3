namespace Heroes.Map;

public interface IAssetManager
{
   void DrawMap(IEnumerable<IMapItem> mapItems);
}