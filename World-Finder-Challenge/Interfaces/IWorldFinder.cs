using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_Word_Finder.Interfaces
{
    public interface IWordFinder
    {
        Dictionary<string, int> Find(IEnumerable<string> wordStream);
    }
}
