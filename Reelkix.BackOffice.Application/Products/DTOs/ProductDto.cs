using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reelkix.BackOffice.Application.Products.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }

        public List<string> ImageUrls { get; set; } = new(); // List of image URLs associated with the product. new() indicates it starts as an empty list.
    }
}
