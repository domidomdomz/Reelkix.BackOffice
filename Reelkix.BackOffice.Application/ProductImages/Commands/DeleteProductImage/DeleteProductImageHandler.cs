using Reelkix.BackOffice.Persistence.Data;

namespace Reelkix.BackOffice.Application.ProductImages.Commands.DeleteProductImage
{
    public class DeleteProductImageHandler
    {
        private readonly ApplicationDbContext _db;

        public DeleteProductImageHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Handle(DeleteProductImageCommand command, CancellationToken cancellationToken)
        {
            var productImage = await _db.ProductImages.FindAsync(new object[] { command.ProductImageId }, cancellationToken);

            if (productImage != null)
            {
                _db.ProductImages.Remove(productImage);
                await _db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
