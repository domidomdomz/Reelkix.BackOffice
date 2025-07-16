
using Microsoft.EntityFrameworkCore;
using Reelkix.BackOffice.Application.Products.DTOs;
using Reelkix.BackOffice.Persistence.Data;

namespace Reelkix.BackOffice.Application.Products.Queries.GetDraftProductById
{
    public class GetDraftProductByIdHandler
    {
        private readonly ApplicationDbContext _db;
        public GetDraftProductByIdHandler(ApplicationDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<ProductDraftDto?> Handle(GetDraftProductByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var draftProduct = await _db.Products
                .FirstOrDefaultAsync(p => p.Id == request.Id && p.IsDraft, cancellationToken);
            
            return draftProduct == null ? null : new ProductDraftDto
            {
                Name = draftProduct.Name,
                Description = draftProduct.Description,
                ManufacturerId = draftProduct.ManufacturerId,
                CostPrice = draftProduct.CostPrice,
                SellingPrice = draftProduct.SellingPrice
            };
        }
    }
}
