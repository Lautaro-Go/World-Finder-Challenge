using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace World_Finder_Challenge
{
    public static class MatrixGenerator
    {
        private static readonly Random random = new Random();

        public static (List<string> matrix, List<string> wordStream) GenerateMatrixWithWords(int rows = 64, int columns = 64, int wordCount = 15, bool useExistingFile = true)
        {
            List<string> matrix;
            var availableWords = new List<string>
            {
                "HELLO", "WORLD", "SEARCH", "STACK", "QUEUE", "MATRIX",
                "ALGORITHM", "TREE", "GRAPH", "CODE", "DEBUG", "SYSTEM",
                "VARIABLE", "FUNCTION", "LOOP", "CLASS", "OBJECT", "METHOD"
            };

            if (useExistingFile)
            {
                matrix = LoadExistingMatrix();
                if (matrix != null) return (matrix, availableWords);
                
                Console.WriteLine("The file has not been found, generating new matrix.");
            }

            matrix = GenerateRandomMatrix(rows, columns);
            var matrixArray = matrix.Select(row => row.ToCharArray()).ToArray();

           

            var selectedWords = availableWords.OrderBy(_ => random.Next()).Take(wordCount).ToList();
            var repeatedWords = selectedWords.Take(10);
            selectedWords.AddRange(repeatedWords);

            foreach (var word in selectedWords)
            {
                InsertWordRandomly(matrixArray, word);
            }

            matrix = matrixArray.Select(row => new string(row)).ToList();

            if (!useExistingFile)
            {
                SaveMatrixToFile(matrix);
            }

            return (matrix, selectedWords);
        }

        private static void SaveMatrixToFile(List<string> matrix)
        {
            string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string filePath = Path.Combine(directoryPath, $"matrix_{timestamp}.txt");

            File.WriteAllLines(filePath, matrix);
            Console.WriteLine($"Matrix saved in: {filePath}");
        }

        private static List<string> LoadExistingMatrix()
        {
            string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
            var files = Directory.GetFiles(directoryPath, "matrix_2025*.txt");

            if (files.Length == 0) return null;

            string selectedFile = files.OrderByDescending(File.GetCreationTime).First();
            Console.WriteLine($"using file: {selectedFile}");
            return File.ReadAllLines(selectedFile).ToList();
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
                int row = random.Next(size);
                int col = random.Next(size - word.Length);
                word.ToCharArray().CopyTo(matrixArray[row], col);
            }
            else
            {
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
