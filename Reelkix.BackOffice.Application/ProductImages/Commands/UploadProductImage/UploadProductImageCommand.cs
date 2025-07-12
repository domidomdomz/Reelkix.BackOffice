using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reelkix.BackOffice.Application.ProductImages.Commands.UploadProductImage
{
    public class UploadProductImageCommand
    {
        // The unique identifier of the product to which the image will be uploaded.
        public Guid ProductId { get; set; }

        // The stream containing the image file to be uploaded. This is typically a file stream from a file upload control. 
        // The default! indicates that this property must be set before use, ensuring it is not null.
        public Stream FileStream { get; set; } = default!;
        
        // The name of the file being uploaded, cannot be null or empty.
        public string FileName { get; set; } = default!; 

        // The MIME type of the file being uploaded, which helps in determining how to handle the file.
        public string ContentType { get; set; } = default!; 

        // The alternative text for the image, which is useful for accessibility and SEO purposes.
        public string AltText { get; set; } = default!;

        // The order in which the image should be displayed, defaulting to 0.
        public int SortOrder { get; set; } = 0; 
    }
}
