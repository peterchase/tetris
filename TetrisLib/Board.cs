using System.Drawing;

namespace TetrisLib;

public sealed class Board
{
    private readonly int?[][] mFixedPieceKinds;

    public Board(Size size) : this(size, Array.Empty<Piece>()) { }

    internal Board(Size size, params Piece[] fixedPieces): this(size, GetFixedPieceKinds(size, fixedPieces)) { }

    private Board(Size size, int?[][] fixedPieceKinds) => (mFixedPieceKinds, Size) = (fixedPieceKinds, size);

    public Piece? MovingPiece { get; init; }

    public Size Size { get; }

    public int? KindAt(Point position) => (MovingPiece?.Contains(position) == true)
        ? MovingPiece.Kind
        : FixedPieceKindAt(position);

    public int? FixedPieceKindAt(Point position) => mFixedPieceKinds[position.Y][position.X];

    public Board WithMovingPiece(Piece? piece) => new(Size, mFixedPieceKinds) { MovingPiece = piece };

    public Board WithMovingPieceFixed()
    {
        if (MovingPiece is null)
        {
            return this;
        }

        var newKinds = mFixedPieceKinds.Select(a => (int?[])a.Clone()).ToArray();
        WritePieceKinds(newKinds, MovingPiece);
        return new(Size, newKinds);
    }

    public bool IsFullAcrossWidth(int y) => mFixedPieceKinds[y].All(k => k.HasValue);

    private static int?[][] GetFixedPieceKinds(Size size, Piece[] fixedPieces)
    {
        var newKinds = Enumerable.Range(0, size.Height).Select(y => new int?[size.Width]).ToArray();
        foreach (Piece piece in fixedPieces)
        {
            WritePieceKinds(newKinds, piece);
        }

        return newKinds;
    }

    private static void WritePieceKinds(int?[][] kinds, Piece piece)
    {
        for (int y = piece.Boundary.Y; y < piece.Boundary.Bottom; ++y)
        {
            int?[] row = kinds[y];
            for (int x = piece.Boundary.X; x < piece.Boundary.Right; ++x)
            {
                if (piece.Contains(new Point(x, y)))
                {
                    row[x] = piece.Kind;
                }
            }
        }
    }
}
