using System.Reactive.Linq;

namespace TetrisLib;

public sealed class Game
{
    public Game(IObservable<long> timerCounts, IObservable<IPlayEvent> playerMoves, Board initialBoard, IRules rules)
    {
        Boards = timerCounts
            .Select(TimerCountPlayEvent.For)
            .Merge(playerMoves)
            .Scan(initialBoard, (prevBoard, playEvent) => playEvent.Accept(rules, prevBoard, this))
            .StartWith(initialBoard)
            .TakeUntil(board => rules.Finished(board, this));
    }

    public IObservable<Board> Boards { get; }
}
