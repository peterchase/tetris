namespace TetrisLib;

public readonly struct Movement
{
    public int Down { get; }
    public int Right { get; }

    public Movement(int right, int down = 0) : this()
    {
        Right = right;
        Down = down;
    }
}
