
using FluentAssertions;
using Inventory.Application.Products;
using Inventory.Domain.Abstractions;
using Inventory.Domain.Entities;
using Moq;

namespace Inventory.UnitTests
{
    public class ProductQueryTests
    {
        [Fact]
        public async Task List_Filters_By_Search_And_Paginates()
        {
            // In-memory data via IQueryable
            var data = new List<Product>
        {
            new("Pen", "Stationery", 1.5m, 10),
            new("Pencil", "Stationery", 0.8m, 50),
            new("Laptop", "Electronics", 999m, 3),
            new("Lamp", "Electronics", 29m, 8)
        }.AsQueryable();

            var repo = new Mock<IProductRepository>();
            repo.Setup(r => r.Query()).Returns(data);

            var svc = new ProductQueryService(repo.Object);
            var (items, total) = await svc.ListAsync(search: "p", category: "Stationery", page: 1, pageSize: 1);

            total.Should().Be(2);              // Pen, Pencil
            items.Count.Should().Be(1);        // pageSize = 1
            items[0].Name.Should().BeOneOf("Pen", "Pencil");
        }
    }
}
