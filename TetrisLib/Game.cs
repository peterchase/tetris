using System.Reactive.Linq;

namespace TetrisLib;

public sealed class Game
{
    public Game(IObservable<long> timerCounts, IObservable<Movement> playerMoves, Board initialBoard, IRules rules)
    {
        TimerSteps = timerCounts;
        PlayerMoves = playerMoves;

        Boards = timerCounts
            .Select(TimerCountPlayEvent.For)
            .Merge(playerMoves.Select(pm => PlayerMovePlayEvent.For(pm)))
            .Scan(initialBoard, (prevBoard, playEvent) => playEvent.Accept(rules, prevBoard, this))
            .StartWith(initialBoard)
            .TakeUntil(board => rules.Finished(board, this));
    }

    public IObservable<long> TimerSteps { get; }

    public IObservable<Movement> PlayerMoves { get; }

    public IObservable<Board> Boards { get; }

    public TimeSpan TimedMovePeriod { get; }
}
