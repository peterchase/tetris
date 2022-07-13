namespace TetrisLib;

public sealed class TimerCountPlayEvent : IPlayEvent
{
    private readonly long mTimerCount;

    private TimerCountPlayEvent(long timerCount) => mTimerCount = timerCount;

    public static IPlayEvent For(long timerCount) => new TimerCountPlayEvent(timerCount);
    
    public Board GetNextBoard(Board board)
    {
        throw new NotImplementedException();
    }
}
