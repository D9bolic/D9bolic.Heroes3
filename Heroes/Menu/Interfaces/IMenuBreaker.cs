namespace Heroes.Menu.Interfaces;

public interface IMenuBreaker
{
    bool ShouldMenuBreak { get; set; }
    
    bool AnyActionInvoked { get; set; }
}