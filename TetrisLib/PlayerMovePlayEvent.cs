namespace TetrisLib;

public sealed class PlayerMovePlayEvent : IPlayEvent
{
    private readonly Movement mPlayerMovement;

    private PlayerMovePlayEvent(in Movement playerMovement) => mPlayerMovement = playerMovement;

    public static IPlayEvent For(in Movement playerMovement) => new PlayerMovePlayEvent(playerMovement);
    
    public Board GetNextBoard(Board board)
    {
        throw new NotImplementedException();
    }
}
