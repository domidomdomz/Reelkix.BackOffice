using FluentValidation;
using Reelkix.BackOffice.Application.Products.Commands.CreateProduct.Validators;
using Reelkix.BackOffice.Domain.Products;
using Reelkix.BackOffice.Persistence.Data;

namespace Reelkix.BackOffice.Application.Products.Commands.CreateProduct
{
    public class CreateProductHandler
    {
        private readonly ApplicationDbContext _db;
        private readonly CreateProductCommandValidator _validator;
        public CreateProductHandler(ApplicationDbContext db, CreateProductCommandValidator validator)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<Guid> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (command == null) throw new ArgumentNullException(nameof(command));
            var product = new Product(
                id: Guid.NewGuid(), 
                name: command.Name, 
                description: command.Description,
                manufacturerId: command.ManufacturerId,
                costPrice: command.CostPrice, 
                sellingPrice: command.SellingPrice);

            if (command.ImageUrls != null && command.ImageUrls.Any())
            {
                foreach (var imageUrl in command.ImageUrls)
                {
                    var image = new ProductImage(productId: product.Id, url: imageUrl, altText: product.Name, sortOrder: 0);
                    product.AddImage(image);
                }
            }

            _db.Products.Add(product);
            await _db.SaveChangesAsync(cancellationToken);
            return product.Id;
        }
    }
}
