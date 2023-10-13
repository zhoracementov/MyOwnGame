using System.Collections.Generic;

namespace WPF.Extenstions
{
    public static class MatrixExtenstions
    {
        public static IEnumerable<T> AsLinear<T>(this T[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    yield return matrix[i, j];
                }
            }
        }

        public static int GetLinearArrayIndex<T>(this T[,] ar, int x, int y)
        {
            return GetLinearArrayIndex(x, y, ar.GetLength(0));
        }

        public static int GetLinearArrayIndex(int x, int y, int w)
        {
            return (w * x) + y;
        }
    }
}
