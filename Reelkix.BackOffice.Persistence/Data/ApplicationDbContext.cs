using Microsoft.EntityFrameworkCore;
using Reelkix.BackOffice.Domain.Manufacturers;
using Reelkix.BackOffice.Domain.Products;
using Reelkix.BackOffice.SharedKernel;

namespace Reelkix.BackOffice.Persistence.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductImage> ProductImages => Set<ProductImage>();
        public DbSet<Manufacturer> Manufacturers => Set<Manufacturer>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly); // Automatically apply all configurations in the assembly
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<IAuditable>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.Touch();
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
