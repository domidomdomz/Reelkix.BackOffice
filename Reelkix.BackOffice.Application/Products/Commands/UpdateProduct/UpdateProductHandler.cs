using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Reelkix.BackOffice.Persistence.Data;

namespace Reelkix.BackOffice.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductHandler
    {
        private readonly ApplicationDbContext _db;

        public UpdateProductHandler(ApplicationDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));


            var product = await _db.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);

            if (product == null)
            {
                throw new ValidationException($"Product with ID {command.ProductId} not found.");
            }

            product.UpdateDetails(
                command.Name,
                command.Description,
                command.ManufacturerId,
                command.CostPrice,
                command.SellingPrice
            );

            product.MarkFinalized();

            //// Update image metadata
            //foreach (var image in product.Images)
            //{
            //    var update = command.Images.FirstOrDefault(i => i.ImageId == image.Id);
            //    if (update != null)
            //    {
            //        image.UpdateSortOrder(update.SortOrder);
            //        image.UpdateAltText(update.AltText);
            //    }
            //}

            foreach (var commandImage in command.Images)
            {
                var existingImage = _db.ProductImages.FirstOrDefault(i => i.Id == commandImage.ImageId);
                if (existingImage != null)
                {
                    existingImage.UpdateSortOrder(commandImage.SortOrder);
                    existingImage.UpdateAltText(commandImage.AltText);

                    _db.ProductImages.Update(existingImage);
                }
            }

            _db.Products.Update(product);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
