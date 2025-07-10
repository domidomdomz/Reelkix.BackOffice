using Microsoft.EntityFrameworkCore;
using Reelkix.BackOffice.Application.Manufacturers.DTOs;
using Reelkix.BackOffice.Persistence.Data;

namespace Reelkix.BackOffice.Application.Manufacturers.Queries.GetManufacturerById
{
    public class GetManufacturerByIdHandler
    {
        private readonly ApplicationDbContext _db;
        public GetManufacturerByIdHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ManufacturerDto?> Handle(GetManufacturerByIdQuery request, CancellationToken cancellationToken)
        {
            var manufacturer = await _db.Manufacturers
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
            if (manufacturer is null)
            {
                return null;
            }
            return new ManufacturerDto
            {
                Id = manufacturer.Id,
                Name = manufacturer.Name,
                Description = manufacturer.Description,
                CreatedAt = manufacturer.CreatedAt,
                UpdatedAt = manufacturer.UpdatedAt
            };
        }
    }
}
