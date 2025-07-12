using FluentValidation;

namespace Reelkix.BackOffice.Application.Products.Commands.CreateDraftProduct.Validators
{
    public class CreateDraftProductCommandValidator : AbstractValidator<CreateDraftProductCommand>
    {
        public CreateDraftProductCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Product name cannot be empty.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(command => command.ManufacturerId)
                .NotEmpty().WithMessage("Manufacturer cannot be empty.")
                .Must(manufacturerId => manufacturerId != Guid.Empty).WithMessage("Manufacturer is required");
        }
    }
}
