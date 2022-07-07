namespace TetrisLib;

internal static class NullableExtensions
{
    public static IEnumerable<T> AsEnumerable<T>(this T? nullable) where T : class
    {
        if (nullable is null)
        {
            yield break;
        }

        yield return nullable;
    }

    public static IEnumerable<T> AsEnumerable<T>(this T? nullable) where T : struct
    {
        if (nullable is null)
        {
            yield break;
        }

        yield return nullable.Value;
    }
}
