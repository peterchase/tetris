namespace TetrisLib;

public interface IRules : IPlayEventVisitor<Board, Board, Game>
{
    bool Finished(Board board, Game game);
}
