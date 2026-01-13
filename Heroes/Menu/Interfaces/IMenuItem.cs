namespace Heroes.Menu;

public interface IMenuItem : IDisposable
{
    bool CanRender();

    string Render();

    void Select();
}