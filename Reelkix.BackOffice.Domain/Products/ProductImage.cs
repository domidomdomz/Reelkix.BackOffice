using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reelkix.BackOffice.SharedKernel;

namespace Reelkix.BackOffice.Domain.Products
{
    public class ProductImage : IAuditable
    {
        public Guid Id { get; private set; } = Guid.NewGuid(); // Unique identifier for the product image, initialized to a new GUID.
        public Guid ProductId { get; private set; }
        public string Url { get; private set; } = default!; // URL of the product image. Default! indicates it must be set before use.
        public string AltText { get; private set; } = default!; // Alternative text for the image, useful for accessibility.
        public int SortOrder { get; set; } = 0; // Order in which the image should be displayed, defaulting to 0.

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public ProductImage(Guid productId, string url, string altText, int sortOrder)
        {
            Id = Guid.NewGuid(); // Generate a new unique identifier for the product image.
            ProductId = productId;
            Url = url;
            AltText = altText;
            SortOrder = sortOrder;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateSortOrder(int sortOrder)
        {
            SortOrder = sortOrder; // Update the sort order of the image.
            UpdatedAt = DateTime.UtcNow; // Update the timestamp to the current UTC time.
        }

        public void UpdateAltText(string altText)
        {
            AltText = altText; // Update the alternative text for the image.
            UpdatedAt = DateTime.UtcNow; // Update the timestamp to the current UTC time.
        }
    }
}
