using System.Drawing;

namespace TetrisLib;

public sealed class Shape
{
    private static int sKindSerial;

    private readonly Rectangle[] mRects;

    public Shape(params Rectangle[] rects)
    {
        Rectangle boundaryRect = rects.Aggregate((tot, cur) => cur.Union(tot));
        if (boundaryRect.Location != new Point(0, 0))
        {
            throw new ArgumentException("Rectangles must result in a top left location of (0, 0)", nameof(rects));
        }

        mRects = rects;
        Size = boundaryRect.Size;
        Kind = Interlocked.Increment(ref sKindSerial);
    }

    public Shape(IEnumerable<Rectangle> rects) : this(rects.ToArray()) { }

    public Size Size { get; }

    public int Kind { get; }

    public bool Contains(Point point) => mRects.Any(r => r.Contains(point));
}
