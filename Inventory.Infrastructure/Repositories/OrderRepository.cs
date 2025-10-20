using Inventory.Domain.Abstractions;
using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _db;
        public OrderRepository(AppDbContext db) => _db = db;

        public Task<Order?> GetByIdWithItemsAsync(int id, CancellationToken ct = default) => _db.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id, ct);
        public Task AddAsync(Order order, CancellationToken ct = default) => _db.Orders.AddAsync(order, ct).AsTask();

        public Task<int> SaveChangesAsync(CancellationToken ct = default) => _db.SaveChangesAsync(ct);
    }
}
