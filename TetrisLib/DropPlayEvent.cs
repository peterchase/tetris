namespace TetrisLib;

public sealed class DropPlayEvent : IPlayEvent
{
    public static IPlayEvent Instance { get; } = new DropPlayEvent();

    private DropPlayEvent() { }

    TR IPlayEvent.Accept<TR, TA1, TA2>(IPlayEventVisitor<TR, TA1, TA2> visitor, TA1 arg1, TA2 arg2) => visitor.VisitDrop(this, arg1, arg2);
}
