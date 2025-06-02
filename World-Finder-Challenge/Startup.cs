using Challenge_Word_Finder.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace World_Finder_Challenge
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IEnumerable<string> matrixData)
        {
            var services = new ServiceCollection();

            // Registrar WordFinderTree con una instancia de datos
            services.AddSingleton<IWordFinder>(provider => new WorldFinder(matrixData));

            return services.BuildServiceProvider();
        }
    }
}
