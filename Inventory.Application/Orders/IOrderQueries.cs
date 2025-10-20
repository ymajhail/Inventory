using Inventory.Application.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Orders
{
    public interface IOrderQueries
    {
        Task<OrderReadDto?> GetByIdAsync(int id, CancellationToken ct = default);
    }
}
