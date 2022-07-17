using System.Drawing;

namespace TetrisLib;

internal static class RectangleExtensions
{
    private static readonly int[][]?[] sRotationMatrices = new[] {
        null,
        new[] { new[] { 0, -1}, new[] { 1, 0 }},
        new[] { new[] { -1, 0 }, new[] { 0, -1 }},
        new[] { new[] { 0, 1}, new[] { -1, 0 }}
    };

    public static Rectangle Union(this Rectangle first, Rectangle second)
    {
        var xMin = Math.Min(first.X, second.X);
        var yMin = Math.Min(first.Y, second.Y);
        var width = Math.Max(first.X + first.Width, second.X + second.Width) - xMin;
        var height = Math.Max(first.Y + first.Height, second.Y + second.Height) - yMin;
        return new Rectangle(xMin, yMin, width, height);
    }

    public static Rectangle RotateClockwise(this Rectangle rect, int rotation)
    {
        if (rotation < 0 || rotation >= sRotationMatrices.Length)
        {
            throw new ArgumentException($"Unsupported rotation {rotation}", nameof(rotation));
        }

        int[][]? rotationMatrix = sRotationMatrices[rotation];
        if (rotationMatrix is null)
        {
            return rect;
        }

        int[][] orig = new[] { new[] { rect.X, rect.Right }, new[] { rect.Y, rect.Bottom } };

        int[][] rotated = rotationMatrix.MatrixMultiply2x2(orig);

        return new Rectangle(
            Math.Min(rotated[0][0], rotated[0][1]),
            Math.Min(rotated[1][0], rotated[1][1]),
            Math.Abs(rotated[0][0] - rotated[0][1]),
            Math.Abs(rotated[1][0] - rotated[1][1]));
    }
}
