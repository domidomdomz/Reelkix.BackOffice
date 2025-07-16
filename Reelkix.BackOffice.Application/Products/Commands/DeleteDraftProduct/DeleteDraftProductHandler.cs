using FluentValidation;
using Reelkix.BackOffice.Persistence.Data;

namespace Reelkix.BackOffice.Application.Products.Commands.DeleteDraftProduct
{
    public class DeleteDraftProductHandler
    {
        private readonly ApplicationDbContext _db;

        public DeleteDraftProductHandler(ApplicationDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task Handle(Guid productId, CancellationToken cancellationToken)
        {
            if (productId == Guid.Empty) throw new ArgumentException("Product ID cannot be empty.", nameof(productId));
            var product = await _db.Products.FindAsync(new object[] { productId }, cancellationToken);
            if (product == null)
            {
                throw new ValidationException($"Draft product with ID {productId} not found.");
            }
            // Assuming draft products are marked with a specific status or flag
            if (!product.IsDraft)
            {
                throw new ValidationException("Only draft products can be deleted.");
            }
            _db.Products.Remove(product);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
