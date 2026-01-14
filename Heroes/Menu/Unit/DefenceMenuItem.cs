using Heroes.Units.Army;
using Heroes.Units.Effects;

namespace Heroes.Menu.Unit;

public class DefenceMenuItem : IMenuItem
{
    private readonly IMenuBreaker _breaker;
    
    public TurnInformation Turn { get; }

    public DefenceMenuItem(TurnInformation turn, IMenuBreaker breaker)
    {
        _breaker = breaker;
        Turn = turn;
    }

    public bool CanRender() => true;

    public string Render()
    {
        return $"Defence unit";
    }

    public void Select()
    {
        Turn.ActiveUnit.LongEffects.Add(new Defence
        {
            TurnsLeft = 1,
        });
        _breaker.ShouldMenuBreak = true;
    }

    public void Dispose()
    {
    }
}