using System.Drawing;

namespace TetrisLib;

public sealed class Piece
{
    public Piece(Shape shape, Point? position = null)
    {
        Shape = shape;
        Position = position ?? new Point(0, 0);
    }

    public Point Position { get; }

    public Shape Shape { get; }

    public int Rotation { get; private set; }

    public int Kind => Shape.Kind;

    public bool Contains(Point point)
    {
        // TODO: respect rotation
        point.Offset(-Position.X, -Position.Y);
        return Shape.Contains(point);
    }

    public int RotateClockwise() => Rotation = (Rotation + 1) % 4;

    public int RotateCounterClockwise() => Rotation = Rotation == 0 ? 3 : Rotation - 1;
}
