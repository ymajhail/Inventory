using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Products.Models;

public record ProductCreateDto(string Name, string? Category, decimal Price, int Stock);
public record ProductReadDto(int Id, string Name, string? Category, decimal Price, int Stock);