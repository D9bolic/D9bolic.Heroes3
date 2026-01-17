using Heroes.Assets;

namespace Heroes.Menu.Interfaces;

public interface IMenuItem
{
    IEnumerable<IMapItem> ExtraObjects { get; }
    
    bool CanRender();

    string Render();

    void Select();
}