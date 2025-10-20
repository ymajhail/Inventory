using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Orders.Models;
public record OrderLineByNameDto(string ProductName, int Quantity);
public record CreateOrderDto(List<OrderLineByNameDto> Items);