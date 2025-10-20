using Inventory.Application.Orders;
using Inventory.Application.Orders.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebApi.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDto dto, CancellationToken ct)
        {
            try
            {
                var(id, total) = await _service.PlaceAsync(dto, ct);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
