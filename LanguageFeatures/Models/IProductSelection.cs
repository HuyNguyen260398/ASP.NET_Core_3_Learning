using System.Collections.Generic;
using System.Linq;

namespace LanguageFeatures.Models
{
    // Using Default Implementations in Interface
    public interface IProductSelection
    {
        IEnumerable<Product> Products { get; }

        // Adding a new feature to an interface
        IEnumerable<string> Names => Products.Select(p => p.Name);
    }
}