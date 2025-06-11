using Challenge_Word_Finder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World_Finder_Challenge
{
    public class WordFinder : IWordFinder
    {
        private readonly char[,] _matrix;
        private readonly int rows;
        private readonly int cols;
        private TrieNode root;

        public WordFinder(IEnumerable<string> matrixData)
        {
            var data = matrixData.ToArray();
            rows = data.Length;
            cols = data[0].Length;
            _matrix = new char[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    _matrix[i, j] = data[i][j];
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

            // Horizontal backtracking (left-to-right)
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    // Complexity: O(L) where L is the word length in the horizontal direction
                    Backtrack(r, c, 0, 1, root, foundWords);
                }
            }

            // Vertical backtracking (top-to-bottom)
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    // Complexity: O(L) where L is the word length in the vertical direction
                    Backtrack(r, c, 1, 0, root, foundWords);
                }
            }

            return foundWords.OrderByDescending(kvp => kvp.Value)
                             .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        // Complexity per recursive chain: O(L) (worst case, where L is the length of the word in that direction)
        private void Backtrack(int r, int c, int dr, int dc, TrieNode node, Dictionary<string, int> foundWords)
        {
            if (r < 0 || r >= rows || c < 0 || c >= cols)
                return;

            char ch = _matrix[r, c];
            if (!node.Children.ContainsKey(ch))
                return;

            node = node.Children[ch];

            if (node.IsWord)
            {
                if (!foundWords.ContainsKey(node.Word))
                    foundWords[node.Word] = 0;
                foundWords[node.Word]++;
            }

            Backtrack(r + dr, c + dc, dr, dc, node, foundWords);
        }

        private class TrieNode
        {
            public Dictionary<char, TrieNode> Children { get; } = new Dictionary<char, TrieNode>();
            public bool IsWord { get; set; } = false;
            public string Word { get; set; } = null;
        }
    }
}