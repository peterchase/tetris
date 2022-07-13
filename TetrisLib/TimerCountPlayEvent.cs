namespace TetrisLib;

public sealed class TimerCountPlayEvent : IPlayEvent
{
    private TimerCountPlayEvent(long timerCount) => TimerCount = timerCount;

    public long TimerCount { get; }

    public static IPlayEvent For(long timerCount) => new TimerCountPlayEvent(timerCount);

    TR IPlayEvent.Accept<TR, TA>(IPlayEventVisitor<TR, TA> visitor, TA arg) => visitor.VisitTimerCount(this, arg);
}
