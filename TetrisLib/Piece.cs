using System.Drawing;

namespace TetrisLib;

public sealed class Piece
{
    public Piece(Shape shape, Point? position = null)
     : this(shape, position ?? new Point(0, 0), 0) { }

    private Piece(Shape shape, Point position, int rotation)
    {
        Shape = shape;
        Position = position;
        Rotation = rotation;
    }

    public Point Position { get; }

    public Shape Shape { get; }

    public int Rotation { get; }

    public int Kind => Shape.Kind;

    public Rectangle Boundary => new(Position, Shape.Size);

    public bool Contains(Point point)
    {
        // TODO: respect rotation
        point.Offset(-Position.X, -Position.Y);
        return Shape.Contains(point);
    }

    public bool ContainedBy(Size size)
    {
        var fitWithin = new Rectangle(new Point(0, 0), size);

        // TODO: respect rotation
        return fitWithin.Contains(Boundary);
    }

    public bool Intersects(Piece other)
    {
        int xOffset = Position.X - other.Position.X;
        int yOffset = Position.Y - other.Position.Y;
        return Shape.Intersects(other.Shape.Offset(xOffset, yOffset));
    }

    public Piece MoveTo(Point position) => new(Shape, position, Rotation);

    public Piece RotateClockwise() => new(Shape, Position, (Rotation + 1) % 4);

    public Piece RotateCounterClockwise() => new (Shape, Position, Rotation == 0 ? 3 : Rotation - 1);
}
