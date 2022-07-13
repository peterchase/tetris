
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using TetrisLib;

internal class Program
{
    private static async Task Main(string[] args)
    {
        ConsoleKeyMonitor.Start();
        using var finished = new Subject<Unit>();

        var playerMoves = ConsoleKeyMonitor.Movements.Finally(() => finished.OnNext(Unit.Default));

        var timerCounts = Observable.Interval(TimeSpan.FromSeconds(1)).TakeUntil(finished);

        var t2 = playerMoves.ForEachAsync(async m => await Console.Out.WriteLineAsync(m.ToString()));
        var t1 = timerCounts.ForEachAsync(async c => await Console.Out.WriteLineAsync(c.ToString()));

        await Task.WhenAll(t1, t2);
    }
}