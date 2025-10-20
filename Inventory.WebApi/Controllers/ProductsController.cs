using Inventory.Application.Products;
using Inventory.Application.Products.Models;
using Inventory.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _create;
        private readonly IProductQueries _queries;
        private readonly ProductQueryService _query;

        public ProductsController(IProductService create, IProductQueries queries, ProductQueryService query)
        {
            _create = create;
            _queries = queries;
            _query = query;
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<ActionResult<ProductReadDto>> GetById(int id, CancellationToken ct)
        {
            var dto = await _queries.GetByIdAsync(id, ct);
            return dto is null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductReadDto>> Create(ProductCreateDto dto, CancellationToken ct)
        {
            var created = await _create.CreateAsync(dto, ct);
            return CreatedAtRoute("GetProductById", new { id = created.Id }, created);
        }

        [HttpGet]
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
