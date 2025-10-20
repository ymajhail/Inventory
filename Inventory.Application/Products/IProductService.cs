using Inventory.Application.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Products
{
    public interface IProductService
    {
        Task<ProductReadDto> CreateAsync(ProductCreateDto productReadDto, CancellationToken ct = default);
    }
}
