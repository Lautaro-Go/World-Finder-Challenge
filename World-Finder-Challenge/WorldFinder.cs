using Challenge_Word_Finder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World_Finder_Challenge
{
    public class WorldFinder : IWordFinder
    {
        private readonly char[,] _matrix;
        private readonly int rows;
        private readonly int cols;
        private TrieNode root;

        public WorldFinder(IEnumerable<string> matrix)
        {
            var data = matrix.ToArray();
            rows = data.Length;
            cols = data[0].Length;
            _matrix = new char[rows, cols];

            //it just populates the _matrix with the input data
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    _matrix[i, j] = data[i][j];
        }

        private void BuildTrie(IEnumerable<string> words) // O(n*m)
        {
            root = new TrieNode();
            foreach (var word in words) // O(r^2)
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

        public IEnumerable<string> Find(IEnumerable<string> wordStream) // O(r^2)
        {
            BuildTrie(wordStream.Distinct());
            var foundWords = new Dictionary<string, int>();
            var visited = new bool[rows, cols]; // To avoid duplicate searches on the same cell

            //it iterates through the _matrix and searches for words (wordStream)
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    Backtrack(r, c, root, foundWords, visited);

            // Return only the 10 most frequent words, O (n log n)
            return foundWords
                .OrderByDescending(kvp => kvp.Value)
                .Take(10)
                .Select(kvp => kvp.Key);

        }

        private void Backtrack(int r, int c, TrieNode node, Dictionary<string, int> foundWords, bool[,] visited)
        {
            if (r < 0 || r >= rows || c < 0 || c >= cols || visited[r, c]) return; // Pruning: out of _matrix bounds or already explored cell

            char ch = _matrix[r, c];
            if (!node.Children.ContainsKey(ch)) return; // Pruning: if the character is not in the Trie, it cuts early

            node = node.Children[ch];
            if (node.IsWord)
            {
                if (!foundWords.ContainsKey(node.Word)) foundWords[node.Word] = 0;
                foundWords[node.Word]++;
            }

            visited[r, c] = true; // it marks cell as visited

            Backtrack(r + 1, c, node, foundWords, visited); // Down (vertical)
            Backtrack(r, c + 1, node, foundWords, visited); // Right (horizontal)

            visited[r, c] = false; // Unmark cell (Backtracking)
        }

        private class TrieNode
        {
            public Dictionary<char, TrieNode> Children { get; } = new Dictionary<char, TrieNode>();
            public bool IsWord { get; set; } = false;
            public string Word { get; set; } = null;
        }
    }
}
