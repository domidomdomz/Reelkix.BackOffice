using Microsoft.EntityFrameworkCore;
using Reelkix.BackOffice.Application.Products.DTOs;
using Reelkix.BackOffice.Persistence.Data;

namespace Reelkix.BackOffice.Application.Products.Queries.GetProductById
{
    public class GetProductByIdHandler
    {
        private readonly ApplicationDbContext _db;
        public GetProductByIdHandler(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _db.Products
                .Include(p => p.Images)
                .Include(p => p.Manufacturer) // Assuming you want to include Manufacturer details
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product is null)
            {
                return null;
                //throw new NotFoundException($"Product with ID {request.Id} not found.");
            }

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ManufacturerId = product.ManufacturerId,
                ManufacturerName = product.Manufacturer?.Name ?? string.Empty, // Assuming Manufacturer is included in the query
                CostPrice = product.CostPrice,
                SellingPrice = product.SellingPrice,
                ImageUrls = product.Images.OrderBy(i => i.SortOrder).Select(pi => pi.Url).ToList()
            };
        }
    }
}
