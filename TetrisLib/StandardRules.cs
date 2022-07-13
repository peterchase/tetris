namespace TetrisLib;

public sealed class StandardRules : IPlayEventVisitor<Board, (Board, Game)>
{
    public Board VisitPlayerMove(PlayerMovePlayEvent playEvent, (Board, Game) arg)
    {
        var (prevBoard, game) = arg;
        throw new NotImplementedException();
    }

    public Board VisitTimerCount(TimerCountPlayEvent playEvent, (Board, Game) arg)
    {
        var (prevBoard, game) = arg;
        throw new NotImplementedException();
    }
}
