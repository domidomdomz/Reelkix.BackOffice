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

        public Guid ManufacturerId { get; set; } // The unique identifier of the manufacturer associated with the product, cannot be empty.
        public string ManufacturerName { get; set; } = default!; // The name of the manufacturer associated with the product, cannot be null or empty.

        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }

        public List<string> ImageUrls { get; set; } = new(); // List of image URLs associated with the product. new() indicates it starts as an empty list.
    }
}
