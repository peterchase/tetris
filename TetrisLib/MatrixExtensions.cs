namespace TetrisLib;

public static class MatrixExtensions
{
    public static int[][] MatrixMultiply2x2(this int[][] matrix1, int[][] matrix2)
    {
        Check2x2(matrix1, nameof(matrix1));
        Check2x2(matrix2, nameof(matrix2));
        
        int[][] result = new[] { new int[2], new int[2] };
        for (int i = 0; i < 2; ++i)
        {
            for (int j = 0; j < 2; ++j)
            {
                for (int k = 0; k < 2; ++k)
                {
                    result[i][j] += matrix1[i][k] * matrix2[k][j];
                }
            }
        }

        return result;
    }

    public static void Check2x2(this int[][] matrix, string? name = null)
    {
        name ??= nameof(matrix);
        if (matrix.Length != 2 || !matrix.All(v => v.Length == 2))
        {
            throw new ArgumentException("2x2 matrix required", name);
        }
    }
}
