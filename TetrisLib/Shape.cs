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

    public Shape Rotated(int rotation)
    {
        if (rotation < 0)
        {
            throw new ArgumentException("Negative rotation not supported", nameof(rotation));
        }

        switch (rotation % 4)
        {
            case 0:
                return this;
            case 1:
                throw new NotImplementedException();
            case 2:
                throw new NotImplementedException();
            case 3:
                throw new NotImplementedException();
            default:
                throw new InvalidOperationException("Should be impossible");
        }
    }
}
