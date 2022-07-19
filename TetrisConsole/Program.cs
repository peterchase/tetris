
using System.Drawing;
using System.Reactive.Linq;
using System.Text;
using TetrisLib;

internal class Program
{
    private const double cInitialDelayMillis = 500.0, cMinDelayMillis = 25.0;
    private const double cDelayReductionFactor = 0.9998;
    private const int cBoardWidth = 21, cBoardHeight = 19;
    private const int cScaleWidth = 3, cScaleHeight = 2;

    private static async Task Main(string[] args)
    {
        ConsoleKeyMonitor.Start();

        Console.Clear();

        var playerMoves = ConsoleKeyMonitor.Moves;

        var timerCounts = Observable
            .Generate(cInitialDelayMillis, _ => true, GetFaster, d => (long)d, d => TimeSpan.FromMilliseconds(d))
            .TakeUntil(playerMoves.LastOrDefaultAsync());

        var initialMovingPiece = new Piece(StandardShapes.L42, new Point(2, 0));

        var initialBoard = new Board(new Size(cBoardWidth, cBoardHeight)) { MovingPiece = initialMovingPiece };
        var game = new Game(timerCounts, playerMoves, initialBoard, new StandardRules(StandardShapes.All));

        var builder = new StringBuilder();
        await game.Boards.ForEachAsync(async b => await DisplayBoard(b, builder));
    }

    private static async Task DisplayBoard(Board b, StringBuilder builder)
    {
        Size scale = new Size(cScaleWidth, cScaleHeight);
        Console.CursorVisible = false;
        try
        {
            await Console.Out.WriteAsync(b.ToConsoleString(scale, builder));
        }
        finally
        {
            Console.CursorVisible = true;
        }
    }

    private static double GetFaster(double prevDelay) => Math.Max(cMinDelayMillis, prevDelay * cDelayReductionFactor);
}