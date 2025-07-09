using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reelkix.BackOffice.Domain.Manufacturers;
using Reelkix.BackOffice.SharedKernel;

namespace Reelkix.BackOffice.Domain.Products
{
    public class Product : IAuditable
    {
        private readonly List<ProductImage> _images = new();
        public IReadOnlyCollection<ProductImage> Images => _images.AsReadOnly(); // Read-only collection of product images


        public Guid Id { get; private set; } // Unique identifier for the product. Private set to ensure it can only be set internally.
        public string Name { get; private set; } = default!; // Name of the product. Default! indicates it must be set before use.
        public string Description { get; private set; } = default!; // Description of the product. Default! indicates it must be set before use.
        
        public Guid ManufacturerId { get; private set; } // Foreign key to the Manufacturer entity. This is used to establish a relationship with the Manufacturer.
        public Manufacturer Manufacturer { get; private set; } = default!; // Manufacturer of the product. Default! indicates it must be set before use. 
        // The Manufacturer property is used to navigate to the Manufacturer entity, allowing access to its properties and methods.
        // The ManufacturerId is used to establish a foreign key relationship with the Manufacturer entity, allowing for database integrity and navigation.
        // The ManufacturerId and Manufacturer properties together allow for easy access to the manufacturer details of the product.


        public decimal CostPrice { get; private set; } // Cost price of the product. Private set to ensure it can only be set internally.
        public decimal SellingPrice { get; private set; } // Selling price of the product.

        public DateTime CreatedAt { get; set; } // Timestamp when the product was created.
        public DateTime UpdatedAt { get; set; } // Timestamp when the product was last updated.

        private Product() { } // Private constructor for EF Core or other ORM usage. Ensures that the product can only be created through factory methods or constructors.

        public Product(Guid id, string name, string description, decimal costPrice, decimal sellingPrice, Guid manufacturerId)
        {
            Id = id;
            Name = name;
            Description = description;
            ManufacturerId = manufacturerId; // Set the foreign key to the Manufacturer entity.
            CostPrice = costPrice;
            SellingPrice = sellingPrice;
            CreatedAt = DateTime.UtcNow; // Set the creation timestamp to the current UTC time.
            UpdatedAt = DateTime.UtcNow; // Set the update timestamp to the current UTC time.
        }

        public void AddImage(ProductImage image)
        {
            if (image == null) throw new ArgumentNullException(nameof(image), "Image cannot be null.");
            if (image.ProductId != Id) throw new InvalidOperationException("Image does not belong to this product.");
            _images.Add(image);
        }

        public void UpdatePrice(decimal newSellingPrice)
        {
            if (newSellingPrice < 0) throw new ArgumentOutOfRangeException(nameof(newSellingPrice), "Price cannot be negative.");
            SellingPrice = newSellingPrice;
        }
    }
}
