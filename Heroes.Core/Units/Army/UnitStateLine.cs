namespace Heroes.Units.Army;

public class UnitStateLine
{
    public int Initiative { get; init; }

    public int Speed { get; init; }

    public int HitPoints { get; set; }

    public int DamageMin { get; init; }

    public int DamageMax { get; init; }

    public int Attack { get; init; }

    public int Defence { get; init; }
}
