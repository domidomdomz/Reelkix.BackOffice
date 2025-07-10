using FluentValidation;

namespace Reelkix.BackOffice.Application.Manufacturers.Commands.UpdateManufacturer.Validators
{
    public class UpdateManufacturerCommandValidator : AbstractValidator<UpdateManufacturerCommand>
    {
        public UpdateManufacturerCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Manufacturer name is required.")
                .MaximumLength(100).WithMessage("Manufacturer name cannot exceed 100 characters.");
        }
    }
}
