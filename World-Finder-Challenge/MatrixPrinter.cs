namespace Challenge_Word_Finder
{
    public static class MatrixPrinter
    {
        public static void PrintMatrixWithHighlight(List<string> matrix, List<string> words)
        {
            var wordsSet = new HashSet<string>(words);
            ConsoleColor defaultColor = ConsoleColor.DarkGray;
            ConsoleColor highlightColor = ConsoleColor.White;

            for (int row = 0; row < matrix.Count; row++)
            {
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    char currentChar = matrix[row][col];

                    bool isHighlighted = IsCharPartOfWord(matrix, wordsSet, row, col, currentChar);

                    Console.ForegroundColor = isHighlighted ? highlightColor : defaultColor;
                    Console.Write(currentChar);
                }
                Console.WriteLine();
            }

            Console.ResetColor();
        }

        private static bool IsCharPartOfWord(List<string> matrix, HashSet<string> wordsSet, int row, int col, char currentChar)
        {
            int size = matrix.Count;

            foreach (var word in wordsSet)
            {
                if (CheckHorizontal(matrix, word, row, col) || CheckVertical(matrix, word, row, col))
                    return true;
            }

            return false;
        }

        private static bool CheckHorizontal(List<string> matrix, string word, int row, int col)
        {
            if (col + word.Length > matrix[row].Length) return false;
            return matrix[row].Substring(col, word.Length) == word;
        }

        private static bool CheckVertical(List<string> matrix, string word, int row, int col)
        {
            if (row + word.Length > matrix.Count) return false;
            for (int i = 0; i < word.Length; i++)
                if (matrix[row + i][col] != word[i]) return false;
            return true;
        }
    }
}
