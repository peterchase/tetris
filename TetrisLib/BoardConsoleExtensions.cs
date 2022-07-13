using System.Drawing;
using System.Text;

namespace TetrisLib;

public static class BoardConsoleExtensions
{
    private static readonly Dictionary<int, int> sColours = new()
    {
        { 0, 34 },
        { 1, 32 },
        { 2, 33 },
        { 3, 31 },
        { 4, 35 },
        { 5, 36 },
        { 6, 37 },
        { 7, 91 },
        { 8, 92 },
        { 9, 93 },
        { 10, 94 },
        { 11, 95 },
        { 12, 96 },
        { 13, 97 },
    };

    public const string cHome = "\u001b[0;0H";
    private const string cDefaultColour = "\u001b[0m";

    internal static string ColourCode(int n) => $"\u001b[{sColours[n % sColours.Count]}m";

    public static string ToConsoleString(this Board board, StringBuilder? builder = null)
    {
        builder ??= new StringBuilder();

        builder.Append(cHome);

        for (int y = 0; y < board.Size.Height; ++y)
        {
            for (int x = 0; x < board.Size.Width; ++x)
            {
                int? kind = board.PieceAt(new Point(x, y))?.Kind;
                if (kind.HasValue)
                {
                    builder.Append(ColourCode(kind.Value));
                    builder.Append('█');
                }
                else
                {
                    builder.Append(' ');
                }
            }

            builder.Append(Environment.NewLine);
        }

        builder.Append(cDefaultColour);

        return builder.ToString();
    }
}
