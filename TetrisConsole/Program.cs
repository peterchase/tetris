
using System.Drawing;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using TetrisLib;

internal class Program
{
    private static async Task Main(string[] args)
    {
        ConsoleKeyMonitor.Start();

        Console.Clear();

        using var finished = new Subject<Unit>();
        var playerMoves = ConsoleKeyMonitor.Moves.Finally(() => finished.OnNext(Unit.Default));

        var timerCounts = Observable
            .Generate(500.0, _ => true, GetFaster, d => (long)d, d => TimeSpan.FromMilliseconds(d))
            .TakeUntil(finished);

        var initialMovingPiece = new Piece(StandardShapes.L42, new Point(2, 0));

        var initialBoard = new Board(new Size(20, 20)) { MovingPiece = initialMovingPiece };
        var game = new Game(timerCounts, playerMoves, initialBoard, new StandardRules(StandardShapes.All));

        var builder = new StringBuilder();
        await game.Boards.ForEachAsync(async b => await Console.Out.WriteAsync(b.ToConsoleString(builder)));
    }

    private static double GetFaster(double prevDelay) => Math.Max(25.0, prevDelay * 0.995);
}