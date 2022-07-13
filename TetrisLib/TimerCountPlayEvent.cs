namespace TetrisLib;

public sealed class TimerCountPlayEvent : IPlayEvent
{
    private TimerCountPlayEvent(long timerCount) => TimerCount = timerCount;

    public long TimerCount { get; }

    public static IPlayEvent For(long timerCount) => new TimerCountPlayEvent(timerCount);

    TR IPlayEvent.Accept<TR, TA1, TA2>(IPlayEventVisitor<TR, TA1, TA2> visitor, TA1 arg1, TA2 arg2) => visitor.VisitTimerCount(this, arg1, arg2);
}
