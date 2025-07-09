using Microsoft.EntityFrameworkCore;
using Reelkix.BackOffice.Application.Products.DTOs;
using Reelkix.BackOffice.Persistence.Data;

namespace Reelkix.BackOffice.Application.Products.Queries.CreateProductById
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
                CostPrice = product.CostPrice,
                SellingPrice = product.SellingPrice,
                ImageUrls = product.Images.Select(pi => pi.Url).ToList()
            };
        }
    }
}
