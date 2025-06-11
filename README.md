# World-Finder-Challenge

🧩 Solution Strategy

To solve the challenge, I approached it in three main steps:

🥇 Step 1 – Naive Approach & Complexity Analysis

The first step was to analyze the problem and try to solve it in the simplest possible way, without initially worrying about performance or optimizations.
I mechanically traversed the matrix and implemented a basic search for the given words, analyzing its time complexity.

While the implementation worked functionally, its performance was not optimal. The complexity was too high and inefficient for larger inputs.

🥈 Step 2 – Trie Optimization & Pruning

The second step was focused on improving performance. I implemented a Trie (prefix tree) to store the words. Each node represented a character, and complete words were stored at the leaf nodes. This significantly reduced repeated comparisons by grouping common prefixes.

However, this still led to a high time complexity—roughly O(n³) in the worst case.

To optimize further, I implemented pruning: if a path in the Trie (i.e., a node) had already been visited and no word was found, it would no longer be explored again. This avoided redundant recursive calls and saved computation time.

I also used a dictionary (hash map) to store found words with their frequencies. This ensures:

    Unique keys

    Fast lookup and insertion

As the algorithm traverses the matrix, it explores valid Trie branches, and if no valid continuation exists, it backtracks and prunes.

🥉 Step 3 – Final Implementation & Integration

The final implementation achieves quadratic complexity O(n²) in the worst-case scenario, which is acceptable given the matrix size limit of 64x64.

To support testing and visualization:

    A helper class was added to generate a random matrix.

    A separate class was created to print the matrix, optionally highlighting found words.

Finally, the WordFinder class was implemented with dependency injection in mind. This allows for easy replacement or extension with future or improved implementations.
