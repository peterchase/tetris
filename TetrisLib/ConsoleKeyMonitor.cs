using System.Reactive.Subjects;

namespace TetrisLib;

public static class ConsoleKeyMonitor
{
    private static readonly Subject<Movement> sMovements = new();

    public static IObservable<Movement> Movenents => sMovements;

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
                    sMovements.OnNext(new Movement(-1));
                    break;

                case ConsoleKey.RightArrow:
                    sMovements.OnNext(new Movement(1));
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
