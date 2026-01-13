using System.Collections;
using Alba.CsConsoleFormat;
using Heroes.Units;
using Heroes.Units.Army;
using Point = System.Drawing.Point;

namespace Heroes.Map;

public interface IMap
{
    IEnumerable<ICell> Cells { get; }
}