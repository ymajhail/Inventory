using FluentAssertions;
using Inventory.Application.Products;
using Inventory.Application.Products.Models;
using Inventory.Domain.Abstractions;
using Inventory.Domain.Entities;
using Moq;

namespace Inventory.UnitTests
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task CreateProduct_ValidInput_Persists_And_Returns_ReadModel()
        {
            var repo = new Mock<IProductRepository>();
            repo.Setup(r => r.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            repo.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
            // fail first no service 
            var svc = new ProductService(repo.Object);

            var input = new ProductCreateDto("Pen", "Stationary", 1.5m, 10);
            var result = await svc.CreateAsync(input);

            result.Id.Should().BeGreaterThanOrEqualTo(0);
            result.Name.Should().Be("Pen");
            repo.Verify(r => r.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once);
            repo.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}