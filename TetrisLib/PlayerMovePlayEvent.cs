namespace TetrisLib;

public sealed class PlayerMovePlayEvent : IPlayEvent
{
    private PlayerMovePlayEvent(in Movement playerMovement) => Movement = playerMovement;

    public Movement Movement { get; }

    public static IPlayEvent For(in Movement playerMovement) => new PlayerMovePlayEvent(playerMovement);

    TR IPlayEvent.Accept<TR, TA1, TA2>(IPlayEventVisitor<TR, TA1, TA2> visitor, TA1 arg1, TA2 arg2) => visitor.VisitPlayerMove(this, arg1, arg2);
}
