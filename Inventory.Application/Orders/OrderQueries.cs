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
    public class OrderQueries : IOrderQueries
    {
        private readonly IOrderRepository _orders;
        public OrderQueries(IOrderRepository orders) => _orders = orders;
        public async Task<OrderReadDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var o = await _orders.GetByIdWithItemsAsync(id, ct);
            if (o is null) return null;

            return new OrderReadDto(
               o.Id, o.CreatedAt, o.Total,
               o.Items.Select(i => new OrderLineOutDto(i.ProductId, i.UnitPrice, i.Quantity)).ToList()
           );
        }
    }
}
