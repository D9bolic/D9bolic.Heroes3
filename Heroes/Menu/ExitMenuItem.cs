using Heroes.Assets;
using Heroes.Map;
using Heroes.Menu.Interfaces;

namespace Heroes.Menu;

public class ExitMenuItem : IMenuItem
{
    private readonly IMenuBreaker _breaker;

    public ExitMenuItem(IMenuBreaker breaker)
    {
        _breaker = breaker;
    }

    public void Dispose()
    {
    }

    public IEnumerable<IMapItem> ExtraObjects => Array.Empty<IMapItem>();

    public bool CanRender() => true;

    public string Render()
    {
        return $"Exit";
    }

    public void Select()
    {
        _breaker.ShouldMenuBreak = true;
    }
}