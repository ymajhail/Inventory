using Inventory.Application.Products;
using Inventory.Application.Products.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _create;
        private readonly ProductQueryService _query;

        public ProductsController(IProductService create, ProductQueryService query)
        {
            _create = create;
            _query = query;
        }

        [HttpPost]
        public async Task<ActionResult<ProductReadDto>> Create(ProductCreateDto dto, CancellationToken ct)
        {
            var created = await _create.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPost]
        public async Task<ActionResult<List<ProductReadDto>>> List(
            [FromQuery] string? search, [FromQuery] string? category,
            [FromQuery] int page =1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
        {
            var (items, total) = await _query.ListAsync(search, category, page, pageSize, ct);
            Response.Headers["X-Pagination"] = System.Text.Json.JsonSerializer.Serialize(new { total, page,  pageSize });
            return items;

        }


    }
}
