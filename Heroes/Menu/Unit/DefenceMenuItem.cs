using Heroes.Units.Army;
using Heroes.Units.Effects;

namespace Heroes.Menu.Unit;

public class DefenceMenuItem : IMenuItem
{
    private readonly IMenuBreaker _breaker;
    
    public IUnit Unit { get; }

    public DefenceMenuItem(IUnit unit, IMenuBreaker breaker)
    {
        _breaker = breaker;
        Unit = unit;
    }

    public bool CanRender() => true;

    public string Render()
    {
        return $"Defence unit";
    }

    public void Select()
    {
        Unit.LongEffects.Add(new Defence
        {
            TurnsLeft = 1,
        });
        _breaker.ShouldMenuBreak = true;
    }

    public void Dispose()
    {
    }
}