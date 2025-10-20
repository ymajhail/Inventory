using Inventory.Application.Products.Models;
using Inventory.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Products
{
    public class ProductQueryService
    {
        private readonly IProductRepository _repo;
        public ProductQueryService(IProductRepository repo) => _repo = repo;

        public async Task<(List<ProductReadDto> Items, int Total)> ListAsync(
            string? search, string? category, int page, int pageSize, CancellationToken ct = default)
        {
            if (page < 1) page = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            var q = _repo.Query();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.ToLowerInvariant();
                q = q.Where(p => p.Name.ToLowerInvariant().Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(p => p.Name.ToLowerInvariant().Contains(search));

            if (!string.IsNullOrWhiteSpace(category))
                q = q.Where(p => p.Category == category);

            var total = q.Count();

            var items = q.OrderBy(p => p.Id)
                         .Skip((page - 1) * pageSize)
                         .Take(pageSize)
                         .Select(p => new ProductReadDto(p.Id, p.Name, p.Category, p.Price, p.Stock))
                         .ToList();

            return (items, total);
        }
    }
}
