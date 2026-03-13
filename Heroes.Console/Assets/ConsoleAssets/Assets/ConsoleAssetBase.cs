namespace Heroes.Console.Assets.ConsoleAssets.Assets;

public abstract class ConsoleAssetBase
{
    protected void SetCursorPosition(int col, int row)
    {
        int originalBufferWidth = System.Console.BufferWidth;
        int originalBufferHeight = System.Console.BufferHeight;

        if (originalBufferWidth < col + 1)
        {
            SetWindowSize(col + 1, originalBufferHeight);
        }

        if (originalBufferHeight < row + 1)
        {
            SetWindowSize(originalBufferWidth, row + 1);
        }

        System.Console.SetCursorPosition(col, row);
    }

    protected void SetWindowSize(int width, int height)
    {
        try
        {
            System.Console.SetBufferSize(width, height);
        }
        catch (ArgumentOutOfRangeException)
        {
        }
        catch (IOException)
        {
        }
    }
}
