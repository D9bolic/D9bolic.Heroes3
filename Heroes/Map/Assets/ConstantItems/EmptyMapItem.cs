using System.Drawing;

namespace Heroes.Map;

public class EmptyMapItem : IDrawableItem
{
    public string Name => "Empty";
}

public class NewLineItem : IDrawableItem
{
    public string Name => "New Line";
}