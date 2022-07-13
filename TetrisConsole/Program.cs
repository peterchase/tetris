
using System.Drawing;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using TetrisLib;

internal class Program
{
    private static async Task Main(string[] args)
    {
        ConsoleKeyMonitor.Start();

        Console.Clear();

        using var finished = new Subject<Unit>();
        var playerMoves = ConsoleKeyMonitor.Movements.Finally(() => finished.OnNext(Unit.Default));

        var timerCounts = Observable.Interval(TimeSpan.FromSeconds(1)).TakeUntil(finished);

        var initialBoard = new Board(new Size(40, 25)); // TODO add moving piece
        var game = new Game(timerCounts, playerMoves, initialBoard, StandardRules.Instance);

        await game.Boards.ForEachAsync(async b => await Console.Out.WriteAsync(b.ToConsoleString()));
    }
}