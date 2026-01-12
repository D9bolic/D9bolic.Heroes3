namespace Heroes.Units.Effects;

public class LongEffectsList : List<ILongEffect>
{
    public void CheckTurn()
    {
        foreach (var effect in this.Where(x => x.TurnsLeft > 0))
        {
            effect.TurnsLeft--;
            if (effect.TurnsLeft == 0)
            {
                this.Remove(effect);
            }
        }
    }
}