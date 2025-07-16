using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Reelkix.BackOffice.Application.Products.Commands.CreateDraftProduct.Validators;
using Reelkix.BackOffice.Persistence.Data;

namespace Reelkix.BackOffice.Application.Products.Commands.CreateDraftProduct
{
    public class CreateDraftProductHandler
    {
        private readonly ApplicationDbContext _db;
        private readonly CreateDraftProductCommandValidator _validator;

        public CreateDraftProductHandler(ApplicationDbContext db, CreateDraftProductCommandValidator validator)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _validator = validator;
        }

        public async Task<Guid> Handle(CreateDraftProductCommand command, CancellationToken cancellationToken)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var manufacturerExists = await _db.Manufacturers
                .AnyAsync(m => m.Id == command.ManufacturerId, cancellationToken);

            if (!manufacturerExists)
                throw new ValidationException($"Manufacturer with ID {command.ManufacturerId} does not exist.");

            var draft = new Domain.Products.Product(
                id: Guid.NewGuid(),
                name: command.Name,
                description: command.Description,
                manufacturerId: command.ManufacturerId,
                costPrice: command.CostPrice,
                sellingPrice: command.SellingPrice
            );
            _db.Products.Add(draft);
            await _db.SaveChangesAsync(cancellationToken);
            return draft.Id;
        }
    }
}
