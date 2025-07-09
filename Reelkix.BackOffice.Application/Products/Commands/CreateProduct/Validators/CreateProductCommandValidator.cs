using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Reelkix.BackOffice.Application.Products.Commands.CreateProduct.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(1000);

            RuleFor(x => x.CostPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Cost price cannot be negative.");

            RuleFor(x => x.SellingPrice)
                .GreaterThan(0)
                    .WithMessage("Selling price must be greater than zero.")
                .GreaterThanOrEqualTo(x => x.CostPrice)
                    .WithMessage("Selling price must be greater than or equal to cost price.");

            RuleForEach(x => x.ImageUrls)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("Each image URL must be a valid absolute URI.");

        }
    }
}
