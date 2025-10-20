using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Products.Models
{
    public interface IProductQueries
    {
        Task<ProductReadDto?> GetByIdAsync(int id, CancellationToken ct = default);
    }
}
