using Heroes.Map;

namespace Heroes.Menu;

public interface IMenuItem
{
    IEnumerable<IMapItem> ExtraObjects { get; }
    
    bool CanRender();

    string Render();

    void Select();
}