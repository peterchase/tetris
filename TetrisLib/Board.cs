using System.Drawing;

namespace TetrisLib;

public sealed class Board
{
    private readonly Piece[] mFixedPieces;

    public Board() : this(Array.Empty<Piece>()) { }

    private Board(Piece[] fixedPieces) => mFixedPieces = fixedPieces;

    public Piece? MovingPiece { get; init; }

    public IReadOnlyCollection<Piece> FixedPieces => mFixedPieces;

    public IEnumerable<Piece> AllPieces => MovingPiece.AsEnumerable().Concat(FixedPieces);

    public Piece? PieceAt(Point position) => AllPieces.FirstOrDefault(p => p.Contains(position));

    public Board WithMovingPiece(Piece? piece) => new(mFixedPieces) { MovingPiece = piece };
}
