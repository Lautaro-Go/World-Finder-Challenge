using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace World_Finder_Challenge
{
    public static class MatrixGenerator
    {
        private static readonly Random random = new Random();

        public static (List<string> matrix, List<string> wordStream) GenerateMatrixWithWords(int rows = 64, int columns = 64, int wordCount = 15)
        {
            var matrix = GenerateRandomMatrix(rows, columns);
            var matrixArray = matrix.Select(row => row.ToCharArray()).ToArray();

            var availableWords = new List<string>
        {
            "HELLO", "WORLD", "SEARCH", "STACK", "QUEUE", "MATRIX",
            "ALGORITHM", "TREE", "GRAPH", "CODE", "DEBUG", "SYSTEM",
            "VARIABLE", "FUNCTION", "LOOP", "CLASS", "OBJECT", "METHOD"
        };

            // it takes random words from the variable above
            var selectedWords = availableWords.OrderBy(_ => random.Next()).Take(wordCount).ToList();

            // it adds words repeatdes, 10 will be twice
            var repeatedWords = selectedWords.Take(10);
            selectedWords.AddRange(repeatedWords);

            foreach (var word in selectedWords)
            {
                InsertWordRandomly(matrixArray, word);
            }

            matrix = matrixArray.Select(row => new string(row)).ToList();

            return (matrix, selectedWords);
        }
        private static List<string> GenerateRandomMatrix(int rows, int cols)
        {
            var matrix = new List<string>();

            for (int i = 0; i < rows; i++)
            {
                char[] row = new char[cols];

                for (int j = 0; j < cols; j++)
                {
                    row[j] = (char)('A' + random.Next(26));
                }

                matrix.Add(new string(row));
            }

            return matrix;
        }
        private static void InsertWordRandomly(char[][] matrixArray, string word)
        {
            int size = matrixArray.Length;
            bool isHorizontal = random.Next(2) == 0;

            if (isHorizontal)
            {
                // Insertar horizontalmente
                int row = random.Next(size);
                int col = random.Next(size - word.Length);
                word.ToCharArray().CopyTo(matrixArray[row], col);
            }
            else
            {
                // Insertar verticalmente
                int col = random.Next(size);
                int row = random.Next(size - word.Length);
                for (int i = 0; i < word.Length; i++)
                {
                    matrixArray[row + i][col] = word[i];
                }
            }
        }
    }
}
