using Heroes.Assets;

namespace Heroes.Map;

public interface IMapVisitor
{
    void VisitCell(IDrawableItem item);

    void VisitNewLine();

    void VisitShift();
}
