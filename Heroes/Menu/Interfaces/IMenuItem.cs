namespace Heroes.Menu;

public interface IMenuItem
{
    bool CanRender();

    string Render();

    void Select();
}