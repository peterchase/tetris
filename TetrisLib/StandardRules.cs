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

        // TODO: support rotation
        var prevPiece = prevBoard.MovingPiece;
        var newPiece = prevPiece.MoveTo(new Point(prevPiece.Position.X + playEvent.Movement.Right, prevPiece.Position.Y));
        newPiece = newPiece.ContainedBy(prevBoard.Size) ? newPiece : prevPiece;
        return prevBoard.WithMovingPiece(newPiece);
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
            Board board = prevBoard.WithMovingPieceFixed();
            while (board.IsFullAcrossWidth(board.Size.Height - 1))
            {
                board = board.MoveFixedPiecesDown();
            }

            return board.WithMovingPiece(CreateNewMovingPiece(board.Size));
        }

        // TODO: detect full row(s) at bottom. If so, move fixed pieces down and remove any wholly outside board.

        return prevBoard.WithMovingPiece(movedDownPiece);
    }

    private static bool Collision(Board prevBoard, Piece piece)
    {
        return prevBoard.FixedPieces.Any(piece.Intersects)
                    || piece.Boundary.Bottom == prevBoard.Size.Height;
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
                Piece newMovingPiece = CreateNewMovingPiece(prevBoard.Size);
                Board boardWithDroppedPiece = prevBoard.WithMovingPiece(piece);
                return boardWithDroppedPiece.WithMovingPieceFixed().WithMovingPiece(newMovingPiece);
            }

            piece = movedDownPiece;
        }
    }

    public bool Finished(Board board, Game game)
    {
        return board.MovingPiece != null && board.MovingPiece.Position.Y == 0 && Collision(board, board.MovingPiece);
    }

    private Piece CreateNewMovingPiece(Size boardSize)
    {
        Shape shape = mAllShapes[mRandom.Next(mAllShapes.Length)];
        int xRange = boardSize.Width - shape.Size.Width;
        int x = mRandom.Next(xRange);
        return new Piece(shape, new Point(x, 0));
    }
}
