using Inventory.Application.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Orders
{
    public interface IOrderService
    {
        Task<(int OrderId, decimal Total)> PlaceAsync(CreateOrderDto dto, CancellationToken ct = default);
    }
}
