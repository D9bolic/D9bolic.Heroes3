using System.Drawing;
using System.Security.Cryptography;
using Heroes.Assets;
using Heroes.Events;
using Heroes.Map;
using Heroes.Battle.Units.Army.Attack;
using Heroes.Battle.Utils;
using Heroes.Units.Army;
using Heroes.Units.Effects;

namespace Heroes.Battle.Units.Army;

public abstract class UnitBase : IUnit
{
    private readonly string _name;
    private readonly IGameEventBus _eventBus;
    protected readonly UnitStateLine _unitStateLine;
    protected readonly IAttackPattern _attackPattern;

    protected UnitBase(string name, Point coordinates, IGameEventBus eventBus)
    {
        _name = name;
        _eventBus = eventBus;
        Coordinates = coordinates;
        LongEffects = new LongEffectsList();
    }

    public virtual bool CanFly => false;

    public Point Coordinates { get; set; }

    public virtual int CounterAttacks { get; set; } = 1;

    public int Wounds { get; set; }

    public IAttackPattern AttackPattern
    {
        get => _attackPattern;
        protected init => _attackPattern = value;
    }

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

    public string Name => _name;

    string IDrawableItem.Name => GetType().Name;

    public void Activate()
    {
        if (this.IsDead())
        {
            return;
        }

        LongEffects.CheckTurn();

        var currentStateLine = StateLine;
        _eventBus.Publish(new UnitActivated(
            _name,
            LongEffects.Select(e => e.ToString()!).ToList(),
            currentStateLine.Speed,
            currentStateLine.HitPoints,
            currentStateLine.DamageMin,
            currentStateLine.DamageMax,
            currentStateLine.Attack,
            currentStateLine.Defence));

        CounterAttacks = 1;
        MovementLeft = StateLine.Speed;
    }

    public void CounterAttack(IUnit target)
    {
        if (this.IsDead() || CounterAttacks <= 0)
        {
            return;
        }

        _eventBus.Publish(new CounterAttackStarted(_name, target.Name));
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

        _eventBus.Publish(new DamageDealt(attacker.Name, _name, damage));
        this.Wounds += damage;
        if (this.IsDead())
        {
            _eventBus.Publish(new UnitDied(_name));
        }
        else
        {
            _eventBus.Publish(new UnitHitPointsRemaining(_name, this.HitPoints()));
        }
    }
}
