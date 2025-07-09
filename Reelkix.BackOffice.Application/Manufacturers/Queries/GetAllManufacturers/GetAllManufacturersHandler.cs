using Microsoft.EntityFrameworkCore;
using Reelkix.BackOffice.Application.Manufacturers.DTOs;
using Reelkix.BackOffice.Persistence.Data;

namespace Reelkix.BackOffice.Application.Manufacturers.Queries.GetAllManufacturers
{
    public class GetAllManufacturersHandler
    {
        private readonly ApplicationDbContext _db;

        public GetAllManufacturersHandler(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<ManufacturerDto>> Handle(CancellationToken cancellationToken)
        {
            var manufacturers = await _db.Manufacturers
                .Select(m => new ManufacturerDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description
                })
                .ToListAsync(cancellationToken);
            return manufacturers;
        }
    }
}
