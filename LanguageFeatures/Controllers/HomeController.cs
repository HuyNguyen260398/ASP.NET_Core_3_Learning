using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LanguageFeatures.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        // Using a Function to Filter Objects
        bool FilterByPrice(Product p)
        {
            return (p?.Price ?? 0) >= 20;
        }

        public ViewResult Index1()
        {
            List<string> results = new List<string>();

            foreach (Product p in Product.GetProducts())
            {
                string name = p?.Name ?? "<No Name>"; // Combining the Conditional and Coalescing Operators
                decimal? price = p?.Price ?? 0;
                string relatedName = p?.Related?.Name ?? "<None>"; // Chaining the Null Conditional Operator

                //results.Add(string.Format("Name: {0}, Price: {1}, Related: {2}", name, price, relatedName)); // String Format
                results.Add($"Name: {name}, Price: {price}, Related: {relatedName}"); // String Interpolation
            }
            return View(results);
        }

        public ViewResult Index2()
        {
            string[] names = new string[3];
            names[0] = "Bob";
            names[1] = "Joe";
            names[2] = "Alice";
            return View("Index", names);
        }

        public ViewResult Index3()
        {
            return View("Index", new string[] { "Bob", "Joe", "Alice" });
        }

        // Using an Index Initializer
        public ViewResult Index4()
        {
            Dictionary<string, Product> products = new Dictionary<string, Product>
            {
                {"Kayak", new Product { Name = "Kayak", Price = 275M }},
                { "Lifejacket", new Product { Name = "Lifejacket", Price = 48.95M }}
            };
            return View("Index", products.Keys);
        }

        public ViewResult Index5()
        {
            Dictionary<string, Product> products = new Dictionary<string, Product>
            {
                ["Kayak"] = new Product { Name = "Kayak", Price = 275M },
                ["Lifejacket"] = new Product { Name = "Lifejacket", Price = 48.95M }
            };
            return View("Index", products.Keys);
        }

        // Pattern Matching
        public ViewResult Index6()
        {
            object[] data = new object[] { 275M, 29.95M, "apple", "orange", 100, 10 };
            decimal total = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[1] is decimal d)
                {
                    total += d;
                }
            }
            return View("Index", new string[] { $"Total: {total:C2}" });
        }

        // Pattern Matching in switch Statements
        public ViewResult Index7()
        {
            object[] data = new object[] { 275M, 29.95M, "apple", "orange", 100, 10 };
            decimal total = 0;
            for (int i = 0; i < data.Length; i++)
            {
                switch (data[i])
                {
                    case decimal decimalValue:
                        total += decimalValue;
                        break;
                    case int intValue when intValue > 50:
                        total += intValue;
                        break;
                }
            }
            return View("Index", new string[] { $"Total: {total:C2}" });
        }

        // Applying Extension Methods
        // public ViewResult Index8()
        // {
        //     ShoppingCart cart = new ShoppingCart { Products = Product.GetProducts() };
        //     decimal cartTotal = cart.TotalPrices();
        //     return View("Index", new string[] { $"Total: {cartTotal:C2}" });
        // }

        // Implementing Interface
        // public ViewResult Index9()
        // {
        //     ShoppingCart cart = new ShoppingCart { Products = Product.GetProducts() };

        //     Product[] productArray =
        //     {
        //         new Product{ Name = "Kayak", Price = 275M },
        //         new Product {Name = "Lifejacket", Price = 48.95M }
        //     };

        //     decimal cartTotal = cart.TotalPrices();
        //     decimal arrayTotal = productArray.TotalPrices();

        //     return View("Index", new string[] {
        //         $"Cart Total: {cartTotal:C2}",
        //         $"Array Total: {arrayTotal:C2}" });
        // }

        // Using the Filter Extension Method
        public ViewResult Index10()
        {
            Product[] productArray =
            {
                new Product {Name = "Kayak", Price = 275M},
                new Product {Name = "Lifejacket", Price = 48.95M},
                new Product {Name = "Soccer ball", Price = 19.50M},
                new Product {Name = "Corner flag", Price = 34.95M}
            };

            decimal priceFilterTotal = productArray.FilterByPrice(20).TotalPrices();
            decimal nameFilterTotal = productArray.FilterByName('S').TotalPrices();

            return View("Index", new string[] {
                $"Price Total: {priceFilterTotal:C2}",
                $"Name Total: {nameFilterTotal:C2}" });
        }

        // Using the Filter Function defined above
        public ViewResult Index11()
        {
            Product[] productArray =
            {
                new Product {Name = "Kayak", Price = 275M},
                new Product {Name = "Lifejacket", Price = 48.95M},
                new Product {Name = "Soccer ball", Price = 19.50M},
                new Product {Name = "Corner flag", Price = 34.95M}
            };

            Func<Product, bool> nameFilter = delegate (Product prod)
            {
                return prod?.Name?[0] == 'S';
            };

            decimal priceFilterTotal = productArray.Filter(FilterByPrice).TotalPrices();

            decimal nameFilterTotal = productArray.Filter(nameFilter).TotalPrices();

            return View("Index", new string[] {
                $"Price Total: {priceFilterTotal:C2}",
                $"Name Total: {nameFilterTotal:C2}" });
        }

        // Using Lambda Expression
        public ViewResult Index12()
        {
            Product[] productArray =
            {
                new Product {Name = "Kayak", Price = 275M},
                new Product {Name = "Lifejacket", Price = 48.95M},
                new Product {Name = "Soccer ball", Price = 19.50M},
                new Product {Name = "Corner flag", Price = 34.95M}
            };

            decimal priceFilterTotal = productArray.Filter(p => (p?.Price ?? 0) >= 20).TotalPrices();

            decimal nameFilterTotal = productArray.Filter(p => p?.Name?[0] == 'S').TotalPrices();

            return View("Index", new string[] {
                $"Price Total: {priceFilterTotal:C2}",
                $"Name Total: {nameFilterTotal:C2}" });
        }

        // Other forms for Lambda Expressions
        /*
            prod => EvaluateProduct(prod)

            (prod, count) => prod.Price > 20 && count > 0

            (prod, count) => {
                // ...multiple code statements...
                return result;
            }
        */

        // Using Lambda Expression Methods and Properties
        public ViewResult Index13()
        {
            return View(Product.GetProducts().Select(p => p?.Name));
        }

        // A Lambda Action Method
        public ViewResult Index14() =>
            View(Product.GetProducts().Select(p => p?.Name));

        // Using Type Inference
        public ViewResult Index15()
        {
            var names = new[] { "Kayak", "Lifejacket", "Soccer ball" };
            return View(names);
        }

        // Using anonymous type
        public ViewResult Index16()
        {
            var products = new[]
            {
                new { Name = "Kayak", Price = 275M },
                new { Name = "Lifejacket", Price = 48.95M },
                new { Name = "Soccer ball", Price = 19.50M },
                new { Name = "Corner flag", Price = 34.95M }
            };
            return View(products.Select(p => p.Name));
        }

        // Displaying the Type Name
        public ViewResult Index17()
        {
            var products = new[]
            {
                new { Name = "Kayak", Price = 275M },
                new { Name = "Lifejacket", Price = 48.95M },
                new { Name = "Soccer ball", Price = 19.50M },
                new { Name = "Corner flag", Price = 34.95M }
            };
            return View(products.Select(p => p.GetType().Name));
        }

        // Using an interface in the by using the Shopping Cart class
        public ViewResult Index18()
        {
            IProductSelection cart = new ShoppingCart
            (
                new Product { Name = "Kayak", Price = 275M },
                new Product { Name = "Lifejacket", Price = 48.95M },
                new Product { Name = "Soccer ball", Price = 19.50M },
                new Product { Name = "Corner flag", Price = 34.95M }
            );
            return View(cart.Products.Select(p => p.Name));
        }

        // Using the new feature added in IProductSelection
        public ViewResult Index19()
        {
            IProductSelection cart = new ShoppingCart
            (
                new Product { Name = "Kayak", Price = 275M },
                new Product { Name = "Lifejacket", Price = 48.95M },
                new Product { Name = "Soccer ball", Price = 19.50M },
                new Product { Name = "Corner flag", Price = 34.95M }
            );
            return View(cart.Names);
        }

        // Asynchronous Action Method
        public async Task<ViewResult> Index20()
        {
            long? length = await MyAsyncMethods.GetPageLengths2();
            return View(new string[] { $"Length: {length}" });
        }

        // Using Non-Asynchronous IEnumerable Method
        public async Task<ViewResult> Index21()
        {
            List<string> output = new List<string>();
            foreach (long? len in await MyAsyncMethods.GetPageLengths3(output, "apress.com", "microsoft.com", "amazon.com"))
            {
                output.Add($"Page length: {len}");
            }
            return View(output);
        }

        // Using Asynchronous IEnumerable Method
        public async Task<ViewResult> Index22()
        {
            List<string> output = new List<string>();
            await foreach (long? len in MyAsyncMethods.GetPageLengths4(output, "apress.com", "microsoft.com", "amazon.com"))
            {
                output.Add($"Page length: {len}");
            }
            return View(output);
        }

        // Hard-Coding a Name in controller
        public ViewResult Index23()
        {
            var products = new[] {
                new { Name = "Kayak", Price = 275M },
                new { Name = "Lifejacket", Price = 48.95M },
                new { Name = "Soccer ball", Price = 19.50M },
                new { Name = "Corner flag", Price = 34.95M }
            };
            return View(products.Select(p => $"Name: {p.Name}, Price: {p.Price}"));
        }

        // Using nameof Expression


        // Hard-Coding a Name in controller
        public ViewResult Index()
        {
            var products = new[] {
                new { Name = "Kayak", Price = 275M },
                new { Name = "Lifejacket", Price = 48.95M },
                new { Name = "Soccer ball", Price = 19.50M },
                new { Name = "Corner flag", Price = 34.95M }
            };
            return View(products.Select(p => $"{nameof(p.Name)}: {p.Name}, {nameof(p.Price)}: {p.Price}"));
        }
    }
}