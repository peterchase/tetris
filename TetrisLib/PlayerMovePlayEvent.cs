namespace TetrisLib;

public sealed class PlayerMovePlayEvent : IPlayEvent
{
    private PlayerMovePlayEvent(in Movement playerMovement) => Movement = playerMovement;

    public Movement Movement { get; }

    public static IPlayEvent For(in Movement playerMovement) => new PlayerMovePlayEvent(playerMovement);

    TR IPlayEvent.Accept<TR, TA>(IPlayEventVisitor<TR, TA> visitor, TA arg) => visitor.VisitPlayerMove(this, arg);
}
