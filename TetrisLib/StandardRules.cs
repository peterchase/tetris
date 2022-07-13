namespace TetrisLib;

using System.Drawing;

public sealed class StandardRules : IRules
{
    private readonly Shape[] mAllShapes;
    private readonly Random mRandom = new();

    public StandardRules(params Shape[] allShapes) => mAllShapes = allShapes;

    public StandardRules(IEnumerable<Shape> allShapes) : this(allShapes.ToArray()) { }

    public Board VisitPlayerMove(PlayerMovePlayEvent playEvent, Board prevBoard, Game game)
    {
        if (prevBoard.MovingPiece is null)
        {
            return prevBoard;
        }

        // TODO: support rotation
        var prevPiece = prevBoard.MovingPiece;
        var newPiece = prevPiece.MoveTo(new Point(prevPiece.Position.X + playEvent.Movement.Right, prevPiece.Position.Y));
        newPiece = newPiece.ContainedBy(prevBoard.Size) ? newPiece : prevPiece;
        return prevBoard.WithMovingPiece(newPiece);
    }

    public Board VisitTimerCount(TimerCountPlayEvent playEvent, Board prevBoard, Game game)
    {
        if (prevBoard.MovingPiece is null)
        {
            return prevBoard;
        }

        Piece prevPiece = prevBoard.MovingPiece;
        Piece movedDownPiece = prevPiece.MoveTo(new Point(prevPiece.Position.X, prevPiece.Position.Y + 1));

        if (prevBoard.FixedPieces.Any(movedDownPiece.Intersects)
            || movedDownPiece.Boundary.Bottom == prevBoard.Size.Height - 1)
        {
            Piece newMovingPiece = CreateNewMovingPiece(prevBoard);
            return prevBoard.WithMovingPieceFixed().WithMovingPiece(newMovingPiece);
        }

        // TODO: detect full row(s) at bottom. If so, move fixed pieces down and remove any wholly outside board.

        return prevBoard.WithMovingPiece(movedDownPiece);
    }

    private Piece CreateNewMovingPiece(Board board)
    {
        Shape shape = mAllShapes[mRandom.Next(mAllShapes.Length)];
        int xRange = board.Size.Width - shape.Size.Width;
        int x = mRandom.Next(xRange);
        return new Piece(shape, new Point(x, 0));
    }

    public bool Finished(Board board, Game game)
    {
        if (board.MovingPiece is null)
        {
            return false;
        }

        // TODO: detect collision with fixed piece when moving piece is at top
        return false;
    }
}
