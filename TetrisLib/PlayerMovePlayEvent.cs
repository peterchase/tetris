namespace TetrisLib;

public sealed class PlayerMovePlayEvent : IPlayEvent
{
    private PlayerMovePlayEvent(in Movement playerMovement) => PlayerMovement = playerMovement;

    public Movement PlayerMovement { get; }

    public static IPlayEvent For(in Movement playerMovement) => new PlayerMovePlayEvent(playerMovement);

    TR IPlayEvent.Accept<TR, TA>(IPlayEventVisitor<TR, TA> visitor, TA arg) => visitor.VisitPlayerMove(this, arg);
}
