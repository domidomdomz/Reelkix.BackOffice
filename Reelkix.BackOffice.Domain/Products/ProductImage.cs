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
        public Guid ProductId { get; private set; }
        public string Url { get; private set; } = default!; // URL of the product image. Default! indicates it must be set before use.
        public string AltText { get; private set; } = default!; // Alternative text for the image, useful for accessibility.

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public ProductImage(Guid productId, string url, string altText)
        {
            ProductId = productId;
            Url = url;
            AltText = altText;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
