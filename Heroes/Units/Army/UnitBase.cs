using System.Security.Cryptography;
using Heroes.Map;
using Heroes.Players;
using Heroes.Units.Effects;
using Heroes.Utils;

namespace Heroes.Units.Army;

public abstract class UnitBase : MovableMapItemBase, IUnit, IDisposable
{
    private readonly UnitStateLine _unitStateLine;
    
    protected UnitBase(IPlayer player, string literal, string name)
        : base(literal, name)
    {
        Player = player;
        player.Army.Add(this);
        LongEffects = new LongEffectsList();
    }

    protected virtual int CounterAttacks { get; set; } = 1;

    public int Wounds { get; set; }

    public IMap Map { get; set; }

    public IPlayer Player { get; }


    public override ICell? Cell
    {
        get => _cell;
        set
        {
            if (_cell is not null)
            {
                _cell.PlacedItem = null;
            }

            _cell = value;
            
            if (value is not null)
            {
                value.PlacedItem = this;
            }
        }
    }

    public UnitStateLine StateLine
    {
        get
        {
            var mutated = LongEffects
                .Aggregate(_unitStateLine, (current, effect) => effect.Mutate(current));

            return Player.Hero.Abilities
                .Aggregate(mutated, (current, effect) => effect.Mutate(current));
        }

        protected init => _unitStateLine = value;
    }

    public LongEffectsList LongEffects { get; }
    
    public int MovementLeft { get; set; }

    public int HitPoints => StateLine.HitPoints - Wounds;

    public void Activate()
    {
        if(_cell is null || HitPoints <= 0)
        {
            return;
        }
        
        LongEffects.CheckTurn();
        Console.WriteLine($"Active creature: {Name}");
        Console.WriteLine($"Active effects");
        foreach (var effect in LongEffects)
        {
            Console.WriteLine(effect.ToString());
        }
 
        StateLine.ToConsole();
        _cell.BackgroundColor = ConsoleColor.Green;
        _cell.TextColor = ConsoleColor.Black;

        CounterAttacks = 1;
        MovementLeft = StateLine.Speed;
    }
    
    public void Deactivate()
    {
        if(_cell is null || HitPoints <= 0)
        {
            return;
        }

        _cell.BackgroundColor = ConsoleColor.Black;
    }

    public void MarkAsAlly()
    {
        if(_cell is null || HitPoints <= 0)
        {
            return;
        }

        _cell.TextColor = ConsoleColor.Green;
        _cell.BackgroundColor = ConsoleColor.Black;
    }

    public void MarkAsEnemy()
    {
        if(_cell is null || HitPoints <= 0)
        {
            return;
        }

        _cell.TextColor = ConsoleColor.Red;
        _cell.BackgroundColor = ConsoleColor.Black;
    }

    public void CounterAttack(IUnit target)
    {
        if (HitPoints <= 0 || CounterAttacks <= 0 || !Cell.IsInDistance(target.Cell, StateLine.AttackRange))
        {
            return;
        }

        Console.WriteLine($"{this.Name} counter attack {target.Name}");
        target.Defence(this);
        CounterAttacks--;
    }

    public void Defence(IUnit attacker)
    {
        if (!attacker.Cell.IsInDistance(Cell, attacker.StateLine.AttackRange))
        {
            return;
        }
        
        var different = attacker.StateLine.Attack - this.StateLine.Defence;
        var damage = RandomNumberGenerator.GetInt32(attacker.StateLine.DamageMin, attacker.StateLine.DamageMax);
        if (different > 0)
        {
            damage = (int)Math.Round(damage + (different * damage * 0.1));
        }
        
        Console.WriteLine($"{attacker.Name} attacking {this.Name} with damage {damage}");
        this.Wounds += damage;
        if (HitPoints <= 0)
        {
            Console.WriteLine( $"{Name} is dead");
            Dispose();
        }
        else
        {
            Console.WriteLine($"{Name} {HitPoints} hit points left");
            
        }
    }

    public virtual bool CanMove(ICell cell)
    {
        return cell.PlacedItem is ILandscape landscape && landscape.IsPointToMove;
    }

    public void Dispose()
    {
        Player.Army.Remove(this);
        Cell = null;
    }
}