using FluentValidation;

namespace Reelkix.BackOffice.Application.Manufacturers.Commands.UpdateManufacturer.Validators
{
    public class UpdateManufacturerCommandValidator : AbstractValidator<UpdateManufacturerCommand>
    {
        public UpdateManufacturerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Manufacturer ID is required.")
                .Must(id => id != Guid.Empty).WithMessage("Manufacturer ID cannot be empty.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Manufacturer name is required.")
                .MaximumLength(100).WithMessage("Manufacturer name cannot exceed 100 characters.");
        }
    }
}
