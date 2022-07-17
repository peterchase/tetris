using System.Drawing;

namespace TetrisLib;

public sealed class Shape
{
    private static int sKindSerial;

    private readonly Rectangle[] mRects;

    private Shape(Rectangle[] rects, int kind)
    {
        Rectangle boundaryRect = rects.Aggregate((tot, cur) => cur.Union(tot));
        mRects = rects;
        Size = boundaryRect.Size;
        Kind = kind;
    }

    public Shape(params Rectangle[] rects) : this(rects, Interlocked.Increment(ref sKindSerial)) { }

    public Shape(IEnumerable<Rectangle> rects) : this(rects.ToArray()) { }

    public Size Size { get; }

    public int Kind { get; }

    public bool Contains(Point point) => mRects.Any(r => r.Contains(point));

    public Shape Rotated(int rotation) => this; // TODO: respect rotation
}
