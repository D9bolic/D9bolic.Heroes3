using Heroes.Menu.Interfaces;

namespace Heroes.Menu;

public class MenuBreaker : IMenuBreaker
{
    public bool ShouldMenuBreak { get; set; }

    public bool AnyActionInvoked { get; set; }
}
