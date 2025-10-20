using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Orders.Models
{
    public record OrderLineOutDto(int ProductId, decimal UnitPrice, int Quantity);
    public record OrderReadDto(int Id, DateTime CreatedAt, decimal Total, List<OrderLineOutDto> Items);
}
