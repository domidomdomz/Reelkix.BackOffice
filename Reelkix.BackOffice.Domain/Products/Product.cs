using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reelkix.BackOffice.Domain.Products
{
    public class Product
    {
        private readonly List<ProductImage> _images = new();
        public IReadOnlyCollection<ProductImage> Images => _images.AsReadOnly(); // Read-only collection of product images


        public Guid Id { get; private set; } // Unique identifier for the product. Private set to ensure it can only be set internally.
        public string Name { get; private set; } = default!; // Name of the product. Default! indicates it must be set before use.
        public string Description { get; private set; } = default!; // Description of the product. Default! indicates it must be set before use.
        public decimal CostPrice { get; private set; } // Cost price of the product. Private set to ensure it can only be set internally.
        public decimal SellingPrice { get; private set; } // Selling price of the product.

        
        private Product() { } // Private constructor for EF Core or other ORM usage. Ensures that the product can only be created through factory methods or constructors.

        public Product(Guid id, string name, string description, decimal costPrice, decimal sellingPrice)
        {
            Id = id;
            Name = name;
            Description = description;
            CostPrice = costPrice;
            SellingPrice = sellingPrice;
        }

        public void AddImage(ProductImage image)
        {
            if (image == null) throw new ArgumentNullException(nameof(image), "Image cannot be null.");
            if (image.ProductId != Id) throw new InvalidOperationException("Image does not belong to this product.");
            _images.Add(image);
        }
    }
}
