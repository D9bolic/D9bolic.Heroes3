namespace Heroes.Units.Menu.UnitMenu;

public interface IUnitMenuItem: IDisposable
{
    public string Render();
        
    public void OnSelected();
}