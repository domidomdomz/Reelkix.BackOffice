using FluentValidation;

namespace Reelkix.BackOffice.Application.Manufacturers.Commands.CreateManufacturer.Validators
{
    public class CreateManufacturerCommandValidator : AbstractValidator<CreateManufacturerCommand>
    {
        public CreateManufacturerCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Manufacturer name is required.")
                .MaximumLength(100);

            //RuleFor(x => x.Description)
            //    .NotEmpty().WithMessage("Manufacturer description is required.")
            //    .MaximumLength(1000);
        }
    }
}
