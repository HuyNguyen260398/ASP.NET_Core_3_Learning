using System.Collections;
using System.Collections.Generic;

namespace LanguageFeatures.Models
{
    // Implementing an Interface
    // public class ShoppingCart : IEnumerable<Product>
    // {
    //     public IEnumerable<Product> Products { get; set; }

    //     public IEnumerator<Product> GetEnumerator()
    //     {
    //         return Products.GetEnumerator();
    //     }

    //     IEnumerator IEnumerable.GetEnumerator()
    //     {
    //         return GetEnumerator();
    //     }
    // }

    // Implementing the new interface
    public class ShoppingCart : IProductSelection
    {
        private List<Product> products = new List<Product>();

        public ShoppingCart(params Product[] prods)
        {
            products.AddRange(prods);
        }

        public IEnumerable<Product> Products { get => products; }
    }
}