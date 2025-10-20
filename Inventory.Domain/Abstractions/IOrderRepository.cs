using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Abstractions
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdWithItemsAsync(int id, CancellationToken ct = default);
        Task AddAsync(Order order, CancellationToken ct = default);
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
