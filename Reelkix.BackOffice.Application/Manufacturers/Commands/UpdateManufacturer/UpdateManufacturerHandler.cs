using FluentValidation;
using Reelkix.BackOffice.Application.Manufacturers.Commands.UpdateManufacturer.Validators;
using Reelkix.BackOffice.Persistence.Data;

namespace Reelkix.BackOffice.Application.Manufacturers.Commands.UpdateManufacturer
{
    public class UpdateManufacturerHandler
    {
        private readonly ApplicationDbContext _db; // private readonly field for the database context. readonly because it should not change after initialization.

        private readonly UpdateManufacturerCommandValidator _validator; // Validator for the command

        public UpdateManufacturerHandler(ApplicationDbContext db, UpdateManufacturerCommandValidator validator)
        {
            _db = db;
            _validator = validator ?? throw new ArgumentNullException(nameof(validator)); // Ensure the validator is not null
        }

        public async Task<bool> Handle(Guid id, UpdateManufacturerCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                // If validation fails, throw an exception with the validation errors
                throw new ValidationException(validationResult.Errors);
            }

            if (command == null) throw new ArgumentNullException(nameof(command));
            // Find the existing manufacturer by ID
            var manufacturer = await _db.Manufacturers.FindAsync(new object[] { id }, cancellationToken);
            if (manufacturer == null)
            {
                throw new KeyNotFoundException($"Manufacturer with ID {id} not found.");
            }
            // Update the properties of the manufacturer
            manufacturer.Name = command.Name;
            manufacturer.Description = command.Description;
            // Save changes to the database
            await _db.SaveChangesAsync(cancellationToken);

            // Return true to indicate the update was successful
            return true;
        }
    }
}
