using FluentValidation;
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

            var draft = new Domain.Products.Product(
                id: Guid.NewGuid(),
                name: command.Name,
                description: string.Empty, // Default to empty description for draft
                manufacturerId: command.ManufacturerId,
                costPrice: 0, // Default to 0 for draft
                sellingPrice: 0 // Default to 0 for draft
            );
            _db.Products.Add(draft);
            await _db.SaveChangesAsync(cancellationToken);
            return draft.Id;
        }
    }
}
