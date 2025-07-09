using FluentValidation;
using Reelkix.BackOffice.Application.Manufacturers.Commands.CreateManufacturer.Validators;
using Reelkix.BackOffice.Domain.Manufacturers;
using Reelkix.BackOffice.Persistence.Data;

namespace Reelkix.BackOffice.Application.Manufacturers.Commands.CreateManufacturer
{
    public class CreateManufacturerHandler
    {
        private readonly ApplicationDbContext _db; // private readonly field for the database context. readonly because it should not change after initialization.
        private readonly CreateManufacturerCommandValidator _validator; // Validator for the command

        public CreateManufacturerHandler(ApplicationDbContext db, CreateManufacturerCommandValidator validator)
        {
            _db = db;
            _validator = validator ?? throw new ArgumentNullException(nameof(validator)); // Ensure the validator is not null
        }

        public async Task<Guid> Handle(CreateManufacturerCommand command, CancellationToken cancellationToken)
        {
            // Validate the command properties
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                // If validation fails, throw an exception with the validation errors
                throw new ValidationException(validationResult.Errors);
            }

            if (command == null) throw new ArgumentNullException(nameof(command));

            // Create a new Manufacturer entity
            var manufacturer = new Manufacturer(Guid.NewGuid(), command.Name, command.Description);

            // Add the manufacturer to the context
            _db.Manufacturers.Add(manufacturer);
            
            // Save changes to the database
            await _db.SaveChangesAsync();
            
            // Return the ID of the newly created manufacturer
            return manufacturer.Id;
        }
    }
}
