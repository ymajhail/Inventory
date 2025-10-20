using Inventory.Domain.Abstractions;
using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;

        public ProductRepository(AppDbContext db) => _db = db;
        public Task AddAsync(Product product, CancellationToken ct = default) => _db.Products.AddAsync(product, ct).AsTask();
        public Task<Product> GetByIdAsync(int id, CancellationToken ct = default) => _db.Products.FirstOrDefaultAsync(p => p.Id == id, ct);

        public IQueryable<Product> Query() => _db.Products.AsQueryable();

        public Task<int> SaveChangesAsync(CancellationToken ct = default) => _db.SaveChangesAsync(ct);
    }
}
