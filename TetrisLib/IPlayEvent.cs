namespace TetrisLib;

public interface IPlayEvent
{
    TR Accept<TR, TA1, TA2>(IPlayEventVisitor<TR, TA1, TA2> visitor, TA1 arg1, TA2 arg2);
}
