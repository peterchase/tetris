using System.Drawing;

namespace TetrisLib;

public sealed class Board
{
    private readonly Piece[] mFixedPieces;

    public Board(Size size) : this(size, Array.Empty<Piece>()) { }

    private Board(Size size, Piece[] fixedPieces) => (mFixedPieces, Size) = (fixedPieces, size);

    public Piece? MovingPiece { get; init; }

    public Size Size { get; }

    public IReadOnlyCollection<Piece> FixedPieces => mFixedPieces;

    public IEnumerable<Piece> AllPieces => MovingPiece.AsEnumerable().Concat(FixedPieces);

    public Piece? PieceAt(Point position) => AllPieces.FirstOrDefault(p => p.Contains(position));

    public Board WithMovingPiece(Piece? piece) => new(Size, mFixedPieces) { MovingPiece = piece };
}
