
using System.Drawing;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using TetrisLib;

internal class Program
{
    private const double cInitialDelayMillis = 500.0, cMinDelayMillis = 25.0;
    private const double cDelayReductionFactor = 0.995;
    private const int cBoardWidth = 20, cBoardHeight = 20;

    private static async Task Main(string[] args)
    {
        ConsoleKeyMonitor.Start();

        Console.Clear();

        using var finished = new Subject<Unit>();
        var playerMoves = ConsoleKeyMonitor.Moves.Finally(() => finished.OnNext(Unit.Default));

        var timerCounts = Observable
            .Generate(cInitialDelayMillis, _ => true, GetFaster, d => (long)d, d => TimeSpan.FromMilliseconds(d))
            .TakeUntil(finished);

        var initialMovingPiece = new Piece(StandardShapes.L42, new Point(2, 0));

        var initialBoard = new Board(new Size(cBoardWidth, cBoardHeight)) { MovingPiece = initialMovingPiece };
        var game = new Game(timerCounts, playerMoves, initialBoard, new StandardRules(StandardShapes.All));

        var builder = new StringBuilder();
        await game.Boards.ForEachAsync(async b => await Console.Out.WriteAsync(b.ToConsoleString(builder)));
    }

    private static double GetFaster(double prevDelay) => Math.Max(cMinDelayMillis, prevDelay * cDelayReductionFactor);
}