namespace TetrisLib;

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

        return prevBoard; // TODO
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
