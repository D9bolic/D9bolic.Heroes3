namespace Heroes.Units.Army;

public class UnitStateLine
{
    public UnitStateLine()
    {
        AttackRange = 1;
    }

    public int Initiative { get; init; }

    public int AttackRange { get; init; }
    
    public int Speed { get; init; }
    
    public int HitPoints { get; set; }
    
    public int DamageMin { get; init; }
    
    public int DamageMax { get; init; }
    
    public int Attack { get; init; }
    
    public int Defence { get; init; }

    public void ToConsole()
    {
        Console.WriteLine($"Speed: {Speed}");
        Console.WriteLine($"Health: {HitPoints}");
        Console.WriteLine($"Attack range: {AttackRange}");
        Console.WriteLine($"Hit points: {HitPoints}");
        Console.WriteLine($"Damage: {DamageMin}-{DamageMax}");
        Console.WriteLine($"Attack: {Attack}");
        Console.WriteLine($"Defence: {Defence}");
    }
}