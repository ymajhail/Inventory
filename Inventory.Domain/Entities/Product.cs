using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;    
        public string? Category { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }

        private Product() { } //EF

        public Product(string name, string? category, decimal price, int stock)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("Name must be at least 2 chars");
            if (price < 0) throw new ArgumentOutOfRangeException("Price cannot be negative");
            if (stock < 0) throw new ArgumentOutOfRangeException("Stock cannot be negative");

            Name = name;
            Category = category;
            Price = price;
            Stock = stock;            
        }

        public void DecreaseStock(int qty)
        {
            if (qty < 0) throw new ArgumentOutOfRangeException("Quantity must be positive");
            if (Stock < qty) throw new ArgumentOutOfRangeException("Insuffienct stock");
            Stock -= qty;
        }
    }
}
