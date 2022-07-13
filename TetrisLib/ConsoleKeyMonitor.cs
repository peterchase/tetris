using System.Reactive.Subjects;

namespace TetrisLib;

public static class ConsoleKeyMonitor
{
    private static readonly Subject<IPlayEvent> sMovements = new();

    public static IObservable<IPlayEvent> Moves => sMovements;

    public static void Start()
    {
        new Thread(Monitor) { IsBackground = true }.Start();
    }

    private static void Monitor()
    {
        for (; ; )
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    sMovements.OnNext(PlayerMovePlayEvent.For(Movement.LeftOne));
                    break;

                case ConsoleKey.RightArrow:
                    sMovements.OnNext(PlayerMovePlayEvent.For(Movement.RightOne));
                    break;

                case ConsoleKey.UpArrow:
                    sMovements.OnNext(PlayerMovePlayEvent.For(Movement.AntiClockwise));
                    break;

                case ConsoleKey.DownArrow:
                    sMovements.OnNext(PlayerMovePlayEvent.For(Movement.Clockwise));
                    break;

                case ConsoleKey.D:
                    sMovements.OnNext(DropPlayEvent.Instance);
                    break;
                    
                case ConsoleKey.Q:
                    sMovements.OnCompleted();
                    return;

                default:
                    break;
            }
        }
    }
}
