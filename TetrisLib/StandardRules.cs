namespace TetrisLib;

using System.Drawing;

public sealed class StandardRules : IRules
{
    private readonly Shape[] mAllShapes;
    private readonly Random mRandom = new();

    public StandardRules(params Shape[] allShapes) => mAllShapes = allShapes;

    public StandardRules(IEnumerable<Shape> allShapes) : this(allShapes.ToArray()) { }

    public Board VisitPlayerMove(PlayerMovePlayEvent playEvent, Board prevBoard, Game game)
    {
        if (prevBoard.MovingPiece is null)
        {
            return prevBoard;
        }

        Piece prevPiece = prevBoard.MovingPiece;

        Piece newPiece = prevPiece
            .MoveTo(new Point(prevPiece.Position.X + playEvent.Movement.Right, prevPiece.Position.Y))
            .RotateClockwise(playEvent.Movement.RotateClockwise);

        return newPiece.ContainedBy(prevBoard.Size) ? prevBoard.WithMovingPiece(newPiece) : prevBoard;
    }

    public Board VisitTimerCount(TimerCountPlayEvent playEvent, Board prevBoard, Game game)
    {
        if (prevBoard.MovingPiece is null)
        {
            return prevBoard;
        }

        Piece prevPiece = prevBoard.MovingPiece;
        Piece movedDownPiece = prevPiece.MoveTo(new Point(prevPiece.Position.X, prevPiece.Position.Y + 1));

        if (Collision(prevBoard, movedDownPiece))
        {
            return GetCollisionResult(prevBoard, movedDownPiece);
        }

        return prevBoard.WithMovingPiece(movedDownPiece);
    }

    public Board VisitDrop(DropPlayEvent playEvent, Board prevBoard, Game game)
    {
        if (prevBoard.MovingPiece is null)
        {
            return prevBoard;
        }

        for (Piece piece = prevBoard.MovingPiece; ; )
        {
            var movedDownPiece = piece.MoveTo(new Point(piece.Position.X, piece.Position.Y + 1));
            if (Collision(prevBoard, movedDownPiece))
            {
                return GetCollisionResult(prevBoard.WithMovingPiece(piece), movedDownPiece);
            }

            piece = movedDownPiece;
        }
    }

    public bool Finished(Board board, Game game)
    {
        return board.MovingPiece != null && board.MovingPiece.Position.Y == 0 && Collision(board, board.MovingPiece);
    }

    // Is the piece intersecting a fixed piece or off the bottom of the board?
    private static bool Collision(Board prevBoard, Piece piece)
    {
        if (piece.Boundary.Bottom == prevBoard.Size.Height + 1)
        {
            return true;
        }

        return piece.Points.Any(p => prevBoard.FixedPieceKindAt(p).HasValue);
    }

    private Board GetCollisionResult(Board prevBoard, Piece movedDownPiece)
    {
        return prevBoard.WithMovingPieceFixed().WithFullRowsRemoved().WithMovingPiece(CreateNewMovingPiece(prevBoard.Size));
    }

    private Piece CreateNewMovingPiece(Size boardSize)
    {
        Shape shape = mAllShapes[mRandom.Next(mAllShapes.Length)];
        int xRange = boardSize.Width - shape.Size.Width;
        int x = mRandom.Next(xRange);
        return new Piece(shape, new Point(x, 0));
    }
}
