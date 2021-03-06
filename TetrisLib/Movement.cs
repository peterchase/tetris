namespace TetrisLib;

public readonly struct Movement
{
    public static Movement LeftOne => new() { Right = -1 };

    public static Movement RightOne => new() { Right = 1 };

    public static Movement Clockwise => new() { RotateClockwise = 1 };

    public static Movement AntiClockwise => new() { RotateClockwise = -1 };

    public int Right { get; init; }
    public int RotateClockwise { get; init; }

    public override string ToString() => $"{Right} {RotateClockwise}";
}
