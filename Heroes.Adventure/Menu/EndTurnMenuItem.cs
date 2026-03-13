using Heroes.Assets;
using Heroes.Menu.Interfaces;

namespace Heroes.Adventure.Menu;

public class EndTurnMenuItem : IMenuItem
{
    private readonly IMenuBreaker _breaker;

    public EndTurnMenuItem(IMenuBreaker breaker)
    {
        _breaker = breaker;
    }

    public IEnumerable<IMapItem> ExtraObjects => Array.Empty<IMapItem>();

    public bool CanRender() => true;

    public string Render() => "End turn";

    public void Select()
    {
        _breaker.ShouldMenuBreak = true;
    }
}
