using Challenge_Word_Finder.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace World_Finder_Challenge
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IEnumerable<string> matrixData)
        {
            var services = new ServiceCollection();

            services.AddSingleton<IWordFinder>(provider => new World_Finder_Challenge.WordFinder(matrixData));

            return services.BuildServiceProvider();
        }
    }
}
