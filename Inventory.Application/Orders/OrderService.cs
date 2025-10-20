using Inventory.Application.Orders.Models;
using Inventory.Domain.Abstractions;
using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IProductRepository _products;
        private readonly IOrderRepository _orders;

        public OrderService(IProductRepository product, IOrderRepository orders)
        {
            _products = product;
            _orders = orders;            
        }
        public async Task<(int OrderId, decimal Total)> PlaceAsync(CreateOrderDto dto, CancellationToken ct = default)
        {
            if(dto.Items == null || dto.Items.Count == 0)
                throw new ArgumentNullException("Order must contain at least one item");

            var names = dto.Items.Select(i => i.ProductName).ToList();
            var prods = _products.Query().Where(p => names.Contains(p.Name)).ToList();

            if (prods.Count != names.Distinct().Count())
                throw new KeyNotFoundException("One or more products not found");

            // Check stock
            foreach (var line in dto.Items)
            {
                var p = prods.First(x => x.Name == line.ProductName);
                if (p.Stock < line.Quantity)
                    throw new InvalidOperationException("Insufficient stock");
            }

            //  Apply changes & build order
            var order = new Order();
            foreach (var line in dto.Items)
            {
                var p = prods.First(x => x.Name == line.ProductName);
                p.DecreaseStock(line.Quantity);
                order.AddItem(p.Id, p.Price, line.Quantity);
            }

            await _orders.AddAsync(order, ct);
            await _orders.SaveChangesAsync(ct);
            await _products.SaveChangesAsync(ct);

            return (order.Id, order.Total);

        }
    }
}
