using Inventory.Application.Orders;
using Inventory.Application.Orders.Models;
using Inventory.Domain.Abstractions;
using Inventory.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Inventory.WebApi.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _service;
        private readonly IOrderQueries _queries;

        public OrdersController(IOrderService service, IOrderQueries queries) 
        {
            _service = service;
            _queries = queries;
        }

        [HttpGet("{id:int}", Name = "GetOrderById")]
        public async Task<ActionResult<OrderReadDto>> GetById(int id, CancellationToken ct)
        {
            var dto = await _queries.GetByIdAsync(id, ct);
            return dto is null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDto dto, CancellationToken ct)
        {
            try
            {
                var(id, total) = await _service.PlaceAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id }, new { id, total });

            }
            catch (KeyNotFoundException)
            {

                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message }); 
            }
        }
    }
}
