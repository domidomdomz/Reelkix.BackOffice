using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reelkix.BackOffice.Application.Products.DTOs;
using Reelkix.BackOffice.Domain.Products;
using Reelkix.BackOffice.Persistence.Data;

namespace Reelkix.BackOffice.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsHandler
    {
        private readonly ApplicationDbContext _db;

        public GetAllProductsHandler(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public async Task<List<ProductDto>?> Handle(CancellationToken cancellationToken)
        {
            var products = await _db.Products
                .Include(p => p.Images)
                .Include(p => p.Manufacturer)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CostPrice = p.CostPrice,
                    SellingPrice = p.SellingPrice,
                    ManufacturerId = p.ManufacturerId,
                    ManufacturerName = p.Manufacturer.Name,
                    ImageUrls = p.Images.Select(pi => pi.Url).ToList(),
                }).ToListAsync();

            if (products is null || !products.Any())
            {
                return null; // or throw an exception if you prefer
            }

            return products;
        }
        
    }
}
