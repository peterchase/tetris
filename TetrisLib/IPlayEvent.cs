namespace TetrisLib;

public interface IPlayEvent
{
    TR Accept<TR, TA>(IPlayEventVisitor<TR, TA> visitor, TA arg);
}

public interface IPlayEventVisitor<TR, TA>
{
    TR VisitTimerCount(TimerCountPlayEvent playEvent, TA arg);

    TR VisitPlayerMove(PlayerMovePlayEvent playEvent, TA arg);
}
