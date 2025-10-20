using Inventory.Application.Products.Models;
using Inventory.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Products
{
    public class ProductQueries : IProductQueries
    {
        private readonly IProductRepository _repo;
        public ProductQueries(IProductRepository repo) => _repo = repo;

        public async Task<ProductReadDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var p = await _repo.GetByIdAsync(id, ct);
            return p is null ? null : new ProductReadDto(p.Id, p.Name, p.Category, p.Price, p.Stock);
        }
    }
}
