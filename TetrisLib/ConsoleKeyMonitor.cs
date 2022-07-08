using System.Reactive.Subjects;

namespace TetrisLib;

public static class ConsoleKeyMonitor
{
    private static readonly Subject<Movement> sMovements = new();

    public static IObservable<Movement> Movements => sMovements;

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
                    sMovements.OnNext(Movement.LeftOne);
                    break;

                case ConsoleKey.RightArrow:
                    sMovements.OnNext(Movement.RightOne);
                    break;

                case ConsoleKey.UpArrow:
                    sMovements.OnNext(Movement.AntiClockwise);
                    break;

                case ConsoleKey.DownArrow:
                    sMovements.OnNext(Movement.Clockwise);
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
