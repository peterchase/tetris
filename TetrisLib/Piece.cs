using System.Drawing;

namespace TetrisLib;

public sealed class Piece
{
    public Piece(Shape shape, Point? position = null)
     : this(shape, position ?? new Point(0, 0), 0) { }

    private Piece(Shape shape, Point position, int rotation)
    {
        UnrotatedShape = shape;
        Position = position;
        Rotation = rotation;
    }

    public Point Position { get; }

    public Shape UnrotatedShape { get; }

    public Shape Shape => UnrotatedShape.Rotated(Rotation);

    public int Rotation { get; }

    public int Kind => UnrotatedShape.Kind;

    public Rectangle Boundary => new(Position, Shape.Size);

    public bool Contains(Point point)
    {
        point.Offset(-Position.X, -Position.Y);
        return Shape.Contains(point);
    }

    public bool ContainedBy(Size size)
    {
        var fitWithin = new Rectangle(new Point(0, 0), size);
        return fitWithin.Contains(Boundary);
    }

    public IEnumerable<Point> Points
    {
        get
        {
            Shape rotatedShape = Shape; // don't repeatedly calculate this

            for (int y = 0; y < rotatedShape.Size.Height; ++y)
            {
                for (int x = 0; x < rotatedShape.Size.Width; ++x)
                {
                    Point point = new Point(x, y);
                    if (rotatedShape.Contains(point))
                    {
                        point.Offset(Position);
                        yield return point;
                    }
                }
            }
        }
    }

    public Piece MoveTo(Point position) => new(UnrotatedShape, position, Rotation);

    public Piece RotateClockwise(int rotation)
    {
        if (rotation < -4)
        {
            throw new ArgumentException($"Unsupported rotation {rotation}", nameof(rotation));
        }

        return new(UnrotatedShape, Position, (Rotation + rotation + 4) % 4);
    }
}
