using Reelkix.BackOffice.Persistence.Data;

namespace Reelkix.BackOffice.Application.Products.Commands.CreateDraftProduct
{
    public class CreateDraftProductHandler
    {
        private readonly ApplicationDbContext _db;

        public CreateDraftProductHandler(ApplicationDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<Guid> Handle(CreateDraftProductCommand command, CancellationToken cancellationToken)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            var draft = new Domain.Products.Product(
                id: Guid.NewGuid(),
                name: command.Name,
                description: string.Empty, // Default to empty description for draft
                manufacturerId: Guid.Empty, // Manufacturer can be set later
                costPrice: 0, // Default to 0 for draft
                sellingPrice: 0 // Default to 0 for draft
            );
            _db.Products.Add(draft);
            await _db.SaveChangesAsync(cancellationToken);
            return draft.Id;
        }
    }
}
