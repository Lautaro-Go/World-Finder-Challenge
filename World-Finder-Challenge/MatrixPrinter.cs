namespace Challenge_Word_Finder
{
    public static class MatrixPrinter
    {
        public static void PrintMatrixWithHighlight(List<string> matrix, List<string> words)
        {
            var wordsSet = new HashSet<string>(words);
            ConsoleColor defaultColor = ConsoleColor.DarkGray;
            ConsoleColor highlightColor = ConsoleColor.White;

            bool[,] highlightMap = new bool[matrix.Count, matrix[0].Length];

            foreach (var word in wordsSet)
            {
                MarkHighlightPositions(matrix, word, highlightMap);
            }

            for (int row = 0; row < matrix.Count; row++)
            {
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    Console.ForegroundColor = highlightMap[row, col] ? highlightColor : defaultColor;
                    Console.Write(matrix[row][col]);
                }
                Console.WriteLine();
            }

            Console.ResetColor();
        }

        private static void MarkHighlightPositions(List<string> matrix, string word, bool[,] highlightMap)
        {
            int rows = matrix.Count;
            int cols = matrix[0].Length;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col <= cols - word.Length; col++)
                {
                    if (matrix[row].Substring(col, word.Length) == word)
                    {
                        for (int i = 0; i < word.Length; i++)
                            highlightMap[row, col + i] = true;
                    }
                }
            }

            for (int col = 0; col < cols; col++)
            {
                for (int row = 0; row <= rows - word.Length; row++)
                {
                    bool match = true;
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (matrix[row + i][col] != word[i])
                        {
                            match = false;
                            break;
                        }
                    }

                    if (match)
                    {
                        for (int i = 0; i < word.Length; i++)
                            highlightMap[row + i, col] = true;
                    }
                }
            }
        }
    }
}