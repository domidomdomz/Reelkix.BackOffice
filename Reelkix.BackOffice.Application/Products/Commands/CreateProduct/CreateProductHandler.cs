using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reelkix.BackOffice.Domain.Products;
using Reelkix.BackOffice.Persistence.Data;

namespace Reelkix.BackOffice.Application.Products.Commands.CreateProduct
{
    public class CreateProductHandler
    {
        private readonly ApplicationDbContext _db;
        public CreateProductHandler(ApplicationDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<Guid> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            var product = new Product(id: command.Id, name: command.Name, description: command.Description, costPrice: command.CostPrice, sellingPrice: command.SellingPrice);
            if (command.ImageUrls != null && command.ImageUrls.Any())
            {
                foreach (var imageUrl in command.ImageUrls)
                {
                    var image = new ProductImage(productId: product.Id, url: imageUrl, altText: product.Name);
                    product.AddImage(image);
                }
            }

            _db.Products.Add(product);
            await _db.SaveChangesAsync(cancellationToken);
            return product.Id;
        }
    }
}
