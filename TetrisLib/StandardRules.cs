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

        var prevPiece = prevBoard.MovingPiece;
        var newPiece = prevPiece.MoveTo(new Point(prevPiece.Position.X, prevPiece.Position.Y + 1));

        // TODO: detect collision with fixed piece. If so, change moving piece to fixed piece and add new fixed piece
        // TODO: detect full row(s) at bottom. If so, move fixed pieces down and remove any wholly outside board.

        return prevBoard.WithMovingPiece(newPiece);
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
