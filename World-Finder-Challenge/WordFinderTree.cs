using Challenge_Word_Finder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World_Finder_Challenge
{
    public class WordFinderTree : IWordFinder
    {
        private readonly char[,] matrix;
        private readonly int rows;
        private readonly int cols;
        private TrieNode root;

        public WordFinderTree(IEnumerable<string> matrixData)
        {
            var data = matrixData.ToArray();
            rows = data.Length;
            cols = data[0].Length;
            matrix = new char[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    matrix[i, j] = data[i][j];
        }

        private void BuildTrie(IEnumerable<string> words)
        {
            root = new TrieNode();
            foreach (var word in words)
            {
                var node = root;
                foreach (var c in word)
                {
                    if (!node.Children.ContainsKey(c))
                        node.Children[c] = new TrieNode();
                    node = node.Children[c];
                }
                node.IsWord = true;
                node.Word = word;
            }
        }

        public Dictionary<string, int> Find(IEnumerable<string> wordStream)
        {
            BuildTrie(wordStream.Distinct());

            var foundWords = new Dictionary<string, int>();

            // Search
            //(left to right)
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    SearchFromPosition(r, c, 0, 1, foundWords); // Horizontal: fixed row, col++
                }
            }

            // (top to bottom)
            for (int c = 0; c < cols; c++)
            {
                for (int r = 0; r < rows; r++)
                {
                    SearchFromPosition(r, c, 1, 0, foundWords); // Vertical: row++, fixed col
                }
            }

            return foundWords;
        }

        //It searches for words in the direction (dr, dc) starting from (r, c)
        private void SearchFromPosition(int r, int c, int dr, int dc, Dictionary<string, int> foundWords)
        {
            var node = root;
            int rr = r, cc = c;

            while (rr >= 0 && rr < rows && cc >= 0 && cc < cols)
            {
                char ch = matrix[rr, cc];
                if (!node.Children.ContainsKey(ch))
                    break;

                node = node.Children[ch];
                if (node.IsWord)
                {
                    if (!foundWords.ContainsKey(node.Word))
                        foundWords[node.Word] = 0;
                    foundWords[node.Word]++;
                }

                rr += dr;
                cc += dc;
            }
        }

        private class TrieNode
        {
            public Dictionary<char, TrieNode> Children { get; } = new Dictionary<char, TrieNode>();
            public bool IsWord { get; set; } = false;
            public string Word { get; set; } = null;
        }
    }

}
