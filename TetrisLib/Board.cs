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

    public Board WithMovingPieceFixed() => new(Size, mFixedPieces.Concat(MovingPiece.AsEnumerable()).ToArray());

    public bool IsFullAcrossWidth(int y)
    {
        return Enumerable.Range(0, Size.Width)
            .Select(x => new Point(x, y))
            .All(point => FixedPieces.Any(piece => piece.Contains(point)));
    }

    public Board MoveFixedPiecesDown()
    {
        var newFixedPieces = FixedPieces
            .Select(p => p.MoveTo(new Point(p.Position.X, p.Position.Y + 1)))
            .Where(p => p.Position.Y < Size.Height)
            .ToArray();
        return new Board(Size, newFixedPieces) { MovingPiece = MovingPiece };
    }
}
