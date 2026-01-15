using Heroes.Map;

namespace Heroes.Menu;

public interface IMenu
{
    void Render(TurnInformation turnInformationProvider);
}