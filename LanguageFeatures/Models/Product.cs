namespace LanguageFeatures.Models
{
    public class Product
    {
        // Assigning Value to Read-Only Property
        public Product(bool stock = true)
        {
            InStock = stock;
        }
        public string Name { get; set; }
        public string Category { get; set; } = "Watersports"; // Auto-implemented Property Initializer
        public decimal? Price { get; set; }
        public Product Related { get; set; }

        //public bool InStock { get; } = true; // Read-Only Automatically Implemented Properties
        public bool InStock { get; }

        public bool NameBeginWithS => Name?[0] == 'S'; // Lambda Property
        public static Product[] GetProducts()
        {
            // Object Initializer
            Product kayak = new Product
            {
                Name = "Kayak",
                Category = "Water Craft",
                Price = 275M
            };

            Product lifejacket = new Product(false)
            {
                Name = "Lifejacket",
                Price = 48.95M
            };

            kayak.Related = lifejacket;

            return new Product[] { kayak, lifejacket, null };
        }
    }
}