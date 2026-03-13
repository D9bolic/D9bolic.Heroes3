using Heroes.Menu.Interfaces;

namespace Heroes.Console.Rendering;

public class ConsoleUserInteraction : IUserInteraction
{
    public void WaitForAcknowledgment()
    {
        System.Console.ReadKey();
    }
}
