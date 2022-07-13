namespace TetrisLib;

using System.Drawing;

public sealed class StandardRules : IPlayEventVisitor<Board, (Board, Game)>
{
    public static IPlayEventVisitor<Board, (Board, Game)> Instance { get; } = new StandardRules();

    private StandardRules() { }

    public Board VisitPlayerMove(PlayerMovePlayEvent playEvent, (Board, Game) arg)
    {
        var (prevBoard, _) = arg;
        if (prevBoard.MovingPiece is null)
        {
            return prevBoard;
        }

        // TODO: checks and other kinds of move, also nicer way to apply movement
        var prevPiece = prevBoard.MovingPiece;
        var newPiece = new Piece(prevPiece.Shape, new Point(prevPiece.Position.X + playEvent.Movement.Right, prevPiece.Position.Y + playEvent.Movement.Down));
        return prevBoard.WithMovingPiece(newPiece);
    }

    public Board VisitTimerCount(TimerCountPlayEvent playEvent, (Board, Game) arg)
    {
        var (prevBoard, _) = arg;
        if (prevBoard.MovingPiece is null)
        {
            return prevBoard;
        }

        return prevBoard; // TODO
    }
}
