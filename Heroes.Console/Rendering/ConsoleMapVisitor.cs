using Heroes.Assets;
using Heroes.Console.Assets;
using Heroes.Console.Assets.ConsoleAssets;
using Heroes.Console.Assets.ConsoleAssets.Assets;
using Heroes.Map;

namespace Heroes.Console.Rendering;

public class ConsoleMapVisitor : IMapVisitor
{
    private readonly IAssetsStore _assetsStore;
    private readonly IAsset? _newLineAsset;
    private readonly IAsset? _shiftAsset;

    public ConsoleMapVisitor(IAssetsStore assetsStore)
    {
        _assetsStore = assetsStore;

        var newLineItem = new SentinelItem("New Line");
        try { _newLineAsset = assetsStore.GetAsset(newLineItem); } catch { }

        var shiftItem = new SentinelItem("Shift");
        try { _shiftAsset = assetsStore.GetAsset(shiftItem); } catch { }
    }

    public void VisitCell(IDrawableItem item)
    {
        _assetsStore.GetAsset(item).Draw();
    }

    public void VisitNewLine()
    {
        _newLineAsset?.Draw();
    }

    public void VisitShift()
    {
        _shiftAsset?.Draw();
    }

    private class SentinelItem : IDrawableItem
    {
        public SentinelItem(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
