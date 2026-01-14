using Heroes.Map;

namespace Heroes.Menu;

public interface IMenu
{
    void Render(Func<TurnInformation> turnInformationProvider);
}