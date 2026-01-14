using System.Drawing;
using System.Security.Cryptography;
using Heroes.Map;
using Heroes.Units.Effects;

namespace Heroes.Units.Army;

public abstract class UnitBase : IMapItem, IUnit
{
    private readonly string _name;
    private readonly string _assetBucket;
    protected readonly UnitStateLine _unitStateLine;

    protected UnitBase(string name, Point coordinates)
    {
        _name = name;
        Coordinates = coordinates;
        LongEffects = new LongEffectsList();
    }
    
    public Point Coordinates { get; }
    protected virtual int CounterAttacks { get; set; } = 1;

    public int Wounds { get; set; }

    public UnitStateLine StateLine
    {
        get
        {
            return LongEffects
                .Aggregate(_unitStateLine, (current, effect) => effect.Mutate(current));
        }

        protected init => _unitStateLine = value;
    }

    public LongEffectsList LongEffects { get; }
    
    public int MovementLeft { get; set; }

    public int HitPoints => StateLine.HitPoints - Wounds;

    public string Name => _name;
    
    string IDrawableItem.Name => GetType().Name;
    
    public void Activate()
    {
        if(HitPoints <= 0)
        {
            return;
        }
        
        LongEffects.CheckTurn();
        Console.WriteLine($"Active creature: {_name}");
        Console.WriteLine($"Active effects");
        foreach (var effect in LongEffects)
        {
            Console.WriteLine(effect.ToString());
        }
 
        StateLine.ToConsole();
        //_cell.BackgroundColor = ConsoleColor.Green;
        //_cell.TextColor = ConsoleColor.Black;

        CounterAttacks = 1;
        MovementLeft = StateLine.Speed;
    }

    public void CounterAttack(IUnit target)
    {
        if (HitPoints <= 0 || CounterAttacks <= 0)
        {
            return;
        }

        Console.WriteLine($"{this._name} counter attack {target.Name}");
        target.Defence(this);
        CounterAttacks--;
    }

    public void Defence(IUnit attacker)
    {
        var different = attacker.StateLine.Attack - this.StateLine.Defence;
        var damage = RandomNumberGenerator.GetInt32(attacker.StateLine.DamageMin, attacker.StateLine.DamageMax);
        if (different > 0)
        {
            damage = (int)Math.Round(damage + (different * damage * 0.1));
        }
        
        Console.WriteLine($"{attacker.Name} attacking {_name} with damage {damage}");
        this.Wounds += damage;
        if (HitPoints <= 0)
        {
            Console.WriteLine( $"{_name} is dead");
        }
        else
        {
            Console.WriteLine($"{_name} {HitPoints} hit points left");
            
        }
    }

    public virtual bool CanFly()
    {
        return false;
    }
}