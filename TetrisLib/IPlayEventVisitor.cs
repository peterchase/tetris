namespace TetrisLib;

public interface IPlayEventVisitor<TR, TA1, TA2>
{
    TR VisitTimerCount(TimerCountPlayEvent playEvent, TA1 arg1, TA2 arg2);

    TR VisitPlayerMove(PlayerMovePlayEvent playEvent, TA1 arg1, TA2 arg2);

    TR VisitDrop(DropPlayEvent playEvent, TA1 arg1, TA2 arg2);
}
