using FluentAssertions;
using Inventory.Application.Orders;
using Inventory.Application.Orders.Models;
using Inventory.Domain.Abstractions;
using Inventory.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.UnitTests
{
    public class OrderServiceTests
    {

        [Fact]
        public async Task PlaceOrder_Decrements_Stock_And_Returns_Total_Or_409_On_Insufficient()
        {
            var p1 = new Product("Pen", "Stationery", 1.5m, 2);
            var products = new List<Product> { p1 };

            var prodRepo = new Mock<IProductRepository>();
            prodRepo.Setup(r => r.GetByIdAsync(p1.Id, It.IsAny<CancellationToken>()))
              .ReturnsAsync((Product?)null); // Id is 0 in memory
            
            // For simplicity we’ll query by name via IQueryable in service for this test

            prodRepo.Setup(r => r.Query()).Returns(products.AsQueryable());
            prodRepo.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var orderRepo = new Mock<IOrderRepository>();
            orderRepo.Setup(r => r.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            orderRepo.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            var svc = new OrderService(prodRepo.Object, orderRepo.Object);

            var ok = await svc.PlaceAsync(new CreateOrderDto(new() { new("Pen", 1) }));
            ok.Total.Should().Be(1.5m);

            // Now exceed stock
            Func<Task> act = async () =>
                await svc.PlaceAsync(new CreateOrderDto(new() { new("Pen", 5) }));

            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("*Insufficient stock*");
        }
    }
}
