namespace TetrisLib;

using System.Drawing;

public sealed class StandardRules : IRules
{
    public static IRules Instance { get; } = new StandardRules();

    private StandardRules() { }

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
            Piece newMovingPiece = CreateNewMovingPiece();
            return prevBoard.WithMovingPieceFixed().WithMovingPiece(newMovingPiece);
        }

        // TODO: detect full row(s) at bottom. If so, move fixed pieces down and remove any wholly outside board.

        return prevBoard.WithMovingPiece(movedDownPiece);
    }

    private static Piece CreateNewMovingPiece()
    {
        // TODO: randomise x-position and shape
        return new Piece(StandardShapes.L42, new Point(0, 0));
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
