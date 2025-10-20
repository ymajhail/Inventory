using FluentAssertions;
using Inventory.Application.Products.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;

namespace Inventory.IntegrationTests
{
    public class ProductsApiTests: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ProductsApiTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task Create_Then_List_With_Pagination_Header()
        {
            var client = _factory.CreateClient();
            var create = await client.PostAsJsonAsync("/api/products", new ProductCreateDto("Pen", "Stationery", 1.5m, 10));
            create.EnsureSuccessStatusCode();

            var list = await client.GetAsync("/api/products?page=1&pageSize=1&search=P&category=Stationery");
            list.EnsureSuccessStatusCode();
            list.Headers.Contains("X-Pagination").Should().BeTrue();    
        }
    }
}