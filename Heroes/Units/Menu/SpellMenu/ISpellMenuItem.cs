namespace Heroes.Units.Menu.SpellMenu;

public interface ISpellMenuItem: IDisposable
{
    public string Render();
        
    public void OnSelected();
}