using System.Drawing;

namespace TetrisLib;

public static class StandardShapes
{
    public static readonly Shape L42 = new(new Rectangle(0, 0, 4, 1), new Rectangle(0, 0, 1, 2));
    public static readonly Shape Offset32 = new(new Rectangle(0, 0, 3, 1), new Rectangle(1, 1, 3, 1));
    public static readonly Shape Square33 = new(new Rectangle(0, 0, 3, 3));
    public static readonly Shape U23 = new(new Rectangle(0, 0, 2, 1), new Rectangle(0, 1, 1, 1), new Rectangle(0, 2, 2, 1));

    public static IEnumerable<Shape> All
    {
        get
        {
            yield return L42;
            yield return Offset32;
            yield return Square33;
            yield return U23;
        }
    }
}
