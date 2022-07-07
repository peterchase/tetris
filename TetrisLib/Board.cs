using System.Drawing;

namespace TetrisLib;

public sealed class Board
{
    private readonly List<Piece> mFixedPieces = new();

    public Piece? MovingPiece { get; set; }

    public IReadOnlyCollection<Piece> FixedPieces => mFixedPieces;

    public IEnumerable<Piece> AllPieces => MovingPieceSeq.Concat(FixedPieces);

    public Piece? PieceAt(Point position) => AllPieces.FirstOrDefault(p => p.Contains(position));

    private IEnumerable<Piece> MovingPieceSeq => MovingPiece is null
     ? Enumerable.Empty<Piece>()
     : Enumerable.Repeat(MovingPiece, 1);
}
