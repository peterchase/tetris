using System.Reactive.Linq;

namespace TetrisLib;

public sealed class Game
{
    public Game(IObservable<long> timerSteps, IObservable<Movement> playerMoves)
    {
        TimerSteps = timerSteps;
        PlayerMoves = playerMoves;
    }

    public IObservable<long> TimerSteps { get; }

    public IObservable<Movement> PlayerMoves { get; }

    public IObservable<Board> Boards => null; // TODO

    public TimeSpan TimedMovePeriod { get; }
}