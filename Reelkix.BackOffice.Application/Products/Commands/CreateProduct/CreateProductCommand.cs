using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reelkix.BackOffice.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand
    {
        public string Name { get; set; } = default!; // The name of the product, cannot be null or empty.
        public string Description { get; set; } = default!; // A detailed description of the product, cannot be null or empty.
        public decimal CostPrice { get; set; } // The cost price of the product, must be a non-negative value.
        public decimal SellingPrice { get; set; } // The selling price of the product, must be greater than or equal to the cost price.
        public List<string> ImageUrls { get; set; } = new(); // List of image URLs associated with the product. new() indicates it starts as an empty list.
    }
}
