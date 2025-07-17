using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Reelkix.BackOffice.Persistence.Data;

namespace Reelkix.BackOffice.Application.Products.Commands.UpdateDraftProduct
{
    public class UpdateDraftProductHandler
    {
        private readonly ApplicationDbContext _db;

        public UpdateDraftProductHandler(ApplicationDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task Handle(UpdateDraftProductCommand command, CancellationToken cancellationToken)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);

            if (product == null)
            {
                throw new ValidationException($"Draft product with ID {command.ProductId} not found.");
            }
            // Assuming draft products are marked with a specific status or flag
            if (!product.IsDraft)
            {
                throw new ValidationException("Only draft products can be updated.");
            }

            product.UpdateDetails(
                command.Name,
                command.Description,
                command.ManufacturerId,
                command.CostPrice,
                command.SellingPrice
            );
            
            _db.Products.Update(product);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
