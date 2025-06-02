using Challenge_Word_Finder;
using Challenge_Word_Finder.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using World_Finder_Challenge;


var (matrix, wordStream) = MatrixGenerator.GenerateMatrixWithWords(); // complexity out of scope
//MatrixPrinter.PrintMatrixWithHighlight(matrix, wordStream); // out of scope, just to visualize the matrix used


var startup = new Startup();
var serviceProvider = startup.ConfigureServices(matrix);
var wordFinder = serviceProvider.GetRequiredService<IWordFinder>();


// Search worlds in the matrix
var foundWords = wordFinder.Find(wordStream);


Console.WriteLine("Words found, ordered from most to least frequent (by count of appearance).");
foreach (var word in foundWords)
{
    Console.WriteLine(word);
}

Console.WriteLine("Press any key to finish...");
Console.ReadKey();
