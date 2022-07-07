using System.Drawing;

namespace TetrisLib;

public static class RectangleExtensions
{
    public static Rectangle Union(this Rectangle first, Rectangle second)
    {
        var xMin = Math.Min(first.X, second.X);
        var yMin = Math.Min(first.Y, second.Y);
        var width = Math.Max(first.X + first.Width, second.X + second.Width) - xMin;
        var height = Math.Max(first.Y + first.Height, second.Y + second.Height) - yMin;
        return new Rectangle(xMin, yMin, width, height);
    }
}
