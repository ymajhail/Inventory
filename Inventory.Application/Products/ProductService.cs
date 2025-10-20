using Inventory.Application.Products.Models;
using Inventory.Domain.Abstractions;
using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository) => _repository = repository;
        public async Task<ProductReadDto> CreateAsync(ProductCreateDto dto, CancellationToken ct = default)
        {
            var product = new Product(dto.Name, dto.Category, dto.Price, dto.Stock);
            await _repository.AddAsync(product, ct);
            await _repository.SaveChangesAsync(ct);

            return new ProductReadDto(product.Id, product.Name, product.Category, product.Price, product.Stock);
        }
    }
}
