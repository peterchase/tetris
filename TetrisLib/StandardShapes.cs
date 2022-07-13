using System.Drawing;

namespace TetrisLib;

public static class StandardShapes
{
    public static readonly Shape L42 = new(new Rectangle(0, 0, 4, 1), new Rectangle(0, 0, 1, 2));

    public static IEnumerable<Shape> All
    {
        get
        {
            yield return L42;
        }
    }
}
