using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Abstractions
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id, CancellationToken ct = default);
        Task AddAsync(Product product, CancellationToken ct = default);
        IQueryable<Product> Query();
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
