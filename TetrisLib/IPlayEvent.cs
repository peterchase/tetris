namespace TetrisLib;

public interface IPlayEvent
{
    Board GetNextBoard(Board board);
}
