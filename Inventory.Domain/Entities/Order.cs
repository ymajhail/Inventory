using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Entities
{
    public class Order
    {
        public int Id { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public decimal Total { get; private set; }
        public List<OrderItem> Items { get; private set; } = new();

        public void AddItem(int productId, decimal unitPrice, int quantity)
        {
            if (quantity < 0) throw new ArgumentOutOfRangeException("Qty must be positive");
            Items.Add(new OrderItem(productId, unitPrice, quantity));
            Total += unitPrice * quantity;
        }

    }

    public class OrderItem
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        private OrderItem() { } //Ef
        public OrderItem(int productId, decimal unitPrice, int quantity)
        {
            ProductId = productId; UnitPrice = unitPrice; Quantity = quantity;
        }
    }

}
